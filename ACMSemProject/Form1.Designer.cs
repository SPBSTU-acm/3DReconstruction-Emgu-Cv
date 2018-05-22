namespace ACMSemProject
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
            this.components = new System.ComponentModel.Container();
            this.videoPanel = new System.Windows.Forms.Panel();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.imageBox = new Emgu.CV.UI.ImageBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbBlockSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbHeightOfChessboard = new System.Windows.Forms.TextBox();
            this.tbWidthOfChessboard = new System.Windows.Forms.TextBox();
            this.tbBaseLine = new System.Windows.Forms.TextBox();
            this.tbFocalLength = new System.Windows.Forms.TextBox();
            this.tbCameraIndex2 = new System.Windows.Forms.TextBox();
            this.tbCameraIndex1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lTypeOfFocalLength = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDistanceToChessboard = new System.Windows.Forms.Button();
            this.qbMatrixQ = new System.Windows.Forms.GroupBox();
            this.tb32 = new System.Windows.Forms.TextBox();
            this.tb22 = new System.Windows.Forms.TextBox();
            this.tb12 = new System.Windows.Forms.TextBox();
            this.tb30 = new System.Windows.Forms.TextBox();
            this.tb31 = new System.Windows.Forms.TextBox();
            this.tb33 = new System.Windows.Forms.TextBox();
            this.tb20 = new System.Windows.Forms.TextBox();
            this.tb21 = new System.Windows.Forms.TextBox();
            this.tb23 = new System.Windows.Forms.TextBox();
            this.tb10 = new System.Windows.Forms.TextBox();
            this.tb11 = new System.Windows.Forms.TextBox();
            this.tb13 = new System.Windows.Forms.TextBox();
            this.tb03 = new System.Windows.Forms.TextBox();
            this.tb02 = new System.Windows.Forms.TextBox();
            this.tb01 = new System.Windows.Forms.TextBox();
            this.tb00 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.CalibratePhoto = new System.Windows.Forms.Button();
            this.videoPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.panel2.SuspendLayout();
            this.qbMatrixQ.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoPanel
            // 
            this.videoPanel.Controls.Add(this.imageBox2);
            this.videoPanel.Controls.Add(this.imageBox);
            this.videoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPanel.Location = new System.Drawing.Point(0, 0);
            this.videoPanel.Name = "videoPanel";
            this.videoPanel.Size = new System.Drawing.Size(1284, 559);
            this.videoPanel.TabIndex = 0;
            // 
            // imageBox2
            // 
            this.imageBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.imageBox2.Location = new System.Drawing.Point(621, 0);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(663, 559);
            this.imageBox2.TabIndex = 2;
            this.imageBox2.TabStop = false;
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.imageBox.Location = new System.Drawing.Point(0, 0);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(1284, 559);
            this.imageBox.TabIndex = 2;
            this.imageBox.TabStop = false;
            this.imageBox.DoubleClick += new System.EventHandler(this.imageBox_DoubleClick);
            this.imageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBox_MouseDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.CalibratePhoto);
            this.panel2.Controls.Add(this.tbBlockSize);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.tbHeightOfChessboard);
            this.panel2.Controls.Add(this.tbWidthOfChessboard);
            this.panel2.Controls.Add(this.tbBaseLine);
            this.panel2.Controls.Add(this.tbFocalLength);
            this.panel2.Controls.Add(this.tbCameraIndex2);
            this.panel2.Controls.Add(this.tbCameraIndex1);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.lTypeOfFocalLength);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnDistanceToChessboard);
            this.panel2.Controls.Add(this.qbMatrixQ);
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnStop);
            this.panel2.Controls.Add(this.btnPlay);
            this.panel2.Controls.Add(this.btnCapture);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 559);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1284, 190);
            this.panel2.TabIndex = 1;
            // 
            // tbBlockSize
            // 
            this.tbBlockSize.Location = new System.Drawing.Point(1005, 167);
            this.tbBlockSize.Name = "tbBlockSize";
            this.tbBlockSize.Size = new System.Drawing.Size(100, 20);
            this.tbBlockSize.TabIndex = 15;
            this.tbBlockSize.Text = "25";
            this.tbBlockSize.TextChanged += new System.EventHandler(this.tbBlockSize_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(884, 168);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "block Size (mm)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1049, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Height of chessBoard";
            // 
            // tbHeightOfChessboard
            // 
            this.tbHeightOfChessboard.Location = new System.Drawing.Point(1164, 140);
            this.tbHeightOfChessboard.Name = "tbHeightOfChessboard";
            this.tbHeightOfChessboard.Size = new System.Drawing.Size(28, 20);
            this.tbHeightOfChessboard.TabIndex = 13;
            this.tbHeightOfChessboard.Text = "6";
            this.tbHeightOfChessboard.TextChanged += new System.EventHandler(this.tbHeightOfChessboard_TextChanged);
            // 
            // tbWidthOfChessboard
            // 
            this.tbWidthOfChessboard.Location = new System.Drawing.Point(1005, 140);
            this.tbWidthOfChessboard.Name = "tbWidthOfChessboard";
            this.tbWidthOfChessboard.Size = new System.Drawing.Size(38, 20);
            this.tbWidthOfChessboard.TabIndex = 12;
            this.tbWidthOfChessboard.Text = "9";
            this.tbWidthOfChessboard.TextChanged += new System.EventHandler(this.tbWidthOfChessboard_TextChanged);
            // 
            // tbBaseLine
            // 
            this.tbBaseLine.Location = new System.Drawing.Point(1005, 114);
            this.tbBaseLine.Name = "tbBaseLine";
            this.tbBaseLine.Size = new System.Drawing.Size(100, 20);
            this.tbBaseLine.TabIndex = 12;
            this.tbBaseLine.Text = "1";
            this.tbBaseLine.TextChanged += new System.EventHandler(this.tbBaseLine_TextChanged);
            // 
            // tbFocalLength
            // 
            this.tbFocalLength.Location = new System.Drawing.Point(1005, 90);
            this.tbFocalLength.Name = "tbFocalLength";
            this.tbFocalLength.Size = new System.Drawing.Size(100, 20);
            this.tbFocalLength.TabIndex = 12;
            this.tbFocalLength.Text = "1";
            this.tbFocalLength.TextChanged += new System.EventHandler(this.tbFocalLength_TextChanged);
            // 
            // tbCameraIndex2
            // 
            this.tbCameraIndex2.Location = new System.Drawing.Point(1005, 65);
            this.tbCameraIndex2.Name = "tbCameraIndex2";
            this.tbCameraIndex2.Size = new System.Drawing.Size(100, 20);
            this.tbCameraIndex2.TabIndex = 12;
            this.tbCameraIndex2.Text = "1";
            this.tbCameraIndex2.TextChanged += new System.EventHandler(this.tbCameraIndex2_TextChanged);
            // 
            // tbCameraIndex1
            // 
            this.tbCameraIndex1.Location = new System.Drawing.Point(1005, 35);
            this.tbCameraIndex1.Name = "tbCameraIndex1";
            this.tbCameraIndex1.Size = new System.Drawing.Size(100, 20);
            this.tbCameraIndex1.TabIndex = 12;
            this.tbCameraIndex1.Text = "0";
            this.tbCameraIndex1.TextChanged += new System.EventHandler(this.tbCameraIndex1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(884, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Width of chessboard";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(884, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "base line";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1128, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "mm";
            // 
            // lTypeOfFocalLength
            // 
            this.lTypeOfFocalLength.AutoSize = true;
            this.lTypeOfFocalLength.Location = new System.Drawing.Point(1128, 94);
            this.lTypeOfFocalLength.Name = "lTypeOfFocalLength";
            this.lTypeOfFocalLength.Size = new System.Drawing.Size(23, 13);
            this.lTypeOfFocalLength.TabIndex = 11;
            this.lTypeOfFocalLength.Text = "mm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(884, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "focal length";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(884, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Index of camera 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(884, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "index of camera 1";
            // 
            // btnDistanceToChessboard
            // 
            this.btnDistanceToChessboard.Location = new System.Drawing.Point(470, 77);
            this.btnDistanceToChessboard.Name = "btnDistanceToChessboard";
            this.btnDistanceToChessboard.Size = new System.Drawing.Size(171, 23);
            this.btnDistanceToChessboard.TabIndex = 10;
            this.btnDistanceToChessboard.Text = "Distance to Chessboard";
            this.btnDistanceToChessboard.UseVisualStyleBackColor = true;
            this.btnDistanceToChessboard.Click += new System.EventHandler(this.btnDistanceToChessboard_Click);
            // 
            // qbMatrixQ
            // 
            this.qbMatrixQ.Controls.Add(this.tb32);
            this.qbMatrixQ.Controls.Add(this.tb22);
            this.qbMatrixQ.Controls.Add(this.tb12);
            this.qbMatrixQ.Controls.Add(this.tb30);
            this.qbMatrixQ.Controls.Add(this.tb31);
            this.qbMatrixQ.Controls.Add(this.tb33);
            this.qbMatrixQ.Controls.Add(this.tb20);
            this.qbMatrixQ.Controls.Add(this.tb21);
            this.qbMatrixQ.Controls.Add(this.tb23);
            this.qbMatrixQ.Controls.Add(this.tb10);
            this.qbMatrixQ.Controls.Add(this.tb11);
            this.qbMatrixQ.Controls.Add(this.tb13);
            this.qbMatrixQ.Controls.Add(this.tb03);
            this.qbMatrixQ.Controls.Add(this.tb02);
            this.qbMatrixQ.Controls.Add(this.tb01);
            this.qbMatrixQ.Controls.Add(this.tb00);
            this.qbMatrixQ.Enabled = false;
            this.qbMatrixQ.Location = new System.Drawing.Point(97, 19);
            this.qbMatrixQ.Name = "qbMatrixQ";
            this.qbMatrixQ.Size = new System.Drawing.Size(307, 134);
            this.qbMatrixQ.TabIndex = 9;
            this.qbMatrixQ.TabStop = false;
            this.qbMatrixQ.Text = "QMatrix";
            // 
            // tb32
            // 
            this.tb32.Location = new System.Drawing.Point(155, 97);
            this.tb32.Name = "tb32";
            this.tb32.Size = new System.Drawing.Size(68, 20);
            this.tb32.TabIndex = 0;
            this.tb32.TextChanged += new System.EventHandler(this.tb32_TextChanged);
            // 
            // tb22
            // 
            this.tb22.Location = new System.Drawing.Point(155, 71);
            this.tb22.Name = "tb22";
            this.tb22.Size = new System.Drawing.Size(68, 20);
            this.tb22.TabIndex = 0;
            this.tb22.TextChanged += new System.EventHandler(this.tb22_TextChanged);
            // 
            // tb12
            // 
            this.tb12.Location = new System.Drawing.Point(155, 45);
            this.tb12.Name = "tb12";
            this.tb12.Size = new System.Drawing.Size(68, 20);
            this.tb12.TabIndex = 0;
            this.tb12.TextChanged += new System.EventHandler(this.tb12_TextChanged);
            // 
            // tb30
            // 
            this.tb30.Location = new System.Drawing.Point(7, 98);
            this.tb30.Name = "tb30";
            this.tb30.Size = new System.Drawing.Size(68, 20);
            this.tb30.TabIndex = 0;
            this.tb30.TextChanged += new System.EventHandler(this.tb30_TextChanged);
            // 
            // tb31
            // 
            this.tb31.Location = new System.Drawing.Point(81, 98);
            this.tb31.Name = "tb31";
            this.tb31.Size = new System.Drawing.Size(68, 20);
            this.tb31.TabIndex = 0;
            this.tb31.TextChanged += new System.EventHandler(this.tb31_TextChanged);
            // 
            // tb33
            // 
            this.tb33.Location = new System.Drawing.Point(229, 97);
            this.tb33.Name = "tb33";
            this.tb33.Size = new System.Drawing.Size(68, 20);
            this.tb33.TabIndex = 0;
            this.tb33.TextChanged += new System.EventHandler(this.tb33_TextChanged);
            // 
            // tb20
            // 
            this.tb20.Location = new System.Drawing.Point(7, 72);
            this.tb20.Name = "tb20";
            this.tb20.Size = new System.Drawing.Size(68, 20);
            this.tb20.TabIndex = 0;
            this.tb20.TextChanged += new System.EventHandler(this.tb20_TextChanged);
            // 
            // tb21
            // 
            this.tb21.Location = new System.Drawing.Point(81, 72);
            this.tb21.Name = "tb21";
            this.tb21.Size = new System.Drawing.Size(68, 20);
            this.tb21.TabIndex = 0;
            this.tb21.TextChanged += new System.EventHandler(this.tb21_TextChanged);
            // 
            // tb23
            // 
            this.tb23.Location = new System.Drawing.Point(229, 71);
            this.tb23.Name = "tb23";
            this.tb23.Size = new System.Drawing.Size(68, 20);
            this.tb23.TabIndex = 0;
            this.tb23.TextChanged += new System.EventHandler(this.tb23_TextChanged);
            // 
            // tb10
            // 
            this.tb10.Location = new System.Drawing.Point(7, 46);
            this.tb10.Name = "tb10";
            this.tb10.Size = new System.Drawing.Size(68, 20);
            this.tb10.TabIndex = 0;
            this.tb10.TextChanged += new System.EventHandler(this.tb10_TextChanged);
            // 
            // tb11
            // 
            this.tb11.Location = new System.Drawing.Point(81, 46);
            this.tb11.Name = "tb11";
            this.tb11.Size = new System.Drawing.Size(68, 20);
            this.tb11.TabIndex = 0;
            this.tb11.TextChanged += new System.EventHandler(this.tb11_TextChanged);
            // 
            // tb13
            // 
            this.tb13.Location = new System.Drawing.Point(229, 45);
            this.tb13.Name = "tb13";
            this.tb13.Size = new System.Drawing.Size(68, 20);
            this.tb13.TabIndex = 0;
            this.tb13.TextChanged += new System.EventHandler(this.tb13_TextChanged);
            // 
            // tb03
            // 
            this.tb03.Location = new System.Drawing.Point(229, 19);
            this.tb03.Name = "tb03";
            this.tb03.Size = new System.Drawing.Size(68, 20);
            this.tb03.TabIndex = 0;
            this.tb03.TextChanged += new System.EventHandler(this.tb03_TextChanged);
            // 
            // tb02
            // 
            this.tb02.Location = new System.Drawing.Point(155, 19);
            this.tb02.Name = "tb02";
            this.tb02.Size = new System.Drawing.Size(68, 20);
            this.tb02.TabIndex = 0;
            this.tb02.TextChanged += new System.EventHandler(this.tb02_TextChanged);
            // 
            // tb01
            // 
            this.tb01.Location = new System.Drawing.Point(81, 20);
            this.tb01.Name = "tb01";
            this.tb01.Size = new System.Drawing.Size(68, 20);
            this.tb01.TabIndex = 0;
            this.tb01.TextChanged += new System.EventHandler(this.tb01_TextChanged);
            // 
            // tb00
            // 
            this.tb00.Location = new System.Drawing.Point(7, 20);
            this.tb00.Name = "tb00";
            this.tb00.Size = new System.Drawing.Size(68, 20);
            this.tb00.TabIndex = 0;
            this.tb00.TextChanged += new System.EventHandler(this.tb00_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(556, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(85, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "3D";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(470, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Calibrate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(670, 48);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(118, 23);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(670, 19);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(118, 23);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(470, 19);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(171, 23);
            this.btnCapture.TabIndex = 1;
            this.btnCapture.Text = "Distance To Object";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // CalibratePhoto
            // 
            this.CalibratePhoto.Location = new System.Drawing.Point(670, 76);
            this.CalibratePhoto.Name = "CalibratePhoto";
            this.CalibratePhoto.Size = new System.Drawing.Size(118, 23);
            this.CalibratePhoto.TabIndex = 16;
            this.CalibratePhoto.Text = "Photo for Calibrate";
            this.CalibratePhoto.UseVisualStyleBackColor = true;
            this.CalibratePhoto.Click += new System.EventHandler(this.CalibratePhoto_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 749);
            this.Controls.Add(this.videoPanel);
            this.Controls.Add(this.panel2);
            this.Name = "Form1";
            this.Text = "SemProject";
            this.videoPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.qbMatrixQ.ResumeLayout(false);
            this.qbMatrixQ.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel videoPanel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private Emgu.CV.UI.ImageBox imageBox;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button button1;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox qbMatrixQ;
        private System.Windows.Forms.TextBox tb32;
        private System.Windows.Forms.TextBox tb22;
        private System.Windows.Forms.TextBox tb12;
        private System.Windows.Forms.TextBox tb30;
        private System.Windows.Forms.TextBox tb31;
        private System.Windows.Forms.TextBox tb33;
        private System.Windows.Forms.TextBox tb20;
        private System.Windows.Forms.TextBox tb21;
        private System.Windows.Forms.TextBox tb23;
        private System.Windows.Forms.TextBox tb10;
        private System.Windows.Forms.TextBox tb11;
        private System.Windows.Forms.TextBox tb13;
        private System.Windows.Forms.TextBox tb03;
        private System.Windows.Forms.TextBox tb02;
        private System.Windows.Forms.TextBox tb01;
        private System.Windows.Forms.TextBox tb00;
        private System.Windows.Forms.Button btnDistanceToChessboard;
        private System.Windows.Forms.TextBox tbCameraIndex2;
        private System.Windows.Forms.TextBox tbCameraIndex1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbBaseLine;
        private System.Windows.Forms.TextBox tbFocalLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lTypeOfFocalLength;
        private System.Windows.Forms.TextBox tbWidthOfChessboard;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbHeightOfChessboard;
        private System.Windows.Forms.TextBox tbBlockSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button CalibratePhoto;
    }
}

