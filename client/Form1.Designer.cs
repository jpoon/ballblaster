namespace client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.stopButton = new System.Windows.Forms.Button();
            this.leftButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.fireButton = new System.Windows.Forms.Button();
            this.omgButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(602, 207);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(246, 66);
            this.stopButton.TabIndex = 0;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.MouseDown += stopButton_MouseDown;
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(317, 207);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(246, 66);
            this.leftButton.TabIndex = 1;
            this.leftButton.Text = "Left";
            this.leftButton.UseVisualStyleBackColor = true;
            this.leftButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(893, 207);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(246, 66);
            this.rightButton.TabIndex = 2;
            this.rightButton.Text = "Right";
            this.rightButton.UseVisualStyleBackColor = true;
            this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(602, 101);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(246, 66);
            this.upButton.TabIndex = 3;
            this.upButton.Text = "Up";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(602, 310);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(246, 66);
            this.downButton.TabIndex = 4;
            this.downButton.Text = "Down";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // fireButton
            // 
            this.fireButton.BackColor = System.Drawing.Color.Red;
            this.fireButton.Location = new System.Drawing.Point(1227, 394);
            this.fireButton.Name = "fireButton";
            this.fireButton.Size = new System.Drawing.Size(246, 66);
            this.fireButton.TabIndex = 5;
            this.fireButton.Text = "Fire";
            this.fireButton.UseVisualStyleBackColor = false;
            this.fireButton.Click += new System.EventHandler(this.fireButton_Click);
            // 
            // omgButton
            // 
            this.omgButton.Location = new System.Drawing.Point(66, 44);
            this.omgButton.Name = "omgButton";
            this.omgButton.Size = new System.Drawing.Size(246, 66);
            this.omgButton.TabIndex = 6;
            this.omgButton.Text = "OMG";
            this.omgButton.UseVisualStyleBackColor = true;
            this.omgButton.Click += new System.EventHandler(this.omgButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1539, 505);
            this.Controls.Add(this.omgButton);
            this.Controls.Add(this.fireButton);
            this.Controls.Add(this.downButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.rightButton);
            this.Controls.Add(this.leftButton);
            this.Controls.Add(this.stopButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button fireButton;
        private System.Windows.Forms.Button omgButton;
    }
}

