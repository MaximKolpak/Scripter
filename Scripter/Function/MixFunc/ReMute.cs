using Behringer.X32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripter.Function.Naming
{
    public class ReMute
    {
        private X32Console _console;

        public ReMute(X32Console console)
        {
            _console = console;
        }

        public void Channel(int index, int value)
        {
            if (value == 1)
                _console.Channel[index].Strip.Mute.Value = X32OnOff.On;
            if (value == 0)
                _console.Channel[index].Strip.Mute.Value = X32OnOff.Off;
            _console.SendParameter(_console.Channel[index].Strip.Mute);
        }

        public void Bus(int index, int value)
        {
            if (value == 1)
                _console.Bus[index].Strip.Mute.Value = X32OnOff.On;
            if (value == 0)
                _console.Bus[index].Strip.Mute.Value = X32OnOff.Off;
            _console.SendParameter(_console.Bus[index].Strip.Mute);
        }

        public void Dca(int index, int value)
        {
            if (value == 1)
                _console.Dca[index].Strip.Mute.Value = X32OnOff.On;
            if (value == 0)
                _console.Dca[index].Strip.Mute.Value = X32OnOff.Off;
            _console.SendParameter(_console.Dca[index].Strip.Mute);
        }

        public void Aux(int index, int value)
        {
            if (value == 1)
                _console.Aux[index].Strip.Mute.Value = X32OnOff.On;
            if (value == 0)
                _console.Aux[index].Strip.Mute.Value = X32OnOff.Off;
            _console.SendParameter(_console.Aux[index].Strip.Mute);
        }

        public void Main(int value)
        {
            if (value == 1)
                _console.Main.Strip.Mute.Value = X32OnOff.On;
            if (value == 0)
                _console.Main.Strip.Mute.Value = X32OnOff.Off;
            _console.SendParameter(_console.Main.Strip.Mute);
        }

        public void Matrix(int index, int value)
        {
            if (value == 1)
                _console.Matrix[index].Strip.Mute.Value = X32OnOff.On;
            if (value == 0)
                _console.Matrix[index].Strip.Mute.Value = X32OnOff.Off;
            _console.SendParameter(_console.Matrix[index].Strip.Mute);
        }

        public void Fx(int index, int value)
        {
            if (value == 1)
                _console.FxRtn[index].Strip.Mute.Value = X32OnOff.On;
            if (value == 0)
                _console.FxRtn[index].Strip.Mute.Value = X32OnOff.Off;
            _console.SendParameter(_console.FxRtn[index].Strip.Mute);
        }
    }
}
