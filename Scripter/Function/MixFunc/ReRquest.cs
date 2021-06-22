using Behringer.X32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripter.Function.Naming
{
    public class ReRquest
    {
        private X32Console _console;

        public ReRquest(X32Console console)
        {
            _console = console;
        }

        public void Fader(string value, int index = 0)
        {
            switch (value)
            {
                case "Channel":
                    Console.WriteLine("[Channel]Request: {0}", index);
                    _console.ControlRequest(_console.Channel[index].Strip.Fader);
                    break;
                case "Bus":
                    Console.WriteLine("[Bus]Request: {0}", index);
                    _console.ControlRequest(_console.Bus[index].Strip.Fader);
                    break;
                case "Dca":
                    Console.WriteLine("[Dca]Request: {0}", index);
                    _console.ControlRequest(_console.Dca[index].Strip.Fader);
                    break;
                case "Aux":
                    Console.WriteLine("[Aux]Request: {0}", index);
                    _console.ControlRequest(_console.Aux[index].Strip.Fader);
                    break;
                case "Main":
                    Console.WriteLine("[Main]Request");
                    _console.ControlRequest(_console.Main.Strip.Fader);
                    break;
                case "Matrix":
                    Console.WriteLine("[Matrix]Request: {0}", index);
                    _console.ControlRequest(_console.Matrix[index].Strip.Fader);
                    break;
                case "Fx":
                    Console.WriteLine("[Fx]Request: {0}", index);
                    _console.ControlRequest(_console.FxRtn[index].Strip.Fader);
                    break;

            }
        }

        public void BusFader(string value, int indexBus, int index)
        {
            switch (value)
            {
                case "Channel":
                    Console.WriteLine("[Bus{0}]->[Channel]Request: {1}", indexBus, index);
                    _console.ControlRequest(_console.Channel[index].Strip.MixBuss[indexBus].Fader);
                    break;
                case "Aux":
                    Console.WriteLine("[Bus{0}]->[Aux]Request: {1}", indexBus, index);
                    _console.ControlRequest(_console.Aux[index].Strip.MixBuss[indexBus].Fader);
                    break;
                case "Fx":
                    Console.WriteLine("[Bus{0}]->[Fx]Request: {1}", indexBus, index);
                    _console.ControlRequest(_console.FxRtn[index].Strip.MixBuss[indexBus].Fader);
                    break;
            }
        }

        public void Mute(string value, int index = 0)
        {
            switch (value)
            {
                case "Channel":
                    Console.WriteLine("[Channel]Request: {0}", index);
                    _console.ControlRequest(_console.Channel[index].Strip.Mute);
                    break;
                case "Bus":
                    Console.WriteLine("[Bus]Request: {0}", index);
                    _console.ControlRequest(_console.Bus[index].Strip.Mute);
                    break;
                case "Dca":
                    Console.WriteLine("[Dca]Request: {0}", index);
                    _console.ControlRequest(_console.Dca[index].Strip.Mute);
                    break;
                case "Aux":
                    Console.WriteLine("[Aux]Request: {0}", index);
                    _console.ControlRequest(_console.Aux[index].Strip.Mute);
                    break;
                case "Main":
                    Console.WriteLine("[Main]Request");
                    _console.ControlRequest(_console.Main.Strip.Mute);
                    break;
                case "Matrix":
                    Console.WriteLine("[Matrix]Request: {0}", index);
                    _console.ControlRequest(_console.Matrix[index].Strip.Mute);
                    break;
                case "Fx":
                    Console.WriteLine("[Fx]Request: {0}", index);
                    _console.ControlRequest(_console.FxRtn[index].Strip.Mute);
                    break;

            }
        }
    }
}
