namespace _2048Tetris
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            label3 = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            label4 = new Label();
            label5 = new Label();
            timer2 = new System.Windows.Forms.Timer(components);
            label6 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft JhengHei UI", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(109, 41);
            label1.TabIndex = 1;
            label1.Text = "Score:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft JhengHei UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(283, 315);
            label2.Name = "label2";
            label2.Size = new Size(151, 150);
            label2.TabIndex = 2;
            label2.Text = "此遊戲為2048\r\n跟俄羅斯方塊的融合體\r\n玩法說明:\r\n使用↑↓←→來控制\r\n先選擇要在哪一列落下\r\n之後再按下左或右\r\n來讓整個版面移動\r\n落下的數字會隨著分數變高\r\n而出現更高的數字\r\n\r\n";
            // 
            // button1
            // 
            button1.Location = new Point(289, 126);
            button1.Name = "button1";
            button1.Size = new Size(109, 47);
            button1.TabIndex = 3;
            button1.Text = "重新開始";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(289, 190);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 4;
            label3.Text = "最佳成績:";
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft JhengHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(301, 48);
            label4.Name = "label4";
            label4.Size = new Size(97, 25);
            label4.TabIndex = 5;
            label4.Text = "倒數時間:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft JhengHei UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(215, 13);
            label5.Name = "label5";
            label5.Size = new Size(183, 35);
            label5.TabIndex = 6;
            label5.Text = "遊戲剩餘時間:";
            // 
            // timer2
            // 
            timer2.Interval = 1000;
            timer2.Tick += timer2_Tick;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(289, 225);
            label6.Name = "label6";
            label6.Size = new Size(109, 20);
            label6.TabIndex = 4;
            label6.Text = "最大合成數字:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(446, 596);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label6);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            KeyPreview = true;
            Name = "Form1";
            Text = "2048俄羅斯方塊";
            Load += Form1_Load;
            PreviewKeyDown += Form1_PreviewKeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Button button1;
        private Label label3;
        private System.Windows.Forms.Timer timer1;
        private Label label4;
        private Label label5;
        private System.Windows.Forms.Timer timer2;
        private Label label6;
    }
}