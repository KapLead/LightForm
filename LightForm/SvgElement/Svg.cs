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
            bmp = new Bitmap(Size.Width, Size.Height, PixelFormat.Format32bppArgb);



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
