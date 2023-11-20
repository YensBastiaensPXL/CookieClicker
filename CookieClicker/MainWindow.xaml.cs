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
        double aantalCookies = 10000; //aantal nog aanpassen bij upload
        long aantalInvestering = 0;
        long aantalMinusCookies = 0;
        double origineleAfbeeldingBreedte;
        bool isMouseDown = false;
        bool isMuisOverAfbeelding = false;
        DispatcherTimer passieveCookieTimer1;
        DispatcherTimer passieveCookieTimer10ms;
        double passieveCookieRatio1s = 0;
        double passieveCookieRatio10ms = 0;
        const int basisPrijsCursor = 2;
        double nieuweAankoopPrijs;
        double huidigeAankoopPrijs;


        long aantalKeerGekocht = 0;

        public MainWindow()
        {
            InitializeComponent();
            UpdateCookies();
            origineleAfbeeldingBreedte = klikebareCookie.Width;
            passieveCookieTimer1 = new DispatcherTimer();
            passieveCookieTimer10ms = new DispatcherTimer();
            passieveCookieTimer1.Interval = TimeSpan.FromSeconds(1);
            passieveCookieTimer10ms.Interval = TimeSpan.FromMilliseconds(10);

        }
        private void PassieveCookieTimer1s_Tick(object sender, EventArgs e)
        {
            double passieveCookiesPerTick = passieveCookieRatio1s;

            aantalCookies += passieveCookiesPerTick;
            UpdateCookies();
        }

        private void PassieveCookieTimer10ms_Tick(object sender, EventArgs e)
        {
            double passieveCookiesPerTick = passieveCookieRatio10ms;
            aantalCookies += passieveCookiesPerTick;
            UpdateCookies();
        }

        


        private void CursorClicked(object sender, MouseButtonEventArgs e)
        {
            Grid geklikteKnop = (Grid)sender;
            if (geklikteKnop.Name == "BtnInvesteringCursor")
            {
                if (aantalInvestering > 0)
                {
                    aantalInvestering++;
                   

                    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
                    nieuweAankoopPrijs = basisPrijsCursor * (1.15 * aantalInvestering);
                    huidigeAankoopPrijs = basisPrijsCursor * (1.15 * (aantalInvestering - 1));

                    PrijsCursor.Content = Math.Ceiling(nieuweAankoopPrijs).ToString();
                    aantalCookies -= Math.Ceiling(huidigeAankoopPrijs); 

                }
                else
                {
                    aantalCookies -= 2;
                    nieuweAankoopPrijs = basisPrijsCursor * 1.15;
                    PrijsCursor.Content = Math.Ceiling(nieuweAankoopPrijs).ToString();
                    aantalInvestering = 1;
                }

                CursorAantal.Content = aantalInvestering.ToString();

                //TERUG UIT COMMENTEN IN DE CODE ZODAT PASSIEVE COOKIES WERKEN

                //passieveCookieTimer1.Tick -= PassieveCookieTimer1s_Tick;
                //passieveCookieTimer1.Tick -= PassieveCookieTimer10ms_Tick;
                //passieveCookieRatio1s += 0.1;
                //passieveCookieRatio10ms += 0.001;
                //passieveCookieTimer1.Tick += PassieveCookieTimer1s_Tick;
                //passieveCookieTimer1.Tick += PassieveCookieTimer10ms_Tick;


                //passieveCookieTimer10ms.Start();
                //passieveCookieTimer1.Start();
                

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

        private void UpdateCookies()
        {
            //double aantalCookiesAfgerond = Math.Floor(aantalCookies);
            double aantalCookiesAfgerond = aantalCookies;
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

        private void toolTip(object sender, MouseEventArgs e)
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
