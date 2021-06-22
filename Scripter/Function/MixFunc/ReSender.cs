using Behringer.X32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripter.Function.MixFunc
{
    public class ReSender
    {
        private X32Console _console;

        public ReSender(X32Console console)
        {
            _console = console;
        }

        public void Fader(string type, float value, int index = 0)
        {
            switch (type)
            {
                case "Channel":
                    Console.WriteLine("[Channel ({0})]Send: {1}", index, value);
                    _console.Channel[index].Strip.Fader.Value = value;
                    _console.SendParameter(_console.Channel[index].Strip.Fader);
                    break;
                case "Bus":
                    Console.WriteLine("[Bus ({0})]Send: {1}", index, value);
                    _console.Bus[index].Strip.Fader.Value = value;
                    _console.SendParameter(_console.Bus[index].Strip.Fader);
                    break;
                case "Dca":
                    Console.WriteLine("[Dca ({0})]Send: {1}", index, value);
                    _console.Dca[index].Strip.Fader.Value = value;
                    _console.SendParameter(_console.Dca[index].Strip.Fader);
                    break;
                case "Aux":
                    Console.WriteLine("[Aux ({0})]Send: {1}", index, value);
                    _console.Aux[index].Strip.Fader.Value = value;
                    _console.SendParameter(_console.Aux[index].Strip.Fader);
                    break;
                case "Main":
                    Console.WriteLine("[Main]Send: {0}", value);
                    _console.Main.Strip.Fader.Value = value;
                    _console.SendParameter(_console.Main.Strip.Fader);
                    break;
                case "Matrix":
                    Console.WriteLine("[Matrix ({0})]Send: {1}", index, value);
                    _console.Matrix[index].Strip.Fader.Value = value;
                    _console.SendParameter(_console.Matrix[index].Strip.Fader);
                    break;
                case "Fx":
                    Console.WriteLine("[Fx ({0})]Send: {1}", index, value);
                    _console.FxRtn[index].Strip.Fader.Value = value;
                    _console.SendParameter(_console.FxRtn[index].Strip.Fader);
                    break;
            }
        }

        public void BusFader(string type, float value, int indexBus, int index)
        {
            switch (type)
            {
                case "Channel":
                    Console.WriteLine("[Bus ({0})]->[Channel ({1})]Send: {2}", indexBus, index, value);
                    _console.Channel[index].Strip.MixBuss[indexBus].Fader.Value = value;
                    _console.SendParameter(_console.Channel[index].Strip.MixBuss[indexBus].Fader);
                    break;
                case "Aux":
                    Console.WriteLine("[Bus ({0})]->[Aux ({1})]Send: {2}", indexBus, index, value);
                    _console.Aux[index].Strip.MixBuss[indexBus].Fader.Value = value;
                    _console.SendParameter(_console.Aux[index].Strip.MixBuss[indexBus].Fader);
                    break;
                case "Fx":
                    Console.WriteLine("[Bus ({0})]->[Fx ({1})]Send: {2}", indexBus, index, value);
                    _console.FxRtn[index].Strip.MixBuss[indexBus].Fader.Value = value;
                    _console.SendParameter(_console.FxRtn[index].Strip.MixBuss[indexBus].Fader);
                    break;
            }
        }
    }
}
