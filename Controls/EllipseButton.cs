using FirstClicker;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MoneyMiner.Controls
{
    public class EllipseButton : Button
    {
        internal GraphicsPath graphicsPath;
        public EllipseButton() : base()
        {
            graphicsPath = new();
            base.Paint += this.DrawEllipseButton;
            Region ellipseRegion = new Region(graphicsPath);
            base.Region = ellipseRegion;
            base.SetBounds(base.Location.X, base.Location.Y, base.Width, base.Height);
        }
        internal void DrawEllipseButton(object? sender, PaintEventArgs e)
        {
            this.graphicsPath = CreateEllipse(this.Location.X, this.Location.Y, this.Width, this.Height);

            Pen myPen = new Pen(Colors.colButtonEnabled, 3);
            e.Graphics.DrawPath(myPen, graphicsPath);
        }
        public static GraphicsPath CreateEllipse(int startx, int starty, int width, int height)
        {
            Rectangle myRec = new Rectangle(startx, starty, width, height);
            GraphicsPath myPath = new GraphicsPath(FillMode.Winding);
            myPath.AddEllipse(myRec);
            return myPath;
        }
    }
}
