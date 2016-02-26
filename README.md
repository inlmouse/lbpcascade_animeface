# lbpcascade_animeface.NET

The face detector for anime/manga using OpenCV in C\#.

Original release since 2011 at [OpenCVによるアニメ顔検出ならlbpcascade_animeface.xml](http://ultraist.hatenablog.com/entry/20110718/1310965532) (in Japanese)

## Usage

Build the project and follow the UserInterface instruction.

### C\# Example

```CSharp
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
```
Run

    image = FaceDetection(image, 1.1, 5, out times);

![result](https://raw.githubusercontent.com/inlmouse/lbpcascade_animeface/master/AnimeFace/AnimeFace/bin/x86/Debug/output/Detected_LoveLive.jpg)
