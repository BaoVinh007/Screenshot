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
            //this.Button_Click_1(sender, e);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int height = int.Parse(txtHeight.Text.ToString());
            int width = int.Parse(txtWidth.Text.ToString());
            Bitmap bitmap = new Bitmap(width, height);
            System.Drawing.Rectangle bitmapRect = new System.Drawing.Rectangle(0, 0, width, height);

            webBrowser1.Height = height;
            webBrowser1.Width = width;

            // This is a method of the WebBrowser control, and the most important part
            webBrowser1.DrawToBitmap(bitmap, bitmapRect);

            // Generate a thumbnail of the screenshot (optional)
            System.Drawing.Image origImage = bitmap;
            Random rnd1 = new Random();
            int num = rnd1.Next(1, 1000000);
            string filename = "test" + width + "_" + height + "_" + num + ".jpg";
            origImage.Save(filename);
            string path = @"C:\Users\VinhNguyen\Documents\GitHub\Screenshot\Screenshot\Screenshot\bin\Debug\";
            BitmapImage src = new BitmapImage(new Uri(path+filename));
            imageScreenshot.Height = height;
            imageScreenshot.Width = width;
            imageScreenshot.Source = src;
            webBrowser1.Visible = false;
        }
    }
}
