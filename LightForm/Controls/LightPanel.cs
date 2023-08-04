using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LightForm.Annotations;

namespace LightForm.Controls
{
    public class LightPanel : Panel, INotifyPropertyChanged
    {

        public bool ShadowVisible 
        {
            get => _shadowVisible;
            set
            {
                if (value == _shadowVisible) return;
                _shadowVisible = value;
                OnPropertyChanged();
                Invalidate();
            }
        } private bool _shadowVisible = true;
        public Color ShadowColor {
            get => _shadowColor;
            set
            {
                if (value.Equals(_shadowColor)) return;
                _shadowColor = value;
                OnPropertyChanged();
            }
        } private Color _shadowColor = Color.Silver;
        public double ShadowAngle {
            get => _shadowAngle;
            set
            {
                if (value.Equals(_shadowAngle)) return;
                _shadowAngle = value;
                OnPropertyChanged();
                Invalidate();
            }
        } private double _shadowAngle = 90;
        public int ShadowDistance {
            get => _shadowDistance;
            set
            {
                if (value == _shadowDistance) return;
                _shadowDistance = value;
                OnPropertyChanged();
                Invalidate();
            }
        } private int _shadowDistance = 4;

        public LightPanel()
        {
            Paint+= OnPaint;
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            SuspendLayout();
            double dx = ShadowDistance*Math.Sin(ShadowAngle/100) , dy = ShadowDistance*Math.Cos(ShadowAngle/100);
            if(ShadowVisible)
                e.Graphics.FillRectangle(new SolidBrush(ShadowColor), Left+(int)dx,Top+(int)dy, Width,Height );
            e.Graphics.FillRectangle(new SolidBrush(BackColor),Bounds);
            PerformLayout();
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
