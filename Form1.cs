// Two Cameras Vision

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

using OrthoAnalisis;

namespace Walking_Analysis
{
    public partial class Form1 : Form
    {
        // list of video devices
        FilterInfoCollection videoDevices;
        Recorder rec;

        public Form1()
        {
            InitializeComponent();

            // show device list
            try
            {
                // enumerate video devices
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    throw new Exception();
                }

                for (int i = 1, n = videoDevices.Count; i <= n; i++)
                {
                    string cameraName = i + " : " + videoDevices[i - 1].Name;

                    camera1Combo.Items.Add(cameraName);
                    camera2Combo.Items.Add(cameraName);
                    camera3Combo.Items.Add(cameraName);
                    camera4Combo.Items.Add(cameraName);
                    camera5Combo.Items.Add(cameraName);
                }
                Console.WriteLine("camaras " + videoDevices.Count);


                // check cameras count
                if (videoDevices.Count == 2)
                {
                    camera2Combo.Items.Clear();

                    camera2Combo.Items.Add("Solo se encontro una cámara");
                    camera2Combo.SelectedIndex = 0;
                    camera2Combo.Enabled = false;

                    camera3Combo.Items.Clear();

                    camera3Combo.Items.Add("Solo se encontro una cámara");
                    camera3Combo.SelectedIndex = 0;
                    camera3Combo.Enabled = false;

                    camera4Combo.Items.Clear();

                    camera4Combo.Items.Add("Solo se encontro una cámara");
                    camera4Combo.SelectedIndex = 0;
                    camera4Combo.Enabled = false;

                    camera5Combo.Items.Clear();

                    camera5Combo.Items.Add("Solo se encontro una cámara");
                    camera5Combo.SelectedIndex = 0;
                    camera5Combo.Enabled = false;
                }
                if (videoDevices.Count ==5)
                {
                    camera3Combo.Items.Clear();

                    camera3Combo.Items.Add("Solo se encontraron dos cámaras");
                    camera3Combo.SelectedIndex = 0;
                    camera3Combo.Enabled = false;

                    camera4Combo.Items.Clear();

                    camera4Combo.Items.Add("Solo se encontraron dos cámaras");
                    camera4Combo.SelectedIndex = 0;
                    camera4Combo.Enabled = false;

                    camera5Combo.Items.Clear();

                    camera5Combo.Items.Add("Solo se encontraron dos cámaras");
                    camera5Combo.SelectedIndex = 0;
                    camera5Combo.Enabled = false;

                    camera2Combo.SelectedIndex = 1;
                }
                if (videoDevices.Count == 3)
                {
                    camera4Combo.Items.Clear();

                    camera4Combo.Items.Add("Solo se encontraron tres cámaras");
                    camera4Combo.SelectedIndex = 0;
                    camera4Combo.Enabled = false;

                    camera5Combo.Items.Clear();

                    camera5Combo.Items.Add("Solo se encontraron tres cámaras");
                    camera5Combo.SelectedIndex = 0;
                    camera5Combo.Enabled = false;

                    camera2Combo.SelectedIndex = 1;
                    camera3Combo.SelectedIndex = 2;
                }
                if (videoDevices.Count == 4)
                {
                    camera5Combo.Items.Clear();

                    camera5Combo.Items.Add("Solo se encontraron cuatro cámaras");
                    camera5Combo.SelectedIndex = 0;
                    camera5Combo.Enabled = false;

                    camera2Combo.SelectedIndex = 1;
                    camera3Combo.SelectedIndex = 2;
                    camera4Combo.SelectedIndex = 3;
                }
                if (videoDevices.Count == 5)
                {
                    camera2Combo.SelectedIndex = 2;
                    camera3Combo.SelectedIndex = 3;
                    camera4Combo.SelectedIndex = 4;
                    camera5Combo.SelectedIndex = 5;
                }
                if (videoDevices.Count == 6)
                {
                    camera2Combo.SelectedIndex = 2;
                    camera3Combo.SelectedIndex = 3;
                    camera4Combo.SelectedIndex = 4;
                    camera5Combo.SelectedIndex = 5;
                }

                camera1Combo.SelectedIndex = 1;
            }
            catch
            {
                startButton.Enabled = false;

                camera1Combo.Items.Add("No se encontraron cámaras");
                camera2Combo.Items.Add("No se encontraron cámaras");
                camera3Combo.Items.Add("No se encontraron cámaras");
                camera4Combo.Items.Add("No se encontraron cámaras");
                camera5Combo.Items.Add("No se encontraron cámaras");

                camera1Combo.SelectedIndex = 0;
                camera2Combo.SelectedIndex = 0;
                camera3Combo.SelectedIndex = 0;
                camera4Combo.SelectedIndex = 0;
                camera5Combo.SelectedIndex = 0;

                camera1Combo.Enabled = false;
                camera2Combo.Enabled = false;
                camera3Combo.Enabled = false;
                camera4Combo.Enabled = false;
                camera5Combo.Enabled = false;

            }

        }

        private void resetFields()
        {
            textBoxNombre.ResetText();
        }


        // Main form closing - stop cameras
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCameras();
        }

        // On "Start" button click - start cameras
        private void startButton_Click(object sender, EventArgs e)
        {
            StartCameras();

            startButton.Enabled = false;
            stopButton.Enabled = true;
        }

        // On "Stop" button click - stop cameras
        private void stopButton_Click(object sender, EventArgs e)
        {
            StopCameras();

            startButton.Enabled = true;
            stopButton.Enabled = false;
        }

        // Start cameras
        private void StartCameras()
        {
            // create first video source
            VideoCaptureDevice videoSource1 = new VideoCaptureDevice(videoDevices[camera1Combo.SelectedIndex].MonikerString);
            videoSource1.VideoResolution = videoSource1.VideoCapabilities[5];
            videoSourcePlayer1.VideoSource = videoSource1;
            videoSourcePlayer1.Start();

            //Para debugear o ver que resoluciones tiene el aparato

            /*for (int i = 0; i < videoSource1.VideoCapabilities.Length; i++)
            {

                string resolution_size = videoSource1.VideoCapabilities[i].FrameSize.ToString();

                string framerate = videoSource1.VideoCapabilities[i].AverageFrameRate.ToString();
                MessageBox.Show(resolution_size +" "+ framerate +" fps");
            }*/

            // create second video source
            if (camera2Combo.Enabled == true)
            {
                System.Threading.Thread.Sleep(500);

                VideoCaptureDevice videoSource2 = new VideoCaptureDevice(videoDevices[camera2Combo.SelectedIndex].MonikerString);

                videoSource2.VideoResolution = videoSource2.VideoCapabilities[5];
                videoSourcePlayer2.VideoSource = videoSource2;
                videoSourcePlayer2.Start();
            }
            if (camera3Combo.Enabled == true)
            {
                System.Threading.Thread.Sleep(500);

                VideoCaptureDevice videoSource3 = new VideoCaptureDevice(videoDevices[camera3Combo.SelectedIndex].MonikerString);

                videoSource3.VideoResolution = videoSource3.VideoCapabilities[5];
                videoSourcePlayer3.VideoSource = videoSource3;
                videoSourcePlayer3.Start();
            }
            if (camera4Combo.Enabled == true)
            {
                System.Threading.Thread.Sleep(500);

                VideoCaptureDevice videoSource4 = new VideoCaptureDevice(videoDevices[camera4Combo.SelectedIndex].MonikerString);

                videoSource4.VideoResolution = videoSource4.VideoCapabilities[5];
                videoSourcePlayer4.VideoSource = videoSource4;
                videoSourcePlayer4.Start();
            }
            if (camera5Combo.Enabled == true)
            {
                System.Threading.Thread.Sleep(500);

                VideoCaptureDevice videoSource5 = new VideoCaptureDevice(videoDevices[camera5Combo.SelectedIndex].MonikerString);

                videoSource5.VideoResolution = videoSource5.VideoCapabilities[5];
                videoSourcePlayer5.VideoSource = videoSource5;
                videoSourcePlayer5.Start();
            }

        }

        // Stop cameras
        private void StopCameras()
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer2.SignalToStop();
            videoSourcePlayer3.SignalToStop();
            videoSourcePlayer4.SignalToStop();
            videoSourcePlayer5.SignalToStop();

            videoSourcePlayer1.WaitForStop();
            videoSourcePlayer2.WaitForStop();
            videoSourcePlayer3.WaitForStop();
            videoSourcePlayer4.WaitForStop();
            videoSourcePlayer5.WaitForStop();

        }


        private void buttonStartRecord_Click(object sender, EventArgs e)
        {
            string filename = string.Format("{0}-{1}.avi", textBoxNombre.Text.Replace(" ", ""), DateTime.Now.ToString("dd-MM-yyyy_HH-mm"));
            RecorderParams recParams = new RecorderParams("D:\\temp\\" + filename, 10, SharpAvi.CodecIds.MotionJpeg, 70);
            rec = new Recorder(recParams);
            labelGrabando.Visible = true;
        }

        private void buttonStopRecord_Click(object sender, EventArgs e)
        {
            rec.Dispose();
            StopCameras();

            MessageBox.Show("Video Guardado Exitosamente");
            resetFields();

            buttonStartRecord.Visible = false;
            buttonStopRecord.Visible = false;
            labelGrabar.Visible = true;
            labelGrabando.Visible = false;
        }

        private void textBoxNombre_MouseLeave(object sender, KeyEventArgs e)
        {
            buttonStartRecord.Visible = true;
            buttonStopRecord.Visible = true;
            labelGrabar.Visible = false;
        }
    }
}
