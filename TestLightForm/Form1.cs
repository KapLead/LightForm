﻿using System;
using System.Drawing;
using LightForm.SvgElement;
using TestLightForm.Properties;

namespace TestLightForm
{
    public partial class Form1 : LightForm.LightForm
    {
        public Form1()
        {
            InitializeComponent();
            HeaderPanel.BackColor=BorderColor = Color.DarkRed;
            TitleBox.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
