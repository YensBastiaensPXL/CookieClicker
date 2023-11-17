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
        long aantalInvestering = 0;
        double origineleAfbeeldingBreedte;
        bool isMouseDown = false;
        bool isMuisOverAfbeelding = false;
        public MainWindow()
        {
            InitializeComponent();
            UpdateCookies();
            origineleAfbeeldingBreedte = klikebareCookie.Width;
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
                aantalInvestering += 1;
                CursorAantal.Content = aantalInvestering.ToString();
            }
            else if (geklikteKnop.Name == "BtnInvesteringGrandma")
            {
                aantalCookies -= 100;
                aantalInvestering += 1;
                GrandmaAantal.Content = aantalInvestering.ToString();
            }
            else if (geklikteKnop.Name == "BtnInvesteringFarm")
            {
                aantalCookies -= 1100;
                aantalInvestering += 1;
                FarmAantal.Content = aantalInvestering.ToString();
            }
            else if (geklikteKnop.Name == "BtnInvesteringMine")
            {
                aantalCookies -= 12000;
                aantalInvestering += 1;
                MineAantal.Content = aantalInvestering.ToString();
            }
            else 
            {
                aantalCookies -= 130000;
                aantalInvestering += 1;
                FactoryAantal.Content = aantalInvestering.ToString();
            }


            UpdateCookies();
        }
        private void MouseDownCookie(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            aantalCookies++;
            klikebareCookie.Width *= 0.9;
            isMuisOverAfbeelding = true;
            UpdateCookies();
            
        }
        private void MouseUpCookie(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
            if (isMuisOverAfbeelding)
            {
                klikebareCookie.Width = origineleAfbeeldingBreedte;
            }
            isMuisOverAfbeelding= false;
        }
        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                isMuisOverAfbeelding = true;
            }
        }

        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                klikebareCookie.Width = origineleAfbeeldingBreedte;
            }
            isMuisOverAfbeelding = false;
        }

        private void BtnInvesteringCursor_MouseEnter(object sender, MouseEventArgs e)
        {
      
            
            Grid hoverBtn = (Grid)sender;
            if (hoverBtn.Name == "BtnInvesteringCursor")
            {
                ToolTipService.SetToolTip(hoverBtn, "Your tooltip text");
                //hoveredButton.ToolTip = 1;
            }
        }
    }
}
