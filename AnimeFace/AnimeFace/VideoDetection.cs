using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.Util;

namespace AnimeFace
{
    public partial class VideoDetection : Form
    {
        private Capture capture;
        Mat frame;
        VideoWriter videowriter;
        int VideoFps;//FPS

        public VideoDetection()
        {
            InitializeComponent();
        }

        private void Record_Click(object sender, EventArgs e)
        {
            if (Record.Text == "Record")
            {//Open camera and record
                try
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "AVI|*.avi";
                    saveFileDialog.AddExtension = true;
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        capture = new Capture();
                        videowriter = new VideoWriter(saveFileDialog.FileName, //file name
                            6, //coding format
                            25, //pfs
                            new Size((int)CvInvoke.cveVideoCaptureGet(capture, Emgu.CV.CvEnum.CapProp.FrameWidth), //width
                            (int)CvInvoke.cveVideoCaptureGet(capture, Emgu.CV.CvEnum.CapProp.FrameHeight)), //height
                            true); //color
                        Application.Idle += new EventHandler(ProcessFrame);
                        Record.Text = "Stop";
                        Play.Enabled = false;
                    }
                }
                catch (NullReferenceException excpt)
                {
                    MessageBox.Show(excpt.Message);
                }
            }
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            if (Play.Text != "Play")//playing
            {
                frame = capture.QueryFrame();
                if (frame != null)
                {
                    //in order to smoothly playback, add delay
                    System.Threading.Thread.Sleep((int)(1000.0 / VideoFps - 15));
                    Image<Bgr,byte> image=new Image<Bgr, byte>(frame.Bitmap);
                    long times;
                    image = Detect.FaceDetection(image, 1.1, 5, out times);
                    
                    VideoBox.Image = image.Bitmap;
                }
                else
                {
                    Play.Text = "Play";
                    Record.Enabled = true;
                    VideoBox.Image = null;
                }
            }
            else if (Record.Text != "Record")//recording
            {
                try
                {
                    frame = capture.QueryFrame();
                    VideoBox.Image = frame.Bitmap;
                    videowriter.Write(frame);
                }
                catch
                {
                    System.Diagnostics.Process.Start(Application.ExecutablePath);
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            if (Play.Text == "Play")
            {//play model
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "AVI file|*.avi|RMVB file|*.rmvb|WMV file|*.wmv|MKV file|*.mkv|all file|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Application.Idle += new EventHandler(ProcessFrame);
                    capture = new Capture(openFileDialog.FileName);
                    VideoFps = (int) CvInvoke.cveVideoCaptureGet(capture,
                        Emgu.CV.CvEnum.CapProp.Fps);
                    Play.Text = "Stop";
                    VideoBox.Image = null;
                    Record.Enabled = false;
                }
            }
            else
            {
                capture.Dispose();
                Application.Idle -= new EventHandler(ProcessFrame);
                Play.Text = "Play";
                Record.Enabled = true;

            }
        }
    }
}
