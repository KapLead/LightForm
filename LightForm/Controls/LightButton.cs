using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LightForm.Annotations;
using LightForm.SvgElement;

namespace LightForm.Controls
{
    public class LightButton : Button, INotifyPropertyChanged
    {

        public string Data
        {
            get=>Svg.Data;
            set
            {
                Svg.Data = value;
                OnPropertyChanged();
            }
        }

        public Svg Svg = new Svg();
        public LightButton() : base()
        {
            this.DoubleBuffered = true;
            this.Text = "";
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize=0;
            this.Svg.PropertyChanged += SvgOnPropertyChanged;
            this.Resize+= OnResize;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void OnResize(object sender, EventArgs e)
        {
            var min = Math.Min(Width, Height);
            Svg.Size = new Size(min,min);
        }

        private void SvgOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            BackgroundImage = Svg.Draw();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
