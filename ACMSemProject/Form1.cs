using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.Tracking;
using System.Diagnostics;

namespace ACMSemProject
{
    public partial class Form1 : Form
    {
        private bool _isCameraMatrixCount = false;
        Matrix<float> cameraMatrix1 = new Matrix<float>(3, 3);
        Matrix<float> cameraMatrix2 = new Matrix<float>(3, 3);
        Matrix<float> distCoeff1 = new Matrix<float>(4, 1);
        Matrix<float> distCoeff2 = new Matrix<float>(4, 1);
        Matrix<double> R1 = new Matrix<double>(3, 3); //rectification transforms (rotation matrices) for Camera 1.
        Matrix<double> R2 = new Matrix<double>(3, 3); //rectification transforms (rotation matrices) for Camera 1.
        Matrix<double> P1 = new Matrix<double>(3, 4); //projection matrices in the new (rectified) coordinate systems for Camera 1.
        Matrix<double> P2 = new Matrix<double>(3, 4); //projection matrices in the new (rectified) coordinate systems for Camera 2.

        private bool _canGrabRightImage = false;
        private int[] _cameraIndex = new int[2] { 0, 1 };
        private bool _isCalibrate = false;
        private VideoCapture _capture = null;
        private VideoCapture _capture2 = null;

        private bool _captureInProgress;
        private Rectangle _regionLeft = new Rectangle(-1, -1, -1, -1);
        private bool _haveRegion = false;
        private Point _startPoint = new Point(-1, -1);

        private Tracker _trackerLeft = null;
        private Mat[] images = new Mat[2] { new Mat(), new Mat() };
        private Mat _left = new Mat();
        private Mat _right = new Mat();
        private Mat _tmpRight = new Mat();
        private bool _makeCalibratingPhoto = false;
        private Viz3d viz3d;

        private const double epsilon = 0.00000001;
        private double _focalLength = 3.5;
        private double _baseLine = 1100;

        private float _blockSize = 25;

        private Size _boardSize = new Size(9, 6);

        private static int _imageBufferLength = 20;

        private Bgr[] lineColourArray = new Bgr[9 * 6];
        private static int N = _imageBufferLength * 9 * 6;

        private PointF[][] corners_points_Left = new PointF[_imageBufferLength][];//stores the calculated points from chessboard detection Camera 1
        private PointF[][] corners_points_Right = new PointF[_imageBufferLength][];//stores the calculated points from chessboard detection Camera 2

        private Matrix<double> Q = new Matrix<double>(4, 4); //This is what were interested in the disparity-to-depth mapping matrix


        private int _currentImageIndex = 0;
        private bool _is3dStart = false;

        public Form1()
        {
            InitializeComponent();
            CvInvoke.UseOpenCL = false;

        }

        private void AddCaptureParams(EventHandler captureHandler)
        {
            try
            {
                _capture.ImageGrabbed += captureHandler;
                _capture2.ImageGrabbed += GrabbRightImage;
            }
            catch (NullReferenceException excpt)
            {
                MessageBox.Show(excpt.Message);
            }
            _capture.Start();
            _capture2.Start();
            _captureInProgress = true;
            btnPlay.Text = "Pause";
        }

        private void GrabbRightImage(object sender, EventArgs e)
        {
            if (_canGrabRightImage)
            {
                _capture2.Read(_tmpRight);
                _canGrabRightImage = false;
            }
        }

        private void ProcessWithoutCalibration(object sender, EventArgs e)
        {
            if (_capture != null && _capture.Ptr != IntPtr.Zero)
            {
                Mat tmp = new Mat();

                CaptureStereoImages(_capture, _capture2, out _left, out _right);
                Mat[] edges = new Mat[2] { new Mat(), new Mat() };
                CvInvoke.Canny(_left, edges[0], 100, 200);
                CvInvoke.Canny(_right, edges[1], 100, 200);

                if (_haveRegion && _trackerLeft == null)
                {
                    tmp = DrawRect(_regionLeft, edges[0]);
                    _trackerLeft = new TrackerKCF();
                    _trackerLeft.Init(_left, _regionLeft);
                }
                else if (_haveRegion)
                {
                    Rectangle rectLeft = new Rectangle();
                    _trackerLeft.Update(_left, out rectLeft);
                    _regionLeft = rectLeft;
                    tmp = DrawRect(_regionLeft, edges[0]);


                    double distances = CountDistanceThroughEdges(edges[0], edges[1], _regionLeft, _baseLine, _focalLength);
                    CvInvoke.PutText(_right, "distance = " + distances, new Point(0, _right.Height - 30),
                        FontFace.HersheySimplex, 0.5, new Bgr(Color.Red).MCvScalar, 2);
                }
                else
                {
                    tmp = edges[0];
                }

                ShowStereoImages(tmp, _right);
            }
        }

        private void CaptureStereoImages(VideoCapture capture1, VideoCapture capture2, out Mat mat1, out Mat mat2)
        {
            Mat mattmp1 = new Mat();

            if (capture1 == null || capture2 == null)
            {
                throw new NullReferenceException("Input exist capture parameters");
            }
            capture1.Read(mattmp1);
            _canGrabRightImage = true;
            while (_canGrabRightImage) { };
            mat1 = mattmp1.Clone();
            mat2 = _tmpRight.Clone();
        }

        private void ShowStereoImages(Mat left, Mat right)
        {
            imageBox.Image = left;
            imageBox2.Image = right;
        }

        private Mat DrawRect(Rectangle rect, Mat image)
        {
            Mat rectangleMat = image.Clone();
            CvInvoke.Rectangle(rectangleMat, new Rectangle(rect.X - 3, rect.Y - 3, rect.Width + 6, rect.Height + 6), new Bgr(Color.Blue).MCvScalar, 2);
            return rectangleMat;
        }

        private static double CountDistanceThroughEdges(Mat leftEdges, Mat rightEdges, Rectangle roi,
            double baseLine, double focalLengthPx)
        {
            if (roi == null)
            {
                return -1;
            }
            Mat prev8bit = leftEdges;
            Mat current8bit = rightEdges;

            Image<Gray, byte> imageLeft = prev8bit.ToImage<Gray, byte>();
            Image<Gray, byte> imageRight = current8bit.ToImage<Gray, byte>();
            Image<Gray, float> flowX = new Image<Gray, float>(prev8bit.Width, prev8bit.Height);
            Image<Gray, float> flowY = new Image<Gray, float>(prev8bit.Width, prev8bit.Height);

            CvInvoke.CalcOpticalFlowFarneback(imageLeft, imageRight, flowX,
                flowY, 0.5, 3, 25, 10, 5, 1.1, OpticalflowFarnebackFlag.FarnebackGaussian);
            Mat magnitude = new Mat();
            Mat angle = new Mat();
            CvInvoke.CartToPolar(flowX.Mat, flowY.Mat, magnitude, angle);
            int matWidth = magnitude.Width;
            int matHeight = magnitude.Height;
            float[] magData = new float[matWidth * matHeight];
            magnitude.CopyTo(magData);
            List<double> results = new List<double>();

            for (int x = roi.X; x <= roi.X + roi.Width; x++)
            {
                for (int y = roi.Y; y <= roi.Y + roi.Height; y++)
                {
                    float delta = GetElementFromArrayAsFromMatrix(magData, y, x, matWidth);
                    if (delta < epsilon)
                    {
                        continue;
                    }
                    double distance = (baseLine * focalLengthPx) / delta;
                    results.Add(distance);
                }
            }
            if (results.Count == 0)
            {
                return -1;
            }

            return results.Average();
        }

        private static T GetElementFromArrayAsFromMatrix<T>(T[] data, int row, int column, int width)
        {
            return data[row * width + column];
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            lTypeOfFocalLength.Text = "px";
            StartStereoCapturing(_capture, _cameraIndex[0], _capture2, _cameraIndex[1], out _capture, out _capture2);
            AddCaptureParams(ProcessWithoutCalibration);

        }

        private void StartStereoCapturing(VideoCapture capture1, int cameraIndex1, VideoCapture capture2, int cameraIndex2, out VideoCapture newCapture1, out VideoCapture newCapture2)
        {
            newCapture1 = StartCapturing(capture1, cameraIndex1);
            newCapture2 = StartCapturing(capture2, cameraIndex2);
        }

        private VideoCapture StartCapturing(VideoCapture stopeingCaptur, int cameraIndex)
        {
            StopCapturing(stopeingCaptur);
            return new VideoCapture(cameraIndex);
        }

        private void StopCapturing(VideoCapture capture)
        {
            if (capture != null)
            {
                capture.Stop();
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (_capture != null && _capture2 != null)
            {
                if (!_captureInProgress)
                {
                    btnPlay.Text = "Pause";
                    _capture.Start();
                    _capture2.Start();
                    _captureInProgress = true;

                }
                else
                {
                    btnPlay.Text = "Play";
                    _capture.Pause();
                    _capture2.Pause();
                    _captureInProgress = false;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (_capture != null && _capture2 != null)
            {
                _capture.Stop();
                _capture2.Stop();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            N = _boardSize.Width * _boardSize.Height * _imageBufferLength;
            _isCalibrate = false;
            _currentImageIndex = 0;
            StartStereoCapturing(_capture, _cameraIndex[0], _capture2, _cameraIndex[1], out _capture, out _capture2);
            CalibrateStereoCamera();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            qbMatrixQ.Enabled = true;
            StartStereoCapturing(_capture, _cameraIndex[0], _capture2, _cameraIndex[1], out _capture, out _capture2);
            Show3DVideo();
        }

        private void imageBox_DoubleClick(object sender, EventArgs e)
        {
            _haveRegion = false;
            _startPoint = new Point(-1, -1);
            _trackerLeft = null;
        }

        private void imageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (_startPoint.X < 0 && !_haveRegion)
            {
                _startPoint = e.Location;
            }
            else
            {
                _haveRegion = true;
                Point pos = e.Location;
                _regionLeft = new Rectangle(_startPoint, new Size(-_startPoint.X + pos.X,
                    -_startPoint.Y + pos.Y));
            }
        }

        private void CalibrateStereoCamera()
        {
            AddCaptureParams(CalibrateStereoCameraProcess);
        }

        private void Show3DVideo()
        {
            InitializeViz3D();
            AddCaptureParams(Show3DVideoProcess);
            InitializeGroup();
            //Display3D(_left, _right);
        }

        private void InitializeViz3D()
        {
            viz3d = new Viz3d("3D");
        }

        private void Show3DVideoProcess(object sender, EventArgs e)
        {
            CaptureStereoImages(_capture, _capture2, out _left, out _right);

            Display3D(_left, _right);

            ShowStereoImages(_left, _right);
        }

        private void Display3D(Mat left, Mat right)
        {
            //TODO: try to use StereoSGBM
            using (StereoBM stereoSolver = new StereoBM())
            {

                Mat output = new Mat();
                Mat left8bit = ConvertInto8bitMat(left);
                Mat right8bit = ConvertInto8bitMat(right);
                stereoSolver.Compute(left8bit, right8bit, output);
                Mat points = new Mat();
                float scale = Math.Max(left.Size.Width, left.Size.Height);
                if (!_isCalibrate)
                {
                    Q = new Matrix<double>(
               new double[,]
               {
                  {1.0, 0.0, 0.0, -left.Width/2}, //shift the x origin to image center
                  {0.0, -1.0, 0.0, left.Height/2}, //shift the y origin to image center and flip it upside down
                  {0.0, 0.0, -1.0, 0.0}, //Multiply the z value by -1.0, 
                  {0.0, 0.0, 0.0, scale}
               });
                    _isCalibrate = true;
                }
                //Construct a simple Q matrix, if you have a matrix from cvStereoRectify, you should use that instead
                //scale the object's coordinate to within a [-0.5, 0.5] cube
                if (_isCameraMatrixCount) {
                    Mat map11 = new Mat();
                    Mat map12 = new Mat();
                    Mat map21 = new Mat();
                    Mat map22 = new Mat();
                    CvInvoke.InitUndistortRectifyMap(cameraMatrix1, distCoeff1, R1, P1, left8bit.Size,
                        DepthType.Cv16S, map11, map12);
                    CvInvoke.InitUndistortRectifyMap(cameraMatrix2, distCoeff2, R2, P2, left8bit.Size,
                        DepthType.Cv16S, map21, map22);

                    Mat img1r = new Mat();
                    Mat img2r = new Mat();
                    CvInvoke.Remap(left8bit, img1r, map11, map12, Inter.Linear);
                    CvInvoke.Remap(right8bit, img2r, map21, map22, Inter.Linear);
                    left8bit = img1r;
                    right8bit = img2r;
                }
                //stereoSolver.FindStereoCorrespondence(left, right, disparityMap);
                CvInvoke.ReprojectImageTo3D(output, points, Q, false, DepthType.Cv32F);
                //points = PointCollection.ReprojectImageTo3D(output, Q);
                Mat pointsArray = points.Reshape(points.NumberOfChannels, points.Rows * points.Cols);
                Mat colorArray = left.Reshape(left.NumberOfChannels, left.Rows * left.Cols);
                Mat colorArrayFloat = new Mat();
                colorArray.ConvertTo(colorArrayFloat, DepthType.Cv32F);
                WCloud cloud = new WCloud(pointsArray, colorArray);

                Display3DImage(cloud);

                //points = PointCollection.ReprojectImageTo3D(outputDisparityMap, q);
            }
        }

        private void InitializeGroup()
        {

            tb00.Text = "1";
            tb01.Text = "0";
            tb02.Text = "0";
            tb03.Text = "-320";
            tb10.Text = "0";
            tb11.Text = "-1";
            tb12.Text = "0";
            tb13.Text = "240";
            tb20.Text = "0";
            tb21.Text = "0";
            tb22.Text = "-1";
            tb23.Text = "0";
            tb30.Text = "0";
            tb31.Text = "0";
            tb32.Text = "0";
            tb33.Text = "640";
        }

        private void Display3DImage(WCloud cloud)
        {
            if (_is3dStart)
            {
                viz3d.RemoveWidget("cloud");
            }
            viz3d.ShowWidget("cloud", cloud);
            viz3d.SpinOnce();
            _is3dStart = true;
        }
        private void CalibrateStereoCameraProcess(object sender, EventArgs e)
        {
            if (_capture == null || _capture.Ptr == IntPtr.Zero || _capture2 == null || _capture2 == IntPtr.Zero)
            {
                throw new Exception("Invalid capture parameters");
            }
            CaptureStereoImages(_capture, _capture2, out _left, out _right);
            Mat displayLeft = _left;
            Mat displayRight = _right;
            //if (_currentImageIndex < bufferLength)
            {
                Mat leftCorners = new Mat();
                Mat rightCorners = new Mat();


                bool find = AddCornersFromCurrentImages(_left, _right, out leftCorners, out rightCorners);

                if (leftCorners != null)
                {
                    CvInvoke.DrawChessboardCorners(displayLeft, _boardSize, leftCorners, find);
                }
                if (rightCorners != null)
                {
                    CvInvoke.DrawChessboardCorners(displayRight, _boardSize, rightCorners, find);
                }

            }

            ShowStereoImages(displayLeft, displayRight);
            if (_currentImageIndex >= _imageBufferLength)
            {
                if (!_isCalibrate)
                {
                    StereoCameraCalibration();
                }
                else
                {
                    MessageBox.Show("Press button 3D");
                    return;
                }
            }

        }

        private bool AddCornersFromCurrentImages(Mat left, Mat right, out Mat leftCorners, out Mat rightCorners)
        {
            bool find1 = false;
            bool find2 = false;
            Mat leftGray = ConvertInto8bitMat(left);
            Mat rightGray = ConvertInto8bitMat(right);

            Mat pointsLeft = new Mat();
            Mat pointsRight = new Mat();
            find1 = CvInvoke.FindChessboardCorners(leftGray, _boardSize, pointsLeft);
            find2 = CvInvoke.FindChessboardCorners(rightGray, _boardSize, pointsRight);
            if (!(find1 && find2))
            {
                leftCorners = null;
                rightCorners = null;
                return false;
            }

            leftCorners = pointsLeft;
            rightCorners = pointsRight;

            if (_currentImageIndex >= _imageBufferLength || !_makeCalibratingPhoto)
            {
                return true;
            }
            PointF[] pointsL = new PointF[_boardSize.Width * _boardSize.Height];
            PointF[] pointsR = new PointF[_boardSize.Width * _boardSize.Height];
            pointsLeft.CopyTo(pointsL);
            pointsRight.CopyTo(pointsR);
            corners_points_Left[_currentImageIndex] = new PointF[_boardSize.Width * _boardSize.Height];
            corners_points_Right[_currentImageIndex] = new PointF[_boardSize.Width * _boardSize.Height];
            corners_points_Left[_currentImageIndex] = pointsL;
            corners_points_Right[_currentImageIndex] = pointsR;


            // corners_points_Right[_currentImageIndex] = (PointF[])pointsLeft.Data;

            _currentImageIndex++;
            _makeCalibratingPhoto = false;
            return true;
        }

        private void StereoCameraCalibration()
        {
            MCvPoint3D32f[][] corners_object_Points = new MCvPoint3D32f[_imageBufferLength][]; //stores the calculated size for the chessboard

            Mat r = new Mat();
            Mat t = new Mat();
            Mat f = new Mat();
            Mat e1 = new Mat();

            //fill the MCvPoint3D32f with correct mesurments
            for (int k = 0; k < _imageBufferLength; k++)
            {
                //Fill our objects list with the real world mesurments for the intrinsic calculations
                List<MCvPoint3D32f> object_list = new List<MCvPoint3D32f>();
                for (int i = 0; i < _boardSize.Height; i++)
                {
                    for (int j = 0; j < _boardSize.Width; j++)
                    {
                        object_list.Add(new MCvPoint3D32f(j * _blockSize, i * _blockSize, 0.0F));
                    }
                }
                corners_object_Points[k] = object_list.ToArray();
            }

            MessageBox.Show("End capturing images for calibration stereo camera");

            CvInvoke.StereoCalibrate(corners_object_Points, corners_points_Left, corners_points_Right, cameraMatrix1, distCoeff1,
                cameraMatrix2, distCoeff2, _left.Size, r, t, e1, f, CalibType.Default, new MCvTermCriteria(0.1e5));

            MessageBox.Show("Succesful calibrate stereo camera");
            Rectangle rec1 = new Rectangle();
            Rectangle rec2 = new Rectangle();
            //Computes rectification transforms for each head of a calibrated stereo camera.
            MessageBox.Show("Start Rectify");
            CvInvoke.StereoRectify(cameraMatrix1, distCoeff1,
                                     cameraMatrix2, distCoeff2,
                                    _left.Size,
                                     r, t,
                                     R1, R2, P1, P2, Q,
                                     StereoRectifyType.Default, 0,
                                     _left.Size, ref rec1, ref rec2);
            MessageBox.Show("rect1 = " + rec1 + "\nrect2 = " + rec2 + "\nQ= \n" + Q[0, 0] + " " + Q[0, 1] + " " + Q[0, 2] + " " + Q[0, 3] +
                "\n" + Q[1, 0] + " " + Q[1, 1] + " " + Q[1, 2] + " " + Q[1, 3] + "\n" + Q[2, 0] + " " + Q[2, 1] + " " +
                Q[2, 2] + " " + Q[2, 3] + "\n" + Q[3, 0] + " " + Q[3, 1] + " " + Q[3, 2] + " " + Q[3, 3]);

            _isCalibrate = true;
            _isCameraMatrixCount = true;
        }

        private static Mat ConvertInto8bitMat(Mat mat)
        {
            if (mat == null)
            {
                return mat;
            }
            Mat gray = new Mat();
            CvInvoke.CvtColor(mat, gray, ColorConversion.Bgr2Gray);
            return gray;
        }

        private void tb00_TextChanged(object sender, EventArgs e)
        {
            Q[0, 0] = Double.Parse(tb00.Text);
        }

        private void tb10_TextChanged(object sender, EventArgs e)
        {
            Q[1, 0] = Double.Parse(tb00.Text);
        }

        private void tb20_TextChanged(object sender, EventArgs e)
        {

            Q[2, 0] = Double.Parse(tb00.Text);
        }

        private void tb30_TextChanged(object sender, EventArgs e)
        {
            Q[3, 0] = Double.Parse(tb00.Text);
        }

        private void tb01_TextChanged(object sender, EventArgs e)
        {

            Q[0, 1] = Double.Parse(tb00.Text);
        }

        private void tb11_TextChanged(object sender, EventArgs e)
        {

            Q[1, 1] = Double.Parse(tb00.Text);
        }

        private void tb21_TextChanged(object sender, EventArgs e)
        {

            Q[2, 1] = Double.Parse(tb00.Text);
        }

        private void tb31_TextChanged(object sender, EventArgs e)
        {

            Q[3, 1] = Double.Parse(tb00.Text);
        }
        private void tb02_TextChanged(object sender, EventArgs e)
        {

            Q[0, 2] = Double.Parse(tb00.Text);
        }

        private void tb12_TextChanged(object sender, EventArgs e)
        {

            Q[1, 2] = Double.Parse(tb00.Text);
        }

        private void tb22_TextChanged(object sender, EventArgs e)
        {

            Q[2, 2] = Double.Parse(tb00.Text);
        }

        private void tb32_TextChanged(object sender, EventArgs e)
        {

            Q[3, 2] = Double.Parse(tb00.Text);
        }
        private void tb03_TextChanged(object sender, EventArgs e)
        {

            Q[0, 3] = Double.Parse(tb00.Text);
        }

        private void tb13_TextChanged(object sender, EventArgs e)
        {

            Q[1, 3] = Double.Parse(tb00.Text);
        }

        private void tb23_TextChanged(object sender, EventArgs e)
        {

            Q[2, 3] = Double.Parse(tb00.Text);
        }

        private void tb33_TextChanged(object sender, EventArgs e)
        {

            Q[3, 3] = Double.Parse(tb00.Text);
        }

        private void btnDistanceToChessboard_Click(object sender, EventArgs e)
        {
            lTypeOfFocalLength.Text = "mm";
            StartStereoCapturing(_capture, _cameraIndex[0], _capture2, _cameraIndex[1], out _capture, out _capture2);
			AddCaptureParams(CountDistanceToChessboardProcess);
        }

        private void CountDistanceToChessboardProcess(object sender, EventArgs e)
        {
            CaptureStereoImages(_capture, _capture2, out _left, out _right);
            Mat displayLeftMat = _left.Clone();
            Mat displayRightMat = _right.Clone();
            Mat[] corners = new Mat[] { new Mat(), new Mat() };

            bool find1 = CvInvoke.FindChessboardCorners(displayLeftMat, _boardSize, corners[0]);
            bool find2 = CvInvoke.FindChessboardCorners(displayRightMat, _boardSize, corners[1]);
            double distance = -2;
            if (find1 && find2)
            {
                CvInvoke.DrawChessboardCorners(displayLeftMat, _boardSize, corners[0], find1);
                CvInvoke.DrawChessboardCorners(displayRightMat, _boardSize, corners[1], find2);
                distance = FindDistanceToChessBoard(corners[0], corners[1], _blockSize,
                    _boardSize, _focalLength, _baseLine);

            }
            CvInvoke.PutText(displayLeftMat, "distance = " + distance, new Point(10, displayLeftMat.Height - 30),
                    FontFace.HersheySimplex, 0.5, new Bgr(Color.Red).MCvScalar, 2);
            ShowStereoImages(displayLeftMat, displayRightMat);

        }

        private static double FindDistanceToChessBoard(Mat leftCorners, Mat rightCorners, double sizeOfBlock, Size chessboardSize,
            double focalLength, double baseLine)
        {
            int amountOfPoints = chessboardSize.Width * chessboardSize.Height;
            if (leftCorners == null || rightCorners == null || chessboardSize == null)
            {
                return -1;
            }
            PointF[][] corners = new PointF[2][];
            corners[0] = new PointF[amountOfPoints];
            corners[1] = new PointF[amountOfPoints];

            leftCorners.CopyTo(corners[0]);
            rightCorners.CopyTo(corners[1]);

            double[] disparitys = new double[amountOfPoints];
            for (int i = 0; i < amountOfPoints; i++)
            {
                disparitys[i] = Math.Abs(corners[1][i].X - corners[0][i].X);
            }
            double[][] mesurePointsDisparity = new double[chessboardSize.Height][];

            for (int i = 0; i < chessboardSize.Height; i++)
            {
                mesurePointsDisparity[i] = new double[chessboardSize.Width - 1];
                for (int j = 0; j < chessboardSize.Width - 1; j++)
                {
                    mesurePointsDisparity[i][j] = (GetElementFromArrayAsFromMatrix(disparitys, i, j, chessboardSize.Width) +
                        GetElementFromArrayAsFromMatrix(disparitys, i, j + 1, chessboardSize.Width)) / 2;
                }
            }

            double[][] coefficients = new double[chessboardSize.Height - 1][];
            for (int i = 0; i < coefficients.Length; i++)
            {
                coefficients[i] = new double[chessboardSize.Width - 1];
                for (int j = 0; j < coefficients[i].Length; j++)
                {
                    PointF[][] pointsL = new PointF[][] { new PointF[2], new PointF[2] };
                    PointF[][] pointsR = new PointF[][] { new PointF[2], new PointF[2] };
                    pointsL[0][0] = GetElementFromArrayAsFromMatrix(corners[0], i, j, chessboardSize.Width);
                    pointsL[0][1] = GetElementFromArrayAsFromMatrix(corners[0], i, j + 1, chessboardSize.Width);
                    pointsL[1][0] = GetElementFromArrayAsFromMatrix(corners[0], i + 1, j, chessboardSize.Width);
                    pointsL[1][1] = GetElementFromArrayAsFromMatrix(corners[0], i + 1, j + 1, chessboardSize.Width);

                    pointsR[0][0] = GetElementFromArrayAsFromMatrix(corners[1], i, j, chessboardSize.Width);
                    pointsR[0][1] = GetElementFromArrayAsFromMatrix(corners[1], i, j + 1, chessboardSize.Width);
                    pointsR[1][0] = GetElementFromArrayAsFromMatrix(corners[1], i + 1, j, chessboardSize.Width);
                    pointsR[1][1] = GetElementFromArrayAsFromMatrix(corners[1], i + 1, j + 1, chessboardSize.Width);
                    coefficients[i][j] = (CountPixelsToMM(pointsL, sizeOfBlock) +
                        CountPixelsToMM(pointsR, sizeOfBlock)) / 2;
                }
            }

            double[][] distances = new double[chessboardSize.Height][];
            for (int i = 0; i < distances.Length; i++)
            {
                distances[i] = new double[chessboardSize.Width - 1];
                for (int j = 0; j < chessboardSize.Width - 1; j++)
                {
                    if (i < chessboardSize.Height - 1)
                    {
                        distances[i][j] = baseLine * focalLength / (mesurePointsDisparity[i][j] /** coefficients[i][j]*/);
                    }
                    else
                    {
                        distances[i][j] = baseLine * focalLength / (mesurePointsDisparity[i][j]/* *
                            coefficients[chessboardSize.Height - 2][chessboardSize.Width - 2]*/);
                    }
                }
            }

            double distance = 0;
            for (int i = 0; i < distances.Length; i++)
            {
                distance += distances[i].Sum();
            }
            distance /= distances.Length;
            return distance / 1000;
        }

        private static double CountPixelsToMM(PointF[][] points, double sizeOfBlockMM)
        {
            double[] lengths = new double[6];
            {
                lengths[0] = sizeOfBlockMM / Math.Sqrt(Math.Pow((points[0][0].X - points[0][1].X), 2) +
                    Math.Pow(points[0][0].Y - points[0][1].Y, 2));
                lengths[1] = sizeOfBlockMM / Math.Sqrt(Math.Pow((points[0][0].X - points[1][0].X), 2) +
                    Math.Pow(points[0][0].Y - points[1][0].Y, 2));
                lengths[2] = sizeOfBlockMM * Math.Sqrt(2) / Math.Sqrt(Math.Pow((points[0][0].X - points[1][1].X), 2) +
                    Math.Pow(points[0][0].Y - points[1][1].Y, 2));
                lengths[3] = sizeOfBlockMM / Math.Sqrt(Math.Pow((points[0][1].X - points[1][1].X), 2) +
                    Math.Pow(points[0][1].Y - points[1][1].Y, 2));
                lengths[4] = sizeOfBlockMM * Math.Sqrt(2) / Math.Sqrt(Math.Pow((points[0][1].X - points[1][0].X), 2) +
                    Math.Pow(points[0][1].Y - points[1][0].Y, 2));
                lengths[5] = sizeOfBlockMM / Math.Sqrt(Math.Pow((points[1][1].X - points[1][0].X), 2) +
                    Math.Pow(points[1][1].Y - points[1][0].Y, 2));
            }

            return lengths.Average();
        }

        private void tbCameraIndex1_TextChanged(object sender, EventArgs e)
        {
            if (tbCameraIndex1.Text == "")
            {
                return;
            }
            _cameraIndex[0] = Int32.Parse(tbCameraIndex1.Text);
        }

        private void tbCameraIndex2_TextChanged(object sender, EventArgs e)
        {
            if (tbCameraIndex2.Text == "")
            {
                return;
            }
            _cameraIndex[1] = Int32.Parse(tbCameraIndex2.Text);
        }

        private void tbFocalLength_TextChanged(object sender, EventArgs e)
        {
            if (tbFocalLength.Text == "")
            {
                return;
            }
            _focalLength = Double.Parse(tbFocalLength.Text);
        }

        private void tbBaseLine_TextChanged(object sender, EventArgs e)
        {
            if (tbBaseLine.Text == "")
            {
                return;
            }
            _baseLine = Double.Parse(tbBaseLine.Text);
        }

        private void tbWidthOfChessboard_TextChanged(object sender, EventArgs e)
        {
            if (tbWidthOfChessboard.Text == "")
            {
                return;
            }
            int h = _boardSize.Height;
            int w = Int32.Parse(tbWidthOfChessboard.Text);
            _boardSize = new Size(w, h);
        }

        private void tbHeightOfChessboard_TextChanged(object sender, EventArgs e)
        {
            if (tbHeightOfChessboard.Text == "")
            {
                return;
            }
            int w = _boardSize.Width;
            int h = Int32.Parse(tbWidthOfChessboard.Text);
            _boardSize = new Size(w, h);
        }

        private void tbBlockSize_TextChanged(object sender, EventArgs e)
        {
            if (tbBlockSize.Text == "")
            {
                return;
            }
            _blockSize = float.Parse(tbBlockSize.Text);
        }

        private void CalibratePhoto_Click(object sender, EventArgs e)
        {
            _makeCalibratingPhoto = true;
            CalibratePhoto.Text = "Photo for calibrate " + (_imageBufferLength - _currentImageIndex - 1);
        }
    }
}
