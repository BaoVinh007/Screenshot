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

using System.IO;

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
            // get url string
            string url = txtUrl.Text;

            // navigate to website based on url
            webBrowser1.Navigate("http://" + url);
            
            // Create a button and call click event to render image
            Button btn1 = new Button();
            btn1.Click += btn_Click;
            btn1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            
        }
                
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Render image");
            // Get height and width of image
            int height = int.Parse(txtHeight.Text.ToString());
            int width = int.Parse(txtWidth.Text.ToString());

            // Create 
            Bitmap bitmap = new Bitmap(width, height);
            System.Drawing.Rectangle bitmapRect = new System.Drawing.Rectangle(0, 0, width, height);

            // Update height and width of web browser
            webBrowser1.Height = height;
            webBrowser1.Width = width;

            webBrowser1.ScrollBarsEnabled = false;
            webBrowser1.ScriptErrorsSuppressed = true;

            // This is a method of the WebBrowser control, and the most important part
            webBrowser1.DrawToBitmap(bitmap, bitmapRect);

            System.Drawing.Image origImage = bitmap;
            Random rnd1 = new Random();
            int num = rnd1.Next(1, 1000000);
                        
            //Save rendered image to my pictures folder
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                        
            string filename = path + "test" + width + "_" + height + "_" + num + ".jpg";

            origImage.Save(filename);
                        
            BitmapImage src = new BitmapImage(new Uri(filename));

            // Update Image to view
            imageScreenshot.Height = height;
            imageScreenshot.Width = width;
            imageScreenshot.Source = src;            
        }
        
    }
}
