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
        public bool FreeProcess = true; //Незажействован ли процессор
        public bool Abort;//Остановка процессора

        public Script(Action<Lua> RegFunction, string PathFile)
        {
            _lua = new Lua();
            _lua.SetDebugHook(KeraLua.LuaHookMask.Line, 0);
            _lua.DebugHook += _lua_DebugHook;
            this.PathFile = PathFile;
            RegFunction(_lua);
        }

        public void StartScript()
        {
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
                FunctionCall("Main");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void FunctionCall(string NameFunction, params object[] args)
        {
            new Thread(() =>
            {
                LuaFunction func = _lua[NameFunction] as LuaFunction;
                if (func == null)
                    return;

                while (!FreeProcess)
                    Thread.Sleep(100);
                FreeProcess = false;
                try { func.Call(args); } catch (Exception e) { Console.WriteLine("Sources: '{0}', Message: {1}", e.Source, e.Message); }
                FreeProcess = true;
                Thread.Sleep(100);
            }).Start();
        }
    }
}
