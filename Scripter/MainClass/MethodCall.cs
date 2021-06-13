using Behringer.X32;
using NLua;
using OSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scripter.MainClass
{
    public class MethodCall
    {
        private X32Console _console;
        private List<Script> _luas;

        /// <summary>
        /// Передача функций и методов микшера в Lua скрипт
        /// </summary>
        /// <param name="x32Console">Подключение микшера</param>
        /// <param name="luas">Список скриптов</param>
        public MethodCall(X32Console x32Console, List<Script> scripts)
        {
            _console = x32Console;
            _luas = scripts;

            _console.OnBusMute += _console_OnBusMute;
            _console.OnAuxFade += _console_OnAuxFade;
            _console.OnAuxSendLevel += _console_OnAuxSendLevel;
        }

        /// <summary>
        /// При удалении нужно отключить прием
        /// </summary>
        public void Destroy()
        {
            _console.OnBusMute -= _console_OnBusMute;
            _console.OnAuxFade -= _console_OnAuxFade;
            _console.OnAuxSendLevel -= _console_OnAuxSendLevel;
        }

        public void scripts(List<Script> listScrpit)
        {
            _luas = listScrpit;
        }

        private void _console_OnAuxSendLevel(object sender, OSCPacket packet)
        {
            
        }

        private void _console_OnAuxFade(object sender, OSCPacket packet)
        {
            int indexAux = Int16.Parse(packet.Nodes[2]);
            float value = packet.Arguments[0].ToFloat();
            foreach(Script script in _luas)
            {
                LuaFunction func = script._lua["F_AuxFader"] as LuaFunction;

                if (func == null)
                    continue;

                new Thread(() =>
                {
                    while (!script.FreeProcess)
                    {
                        Thread.Sleep(500);
                    }
                    script.FreeProcess = false;
                    func.Call(indexAux, value);
                    script.FreeProcess = true;
                }).Start();
            }
        }

        private void _console_OnBusMute(object sender, OSCPacket packet)
        {
            int indexBus = Int16.Parse(packet.Nodes[2]);
            int value = (int)packet.Arguments[0].ToFloat();
            foreach(Script script in _luas)
            {
                LuaFunction func = script._lua["F_BusMuted"] as LuaFunction;

                if (func == null)
                    continue;

                new Thread(() =>
                {
                    while (!script.FreeProcess)
                    {
                        Thread.Sleep(500);
                    }
                    script.FreeProcess = false;
                    func.Call(indexBus, value);
                    script.FreeProcess = true;
                }).Start();
               
            }
        }



    }
}
