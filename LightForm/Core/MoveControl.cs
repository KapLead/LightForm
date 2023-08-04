using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace LightForm.Core
{
    public class MoveControl
    {
        private Control[] Source;
        private Control Target;
        private Point moved = Point.Empty;
        private Rectangle Bound = Rectangle.Empty;
        public MoveControl(Control[] source,Control target)
        {
            Source = source;
            Target = target;
            foreach (Control c in source)
                AppContorlControl(c);
        }

        /// <summary> Установка ограничивающего прямоугольника </summary>
        public MoveControl SetBound(Rectangle rect)
        {
            Bound = rect;
            return this;
        }

        private void AppContorlControl(Control c)
        {
            c.MouseDown+= SourceOnMouseDown;
            c.MouseUp+= SourceOnMouseUp;
            c.MouseMove+= SourceOnMouseMove;
        }

        private void SourceOnMouseMove(object sender, MouseEventArgs e)
        {
            if(moved== Point.Empty) return;
            if(Target==null) return;
            int dx = Target.Location.X - moved.X + e.X,
                dy = Target.Location.Y - moved.Y + e.Y;
            if (Bound != Rectangle.Empty)
            {
                if (dx < Bound.Left) dx = Bound.Left;
                if(dx+Target.Width>Bound.Right) dx = Bound.Right-Target.Width;
                if(dy<Bound.Top) dy = Bound.Top;
                if(dy+Target.Height>Bound.Bottom) dy  = Bound.Bottom-Target.Height;
            }
            Target.Location = new Point(dx,dy);
        }

        private void SourceOnMouseUp(object sender, MouseEventArgs e)
        {
            moved = Point.Empty;
            Target.Cursor = Cursors.Default;
        }

        private void SourceOnMouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button!= MouseButtons.Left) return;
            moved = e.Location;
            Target.Cursor = Cursors.SizeAll;
        }
    }
}
