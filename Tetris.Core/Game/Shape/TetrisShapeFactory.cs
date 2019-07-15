using System;
using System.Collections.Generic;
using System.Drawing;

namespace Tetris.Core.Game.Shape
{
    public class TetrisShapeFactory : RandomSelector<ITetrisShape>
    {
        public TetrisShapeFactory(Random r) : base(r, new List<ITetrisShape> {
                new FourRotationShape(new Point[]
                    {
                        new Point( 0,-1),
                        new Point( 1,-1),
                        new Point( 1, 0),
                        new Point( 1, 1)
                    }), //L
                new FourRotationShape(new Point[]
                    {
                        new Point( 0,-1),
                        new Point(-1,-1),
                        new Point(-1, 0),
                        new Point(-1, 1)
                    }), //MirrorL
                new TwoRotationShape(new Point[]
                    {
                        new Point( 0,-2),
                        new Point( 0,-1),
                        new Point( 0, 0),
                        new Point( 0, 1)
                    }), //Line
                new StaticShape(new Point[]
                    {
                        new Point(-1, 0),
                        new Point(-1, 1),
                        new Point( 0, 0),
                        new Point( 0, 1)
                    }), //Square
                new FourRotationShape(new Point[]
                    {
                        new Point( 0,-1),
                        new Point( 0, 0),
                        new Point( 0, 1),
                        new Point( 1, 0)
                    }), //D 
                new FourRotationShape(new Point[] 
                    {
                        new Point( 1, 1),
                        new Point( 0, 0),
                        new Point( 0, 1),
                        new Point(-1, 0)
                    }), //Z
                new FourRotationShape(new Point[] 
                    {
                        new Point( 1,-1),
                        new Point( 0, 0),
                        new Point( 0, 1),
                        new Point( 1, 0)
                    }) //MirrorZ
            })
        {
        }
    }
}
