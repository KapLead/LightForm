namespace TestLightForm
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lightLabelShadow1 = new LightForm.Controls.LightLabelShadow();
            this.SuspendLayout();
            // 
            // lightLabelShadow1
            // 
            this.lightLabelShadow1.Angle = 55D;
            this.lightLabelShadow1.Distance = 1;
            this.lightLabelShadow1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lightLabelShadow1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lightLabelShadow1.Location = new System.Drawing.Point(26, 58);
            this.lightLabelShadow1.Name = "lightLabelShadow1";
            this.lightLabelShadow1.Shadow = System.Drawing.Color.Gold;
            this.lightLabelShadow1.Size = new System.Drawing.Size(233, 34);
            this.lightLabelShadow1.TabIndex = 1;
            this.lightLabelShadow1.Text = "lightLabelShadow1";
            this.lightLabelShadow1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 472);
            this.Controls.Add(this.lightLabelShadow1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LightForm.Controls.LightLabelShadow lightLabelShadow1;
    }
}

