using Behringer.X32;
using NLua;
using Scripter.Function;
using Scripter.Function.MixFunc;
using Scripter.Function.Naming;
using Scripter.Settings;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Scripter.MainClass
{
    internal class Mixer
    {
        private X32Console _console;

        private List<Script> _scripts;

        public Mixer(string IpMixer, int port = 0, int interval = 0)
        {
            _console = new X32Console();
            _console.IPAddress = IPAddress.Parse(IpMixer);

            if (port != 0)
                _console.Port = port;
            if (interval != 0)
                _console.Interval = interval;

            Func();
        }
        public void Func()
        {
            #region Mute
            _console.OnChannelMute += _console_OnChannelMute;
            _console.OnBusMute += _console_OnBusMute;
            _console.OnDcaMute += _console_OnDcaMute;
            _console.OnAuxMute += _console_OnAuxMute;
            _console.OnMainMute += _console_OnMainMute;
            _console.OnMatrixMute += _console_OnMatrixMute;
            _console.OnFxRtnMute += _console_OnFxRtnMute;
            #endregion

            #region Fader (Одиночный запрос)

            _console.OnChannelFade += _console_OnChannelFade;
            _console.OnBusFade += _console_OnBusFade;
            _console.OnDcaFade += _console_OnDcaFade;
            _console.OnAuxFade += _console_OnAuxFade;
            _console.OnMainFade += _console_OnMainFade;
            _console.OnMatrixFade += _console_OnMatrixFade;
            _console.OnFxRtnFade += _console_OnFxRtnFade;

            #endregion

            #region Fader (Запросы находящиеся в Bus)
            _console.OnChannelSendLevel += _console_OnChannelSendLevel;
            _console.OnBusSendLevel += _console_OnBusSendLevel;
            _console.OnAuxSendLevel += _console_OnAuxSendLevel;
            _console.OnMainSendLevel += _console_OnMainSendLevel;
            _console.OnFxRtnSendLevel += _console_OnFxRtnSendLevel;
            #endregion
        }
        #region Fader (Запросы при отппрвке)
        private void _console_OnFxRtnSendLevel(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnFxRtnSendLevel",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnMainSendLevel(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnMainSendLevel",
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnAuxSendLevel(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnAuxSendLevel",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnBusSendLevel(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnBusSendLevel",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnChannelSendLevel(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnChannelSendLevel",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        #endregion

        #region Fader 
        private void _console_OnFxRtnFade(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnFxRtnFade",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnMatrixFade(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnMatrixFade",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnMainFade(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnMainFade",
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnAuxFade(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnAuxFade",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnDcaFade(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnDcaFade",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnBusFade(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnBusFade",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }

        private void _console_OnChannelFade(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnChannelFade",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToFloat());
            }
        }
        #endregion

        #region Mute
        private void _console_OnFxRtnMute(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnFxRtnMute",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToInt());
            }
        }

        private void _console_OnMatrixMute(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnMatrixMute",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToInt());
            }
        }

        private void _console_OnMainMute(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnMainMute",
                    packet.Arguments[0].ToInt());
            }
        }

        private void _console_OnAuxMute(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnAuxMute",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToInt());
            }
        }

        private void _console_OnDcaMute(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnDcaMute",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToInt());
            }
        }

        private void _console_OnBusMute(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnBusMute",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToInt());
            }
        }

        private void _console_OnChannelMute(object sender, OSC.OSCPacket packet)
        {
            foreach (Script s in _scripts)
            {
                s.FunctionCall(
                    "_OnChannelMute",
                    Int16.Parse(packet.Nodes[2]) - 1,
                    packet.Arguments[0].ToInt());
            }
        }
        #endregion

        public void Connect()
        {
            _console.Connect();
        }

        private void _InithFunction(Lua l)
        {
            l["Debug"] = new LuaDebug();
            l["Thread"] = new LuaThreads();
            l["Name"] = new ReName(_console);
            l["Mute"] = new ReMute(_console);
            l["Request"] = new ReRquest(_console);
            l["Sender"] = new ReSender(_console);
            l["Color"] = new ReColor(_console);
        }

        public void RunScripts(List<ScriptLua> scripts)
        {
            _scripts = new List<Script>();
            foreach (ScriptLua script in scripts)
            {
                if (script.Enable && script.State != StateScript.Container)
                {
                    Script luaScript = new Script(_InithFunction, script.Path);
                    _scripts.Add(luaScript);
                    luaScript.StartScript();
                }
            }
        }

        public void DestroyFunction()
        {
            foreach (Script sc in _scripts)
            {
                sc.Abort = true;
            }
            _scripts.Clear();
        }
    }
}