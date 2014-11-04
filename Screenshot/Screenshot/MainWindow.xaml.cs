using System;
using System.Collections.Generic;
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
using System.Drawing;
using System.Windows.Threading;

namespace Screenshot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Windows.Forms.WebBrowser webBrowser1 = new System.Windows.Forms.WebBrowser();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        
                
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = txtUrl.Text;
            webBrowser1.Navigate("http://" + url);
            Button btn1 = new Button();
            btn1.Click += btn_Click;
            btn1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(timer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (chkUpdate.IsChecked.Value == true)
            {
                MessageBox.Show("Recapture...");
                Button btn1 = new Button();
                btn1.Click += Button_Click;
                btn1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }


        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Render image");
            int height = int.Parse(txtHeight.Text.ToString());
            int width = int.Parse(txtWidth.Text.ToString());
            Bitmap bitmap = new Bitmap(width, height);
            System.Drawing.Rectangle bitmapRect = new System.Drawing.Rectangle(0, 0, width, height);

            webBrowser1.Height = height;
            webBrowser1.Width = width;

            // This is a method of the WebBrowser control, and the most important part
            webBrowser1.DrawToBitmap(bitmap, bitmapRect);

            System.Drawing.Image origImage = bitmap;
            Random rnd1 = new Random();
            int num = rnd1.Next(1, 1000000);

            // You have to change correct path to show the image            
            string path = @"C:\";
            string filename = path + "test" + width + "_" + height + "_" + num + ".jpg";
            origImage.Save(filename);

            BitmapImage src = new BitmapImage(new Uri(filename));
            imageScreenshot.Height = height;
            imageScreenshot.Width = width;
            imageScreenshot.Source = src;
            webBrowser1.Visible = false;
        }
        
    }
}
