using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using LightForm.Annotations;
using LightForm.Controls;

namespace LightForm
{
    public class LightForm : Form, INotifyPropertyChanged
    {
        public event EventHandler IconChange;
        public Panel HeaderPanel { get; set; }
        public LightButton CloseButton { get; set; }
        public LightButton MaximizedButton { get; set; }
        public LightButton MinimizedButton { get; set; }
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

        public LightForm() : base()
        {
            ToolTip = new ToolTip();

            WindowState = FormWindowState.Normal;
            FormBorderStyle = FormBorderStyle.None;
            HeaderPanel = new Panel{ Height = 28, Dock = DockStyle.Top };
            Controls.Add(HeaderPanel);
            HeaderPanel.BringToFront();
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
            HeaderPanel.Controls.Add(MinimizedButton);
            MinimizedButton.BringToFront();
            ToolTip.SetToolTip(MinimizedButton, "Свернуть окно");

            IconBox = new PictureBox { Width = 28,Dock = DockStyle.Left, BackgroundImage = (Icon==null?Properties.Resources.application:Icon)?.ToBitmap(), BackgroundImageLayout = ImageLayout.Stretch};
            HeaderPanel.Controls.Add(IconBox);
            IconBox.BringToFront();


            TitleBox = new Label
            {
                Dock = DockStyle.Fill, 
                Text = base.Text,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Arial",10, FontStyle.Bold),
                Margin = new Padding(5, 2, 0, 0)
            };
            HeaderPanel.Controls.Add(TitleBox);
            TitleBox.BringToFront();

            TextChanged+= OnTextChanged;
            
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            TitleBox.Text = base.Text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
