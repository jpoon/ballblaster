using System.Security.AccessControl;

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
            this.leftButton = new System.Windows.Forms.Button();
            this.rightButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.fireButton = new System.Windows.Forms.Button();
            this.omgButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // leftButton
            // 
            this.leftButton.Location = new System.Drawing.Point(391, 195);
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(179, 66);
            this.leftButton.TabIndex = 1;
            this.leftButton.Text = "Left";
            this.leftButton.UseVisualStyleBackColor = true;
            // 
            // rightButton
            // 
            this.rightButton.Location = new System.Drawing.Point(810, 195);
            this.rightButton.Name = "rightButton";
            this.rightButton.Size = new System.Drawing.Size(179, 66);
            this.rightButton.TabIndex = 2;
            this.rightButton.Text = "Right";
            this.rightButton.UseVisualStyleBackColor = true;
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(602, 101);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(179, 66);
            this.upButton.TabIndex = 3;
            this.upButton.Text = "Up";
            this.upButton.UseVisualStyleBackColor = true;
            // 
            // downButton
            // 
            this.downButton.Location = new System.Drawing.Point(602, 195);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(179, 66);
            this.downButton.TabIndex = 4;
            this.downButton.Text = "Down";
            this.downButton.UseVisualStyleBackColor = true;
            // 
            // fireButton
            // 
            this.fireButton.BackColor = System.Drawing.Color.LightCoral;
            this.fireButton.Location = new System.Drawing.Point(602, 349);
            this.fireButton.Name = "fireButton";
            this.fireButton.Size = new System.Drawing.Size(387, 66);
            this.fireButton.TabIndex = 5;
            this.fireButton.Text = "Fire";
            this.fireButton.UseVisualStyleBackColor = false;
            this.fireButton.Click += new System.EventHandler(this.fireButton_Click);
            // 
            // omgButton
            // 
            this.omgButton.Location = new System.Drawing.Point(391, 349);
            this.omgButton.Name = "omgButton";
            this.omgButton.Size = new System.Drawing.Size(179, 66);
            this.omgButton.TabIndex = 6;
            this.omgButton.Text = "!!!";
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
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }



        #endregion

        private System.Windows.Forms.Button leftButton;
        private System.Windows.Forms.Button rightButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button fireButton;
        private System.Windows.Forms.Button omgButton;
    }
}

