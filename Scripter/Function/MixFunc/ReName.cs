using Behringer.X32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripter.Function.Naming
{
    public class ReName
    {
        private X32Console _console;

        public ReName(X32Console console)
        {
            _console = console;
        }

        public void Channel(int index, string value)
        {
            _console.Channel[index].Strip.Config.Name.Value = value;
            _console.SendParameter(_console.Channel[index].Strip.Config.Name);
        }

        public void Bus(int index, string value)
        {
            _console.Bus[index].Strip.Config.Name.Value = value;
            _console.SendParameter(_console.Bus[index].Strip.Config.Name);
        }

        public void Dca(int index, string value)
        {
            _console.Dca[index].Strip.Config.Name.Value = value;
            _console.SendParameter(_console.Dca[index].Strip.Config.Name);
        }

        public void Aux(int index, string value)
        {
            _console.Aux[index].Strip.Config.Name.Value = value;
            _console.SendParameter(_console.Aux[index].Strip.Config.Name);
        }

        public void Main(string value)
        {
            _console.Main.Strip.Config.Name.Value = value;
            _console.SendParameter(_console.Main.Strip.Config.Name);
        }

        public void Matrix(int index, string value)
        {
            _console.Matrix[index].Strip.Config.Name.Value = value;
            _console.SendParameter(_console.Matrix[index].Strip.Config.Name);
        }

    }
}
