using System.Drawing;
using System.Windows.Forms;

namespace LightForm.Core
{
    public class ResizeControl
    {
        private Control Target;
        private Point resizing = Point.Empty;
        public ResizeControl(Control target)
        {
            Target = target;
            Target.MouseMove+= TargetOnMouseMove;
            Target.MouseDown += (sender, args) => resizing = args.Location;
            Target.MouseUp += (sender, args) => resizing = Point.Empty;
        }

        private bool IsMoved(MouseEventArgs e)
        {
            return e.X > Target.Width - 16 && e.Y > Target.Height - 16;
        }
        private void TargetOnMouseMove(object sender, MouseEventArgs e)
        {
            Target.Cursor = IsMoved(e)?Cursors.SizeNWSE:Cursors.Default;
            if(resizing== Point.Empty) return;
            int dx=Target.Size.Width + e.X - resizing.X, dy= Target.Size.Height + e.Y - resizing.Y;
            if (Target.MinimumSize != Size.Empty)
            {
                if (dx < Target.MinimumSize.Width) dx = Target.MinimumSize.Width;
                if (dy < Target.MinimumSize.Height) dy = Target.MinimumSize.Height;
            }
            if (Target.MaximumSize != Size.Empty)
            {
                if (dx > Target.MaximumSize.Width) dx = Target.MaximumSize.Width;
                if (dy > Target.MaximumSize.Height) dy = Target.MaximumSize.Height;
            }
            Target.Size = new Size(dx,dy);
            resizing = e.Location;
            Target.Invalidate();

        }
    }
}
