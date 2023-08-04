using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LightForm.Annotations;

namespace LightForm.Controls
{
    public class LightLabelShadow : Label, INotifyPropertyChanged
    {

        public Color Shadow 
        {
            get => _shadow;
            set
            {
                if (value.Equals(_shadow)) return;
                _shadow = value;
                OnPropertyChanged();
                Invalidate();
            }
        } private Color _shadow = Color.Gray;
        public double Angle 
        {
            get => _angle;
            set
            {
                if (value == _angle) return;
                _angle = value;
                OnPropertyChanged();
                Invalidate();
            }
        } private double _angle = 0;
        public int Distance {
            get => _distance;
            set
            {
                if (value == _distance) return;
                _distance = value;
                OnPropertyChanged();
                Invalidate();
            }
        } private int _distance = 5;


        public LightLabelShadow() : base()
        {
            base.AutoSize = false;
            base.DoubleBuffered = true;
            Paint+= OnPaint;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            double dx=Distance*Math.Sin(Angle/100), dy=Distance*Math.Cos(Angle/100);
            e.Graphics.Clear(BackColor);
            StringFormat formatter = new StringFormat();
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    formatter.Alignment = StringAlignment.Far;
                    formatter.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    formatter.Alignment = StringAlignment.Center;
                    formatter.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    formatter.Alignment = StringAlignment.Near;
                    formatter.LineAlignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleLeft:
                    formatter.Alignment = StringAlignment.Far;
                    formatter.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleCenter:
                    formatter.Alignment = StringAlignment.Center;
                    formatter.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    formatter.Alignment = StringAlignment.Near;
                    formatter.LineAlignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    formatter.Alignment = StringAlignment.Far;
                    formatter.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    formatter.Alignment = StringAlignment.Center;
                    formatter.LineAlignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomRight:
                    formatter.Alignment = StringAlignment.Near;
                    formatter.LineAlignment = StringAlignment.Far;
                    break;
            }
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.DrawString(Text, Font, new SolidBrush(Shadow), new RectangleF((float)dx, (float)dy, Width+(float)dx, Height+(float)dy),formatter);
            e.Graphics.DrawString(Text,Font,new SolidBrush(ForeColor),new RectangleF(0,0,Width,Height),formatter);
        }

        #region property event

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
