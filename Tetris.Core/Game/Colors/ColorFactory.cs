using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tetris.Core.Game.Colors
{
    public class ColorFactory : RandomSelector<Color>
    {
        public ColorFactory(Random random) : 
            base(random, new List<Color> {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Purple,
                Color.Orange,
                Color.Turquoise
            })
        {
        }
    }
}
