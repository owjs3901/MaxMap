using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MaxMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Capture()
        {
            while (true)
            {
                Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart) delegate()
                {
                    
                    int rwidth=Screen.PrimaryScreen.Bounds.Width;
                    int rheight=Screen.PrimaryScreen.Bounds.Height;
                    
                    int size=500;
                    Bitmap source = new Bitmap(rwidth,
                        rheight);
                    Graphics g = Graphics.FromImage(source);
                    g.CopyFromScreen(0, 0, 0, 0, source.Size);
                    
                    
                    Bitmap bitmap = new Bitmap(size, size);
                    Graphics gc = Graphics.FromImage(bitmap);
                    gc.DrawImage(source,0,0,new Rectangle(rwidth-size,rheight-size+240,rwidth,rheight),GraphicsUnit.Pixel );
                    
                    // bitmap = bitmap.Clone(new Rectangle(width, height, rwidth - width, rheight - height),
                        // bitmap.PixelFormat);
                    ImgScreen.Source = Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero,
                        Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                });
                // if (ImgScreen.Source.Dispatcher != null)
                // ImgScreen.Source.Dispatcher.BeginInvoke(new Action(() =>
                // {

                // }));
                Thread.Sleep(16);
            }
        }

        private void GameSearch(object a, EventArgs eventArgs)
        {
            Debug.WriteLine("이잉");
            Process[] processes = Process.GetProcesses();
            foreach (Process process in Process.GetProcesses().Where(p => !string.IsNullOrEmpty(p.MainWindowTitle))
                .ToList())
            {
                Debug.WriteLine(process.GetType());
            }
        }

        private void DrawGameScreen(object a, EventArgs eventArgs)
        {
            DrawGameBtn.IsEnabled = false;
            Thread thread = new Thread(Capture);
            thread.Start();
        }
    }
}