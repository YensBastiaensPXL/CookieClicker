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
        double aantalCookies = 100; //aantal nog aanpassen bij upload
        double origineleAfbeeldingBreedte;
        bool isMouseDown = false;
        bool isMuisOverAfbeelding = false;
        //Passieve cookie timer
        DispatcherTimer passieveCookieTimer10ms;
        double passieveCookieRatio10ms = 0;
        
        //basis prijs investeringen
        const int basisPrijsCursor = 15;
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

        bool investerinCursorUpdated = false;
        public MainWindow()
        {
            InitializeComponent();
            UpdateCookies();
            
            origineleAfbeeldingBreedte = klikebareCookie.Width;
            passieveCookieTimer10ms = new DispatcherTimer();
            passieveCookieTimer10ms.Interval = TimeSpan.FromMilliseconds(10);

            BtnInvesteringCursor.IsEnabled = aantalCookies >= basisPrijsCursor;
            BtnInvesteringGrandma.IsEnabled = aantalCookies >= basisPrijsGrandma;
            BtnInvesteringFarm.IsEnabled = aantalCookies >= basisPrijsFarm;
            BtnInvesteringMine.IsEnabled = aantalCookies >= basisPrijsMine;
            BtnInvesteringFactory.IsEnabled = aantalCookies >= basisPrijsFactory;
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
                if (aantalInvesteringCursor > 0)
                {
                    aantalInvesteringCursor++;
                    UpdateInvestering("cursor", ref kostCounterCursor);

                }
                else
                {
                    aantalCookies -= 15;
                    kostCounterCursor = basisPrijsCursor * 1.15;
                    PrijsCursor.Content = Math.Ceiling(kostCounterCursor).ToString();
                    aantalInvesteringCursor = 1;
                }

                CursorAantal.Content = aantalInvesteringCursor.ToString();

                //Passive cookies counter
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 0.001;
                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();

            }
            //else if (geklikteKnop.Name == "BtnInvesteringGrandma")
            //{
            //    if (aantalInvesteringGrandma > 0)
            //    {
            //        aantalInvesteringGrandma++;
            //        UpdateInvestering();
            //        //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering of beide
                    
                    

            //    }
            //    else
            //    {
            //        aantalCookies -= 100;
            //        kostCounterGrandma = basisPrijsGrandma * 1.15;
            //        PrijsGrandma.Content = Math.Ceiling(kostCounterGrandma).ToString();
            //        aantalInvesteringGrandma = 1;
            //    }

            //    GrandmaAantal.Content = aantalInvesteringGrandma.ToString();

            //    //Passive cookies counter
            //    passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
            //    passieveCookieRatio10ms += 0.01;
            //    ////Tooltip part of code
            //    //passieveCookieRatio1sGrandma = passieveCookieRatio10ms * 100;
            //    //upcomingPassiveCookiesGrandma = passieveCookieRatio1sGrandma + 1;

            //    passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
            //    //weglaten of laten staan? bij allemaal buiten cursor moet timer sws
            //    passieveCookieTimer10ms.Start();

            //}
            //else if (geklikteKnop.Name == "BtnInvesteringFarm")
            //{
            //    if (aantalInvesteringFarm > 0)
            //    {
            //        aantalInvesteringFarm++;
            //        UpdateInvestering();

            //    }
            //    else
            //    {
            //        aantalCookies -= 1100;
            //        kostCounterFarm = basisPrijsFarm * 1.15;
            //        PrijsFarm.Content = Math.Ceiling(kostCounterFarm).ToString();
            //        aantalInvesteringFarm = 1;
            //    }

            //    FarmAantal.Content = aantalInvesteringFarm.ToString();

            //    //Passive cookies counter
            //    passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
            //    passieveCookieRatio10ms += 0.08;
            //    ////Tooltip part of code
            //    //passieveCookieRatio1sFarm = passieveCookieRatio10ms * 100;
            //    //upcomingPassiveCookiesFarm = passieveCookieRatio1sFarm + 8;

            //    passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
            //    passieveCookieTimer10ms.Start();
            //}
            //else if (geklikteKnop.Name == "BtnInvesteringMine")
            //{
            //    if (aantalInvesteringMine > 0)
            //    {
            //        aantalInvesteringMine++;
            //        UpdateInvestering();


            //    }
            //    else
            //    {
            //        aantalCookies -= 12000;
            //        kostCounterMine = basisPrijsMine * 1.15;
            //        PrijsMine.Content = Math.Ceiling(kostCounterMine).ToString();
            //        aantalInvesteringMine = 1;
            //    }

            //    MineAantal.Content = aantalInvesteringMine.ToString();

            //    //Passive cookies counter
            //    passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
            //    passieveCookieRatio10ms += 0.47;
            //    ////Tooltip part of code
            //    //passieveCookieRatio1sMine = passieveCookieRatio10ms * 100;
            //    //upcomingPassiveCookiesMine = passieveCookieRatio1sMine + 47;

            //    passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
            //    passieveCookieTimer10ms.Start();
            //}
            //else if (geklikteKnop.Name == "BtnInvesteringFactory")
            //{
            //    if (aantalInvesteringFactory > 0)
            //    {
            //        aantalInvesteringFactory++;

                    
            //        UpdateInvestering();
            //        //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
                    
             
            //    }
            //    else
            //    {
            //        aantalCookies -= 130000;
            //        kostCounterFactory = basisPrijsFactory * 1.15;
            //        PrijsFactory.Content = Math.Ceiling(kostCounterFactory).ToString();
            //        aantalInvesteringFactory = 1;
            //    }

            //    FactoryAantal.Content = aantalInvesteringFactory.ToString();

            //    //Passive cookies counter
            //    passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
            //    passieveCookieRatio10ms += 2.60;

            //    ////Tooltip part of code
            //    //passieveCookieRatio1sFactory = passieveCookieRatio10ms * 100;
            //    //upcomingPassiveCookiesFactory = passieveCookieRatio1sFactory + 260 ;

            //    passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
            //    passieveCookieTimer10ms.Start();

            //}
            
            UpdateCookies();
        }

        // anatomie / signatuur
        // accesmodifier -> returntype -> naam -> param list
        private double Calculate(double basisprijs, double counter )
        {
            return basisprijs * Math.Pow(1.15, counter);
        }

        private void UpdateInvestering(string type, ref double kostCounter)
        {

            if (aantalCookies >= huidigeAankoopPrijsCursor && type.Equals("cursor"))
            {
                //huidigeAankoopPrijsCursor = CalculateInvestmentPrice(basisPrijsCursor, aantalInvesteringCursor);
                //aantalInvesteringCursor++;
                //kostCounterCursor = Caldulate
                huidigeAankoopPrijsCursor = Calculate(basisPrijsCursor, aantalInvesteringCursor - 1);
                kostCounterCursor = Calculate(basisPrijsCursor, aantalInvesteringCursor);
                PrijsCursor.Content = Math.Ceiling(kostCounterCursor).ToString();
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsCursor);

                //kostCounterCursor = basisPrijsCursor * Math.Pow(1.15, aantalInvesteringCursor);
                //huidigeAankoopPrijsCursor = basisPrijsCursor * Math.Pow(1.15, aantalInvesteringCursor - 1);
                //PrijsCursor.Content = Math.Ceiling(kostCounterCursor).ToString();
                //aantalCookies -= Math.Ceiling(huidigeAankoopPrijsCursor);
            }
            //if (aantalCookies >= Calculate(basisPrijsGrandma, aantalInvesteringGrandma))
            //{
            //    kostCounterGrandma = basisPrijsGrandma * Math.Pow(1.15, aantalInvesteringGrandma);
            //    huidigeAankoopPrijsGrandma = basisPrijsGrandma * Math.Pow(1.15, aantalInvesteringGrandma - 1);
            //    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
            //    PrijsGrandma.Content = Math.Ceiling(kostCounterGrandma).ToString();
            //    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsGrandma);
            //}
            //if (aantalCookies >= huidigeAankoopPrijsFarm)
            //{
            //    kostCounterFarm = basisPrijsFarm * Math.Pow(1.15, aantalInvesteringFarm);
            //    huidigeAankoopPrijsFarm = basisPrijsFarm * Math.Pow(1.15, aantalInvesteringFarm - 1);
            //    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
            //    PrijsFarm.Content = Math.Ceiling(kostCounterFarm).ToString();
            //    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsFarm);
            //}
            //if (aantalCookies >= huidigeAankoopPrijsMine)
            //{
            //    kostCounterMine = basisPrijsMine * Math.Pow(1.15, aantalInvesteringMine);
            //    huidigeAankoopPrijsMine = basisPrijsMine * Math.Pow(1.15, aantalInvesteringMine - 1);
            //    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
            //    PrijsMine.Content = Math.Ceiling(kostCounterMine).ToString();
            //    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsMine);
            //}
            //if (aantalCookies >= huidigeAankoopPrijsFactory)
            //{
            //    kostCounterFactory = basisPrijsFactory * Math.Pow(1.15, aantalInvesteringFactory);
            //    huidigeAankoopPrijsFactory = basisPrijsFactory * Math.Pow(1.15, aantalInvesteringFactory - 1);
            //    //Moet getal afgerond worden in berekening en/of bij het visuele aantal cookies/kost van investering
            //    PrijsFactory.Content = Math.Ceiling(kostCounterFactory).ToString();
            //    aantalCookies -= Math.Ceiling(huidigeAankoopPrijsFactory);
            //}


        }

        private void UpdateCookies()
        {
            aantalCookiesTxt.Content = FormatteerNummer(aantalCookies);
            double aantalCookiesAfgerond = Math.Floor(aantalCookies);
            //double aantalCookiesAfgerond = aantalCookies;
            //aantalCookiesTxt.Content = $"{aantalCookiesAfgerond} cookies";
            Title = $"Cookie clicker got {aantalCookiesAfgerond} cookies";
            // als aantal cookies per seconde opgeteld moeten worden moet je bv.
            // https://prnt.sc/UX6il5icQ8jy
            double aantalPerSecondeAfgerond = Math.Round(passieveCookieRatio10ms, 3);
            aantalPerSeconde.Content = $"{aantalPerSecondeAfgerond} per Milliseconde";

            //if (!investerinCursorUpdated)
            //{
            //    BtnInvesteringCursor.IsEnabled = aantalCookies >= basisPrijsCursor;
            //    investerinCursorUpdated = true;
            //}
            //if (investerinCursorUpdated)
            //{
            //    BtnInvesteringCursor.IsEnabled = aantalCookies >= kostCounterCursor;
            //}

            BtnInvesteringCursor.IsEnabled = aantalCookies >= Calculate(basisPrijsCursor, aantalInvesteringCursor);
            BtnInvesteringGrandma.IsEnabled = aantalCookies >= kostCounterGrandma;
            BtnInvesteringFarm.IsEnabled = aantalCookies >= kostCounterFarm;
            BtnInvesteringMine.IsEnabled = aantalCookies >= kostCounterMine;
            BtnInvesteringFactory.IsEnabled = aantalCookies >= kostCounterFactory;



        }
        public static string FormatteerNummer(double cookies)
        {
            if (cookies >= 1_000_000_000_000_000)
            {
                return $"{cookies / 1_000_000_000_000_000:N3} Triljard";
            }
            else if (cookies >= 1_000_000_000_000)
            {
                return $"{cookies / 1_000_000_000_000:N3} Biljoen";
            }
            else if (cookies >= 1_000_000_000)
            {
                return $"{cookies / 1_000_000_000:N3} Miljard";
            }
            else if (cookies >= 1_000_000)
            {
                return $"{cookies / 1_000_000:N3} Miljoen";
            }
            else if (cookies >= 1_000)
            {
                return $"{cookies / 1_000:N3}".Replace(",", " ");
            }
            else
            {
                return $"{cookies:N0}".Replace(",", " ");
            }
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

        private void toolTip(object sender, MouseEventArgs e)
        {


            Grid hoverBtn = (Grid)sender;
            if (hoverBtn.Name == "BtnInvesteringCursor")
            {
                ToolTipService.SetToolTip(hoverBtn,"+" + 0.1
                   + " Cookies per seconde met deze aankoop");

            }
            if (hoverBtn.Name == "BtnInvesteringGrandma")
            {
                ToolTipService.SetToolTip(hoverBtn, "+" + 1
                   + " Cookies per seconde met deze aankoop");
            }
            if (hoverBtn.Name == "BtnInvesteringFarm")
            {
                ToolTipService.SetToolTip(hoverBtn, "+" + 8
                   + " Cookies per seconde met deze aankoop");
            }
            if (hoverBtn.Name == "BtnInvesteringMine")
            {
                ToolTipService.SetToolTip(hoverBtn, "+" + 47
                   + " Cookies per seconde met deze aankoop");
            }
            if (hoverBtn.Name == "BtnInvesteringFactory")
            {
                ToolTipService.SetToolTip(hoverBtn, "+" + 260 
                    + " Cookies per seconde met deze aankoop");
            }
        }
    }
}
