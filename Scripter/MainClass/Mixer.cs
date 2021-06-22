using Behringer.X32;
using NLua;
using Scripter.Function;
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

            #region Mute
            _console.OnChannelMute += _console_OnChannelMute;
            _console.OnBusMute += _console_OnBusMute;
            _console.OnDcaMute += _console_OnDcaMute;
            _console.OnAuxMute += _console_OnAuxMute;
            _console.OnMainMute += _console_OnMainMute;
            _console.OnMatrixMute += _console_OnMatrixMute;
            _console.OnFxRtnMute += _console_OnFxRtnMute;
            #endregion

        }

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
                    Int16.Parse(packet.Nodes[2])-1,
                    packet.Arguments[0].ToInt());
            }
        }

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