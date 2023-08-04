using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LightForm.Annotations;
using LightForm.Controls;
using LightForm.Core;

namespace LightForm
{
    public class LightForm : Form, INotifyPropertyChanged
    {
        public event EventHandler IconChange;
        public Panel HeaderPanel { get; set; }
        public LightButton CloseButton { get; set; }
        public LightButton MaximizedButton { get; set; }
        public LightButton MinimizedButton { get; set; }
        public Color BorderColor 
        {
            get => _borderColor;
            set
            {
                if (value.Equals(_borderColor)) return;
                _borderColor = value;
                Invalidate();
                OnPropertyChanged();
            }
        } private Color _borderColor = Color.Red;
        public bool BorderVisible 
        {
            get => _borderVisible;
            set
            {
                if (value == _borderVisible) return;
                _borderVisible = value;
                base.Padding = new Padding(_borderVisible?2:0);
                OnPropertyChanged();
            }
        } private bool _borderVisible = true;
        public bool IsResize 
        {
            get => _isResize;
            set
            {
                if (value == _isResize) return;
                _isResize = value;
                OnPropertyChanged();
                Invalidate();
            }
        } private bool _isResize = true;

        public new Icon Icon 
        {
            get => _icon;
            set
            {
                if (Equals(value, _icon)) return;
                _icon = value;
                OnPropertyChanged();
                IconChange?.Invoke(this, EventArgs.Empty);
            }
        } private Icon _icon;
        public PictureBox IconBox { get; set; }
        public Label TitleBox { get; set; }
        public ToolTip ToolTip;

        
        private MoveControl moved;
        private ResizeControl resize;
        public LightForm() : base()
        {
            ToolTip = new ToolTip();
            base.DoubleBuffered = true;
            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            HeaderPanel = new Panel{ Height = 28, Dock = DockStyle.Top };
            Controls.Add(HeaderPanel);
            HeaderPanel.SendToBack();
            CloseButton = new LightButton
            {
                Size = new Size(28, 28), BackColor =Color.DarkRed,
                Dock = DockStyle.Right, 
                FlatAppearance = { MouseOverBackColor = Color.Crimson },
                Data = "W2 M7,5 L22,20 M7,20 L22,5"
            };
            ToolTip.SetToolTip(CloseButton,"Закрыть окно");

            CloseButton.Svg.Color = Color.White;
            CloseButton.Click += (sender, args) => Close();
            HeaderPanel.Controls.Add(CloseButton);
            CloseButton.BringToFront();
            MaximizedButton = new LightButton
            {
                Size = new Size(28, 28),
                Dock = DockStyle.Right,
                FlatAppearance = { MouseOverBackColor = Color.DarkGray },
                Data = "W2 M5,5 R23,23"
            };
            MaximizedButton.Click += TitleBoxOnDoubleClick;
            HeaderPanel.Controls.Add(MaximizedButton);
            MaximizedButton.BringToFront();
            ToolTip.SetToolTip(MaximizedButton, "Развернуть / восстановить размер окна");

            MinimizedButton = new LightButton
            {
                Size = new Size(28, 28),
                Dock = DockStyle.Right,
                FlatAppearance = { MouseOverBackColor = Color.DarkGray },
                Data = "W2 M5,22 H20"
            };
            MinimizedButton.Click += (sender, args) => { WindowState = FormWindowState.Minimized; };
            HeaderPanel.Controls.Add(MinimizedButton);
            MinimizedButton.BringToFront();
            ToolTip.SetToolTip(MinimizedButton, "Свернуть окно");

            IconBox = new PictureBox
            {
                Width = 28,
                Dock = DockStyle.Left,
                BackgroundImage = (this.Icon==null?Properties.Resources.application:this.Icon)?.ToBitmap(), 
                BackgroundImageLayout = ImageLayout.Zoom
            };
            HeaderPanel.Controls.Add(IconBox);
            IconBox.BringToFront();

            TitleBox = new Label
            {
                Dock = DockStyle.Fill, 
                Text = base.Text, Padding = new Padding(10,0,0,0),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial",10, FontStyle.Bold),
                Margin = new Padding(5, 2, 0, 0)
            };
            moved = new MoveControl(new Control[]{TitleBox,IconBox}, this);
            HeaderPanel.Controls.Add(TitleBox);
            TitleBox.BringToFront();
            TextChanged+= OnTextChanged;

            resize = new ResizeControl(this);

            Paint+= OnPaint;
            TitleBox.DoubleClick+= TitleBoxOnDoubleClick; 
            IconChange+= OnIconChange;
        }

        private void OnIconChange(object sender, EventArgs e)
        {
            IconBox.BackgroundImage = Icon.ToBitmap();
        }

        private void TitleBoxOnDoubleClick(object sender, EventArgs e)
        {
            if (MaximizedButton.Data == "W2 M5,5 R23,23")
            {
                MaximizedButton.Data = "W2 M8,8 R20,20 M12,8 V-3 H12 V12 H-4";
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                MaximizedButton.Data = "W2 M5,5 R23,23";
                WindowState = FormWindowState.Normal;
            }
            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            if(BorderVisible)
                e.Graphics.DrawRectangle(new Pen(BorderColor),1,1,base.Width-3, base.Height-3);
            if(IsResize)
                e.Graphics.FillPolygon(new SolidBrush(BorderColor),new []
                {
                    new Point(Width-1,Height-1),
                    new Point(Width-1,Height-16),
                    new Point(Width-16,Height-1),
                    new Point(Width-1,Height-1),
                });
        }

        private void OnTextChanged(object sender, EventArgs e) => TitleBox.Text = base.Text;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
