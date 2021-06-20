using Behringer.X32;
using NLua;
using Scripter.Function;
using Scripter.Settings;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Scripter.MainClass
{
    internal class Mixer
    {
        private X32Console _console;

        private List<Script> _scripts;
        private MethodCall _methods;

        public Mixer(string IpMixer, int port = 0, int interval = 0)
        {
            _console = new X32Console();
            _console.IPAddress = IPAddress.Parse(IpMixer);

            if (port != 0)
                _console.Port = port;
            if (interval != 0)
                _console.Interval = interval;
        }

        public void Connect()
        {
            _console.Connect();
        }

        private void _InithFunction(Lua l)
        {
            l["Debug"] = new LuaDebug();
            l["Thread"] = new LuaThreads();
            l.RegisterFunction("BusChangeName", this, typeof(Mixer).GetMethod("ChangeNameBus"));
            l.RegisterFunction("BusSelectMute", this, typeof(Mixer).GetMethod("BusSelectMut"));
            l.RegisterFunction("RequestAuxFader", this, typeof(Mixer).GetMethod("RequestAuxFader"));
            l.RegisterFunction("RequestAuxMixBussFader", this, typeof(Mixer).GetMethod("RequestAuxMixBussFader"));
            l.RegisterFunction("SendAuxFaderValue", this, typeof(Mixer).GetMethod("SendAuxFaderValue"));
            l.RegisterFunction("SendAuxMixBusFader", this, typeof(Mixer).GetMethod("SendAuxMixBusFader"));
            l.RegisterFunction("AuxMute", this, typeof(Mixer).GetMethod("AuxMute"));

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
                }
            }
            _methods = new MethodCall(_console, _scripts);
            CallMainFunction();
        }

        private void CallMainFunction()
        {
            foreach (Script script in _scripts)
            {
                script.MainFunction();
            }
        }

        public void DestroyFunction()
        {
            if (_methods != null)
                _methods.Destroy();

            foreach(Script sc in _scripts)
            {
                sc.Abort = true;
            }
        }


        #region Fuction Lua > c#
        public void ChangeNameBus(double index, string name)
        {
            new Thread(() =>
            {
                int iBus = (int)index;
                _console.Bus[iBus].Strip.Config.Name.Value = name;
                _console.SendParameter(_console.Bus[iBus].Strip.Config.Name);
            }).Start();

        }

        public void BusSelectMut(double index, double value)
        {
            new Thread(() =>
            {
                int iBus = (int)index;
                if (value == 1)
                    _console.Bus[iBus].Strip.Mute.Value = X32OnOff.On;
                if (value == 0)
                    _console.Bus[iBus].Strip.Mute.Value = X32OnOff.Off;
                _console.SendParameter(_console.Bus[iBus].Strip.Mute);
            }).Start();

        }

        public void SendAuxFaderValue(double indexAux, float value)
        {
            new Thread(() =>
            {
                int iAux = (int)indexAux;
                _console.Aux[iAux].Strip.Fader.Value = value;
                _console.SendParameter(_console.Aux[iAux].Strip.Fader);
            }).Start();
        }

        public void SendAuxMixBusFader(double indexAux, double indexBus, float value)
        {
            new Thread(() =>
            {
                int iAux = (int)indexAux;
                int iBus = (int)indexBus;
                _console.Aux[iAux].Strip.MixBuss[iBus].Fader.Value = value;
                _console.SendParameter(_console.Aux[iAux].Strip.MixBuss[iBus].Fader);
            }).Start();
        }

        public void AuxMute(double indexAux, double value)
        {
            new Thread(() =>
            {
                int iAux = (int)indexAux;
                if (value == 1)
                    _console.Aux[iAux].Strip.Mute.Value = X32OnOff.On;
                if (value == 0)
                    _console.Aux[iAux].Strip.Mute.Value = X32OnOff.Off;
                _console.SendParameter(_console.Aux[iAux].Strip.Mute);
            }).Start();
        }


        #region Request value
        public void RequestAuxFader(double indexAux)
        {
            int iAux = (int)indexAux;
            _console.ControlRequest(_console.Aux[iAux].Strip.Fader);
        }

        public void RequestAuxMixBussFader(double indexAux, double indexBus)
        {
            new Thread(() =>
            {
                int iAux = (int)indexAux;
                int iBus = (int)indexBus;
                _console.ControlRequest(_console.Aux[iAux].Strip.MixBuss[iBus].Fader);
            }).Start();
        }
        #endregion

        #endregion
    }
}