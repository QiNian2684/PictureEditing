namespace 图片处理
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
            Main_PictureBox = new PictureBox();
            button_add = new Button();
            button_save = new Button();
            RGB_R = new TrackBar();
            RGB_G = new TrackBar();
            RGB_B = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            LIGHTNESS = new TrackBar();
            CONTRAST = new TrackBar();
            DrawPoint = new Button();
            DrawRectangle = new Button();
            DrawCurve = new Button();
            DrawLine = new Button();
            comboBox1 = new ComboBox();
            ScreenShotHistoryList = new ListView();
            Screen_Shot = new Button();
            button_grayScale = new Button();
            trackBar1 = new TrackBar();
            label6 = new Label();
            label7 = new Label();
            trackBar2 = new TrackBar();
            comboBox_color = new ComboBox();
            comboBox_thick = new ComboBox();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            delete_pic = new Button();
            ((System.ComponentModel.ISupportInitialize)Main_PictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RGB_R).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RGB_G).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RGB_B).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LIGHTNESS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CONTRAST).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).BeginInit();
            SuspendLayout();
            // 
            // Main_PictureBox
            // 
            Main_PictureBox.BackColor = Color.White;
            Main_PictureBox.Location = new Point(590, 171);
            Main_PictureBox.Margin = new Padding(6, 5, 6, 5);
            Main_PictureBox.Name = "Main_PictureBox";
            Main_PictureBox.Size = new Size(1920, 1020);
            Main_PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            Main_PictureBox.TabIndex = 0;
            Main_PictureBox.TabStop = false;
            // 
            // button_add
            // 
            button_add.Location = new Point(6, 4);
            button_add.Margin = new Padding(6, 5, 6, 5);
            button_add.Name = "button_add";
            button_add.Size = new Size(152, 67);
            button_add.TabIndex = 1;
            button_add.Text = "添加图片";
            button_add.UseVisualStyleBackColor = true;
            button_add.MouseClick += button_add_MouseClick;
            // 
            // button_save
            // 
            button_save.Location = new Point(170, 4);
            button_save.Margin = new Padding(6, 5, 6, 5);
            button_save.Name = "button_save";
            button_save.Size = new Size(152, 67);
            button_save.TabIndex = 2;
            button_save.Text = "保存图片";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // RGB_R
            // 
            RGB_R.Location = new Point(126, 190);
            RGB_R.Margin = new Padding(6, 5, 6, 5);
            RGB_R.Maximum = 255;
            RGB_R.Minimum = -255;
            RGB_R.Name = "RGB_R";
            RGB_R.Size = new Size(452, 90);
            RGB_R.TabIndex = 3;
            RGB_R.MouseUp += RGB_R_MouseUp;
            // 
            // RGB_G
            // 
            RGB_G.Location = new Point(126, 283);
            RGB_G.Margin = new Padding(6, 5, 6, 5);
            RGB_G.Maximum = 255;
            RGB_G.Minimum = -255;
            RGB_G.Name = "RGB_G";
            RGB_G.Size = new Size(452, 90);
            RGB_G.TabIndex = 4;
            RGB_G.MouseUp += RGB_G_MouseUp;
            // 
            // RGB_B
            // 
            RGB_B.Location = new Point(126, 376);
            RGB_B.Margin = new Padding(6, 5, 6, 5);
            RGB_B.Maximum = 255;
            RGB_B.Minimum = -255;
            RGB_B.Name = "RGB_B";
            RGB_B.Size = new Size(452, 90);
            RGB_B.TabIndex = 5;
            RGB_B.MouseUp += RGB_B_MouseUp;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(6, 190);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(127, 36);
            label1.TabIndex = 6;
            label1.Text = "红色通道";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(6, 283);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(127, 36);
            label2.TabIndex = 7;
            label2.Text = "绿色通道";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(6, 376);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(127, 36);
            label3.TabIndex = 8;
            label3.Text = "蓝色通道";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(6, 511);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(71, 36);
            label4.TabIndex = 9;
            label4.Text = "亮度";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.Location = new Point(6, 604);
            label5.Margin = new Padding(6, 0, 6, 0);
            label5.Name = "label5";
            label5.Size = new Size(99, 36);
            label5.TabIndex = 10;
            label5.Text = "对比度";
            // 
            // LIGHTNESS
            // 
            LIGHTNESS.Location = new Point(126, 511);
            LIGHTNESS.Margin = new Padding(6, 5, 6, 5);
            LIGHTNESS.Maximum = 255;
            LIGHTNESS.Minimum = -255;
            LIGHTNESS.Name = "LIGHTNESS";
            LIGHTNESS.Size = new Size(452, 90);
            LIGHTNESS.TabIndex = 11;
            LIGHTNESS.MouseUp += LIGHTNESS_MouseUp;
            // 
            // CONTRAST
            // 
            CONTRAST.Location = new Point(126, 604);
            CONTRAST.Margin = new Padding(6, 5, 6, 5);
            CONTRAST.Maximum = 150;
            CONTRAST.Name = "CONTRAST";
            CONTRAST.Size = new Size(452, 90);
            CONTRAST.TabIndex = 12;
            CONTRAST.Value = 75;
            CONTRAST.MouseUp += CONTRAST_MouseUp;
            // 
            // DrawPoint
            // 
            DrawPoint.Location = new Point(1064, 4);
            DrawPoint.Margin = new Padding(6, 5, 6, 5);
            DrawPoint.Name = "DrawPoint";
            DrawPoint.Size = new Size(134, 67);
            DrawPoint.TabIndex = 13;
            DrawPoint.Text = "绘制圆点";
            DrawPoint.UseVisualStyleBackColor = true;
            DrawPoint.Click += DrawPoint_Click;
            // 
            // DrawRectangle
            // 
            DrawRectangle.Location = new Point(918, 4);
            DrawRectangle.Margin = new Padding(6, 5, 6, 5);
            DrawRectangle.Name = "DrawRectangle";
            DrawRectangle.Size = new Size(134, 67);
            DrawRectangle.TabIndex = 15;
            DrawRectangle.Text = "绘制矩形";
            DrawRectangle.UseVisualStyleBackColor = true;
            DrawRectangle.Click += DrawRectangle_Click;
            // 
            // DrawCurve
            // 
            DrawCurve.Location = new Point(772, 4);
            DrawCurve.Margin = new Padding(6, 5, 6, 5);
            DrawCurve.Name = "DrawCurve";
            DrawCurve.Size = new Size(134, 67);
            DrawCurve.TabIndex = 16;
            DrawCurve.Text = "绘制曲线";
            DrawCurve.UseVisualStyleBackColor = true;
            DrawCurve.Click += DrawCurve_Click;
            // 
            // DrawLine
            // 
            DrawLine.Location = new Point(626, 4);
            DrawLine.Margin = new Padding(6, 5, 6, 5);
            DrawLine.Name = "DrawLine";
            DrawLine.Size = new Size(134, 67);
            DrawLine.TabIndex = 17;
            DrawLine.Text = "绘制线段";
            DrawLine.UseVisualStyleBackColor = true;
            DrawLine.Click += DrawLine_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { " 1 倍", " 6 倍", "16倍", "25倍", "40倍" });
            comboBox1.Location = new Point(148, 106);
            comboBox1.Margin = new Padding(6, 5, 6, 5);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(238, 39);
            comboBox1.TabIndex = 18;
            comboBox1.Text = " 1 倍";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // ScreenShotHistoryList
            // 
            ScreenShotHistoryList.Location = new Point(6, 1204);
            ScreenShotHistoryList.Margin = new Padding(6, 5, 6, 5);
            ScreenShotHistoryList.Name = "ScreenShotHistoryList";
            ScreenShotHistoryList.Size = new Size(2500, 270);
            ScreenShotHistoryList.TabIndex = 19;
            ScreenShotHistoryList.UseCompatibleStateImageBehavior = false;
            ScreenShotHistoryList.ItemSelectionChanged += ScreenShotHistoryList_ItemSelectionChanged;
            // 
            // Screen_Shot
            // 
            Screen_Shot.Location = new Point(334, 4);
            Screen_Shot.Margin = new Padding(6, 5, 6, 5);
            Screen_Shot.Name = "Screen_Shot";
            Screen_Shot.Size = new Size(134, 67);
            Screen_Shot.TabIndex = 20;
            Screen_Shot.Text = "截取图片";
            Screen_Shot.UseVisualStyleBackColor = true;
            Screen_Shot.Click += Screen_Shot_Click;
            // 
            // button_grayScale
            // 
            button_grayScale.Location = new Point(1210, 4);
            button_grayScale.Margin = new Padding(6, 5, 6, 5);
            button_grayScale.Name = "button_grayScale";
            button_grayScale.Size = new Size(134, 67);
            button_grayScale.TabIndex = 21;
            button_grayScale.Text = "灰度图像";
            button_grayScale.UseVisualStyleBackColor = true;
            button_grayScale.Click += button_grayScale_Click;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(126, 888);
            trackBar1.Margin = new Padding(6, 5, 6, 5);
            trackBar1.Maximum = 256;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(452, 90);
            trackBar1.TabIndex = 22;
            trackBar1.TickFrequency = 2;
            trackBar1.Value = 128;
            trackBar1.Scroll += trackBar1_Scroll;
            trackBar1.MouseDown += trackBar1_MouseDown;
            trackBar1.MouseUp += trackBar1_MouseUp;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(6, 888);
            label6.Margin = new Padding(6, 0, 6, 0);
            label6.Name = "label6";
            label6.Size = new Size(127, 36);
            label6.TabIndex = 23;
            label6.Text = "外接矩形";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.Location = new Point(6, 981);
            label7.Margin = new Padding(6, 0, 6, 0);
            label7.Name = "label7";
            label7.Size = new Size(127, 36);
            label7.TabIndex = 24;
            label7.Text = "二值轮廓";
            // 
            // trackBar2
            // 
            trackBar2.Location = new Point(126, 981);
            trackBar2.Margin = new Padding(6, 5, 6, 5);
            trackBar2.Maximum = 256;
            trackBar2.Name = "trackBar2";
            trackBar2.Size = new Size(452, 90);
            trackBar2.TabIndex = 25;
            trackBar2.TickFrequency = 2;
            trackBar2.Value = 128;
            trackBar2.Scroll += trackBar2_Scroll;
            trackBar2.MouseDown += trackBar2_MouseDown;
            trackBar2.MouseUp += trackBar2_MouseUp;
            // 
            // comboBox_color
            // 
            comboBox_color.FormattingEnabled = true;
            comboBox_color.Items.AddRange(new object[] { "红色", "橙色", "黄色", "绿色", "青色", "蓝色", "紫色" });
            comboBox_color.Location = new Point(732, 106);
            comboBox_color.Margin = new Padding(6, 5, 6, 5);
            comboBox_color.Name = "comboBox_color";
            comboBox_color.Size = new Size(110, 39);
            comboBox_color.TabIndex = 26;
            comboBox_color.Text = "红色";
            comboBox_color.SelectedIndexChanged += comboBox_color_SelectedIndexChanged;
            // 
            // comboBox_thick
            // 
            comboBox_thick.FormattingEnabled = true;
            comboBox_thick.Items.AddRange(new object[] { " 粗", "中等", " 细" });
            comboBox_thick.Location = new Point(1090, 106);
            comboBox_thick.Margin = new Padding(6, 5, 6, 5);
            comboBox_thick.Name = "comboBox_thick";
            comboBox_thick.Size = new Size(104, 39);
            comboBox_thick.TabIndex = 27;
            comboBox_thick.Text = "中等";
            comboBox_thick.SelectedIndexChanged += comboBox_thick_SelectedIndexChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.Location = new Point(590, 106);
            label8.Margin = new Padding(6, 0, 6, 0);
            label8.Name = "label8";
            label8.Size = new Size(127, 36);
            label8.TabIndex = 28;
            label8.Text = "绘图颜色";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(948, 106);
            label9.Margin = new Padding(6, 0, 6, 0);
            label9.Name = "label9";
            label9.Size = new Size(127, 36);
            label9.TabIndex = 29;
            label9.Text = "墨迹粗细";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft YaHei UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.Location = new Point(6, 106);
            label10.Margin = new Padding(6, 0, 6, 0);
            label10.Name = "label10";
            label10.Size = new Size(127, 36);
            label10.TabIndex = 30;
            label10.Text = "放大倍数";
            // 
            // delete_pic
            // 
            delete_pic.Location = new Point(480, 4);
            delete_pic.Margin = new Padding(6, 5, 6, 5);
            delete_pic.Name = "delete_pic";
            delete_pic.Size = new Size(134, 67);
            delete_pic.TabIndex = 31;
            delete_pic.Text = "删除截图";
            delete_pic.UseVisualStyleBackColor = true;
            delete_pic.Click += delete_pic_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            ClientSize = new Size(2524, 1499);
            Controls.Add(delete_pic);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(comboBox_thick);
            Controls.Add(comboBox_color);
            Controls.Add(trackBar2);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(trackBar1);
            Controls.Add(button_grayScale);
            Controls.Add(Screen_Shot);
            Controls.Add(ScreenShotHistoryList);
            Controls.Add(comboBox1);
            Controls.Add(DrawLine);
            Controls.Add(DrawCurve);
            Controls.Add(DrawRectangle);
            Controls.Add(DrawPoint);
            Controls.Add(CONTRAST);
            Controls.Add(LIGHTNESS);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(RGB_B);
            Controls.Add(RGB_G);
            Controls.Add(RGB_R);
            Controls.Add(button_save);
            Controls.Add(button_add);
            Controls.Add(Main_PictureBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(6, 5, 6, 5);
            Name = "Form1";
            Text = "图像编辑器";
            FormClosing += Form1_FormClosing;
            ((System.ComponentModel.ISupportInitialize)Main_PictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)RGB_R).EndInit();
            ((System.ComponentModel.ISupportInitialize)RGB_G).EndInit();
            ((System.ComponentModel.ISupportInitialize)RGB_B).EndInit();
            ((System.ComponentModel.ISupportInitialize)LIGHTNESS).EndInit();
            ((System.ComponentModel.ISupportInitialize)CONTRAST).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox Main_PictureBox;
        private Button button_add;
        private Button button_save;
        private TrackBar RGB_R;
        private TrackBar RGB_G;
        private TrackBar RGB_B;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TrackBar LIGHTNESS;
        private TrackBar CONTRAST;
        private Button DrawPoint;
        private Button DrawRectangle;
        private Button DrawCurve;
        private Button DrawLine;
        private ComboBox comboBox1;
        private ListView ScreenShotHistoryList;
        private Button Screen_Shot;
        private Button button_grayScale;
        private TrackBar trackBar1;
        private Label label6;
        private Label label7;
        private TrackBar trackBar2;
        private ComboBox comboBox_color;
        private ComboBox comboBox_thick;
        private Label label8;
        private Label label9;
        private Label label10;
        private Button delete_pic;
    }
}
