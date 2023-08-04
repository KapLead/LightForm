using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LightForm.Controls
{
    public class LightScrollBar : ScrollBar
    {
        private Rectangle clickedBarRectangle;
        private Rectangle thumbRectangle;
        private Rectangle leftArrowRectangle;
        private Rectangle rightArrowRectangle;
        private bool leftArrowClicked = false;
        private bool rightArrowClicked = false;
        private bool leftBarClicked = false;
        private bool rightBarClicked = false;
        private bool thumbClicked = false;
        private ScrollBarState thumbState = ScrollBarState.Normal;
        private ScrollBarArrowButtonState leftButtonState = ScrollBarArrowButtonState.LeftNormal;
        private ScrollBarArrowButtonState rightButtonState = ScrollBarArrowButtonState.RightNormal;
        private int thumbWidth = 15;
        private int arrowWidth = 17;
        private int thumbRightLimitRight = 0;
        private int thumbRightLimitLeft = 0;
        private int thumbLeftLimit = 0;
        private int thumbPosition = 0;
        private int trackPosition = 0;
        private Timer progressTimer = new Timer();

        public LightScrollBar() : base()
        {
            this.Location = new Point(10, 10);
            this.Width = 200;
            this.Height = 20;
            this.DoubleBuffered = true;

            SetUpScrollBar();
            progressTimer.Interval = 20;
            progressTimer.Tick += new EventHandler(progressTimer_Tick);
        }

        private void SetUpScrollBar()
        {
            clickedBarRectangle = ClientRectangle;
            thumbRectangle = new Rectangle(
                ClientRectangle.X + ClientRectangle.Width / 2,
                ClientRectangle.Y, thumbWidth,
                ClientRectangle.Height);
            leftArrowRectangle = new Rectangle(
                ClientRectangle.X, ClientRectangle.Y,
                arrowWidth, ClientRectangle.Height);
            rightArrowRectangle = new Rectangle(
                ClientRectangle.Right - arrowWidth,
                ClientRectangle.Y, arrowWidth,
                ClientRectangle.Height);
            thumbPosition = thumbWidth / 2;
            thumbRightLimitRight = ClientRectangle.Right - arrowWidth;
            thumbRightLimitLeft = thumbRightLimitRight - thumbWidth;
            thumbLeftLimit = ClientRectangle.X + arrowWidth;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!ScrollBarRenderer.IsSupported)
            {
                this.Parent.Text = "CustomScrollBar Disabled";
                return;
            }
            this.Parent.Text = "CustomScrollBar Enabled";
            ScrollBarRenderer.DrawRightHorizontalTrack(e.Graphics, ClientRectangle, ScrollBarState.Normal);
            ScrollBarRenderer.DrawHorizontalThumb(e.Graphics, thumbRectangle, thumbState);
            ScrollBarRenderer.DrawHorizontalThumbGrip(e.Graphics, thumbRectangle, thumbState);
            ScrollBarRenderer.DrawArrowButton(e.Graphics, leftArrowRectangle, leftButtonState);
            ScrollBarRenderer.DrawArrowButton(e.Graphics, rightArrowRectangle, rightButtonState);

            if (leftBarClicked)
            {
                clickedBarRectangle.X = thumbLeftLimit;
                clickedBarRectangle.Width = thumbRectangle.X - thumbLeftLimit;
                ScrollBarRenderer.DrawLeftHorizontalTrack(e.Graphics,
                    clickedBarRectangle, ScrollBarState.Pressed);
            }

            else if (rightBarClicked)
            {
                clickedBarRectangle.X =
                    thumbRectangle.X + thumbRectangle.Width;
                clickedBarRectangle.Width =
                    thumbRightLimitRight - clickedBarRectangle.X;
                ScrollBarRenderer.DrawRightHorizontalTrack(e.Graphics,
                    clickedBarRectangle, ScrollBarState.Pressed);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!ScrollBarRenderer.IsSupported) return;
            if (thumbRectangle.Contains(e.Location))
            {
                thumbClicked = true;
                thumbPosition = e.Location.X - thumbRectangle.X;
                thumbState = ScrollBarState.Pressed;
            }
            else if (leftArrowRectangle.Contains(e.Location))
            {
                leftArrowClicked = true;
                leftButtonState = ScrollBarArrowButtonState.LeftPressed;
                progressTimer.Start();
            }
            else if (rightArrowRectangle.Contains(e.Location))
            {
                rightArrowClicked = true;
                rightButtonState = ScrollBarArrowButtonState.RightPressed;
                progressTimer.Start();
            }
            else
            {
                trackPosition = e.Location.X;
                if (e.Location.X < this.thumbRectangle.X) leftBarClicked = true;
                else rightBarClicked = true;
                progressTimer.Start();
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (!ScrollBarRenderer.IsSupported) return;
            if (thumbClicked)
            {
                thumbClicked = false;
                thumbState = ScrollBarState.Normal;

                if (e.Location.X > (thumbLeftLimit + thumbPosition) &&
                    e.Location.X < (thumbRightLimitLeft + thumbPosition))
                {
                    thumbRectangle.X = e.Location.X - thumbPosition;
                    thumbClicked = false;
                }
            }
            else if (leftArrowClicked)
            {
                leftArrowClicked = false;
                leftButtonState = ScrollBarArrowButtonState.LeftNormal;
                progressTimer.Stop();
            }
            else if (rightArrowClicked)
            {
                rightArrowClicked = false;
                rightButtonState = ScrollBarArrowButtonState.RightNormal;
                progressTimer.Stop();
            }
            else if (leftBarClicked)
            {
                leftBarClicked = false;
                progressTimer.Stop();
            }
            else if (rightBarClicked)
            {
                rightBarClicked = false;
                progressTimer.Stop();
            }
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!ScrollBarRenderer.IsSupported) return;
            if (thumbClicked)
            {
                if (e.Location.X <= (thumbLeftLimit + thumbPosition))
                    thumbRectangle.X = thumbLeftLimit;
                else if (e.Location.X >= (thumbRightLimitLeft + thumbPosition))
                    thumbRectangle.X = thumbRightLimitLeft;
                else
                    thumbRectangle.X = e.Location.X - thumbPosition;
                Invalidate();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetUpScrollBar();
        }

        private void progressTimer_Tick(object sender, EventArgs myEventArgs)
        {
            if (!ScrollBarRenderer.IsSupported)
                return;

            if (rightArrowClicked && thumbRectangle.X < thumbRightLimitLeft)
                thumbRectangle.X++;
            else if (leftArrowClicked && thumbRectangle.X > thumbLeftLimit)
                thumbRectangle.X--;
            else if (rightBarClicked &&
                thumbRectangle.X < thumbRightLimitLeft &&
                thumbRectangle.X + thumbRectangle.Width < trackPosition)
                thumbRectangle.X += 3;
            else if (leftBarClicked &&
                thumbRectangle.X > thumbLeftLimit &&
                thumbRectangle.X > trackPosition)
                thumbRectangle.X -= 3;
            Invalidate();
        }
    }
}
