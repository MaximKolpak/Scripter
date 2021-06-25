using Behringer.X32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripter.Function.MixFunc
{
    public class ReColor
    {
        private X32Console _console;

        public ReColor(X32Console console)
        {
            _console = console;
        }

        public void Channel(int index, int color)
        {
            _console.Channel[index].Strip.Config.Color.Value = color;
            _console.SendParameter(_console.Channel[index].Strip.Config.Color);
        }

        public void Bus(int index, int color)
        {
            _console.Bus[index].Strip.Config.Color.Value = color;
            _console.SendParameter(_console.Bus[index].Strip.Config.Color);
        }

        public void Dca(int index, int color)
        {
            _console.Dca[index].Strip.Config.Color.Value = color;
            _console.SendParameter(_console.Dca[index].Strip.Config.Color);
        }

        public void Aux(int index, int color)
        {
            _console.Aux[index].Strip.Config.Color.Value = color;
            _console.SendParameter(_console.Aux[index].Strip.Config.Color);
        }

        public void Main(int color)
        {
            _console.Main.Strip.Config.Color.Value = color;
            _console.SendParameter(_console.Main.Strip.Config.Color);
        }

        public void Matrix(int index, int color)
        {
            _console.Matrix[index].Strip.Config.Color.Value = color;
            _console.SendParameter(_console.Matrix[index].Strip.Config.Color);
        }

        public void Fx(int index, int color)
        {
            _console.FxRtn[index].Strip.Config.Color.Value = color;
            _console.SendParameter(_console.FxRtn[index].Strip.Config.Color);
        }

        /*Lua Color
0 - Black
1 - Red
2 - Green
3 - Yellow
4 - Blue
5 - Pink
6 - Bright blue
7 - WWhite
8 - Grey
9 - Red outline
10 - Green outline
11 - Yellow outline
12 - Blue outline
13 - Pink outline
14 - Bright blue outline
15 - White outline
        */
    }
}
