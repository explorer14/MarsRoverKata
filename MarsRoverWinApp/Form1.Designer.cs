namespace MarsRoverWinApp
{
    partial class frmMarsRover
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaxX = new System.Windows.Forms.TextBox();
            this.txtMaxY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExecute = new System.Windows.Forms.Button();
            this.btnRover = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 722);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "MaxX";
            // 
            // txtMaxX
            // 
            this.txtMaxX.Location = new System.Drawing.Point(50, 722);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(100, 20);
            this.txtMaxX.TabIndex = 1;
            // 
            // txtMaxY
            // 
            this.txtMaxY.Location = new System.Drawing.Point(200, 722);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(100, 20);
            this.txtMaxY.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 722);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "MaxY";
            // 
            // txtStartY
            // 
            this.txtStartY.Location = new System.Drawing.Point(507, 722);
            this.txtStartY.Name = "txtStartY";
            this.txtStartY.Size = new System.Drawing.Size(100, 20);
            this.txtStartY.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(466, 722);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "StartY";
            // 
            // txtStartX
            // 
            this.txtStartX.Location = new System.Drawing.Point(357, 722);
            this.txtStartX.Name = "txtStartX";
            this.txtStartX.Size = new System.Drawing.Size(100, 20);
            this.txtStartX.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(316, 722);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "StartX";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(50, 759);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(557, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 759);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Seq";
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(12, 791);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(75, 23);
            this.btnExecute.TabIndex = 10;
            this.btnExecute.Text = "Execute";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnRover
            // 
            this.btnRover.Location = new System.Drawing.Point(12, 627);
            this.btnRover.Name = "btnRover";
            this.btnRover.Size = new System.Drawing.Size(75, 75);
            this.btnRover.TabIndex = 11;
            this.btnRover.Text = "Oppy";
            this.btnRover.UseVisualStyleBackColor = true;
            // 
            // frmMarsRover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 826);
            this.Controls.Add(this.btnRover);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStartY);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStartX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMaxY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMaxX);
            this.Controls.Add(this.label1);
            this.Name = "frmMarsRover";
            this.Text = "Mars Rover Control Panel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaxX;
        private System.Windows.Forms.TextBox txtMaxY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStartY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnRover;
    }
}

