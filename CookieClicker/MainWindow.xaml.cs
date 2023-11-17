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
            Title = $"Cookie clicker got {aantalCookiesAfgerond} cookies";

            BtnInvesteringCursor.IsEnabled = aantalCookies >= 2;
            BtnInvesteringGrandma.IsEnabled = aantalCookies >= 100;
            BtnInvesteringFarm.IsEnabled = aantalCookies >= 1100;
            BtnInvesteringMine.IsEnabled = aantalCookies >= 12000;
            BtnInvesteringFactory.IsEnabled = aantalCookies >= 130000;

            /*if (aantalCookies >= 2)
            {
                BtnInvesteringCursor.IsEnabled = true;
            }
            else
            {
                BtnInvesteringCursor.IsEnabled = false;
            }

            if (aantalCookies >= 100)
            {
                BtnInvesteringGrandma.IsEnabled = true;
            }
            else
            {
                BtnInvesteringGrandma.IsEnabled = false;
            }

            if (aantalCookies >= 1100)
            {
                BtnInvesteringFarm.IsEnabled = true;
            }
            else
            {
                BtnInvesteringFarm.IsEnabled = false;
            }

            if (aantalCookies >= 12000)
            {
                BtnInvesteringMine.IsEnabled = true;
            }
            else
            {
                BtnInvesteringMine.IsEnabled = false;
            }

            if (aantalCookies >= 12000)
            {
                BtnInvesteringFactory.IsEnabled = true;
            }
            else
            {
                BtnInvesteringFactory.IsEnabled = false;
            } */
        }


        private void CursorClicked(object sender, MouseButtonEventArgs e)
        {
            Grid geklikteKnop = (Grid)sender;
            if (geklikteKnop.Name == "BtnInvesteringCursor")
            {
                aantalCookies -= 2;
            }
            else if (geklikteKnop.Name == "BtnInvesteringGrandma")
            {
                aantalCookies -= 100;
            }
            else if (geklikteKnop.Name == "BtnInvesteringFarm")
            {
                aantalCookies -= 1100;
            }
            else if (geklikteKnop.Name == "BtnInvesteringMine")
            {
                aantalCookies -= 12000;
            }
            else 
            {
                aantalCookies -= 130000;
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
