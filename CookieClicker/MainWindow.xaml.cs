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
using System.Windows.Media.Animation;
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
        double aantalCookies = 1000000; //aantal nog aanpassen bij upload
        double origineleAfbeeldingBreedte;
        bool isMouseDown = false;
        bool isMuisOverAfbeelding = false;
        //Passieve cookie timer
        DispatcherTimer passieveCookieTimer10ms;
        double passieveCookieRatio1s = 0;
        double passieveCookieRatio10ms = 0;
        double NextPassieveCookieRatio10ms = 20;
        double upcomingPassiveCookies;

        //basis prijs investeringen
        const int basisPrijsCursor = 2;
        const int basisPrijsGrandma = 100;
        const int basisPrijsFarm = 1100;
        const int basisPrijsMine = 12000;
        const int basisPrijsFactory = 130000;
        // variabelen per investering
        double kostCounterCursor;
        double huidigeAankoopPrijsCursor;
        long aantalInvesteringCursor = 0;
        double kostCounterGrandma;
        double huidigeAankoopPrijsGrandma;
        long aantalInvesteringGrandma = 0;
        double kostCounterFarm;
        double huidigeAankoopPrijsFarm;
        long aantalInvesteringFarm = 0;
        double kostCounterMine;
        double huidigeAankoopPrijsMine;
        long aantalInvesteringMine = 0;
        double kostCounterFactory;
        double huidigeAankoopPrijsFactory;
        long aantalInvesteringFactory = 0;

        public MainWindow()
        {
            InitializeComponent();
            UpdateCookies();
            origineleAfbeeldingBreedte = klikebareCookie.Width;
            passieveCookieTimer10ms = new DispatcherTimer();
            passieveCookieTimer10ms.Interval = TimeSpan.FromMilliseconds(10);

        }

        private void PassieveCookieTimer10ms_Tick(object sender, EventArgs e)
        {
            double passieveCookiesPerTick = passieveCookieRatio10ms;
            aantalCookies += passieveCookiesPerTick;
            UpdateCookies();
        }

        private void CursorClicked(object sender, MouseButtonEventArgs e)
        {
            BtnInvesteringFactory.IsEnabled = aantalCookies >= huidigeAankoopPrijsFactory;
            Grid geklikteKnop = (Grid)sender;
            if (geklikteKnop.Name == "BtnInvesteringCursor")
            {
                if (aantalInvesteringCursor > 0)
                {
                    aantalInvesteringCursor++;

                    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
                    kostCounterCursor = basisPrijsCursor * (1.15 * aantalInvesteringCursor);
                    huidigeAankoopPrijsCursor = basisPrijsCursor * (1.15 * (aantalInvesteringCursor - 1));
                    PrijsCursor.Content = Math.Ceiling(kostCounterCursor).ToString();
                    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsCursor); 

                }
                else
                {
                    aantalCookies -= 2;
                    kostCounterCursor = basisPrijsCursor * 1.15;
                    PrijsCursor.Content = Math.Ceiling(kostCounterCursor).ToString();
                    aantalInvesteringCursor = 1;
                }

                CursorAantal.Content = aantalInvesteringCursor.ToString();

                //TERUG UIT COMMENTEN IN DE CODE ZODAT PASSIEVE COOKIES WERKEN


                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 0.001;
                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();

            }
            else if (geklikteKnop.Name == "BtnInvesteringGrandma")
            {
                if (aantalInvesteringGrandma > 0)
                {
                    aantalInvesteringGrandma++;

                    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering of beide
                    kostCounterGrandma = basisPrijsGrandma * (1.15 * aantalInvesteringGrandma);
                    huidigeAankoopPrijsGrandma = basisPrijsGrandma * (1.15 * (aantalInvesteringGrandma - 1));
                    PrijsGrandma.Content = Math.Ceiling(kostCounterGrandma).ToString();
                    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsGrandma);

                }
                else
                {
                    aantalCookies -= 100;
                    kostCounterGrandma = basisPrijsGrandma * 1.15;
                    PrijsGrandma.Content = Math.Ceiling(kostCounterGrandma).ToString();
                    aantalInvesteringGrandma = 1;
                }

                GrandmaAantal.Content = aantalInvesteringGrandma.ToString();

                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 2;
                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                //weglaten of laten staan?
                passieveCookieTimer10ms.Start();

            }
            else if (geklikteKnop.Name == "BtnInvesteringFarm")
            {
                if (aantalInvesteringFarm > 0)
                {
                    aantalInvesteringFarm++;

                    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
                    kostCounterFarm = basisPrijsFarm * (1.15 * aantalInvesteringFarm);
                    huidigeAankoopPrijsFarm = basisPrijsFarm * (1.15 * (aantalInvesteringFarm - 1));
                    PrijsFarm.Content = Math.Ceiling(kostCounterFarm).ToString();
                    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsFarm);

                }
                else
                {
                    aantalCookies -= 1100;
                    kostCounterFarm = basisPrijsFarm * 1.15;
                    PrijsFarm.Content = Math.Ceiling(kostCounterFarm).ToString();
                    aantalInvesteringFarm = 1;
                }

                FarmAantal.Content = aantalInvesteringFarm.ToString();
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 3;
                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();
            }
            else if (geklikteKnop.Name == "BtnInvesteringMine")
            {
                if (aantalInvesteringMine > 0)
                {
                    aantalInvesteringMine++;

                    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
                    kostCounterMine = basisPrijsMine * (1.15 * aantalInvesteringMine);
                    huidigeAankoopPrijsMine = basisPrijsMine * (1.15 * (aantalInvesteringMine - 1));
                    PrijsMine.Content = Math.Ceiling(kostCounterMine).ToString();
                    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsMine);

                }
                else
                {
                    aantalCookies -= 12000;
                    kostCounterMine = basisPrijsMine * 1.15;
                    PrijsMine.Content = Math.Ceiling(kostCounterMine).ToString();
                    aantalInvesteringMine = 1;
                }

                MineAantal.Content = aantalInvesteringMine.ToString();
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 4;
                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();
            }
            else if (geklikteKnop.Name == "BtnInvesteringFactory")
            {
                if (aantalInvesteringFactory > 0)
                {
                    aantalInvesteringFactory++;

                    
                    UpdateInvestering();
                    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
                    PrijsFactory.Content = Math.Ceiling(kostCounterFactory).ToString();
                    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsFactory);
             
                }
                else
                {
                    aantalCookies -= 130000;
                    kostCounterFactory = basisPrijsFactory * 1.15;
                    PrijsFactory.Content = Math.Ceiling(kostCounterFactory).ToString();
                    aantalInvesteringFactory = 1;
                }

                FactoryAantal.Content = aantalInvesteringFactory.ToString();

                //COOKIE TIMER
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 20;
                //Tooltip part of code
                upcomingPassiveCookies = passieveCookieRatio10ms + NextPassieveCookieRatio10ms;

                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();

            }
            
            UpdateCookies();
        }

        private void toolTip(object sender, MouseEventArgs e)
        {


            Grid hoverBtn = (Grid)sender;
            if (hoverBtn.Name == "BtnInvesteringCursor")
            {
                ToolTipService.SetToolTip(hoverBtn, passieveCookieRatio10ms.ToString());

            }
            if (hoverBtn.Name == "BtnInvesteringGrandma")
            {
                ToolTipService.SetToolTip(hoverBtn, "lala");
            }
            if (hoverBtn.Name == "BtnInvesteringFactory")
            {
                ToolTipService.SetToolTip(hoverBtn, upcomingPassiveCookies);
            }
        }

        private void UpdateInvestering()
        {
            if (aantalCookies >= huidigeAankoopPrijsFactory)
            {
                kostCounterFactory = basisPrijsFactory * Math.Pow(1.15, aantalInvesteringFactory);
                huidigeAankoopPrijsFactory = basisPrijsFactory * Math.Pow(1.15, aantalInvesteringFactory - 1);
            }
            
        }

        private void UpdateCookies()
        {
            double aantalCookiesAfgerond = Math.Floor(aantalCookies);
            //double aantalCookiesAfgerond = aantalCookies;
            aantalCookiesTxt.Content = $"{aantalCookiesAfgerond} cookies";
            Title = $"Cookie clicker got {aantalCookiesAfgerond} cookies";
            // als aantal cookies per seconde opgeteld moeten worden moet je bv.
            // https://prnt.sc/UX6il5icQ8jy
            double aantalPerSecondeAfgerond = Math.Round(passieveCookieRatio10ms, 3);
            aantalPerSeconde.Content = $"{aantalPerSecondeAfgerond} per Milliseconde";

            //if statement?
            BtnInvesteringCursor.IsEnabled = aantalCookies >= 2;
            BtnInvesteringGrandma.IsEnabled = aantalCookies >= 100;
            BtnInvesteringFarm.IsEnabled = aantalCookies >= 1100;
            BtnInvesteringMine.IsEnabled = aantalCookies >= 12000;
            BtnInvesteringFactory.IsEnabled = aantalCookies >= kostCounterFactory;

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
        private async void MouseDownCookie(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            aantalCookies++;
            klikebareCookie.Width *= 0.9;
            isMuisOverAfbeelding = true;

            if (aantalCookiesTxt != null)
            {
                aantalCookiesTxt.FontSize *= 1.5;
                await Task.Delay(200);
                aantalCookiesTxt.FontSize /= 1.5;
            }

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

        
    }
}
