using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace AnimeFace
{
    public partial class MainForm : Form
    {
        private string filename;
        public MainForm()
        {
            InitializeComponent();
        }

        private static Image<Bgr, byte> FaceDetection(Image<Bgr, byte> oriImage, double ScaleFactor, int MinNeibors, out long times)
        {
            List<Rectangle> Faces = new List<Rectangle>();
            CascadeClassifier lbp = new CascadeClassifier("lbpcascade_animeface.xml");
            Stopwatch watch;
            watch = Stopwatch.StartNew();
            using (Image<Gray, byte> cpuImage = new Image<Gray, byte>(oriImage.Bitmap))
            {
                Rectangle[] facesDetected = lbp.DetectMultiScale(
                    cpuImage,
                    ScaleFactor,
                    MinNeibors,
                    new Size(24, 24));
                Faces.AddRange(facesDetected);
            }
            watch.Stop();
            times = watch.ElapsedMilliseconds;
            foreach (var face in Faces)
            {
                oriImage.Draw(face, new Bgr(Color.DeepSkyBlue), 3);
            }
            return oriImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath+"\\figure";
            openFileDialog.Filter = "jpg|*.jpg|png|*.png|all|*.*";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string figpath = openFileDialog.FileName;
                textBox1.Text = figpath;
                filename = openFileDialog.SafeFileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(textBox1.Text);
                Image<Bgr, byte> image = new Image<Bgr, byte>(bmp);
                long times;
                image = FaceDetection(image, 1.1, 5, out times);
                pictureBox1.Image = image.Bitmap;
            }
            catch (Exception)
            {
                MessageBox.Show("A correct figure path is required.");
                //throw;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image!=null)
            {
                FolderBrowserDialog FBD = new FolderBrowserDialog();
                FBD.SelectedPath = Application.StartupPath + "\\output";
                if (FBD.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image temp = pictureBox1.Image;
                        Bitmap bmp = new Bitmap(temp);
                        bmp.Save(FBD.SelectedPath + "\\Detected_" + filename);
                        MessageBox.Show("Save successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An Exception had occured: " + ex.ToString());
                        //throw;
                    }
                }
            }
            else
            {
                MessageBox.Show("A figure is required to be indicated.");
            }
        }
    }
}
