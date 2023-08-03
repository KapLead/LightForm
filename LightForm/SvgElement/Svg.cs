using LightForm.Annotations;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;

namespace LightForm.SvgElement
{
    public class Svg : INotifyPropertyChanged
    {

        public string Data 
        {
            get => _data;
            set
            {
                if (value == _data) return;
                _data = value;
                OnPropertyChanged();
            }
        } private string _data;

        public Size Size 
        {
            get => _size;
            set
            {
                if (value.Equals(_size)) return;
                _size = value;
                OnPropertyChanged();
            }
        } private Size _size;

        public Color Color 
        {
            get => _color;
            set
            {
                if (value.Equals(_color)) return;
                _color = value;
                OnPropertyChanged();
            }
        } private Color _color = Color.Black;

        public Color Background {
            get => _background;
            set
            {
                if (value.Equals(_background)) return;
                _background = value;
                OnPropertyChanged();
            }
        } private Color _background = SystemColors.Control;

        private Bitmap bmp;

        public Svg()
        {
            Size = Size.Empty;
            Data = "";
        }

        public Svg(string data)
        {
            Data = data;
            Size = new Size(16, 16);
        }

        public Bitmap Draw()
        {
            if (Size == Size.Empty) return null;
            bmp = new Bitmap(Size.Width, Size.Height, PixelFormat.Format32bppArgb);
            var svg = SvgElements.Parse(Data);

            int cx = 0, cy = 0;
            Color c = Color;
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                Pen p = new Pen(c);
                foreach (var el in svg)
                {
                    switch (el.Prefix)
                    {
                        case 'W': p = new Pen(c,(int)el.Params[0]); break;
                        case 'M': cx = (int)el.Params[0]; cy = (int)el.Params[1]; break;
                        case 'L': g.DrawLine(p, cx,cy, (int)el.Params[0], (int)el.Params[1]);
                            cx = (int)el.Params[0]; cy = (int)el.Params[1]; break;
                        case 'V': g.DrawLine(p, cx, cy, cx, cy+(int)el.Params[0]);
                            cy += (int)el.Params[0]; break;
                        case 'H': g.DrawLine(p, cx, cy, cx + (int)el.Params[0],cy);
                            cx += (int)el.Params[0]; break;
                        case 'R': g.DrawRectangle(p,cx,cy, (int)el.Params[0]-cx, (int)el.Params[1]-cy);
                            cx = (int)el.Params[0]; cy = (int)el.Params[1]; break;
                        case 'E': g.DrawEllipse(p, cx, cy, (int)el.Params[0] - cx, (int)el.Params[1] - cy);
                            cx = (int)el.Params[0]; cy = (int)el.Params[1]; break;
                    }
                }
            }
            return bmp;
        }

        #region event notify

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
