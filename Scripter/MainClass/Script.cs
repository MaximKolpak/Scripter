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

        public Script(Action<Lua> RegFunction, string PathFile)
        {
            _lua = new Lua();
            this.PathFile = PathFile;
            RegFunction(_lua);
            DeclaredScripts();
        }

        private void DeclaredScripts()
        {
            _lua.DoFile(PathFile);
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
