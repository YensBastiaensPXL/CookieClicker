using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace CookieClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double aantalCookies = 15; //aantal nog aanpassen bij upload
        double totaalAantalVerdiendeCookies = 15; //aantal nog aanpassen bij upload
        //Variabelen cookie afbeelding vergroting/verkleining
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
        // Gouden cookie variabelen
        DispatcherTimer minuutTimer;
        Random randomGoudenCookieGetal = new Random();
        Random randomCookiePositie = new Random();
        public MainWindow()
        {
            InitializeComponent();
            UpdateCookies();
            OpstartBtnEnabled();
            origineleAfbeeldingBreedte = klikebareCookie.Width;
            //Method Formatteernummer formatteert de getallen bij opstart van applicatie
            PrijsFarm.Content = FormatteerNummer(basisPrijsFarm);
            PrijsMine.Content = FormatteerNummer(basisPrijsMine);
            PrijsFactory.Content = FormatteerNummer(basisPrijsFactory);
            PrijsBank.Content = FormatteerNummer(basisPrijsBank);
            PrijsTemple.Content = FormatteerNummer(basisPrijsTemple);

            //Passieve cookies per 10ms voor passieve inkomst van investeringen
            passieveCookieTimer10ms = new DispatcherTimer();
            passieveCookieTimer10ms.Interval = TimeSpan.FromMilliseconds(10);
            //Gouden Cookie
            minuutTimer = new DispatcherTimer();
            minuutTimer.Tick += MinuutTimer_Tick;
            //minuteTimer.Interval = TimeSpan.FromMinutes(1);
            minuutTimer.Interval = TimeSpan.FromSeconds(3);
            minuutTimer.Start();


        }
        private void MinuutTimer_Tick(object sender, EventArgs e)
        {
            // Genereren van een willekeurig getal tussen 0 en 99
            int randomNumber = randomGoudenCookieGetal.Next(100);

            // Als het willekeurige getal kleiner is dan of gelijk aan 30 is het binnen de 30% kans
            if (randomNumber <= 100)
            {    
                ShowGoudenCookie();
            }
        }
        private void ShowGoudenCookie()
        {
            
            Image goudenCookie = new Image();
            goudenCookie.Source = new BitmapImage(new Uri("/goudencookie.png", UriKind.RelativeOrAbsolute));

            goudenCookie.Width = 40;
            goudenCookie.Height = 40;

            double randomX = randomCookiePositie.NextDouble() * (HoofdGrid.ActualWidth - goudenCookie.Width);
            double randomY = randomCookiePositie.NextDouble() * (HoofdGrid.ActualHeight - goudenCookie.Height);


            goudenCookie.MouseLeftButtonDown += goudenCookie_MouseLeftButtonDown;

            Canvas.SetLeft(goudenCookie, randomX);
            Canvas.SetTop(goudenCookie, randomY);

            HoofdGrid.Children.Add(goudenCookie);
        }
       
    private void goudenCookie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Verwijder de afbeelding bij het klikken
            Image clickedImage = sender as Image;
            HoofdGrid.Children.Remove(clickedImage);

            double CookieBonus15Min = passieveCookieRatio10ms * 100 * 6 * 15;
            aantalCookies += CookieBonus15Min;
            totaalAantalVerdiendeCookies += CookieBonus15Min;
        }
        private void OpstartBtnEnabled()
        {
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

                PrijsCursor.Content = FormatteerNummer(kostCounterCursor);
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

                //Passieve cookie counter
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 0.01;
                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();

                PrijsGrandma.Content = FormatteerNummer(Math.Ceiling(kostCounterGrandma));
                CategorieënZichtbaar(GrandmaInvesteringCategorie);
                VoegAfbeeldingToeInvestering(GrandmaInvesteringCategorie, "/grandma.png", 40, 40, -10);
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

                PrijsFarm.Content = FormatteerNummer(kostCounterFarm);
                CategorieënZichtbaar(FarmInvesteringCategorie);
                VoegAfbeeldingToeInvestering(FarmInvesteringCategorie, "/farm.png", 40, 40, 0);
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

                //Passieve cookie counter
                passieveCookieTimer10ms.Tick -= PassieveCookieTimer10ms_Tick;
                passieveCookieRatio10ms += 0.47;
                passieveCookieTimer10ms.Tick += PassieveCookieTimer10ms_Tick;
                passieveCookieTimer10ms.Start();

                PrijsMine.Content = FormatteerNummer(kostCounterMine);
                CategorieënZichtbaar(MineInvesteringCategorie);
                VoegAfbeeldingToeInvestering(MineInvesteringCategorie, "/mine.png", 40, 40, 3);
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

                PrijsFactory.Content = FormatteerNummer(kostCounterFactory);
                CategorieënZichtbaar(FactoryInvesteringCategorie);
                VoegAfbeeldingToeInvestering(FactoryInvesteringCategorie, "/factory.png", 40, 40, 5);

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

                PrijsBank.Content = FormatteerNummer(kostCounterBank);
                CategorieënZichtbaar(BankInvesteringCategorie);
                VoegAfbeeldingToeInvestering(BankInvesteringCategorie, "/bank.png", 40, 40, 3);
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

                PrijsTemple.Content = FormatteerNummer(kostCounterTemple);
                CategorieënZichtbaar(TempleInvesteringCategorie);
                VoegAfbeeldingToeInvestering(TempleInvesteringCategorie, "/temple.png", 40, 40, 0);
            }

            UpdateCookies();
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
                //PrijsTemple.Content = FormatteerNummer(Math.Ceiling(kostCounterTemple));
                aantalCookies -= Math.Ceiling(huidigeAankoopPrijsTemple);
            }
        }
        private void UpdateCookies()
        {
            aantalCookiesTxt.Content = FormatteerNummer(aantalCookies);
            double aantalCookiesAfgerond = Math.Floor(aantalCookies);
            Title = $"Cookie clicker got {aantalCookiesAfgerond} cookies";
            double aantalPerSecondeAfgerond = Math.Round(passieveCookieRatio10ms * 100, 3);
            aantalPerSeconde.Content = $"{aantalPerSecondeAfgerond} per seconde";

            ZichtbaarheidInvesteringen(BtnInvesteringCursor, 15);
            ZichtbaarheidInvesteringen(BtnInvesteringGrandma, 100);
            ZichtbaarheidInvesteringen(BtnInvesteringFarm, 1100);
            ZichtbaarheidInvesteringen(BtnInvesteringMine, 12000);
            ZichtbaarheidInvesteringen(BtnInvesteringFactory, 130000);
            ZichtbaarheidInvesteringen(BtnInvesteringBank, 1400000);
            ZichtbaarheidInvesteringen(BtnInvesteringTemple, 20000000);
            BtnInvesteringCursor.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsCursor, aantalInvesteringCursor);
            BtnInvesteringGrandma.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsGrandma, aantalInvesteringGrandma);
            BtnInvesteringFarm.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsFarm, aantalInvesteringFarm);
            BtnInvesteringMine.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsMine, aantalInvesteringMine);
            BtnInvesteringFactory.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsFactory, aantalInvesteringFactory);
            BtnInvesteringBank.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsBank, aantalInvesteringBank);
            BtnInvesteringTemple.IsEnabled = aantalCookies >= BerekenKostprijsInvestering(basisPrijsTemple, aantalInvesteringTemple);
        }
        private void VoegAfbeeldingToeInvestering(Grid grid, string urlAfbeelding, double widht, double height, double spacing)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(urlAfbeelding, UriKind.RelativeOrAbsolute));

            image.Height = height;
            image.Width = widht;

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(widht + spacing) });
            Grid.SetColumn(image, grid.ColumnDefinitions.Count - 1);
            grid.Children.Add(image);
        }

        private double BerekenEersteKostPrijsInvestering(double basisprijs)
        {
            return basisprijs * 1.15;
        }

        private double BerekenKostprijsInvestering(double basisprijs, double counter)
        {
            return basisprijs * Math.Pow(1.15, counter);
        }
        private void ZichtbaarheidInvesteringen(Grid btnInvestering, int cookies)
        {
            if (totaalAantalVerdiendeCookies >= cookies)
            {
                btnInvestering.Visibility = Visibility.Visible;
            }
        }
        private void CategorieënZichtbaar(Grid investeringCategorieGrid)
        {
            investeringCategorieGrid.Visibility = Visibility.Visible;
        }
        
        private void PassieveCookieTimer10ms_Tick(object sender, EventArgs e)
        {
            double passieveCookiesPerTick = passieveCookieRatio10ms;
            aantalCookies += passieveCookiesPerTick;
            totaalAantalVerdiendeCookies += passieveCookiesPerTick;
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
            totaalAantalVerdiendeCookies++;
            klikebareCookie.Width = klikebareCookie.ActualWidth * 0.9;
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
                klikebareCookie.Width = double.NaN;
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
            if (hoverBtn.Name == "BtnInvesteringBank")
            {
                ToolTipService.SetToolTip(hoverBtn, "+" + 1400
                   + " Cookies per seconde met deze aankoop");
            }
            if (hoverBtn.Name == "BtnInvesteringTemple")
            {
                ToolTipService.SetToolTip(hoverBtn, "+" + 7800
                   + " Cookies per seconde met deze aankoop");
            }
        }
        private void lblNaam_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string naam = lblNaam.Content.ToString();

            while (true)
            {
                naam = Interaction.InputBox("Wijzig de naam van de bakkerij", "Naam", naam);

                if (naam == "")
                {
                    break;
                }
                if (!string.IsNullOrWhiteSpace(naam) && !naam.Contains(" "))
                {
                    lblNaam.Content = naam;
                    break;
                }
                else
                {
                    MessageBox.Show("Ongeldige input. Zorg ervoor dat de invoer niet leeg is en geen spaties bevat.");
                }
            }
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var scroll = sender as ScrollViewer;

            if (scroll != null)
            {
                if (e.Delta > 0)
                {
                    scroll.LineLeft();
                }
                else
                {
                    scroll.LineRight();
                }
            }
        }
    }
}
