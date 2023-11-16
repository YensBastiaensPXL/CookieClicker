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
using System.Windows.Threading;

namespace CookieClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double aantalCookies = 0;
        double origineleAfbeeldingBreedte;
        bool isMouseDown = false;
        bool isMouseOverImage = false;
        public MainWindow()
        {
            InitializeComponent();
            UpdateCookies();
            origineleAfbeeldingBreedte = clickableImage.Width;
        }

        private void UpdateCookies()
        {
            double aantalCookiesAfgerond = Math.Floor(aantalCookies);
            aantalCookiesTxt.Content = $"{aantalCookiesAfgerond} cookies";
        }

        private void CursorClicked(object sender, MouseButtonEventArgs e)
        {
            aantalCookies -= 2;
            if (aantalCookies < 0)
            {
                aantalCookies = 0;
            }
            UpdateCookies();
        }

        private void MouseDownCookie(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            aantalCookies++;
            clickableImage.Width *= 0.9;
            
            isMouseOverImage = true;
            UpdateCookies();
            
        }
        private void MouseUpCookie(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
            if (isMouseOverImage)
            {
                clickableImage.Width = origineleAfbeeldingBreedte;
            }
            isMouseOverImage= false;
            UpdateCookies();
        }
        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                isMouseOverImage = true;
            }
        }

        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                clickableImage.Width = origineleAfbeeldingBreedte;
            }
            isMouseOverImage = false;
        }
    }
}
