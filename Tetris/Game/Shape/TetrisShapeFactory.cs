using System;
using System.Collections.Generic;

namespace Tetris.Game.Shape
{
    public class TetrisShapeFactory : RandomFactory<ITetrisShape>
    {
        public TetrisShapeFactory(Random r) : base(r, new List<ITetrisShape> {
                new LeftL(),
                new RightL(),
                new Line(),
                new Square(),
                new Tripod(),
                new Z(),
                new MirrorZ()
            })
        {
        }
    }
}
