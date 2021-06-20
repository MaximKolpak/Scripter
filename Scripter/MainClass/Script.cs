using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scripter.MainClass
{
    public class Script
    {
        public Lua _lua; //Lua класс (скрипт)
        public Thread MainThread { get; set; } //Основной поток
        public string PathFile; //Путь к файлу
        public bool FreeProcess; //Незажействован ли процессор
        public bool Abort;//Остановка процессора

        public Script(Action<Lua> RegFunction, string PathFile)
        {
            _lua = new Lua();
            _lua.SetDebugHook(KeraLua.LuaHookMask.Line, 0);
            _lua.DebugHook += _lua_DebugHook;
            this.PathFile = PathFile;
            RegFunction(_lua);

            new Thread(DeclaredScripts).Start();
        }

        private void _lua_DebugHook(object sender, NLua.Event.DebugHookEventArgs e)
        {
            if (Abort)
            {
                Lua l = (Lua)sender;
                l.State.Error("StoppedScript");
            }
        }

        private void DeclaredScripts()
        {
            try
            {
                _lua.DoFile(PathFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void MainFunction()
        {
            LuaFunction func = _lua["Main"] as LuaFunction;
            if (func != null)
            {
                MainThread = new Thread( () =>
                {
                    FreeProcess = false;
                    try
                    {
                        func.Call();
                    }catch { }
                    FreeProcess = true;
                });
                MainThread.Start();
            }
        }
    }
}
