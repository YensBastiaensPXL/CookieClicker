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
        double aantalCookies = 100999999; //aantal nog aanpassen bij upload
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
        const int basisPrijsBank = 1400000;
        const int basisPrijsTemple = 20000000;
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
        double kostCounterBank;
        double huidigeAankoopPrijsBank;
        long aantalInvesteringBank = 0;
        double kostCounterTemple;
        double huidigeAankoopPrijsTemple;
        long aantalInvesteringTemple = 0;
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
            BtnInvesteringBank.IsEnabled = aantalCookies >= basisPrijsBank;
            BtnInvesteringTemple.IsEnabled = aantalCookies >= basisPrijsTemple;
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
                    aantalCookies -= basisPrijsCursor;
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
            else if (geklikteKnop.Name == "BtnInvesteringGrandma")
            {
                if (aantalInvesteringGrandma > 0)
                {
                    aantalInvesteringGrandma++;
                    UpdateInvestering("grandma", ref kostCounterGrandma);
                }
                else
                {
                    aantalCookies -= basisPrijsGrandma;
                    kostCounterGrandma = basisPrijsGrandma * 1.15;
                    PrijsGrandma.Content = Math.Ceiling(kostCounterGrandma).ToString();
                    aantalInvesteringGrandma = 1;
                }
                GrandmaAantal.Content = aantalInvesteringGrandma.ToString();

                //Passive cookies counter
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 0.01;
                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                //weglaten of laten staan? bij allemaal buiten cursor moet timer sws
                passieveCookieTimer10ms.Start();

                CategorieënZichtbaar(GrandmaInvesteringCategorie);
            }
            else if (geklikteKnop.Name == "BtnInvesteringFarm")
            {
                if (aantalInvesteringFarm > 0)
                {
                    aantalInvesteringFarm++;
                    UpdateInvestering("farm", ref kostCounterFarm);

                }
                else
                {
                    aantalCookies -= basisPrijsFarm;
                    kostCounterFarm = basisPrijsFarm * 1.15;
                    PrijsFarm.Content = Math.Ceiling(kostCounterFarm).ToString();
                    aantalInvesteringFarm = 1;
                }

                FarmAantal.Content = aantalInvesteringFarm.ToString();

                //Passive cookies counter
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 0.08;

                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();

                CategorieënZichtbaar(FarmInvesteringCategorie);
            }
            else if (geklikteKnop.Name == "BtnInvesteringMine")
            {
                if (aantalInvesteringMine > 0)
                {
                    aantalInvesteringMine++;
                    UpdateInvestering("mine", ref kostCounterMine);


                }
                else
                {
                    aantalCookies -= basisPrijsMine;
                    kostCounterMine = basisPrijsMine * 1.15;
                    PrijsMine.Content = Math.Ceiling(kostCounterMine).ToString();
                    aantalInvesteringMine = 1;
                }

                MineAantal.Content = aantalInvesteringMine.ToString();

                //Passive cookies counter
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 0.47;
                ////Tooltip part of code
                //passieveCookieRatio1sMine = passieveCookieRatio10ms * 100;
                //upcomingPassiveCookiesMine = passieveCookieRatio1sMine + 47;

                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();
                CategorieënZichtbaar(MineInvesteringCategorie);
            }
            else if (geklikteKnop.Name == "BtnInvesteringFactory")
            {
                if (aantalInvesteringFactory > 0)
                {
                    aantalInvesteringFactory++;
                    UpdateInvestering("factory", ref kostCounterFactory);

                }
                else
                {
                    aantalCookies -= basisPrijsFactory;
                    kostCounterFactory = basisPrijsFactory * 1.15;
                    PrijsFactory.Content = Math.Ceiling(kostCounterFactory).ToString();
                    aantalInvesteringFactory = 1;
                }

                FactoryAantal.Content = aantalInvesteringFactory.ToString();

                //Passive cookies counter
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 2.60;

                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();
                CategorieënZichtbaar(FactoryInvesteringCategorie);

            }
            else if(geklikteKnop.Name == "BtnInvesteringBank")
            {
                if (aantalInvesteringBank > 0)
                {
                    aantalInvesteringBank++;
                    UpdateInvestering("bank", ref kostCounterBank);
                }
                else
                {
                    aantalCookies -= basisPrijsBank;
                    kostCounterBank = BerekenEersteKostPrijsInvestering(basisPrijsBank);
                    PrijsBank.Content = Math.Ceiling(kostCounterBank).ToString();
                    aantalInvesteringBank = 1;
                }
                BankAantal.Content = aantalInvesteringBank.ToString();
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 14;

                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();


                CategorieënZichtbaar(BankInvesteringCategorie);
            }
            else if (geklikteKnop.Name == "BtnInvesteringTemple")
            {
                if (aantalInvesteringTemple > 0)
                {
                    aantalInvesteringTemple++;
                    UpdateInvestering("temple", ref kostCounterTemple);
                }
                else
                {
                    aantalCookies -= basisPrijsTemple;
                    kostCounterTemple = BerekenEersteKostPrijsInvestering(basisPrijsTemple);
                    PrijsTemple.Content = Math.Ceiling(kostCounterTemple).ToString();
                    aantalInvesteringTemple = 1;
                }
                TempleAantal.Content = aantalInvesteringTemple.ToString();

                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 78;

                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();

                CategorieënZichtbaar(TempleInvesteringCategorie);
            }

            UpdateCookies();
        }

        private double BerekenEersteKostPrijsInvestering(double basisprijs)
        {
            return basisprijs * 1.15;
        }

        // anatomie / signatuur
        // accesmodifier -> returntype -> naam -> param list
        private double BerekenKostprijsInvestering(double basisprijs, double counter )
        {
            return basisprijs * Math.Pow(1.15, counter);
        }


        private void UpdateInvestering(string type, ref double kostCounter)
        {

            if (aantalCookies >= BerekenKostprijsInvestering(basisPrijsCursor, aantalInvesteringCursor - 1) && type.Equals("cursor"))
            {                
                kostCounterCursor = BerekenKostprijsInvestering(basisPrijsCursor, aantalInvesteringCursor);
                huidigeAankoopPrijsCursor = BerekenKostprijsInvestering(basisPrijsCursor, aantalInvesteringCursor - 1);
                PrijsCursor.Content = Math.Ceiling(kostCounterCursor).ToString();
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsCursor);

            }
            if (aantalCookies >= BerekenKostprijsInvestering(basisPrijsGrandma, aantalInvesteringGrandma - 1) && type.Equals("grandma"))
            {
                kostCounterGrandma = BerekenKostprijsInvestering(basisPrijsGrandma, aantalInvesteringGrandma);
                huidigeAankoopPrijsGrandma = BerekenKostprijsInvestering(basisPrijsGrandma, aantalInvesteringGrandma - 1);
                PrijsGrandma.Content = Math.Ceiling(kostCounterGrandma).ToString();
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsGrandma);
            }
            if (aantalCookies >= BerekenKostprijsInvestering(basisPrijsFarm, aantalInvesteringFarm - 1) && type.Equals("farm"))
            {
                kostCounterFarm = BerekenKostprijsInvestering(basisPrijsFarm, aantalInvesteringFarm);
                huidigeAankoopPrijsFarm = BerekenKostprijsInvestering(basisPrijsFarm, aantalInvesteringFarm - 1);
                PrijsFarm.Content = Math.Ceiling(kostCounterFarm).ToString();
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsFarm);
            }
            if (aantalCookies >= BerekenKostprijsInvestering(basisPrijsMine, aantalInvesteringMine - 1) && type.Equals("mine"))
            {
                kostCounterMine = BerekenKostprijsInvestering(basisPrijsMine, aantalInvesteringMine);
                huidigeAankoopPrijsMine = BerekenKostprijsInvestering(basisPrijsMine, aantalInvesteringMine - 1);
                PrijsMine.Content = Math.Ceiling(kostCounterMine).ToString();
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsMine);
            }
            if (aantalCookies >= BerekenKostprijsInvestering(basisPrijsFactory, aantalInvesteringFactory - 1) && type.Equals("factory"))
            {
                kostCounterFactory = BerekenKostprijsInvestering(basisPrijsFactory, aantalInvesteringFactory);
                huidigeAankoopPrijsFactory = BerekenKostprijsInvestering(basisPrijsFactory, aantalInvesteringFactory - 1);
                PrijsFactory.Content = Math.Ceiling(kostCounterFactory).ToString();
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsFactory);
            }
            if (aantalCookies >= BerekenKostprijsInvestering(basisPrijsBank, aantalInvesteringBank - 1) && type.Equals("bank"))
            {
                kostCounterBank = BerekenKostprijsInvestering(basisPrijsBank, aantalInvesteringBank);
                huidigeAankoopPrijsBank = BerekenKostprijsInvestering(basisPrijsBank, aantalInvesteringBank - 1);
                PrijsBank.Content = Math.Ceiling(kostCounterBank).ToString();
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsBank);
            }
            if (aantalCookies >= BerekenKostprijsInvestering(basisPrijsTemple, aantalInvesteringTemple - 1) && type.Equals("temple"))
            {
                kostCounterTemple = BerekenKostprijsInvestering(basisPrijsTemple, aantalInvesteringTemple);
                huidigeAankoopPrijsTemple = BerekenKostprijsInvestering(basisPrijsTemple, aantalInvesteringTemple - 1);
                PrijsTemple.Content = Math.Ceiling(kostCounterTemple).ToString();
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsTemple);
            }

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

            BtnInvesteringCursor.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsCursor, aantalInvesteringCursor);
            BtnInvesteringGrandma.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsGrandma, aantalInvesteringGrandma);
            BtnInvesteringFarm.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsFarm, aantalInvesteringFarm);
            BtnInvesteringMine.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsMine, aantalInvesteringMine);
            BtnInvesteringFactory.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsFactory, aantalInvesteringFactory);
            BtnInvesteringBank.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsBank, aantalInvesteringBank);
            BtnInvesteringTemple.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsTemple, aantalInvesteringTemple);


        }
        private void CategorieënZichtbaar(Grid investeringCategorieGrid)
        {
            investeringCategorieGrid.Visibility = Visibility.Visible;
        }
        private void PassieveCookieTimer10ms_Tick(object sender, EventArgs e)
        {
            double passieveCookiesPerTick = passieveCookieRatio10ms;
            aantalCookies += passieveCookiesPerTick;
            UpdateCookies();
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

        private void Tooltip(object sender, MouseEventArgs e)
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

        private void lblNaam_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lblNaam.Visibility = Visibility.Collapsed;
            txtBoxLblNaam.Visibility = Visibility.Visible;
            txtBoxLblNaam.Text = lblNaam.Content.ToString();
            txtBoxLblNaam.Focus();
        }

        private void txtBoxLblNaam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string lblNaamInput = txtBoxLblNaam.Text;
                if (!string.IsNullOrWhiteSpace(lblNaamInput) && !lblNaamInput.Contains(" "))
                {
                    lblNaam.Content = lblNaamInput;
                    lblNaam.Visibility = Visibility.Visible;
                    txtBoxLblNaam.Visibility = Visibility.Collapsed;
                }
                else
                {   
                    MessageBox.Show("Ongeldige Input");
                }
            }
        }
    }
}
