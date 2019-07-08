using System;
using System.Windows.Forms;

namespace Tetris.Winforms
{
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
        }
    }
}
