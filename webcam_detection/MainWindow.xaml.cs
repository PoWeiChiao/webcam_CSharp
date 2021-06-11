using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace webcam_detection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private VideoCapture cap = null;
        private Mat frame;
        public MainWindow()
        {
            InitializeComponent();
            cap = new VideoCapture(0);
            frame = new Mat();
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Tick += FrameProcess;
            Timer.Interval = TimeSpan.FromMilliseconds(30);
            Timer.Start();
        }

        private Bitmap MatToBitmap(Mat image)
        {
            return BitmapConverter.ToBitmap(image);
        }

        private BitmapImage MatToBitmapImage(Mat image)
        {
            Bitmap bitmap = MatToBitmap(image);
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }

        private void FrameProcess(object sender, EventArgs e)
        {
            if (cap.Read(frame))
            {
                image.Source = MatToBitmapImage(frame);
            }
        }
    }
}
