using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace AnimeFace
{
    class Detect
    {
        public static Image<Bgr, byte> FaceDetection(Image<Bgr, byte> oriImage, double ScaleFactor, int MinNeibors, out long times)
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
    }
}
