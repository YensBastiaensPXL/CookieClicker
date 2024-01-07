using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Policy;
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
using Microsoft.VisualBasic.Devices;

namespace CookieClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double aantalCookies = 99999; //aantal nog aanpassen bij upload
        double totaalAantalVerdiendeCookies = 99999; //aantal nog aanpassen bij upload
        
        //Variabelen cookie afbeelding vergroting/verkleining
        double origineleAfbeeldingBreedte;
        bool isMouseDown = false;
        bool isMuisOverAfbeelding = false;
        int aantalCookieAfbeeldingGeklikt = 0;
        //Passieve cookie timer
        DispatcherTimer passieveCookieTimer10ms;
        double passieveCookieRatio10ms = 0;
        double aantalPerSecondeAfgerond;
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
        DispatcherTimer minuutTimerGoudenCookie;
        Random randomGoudenCookieGetal = new Random();
        Random randomCookiePositie = new Random();
        double CookieBonus15Min;
        //Quests variabelen
        bool isQuest1Voltooid = false;
        bool isQuest2Voltooid = false;
        bool isQuest3Voltooid = false;
        bool isQuest4Voltooid = false;
        bool isQuest5Voltooid = false;
        bool isQuest6Voltooid = false;
        bool isQuest7Voltooid = false;
        bool isQuest8Voltooid = false;
        bool isQuest9Voltooid = false;
        bool isQuest10Voltooid = false;
        bool isQuest11Voltooid = false;
        bool isQuest12Voltooid = false;
        bool isQuest13Voltooid = false;
        bool isQuest14Voltooid = false;
        bool isQuest15Voltooid = false;
        bool isQuest16Voltooid = false;
        bool isQuest17Voltooid = false;
        bool isQuest18Voltooid = false;
        bool isQuest19Voltooid = false;
        bool isQuest20Voltooid = false;
        bool isGoudenCookieGeklikt = false;
        public MainWindow()
        {
            InitializeComponent();
            UpdateCookies();
            OpstartBtnEnabled();
            origineleAfbeeldingBreedte = klikebareCookie.Width;
            //Method formatteert de getallen van de investeringen bij opstart van applicatie
            PrijsFarm.Content = FormatteerNummer(basisPrijsFarm);
            PrijsMine.Content = FormatteerNummer(basisPrijsMine);
            PrijsFactory.Content = FormatteerNummer(basisPrijsFactory);
            PrijsBank.Content = FormatteerNummer(basisPrijsBank);
            PrijsTemple.Content = FormatteerNummer(basisPrijsTemple);

            //Passieve cookies per 10ms voor passieve inkomst van investeringen
            passieveCookieTimer10ms = new DispatcherTimer();
            passieveCookieTimer10ms.Interval = TimeSpan.FromMilliseconds(10);
            //Gouden Cookie
            minuutTimerGoudenCookie = new DispatcherTimer();
            minuutTimerGoudenCookie.Tick += MinuutTimer_Tick;
            minuutTimerGoudenCookie.Interval = TimeSpan.FromMinutes(1);
            minuutTimerGoudenCookie.Interval = TimeSpan.FromSeconds(5);
            minuutTimerGoudenCookie.Start();
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
                    ShowQuestNotificaties();
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
            ShowQuestNotificatiesInvesteringen();
        }
        private void UpdateCookies()
        {
            
            aantalCookiesTxt.Content = FormatteerNummer(aantalCookies);
            double aantalCookiesAfgerond = Math.Floor(aantalCookies);
            Title = $"Cookie clicker got {aantalCookiesAfgerond} cookies";
            ShowQuestNotificaties();
            aantalPerSecondeAfgerond = Math.Round(passieveCookieRatio10ms * 100, 3);
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

        private void ShowQuestNotificaties()
        {

            if (aantalPerSecondeAfgerond >= 10 && !isQuest1Voltooid)
            {
                CreatieNotificatie("10 cookies per seconde bereikt!", "Je bakkerij is een succes," +
                    " maar je kunt altijd meer produceren. Ga ervoor!");
                isQuest1Voltooid = true;
            }

            if (aantalPerSecondeAfgerond >= 100 && !isQuest2Voltooid)
            {
                CreatieNotificatie("100 cookies per seconde bereikt!", "Je bent een bekende figuur dankzij " +
                    "je opvallende cookievelden, die regelmatig de krantenkoppen halen." +
                    "Op je akkers cultiveer je met trots en vakmanschap verschillende heerlijke cookies. ");
                isQuest2Voltooid = true;
            }
            if (aantalPerSecondeAfgerond >= 500 && !isQuest12Voltooid)
            {
                CreatieNotificatie("500 cookies per seconde bereikt!", "Je winkel begint op gang te komen en je " +
                    "trekt volk van heel het dorp naar je bakkerij.");
                isQuest12Voltooid = true;
            }

            if (aantalPerSecondeAfgerond >= 1000 && !isQuest3Voltooid)
            {
                CreatieNotificatie("1000 cookies per seconde bereikt!", "Je cookie velden zijn berucht. " +
                    "Je verschijnt dagelijks in de krant over je befaamde cookie planten die je in je akkers teelt.");
                isQuest3Voltooid = true;
            }

            if (aantalPerSecondeAfgerond >= 100000 && !isQuest3Voltooid)
            {
                CreatieNotificatie("100.000 cookies per seconde bereikt!", "Je staat bekend om je opmerkelijke cookievelden, " +
                    "die regelmatig in de krant worden vermeld. Deze beroemde velden van jou bevinden zich op je akkers, " +
                    "waar je met trots en vaardigheid diverse soorten heerlijke cookies teelt.");
                isQuest3Voltooid = true;
            }

            if (totaalAantalVerdiendeCookies >= 100 && !isQuest4Voltooid)
            {
                CreatieNotificatie("100 Cookies in totaal gemaakt!", "Met behendigheid en inzet worden cookies verdiend," +
                    "De cookievelden blijven een betoverende mix van vakmanschap en smaak, een bron van genot en bewondering.");
                isQuest4Voltooid = true;
            }
            if (totaalAantalVerdiendeCookies >= 1000 && !isQuest5Voltooid)
            {
                CreatieNotificatie("1.000 Cookies in totaal gemaakt!", "Met behendigheid en inzet worden cookies verdiend," +
                    " De velden waarop deze lekkernijen gedijen, blijven een fascinerende samensmelting van vakmanschap en smaak," +
                    " een continue bron van vreugde en bewondering.");
                isQuest5Voltooid = true;
            }
            if (totaalAantalVerdiendeCookies >= 10000 && !isQuest6Voltooid)
            {
                CreatieNotificatie("10.000 Cookies in totaal gemaakt!", "Door bedrevenheid en toewijding te tonen, " +
                    "vergaar ik mijn voorraad aan cookies. De velden waarop deze verrukkingen gedijen, " +
                    "blijven een betoverende synergie van vakmanschap en smaak, " +
                    "een voortdurende bron van genoegen en bewondering.");
                isQuest6Voltooid = true;
            }
            if (totaalAantalVerdiendeCookies >= 100000 && !isQuest7Voltooid)
            {
                CreatieNotificatie("100.000 Cookies in totaal gemaakt!", "Gisteravond heb je je keuken omgetoverd" +
                    " tot een culinair paradijs, waar je met toewijding en precisie een overvloed aan heerlijke " +
                    "chocoladekoekjes hebt gebakken. De geur van versgebakken lekkernijen vulde je huis," +
                    " en de knapperige textuur en rijke smaak maakten je inspanningen meer dan de moeite waard.");
                isQuest7Voltooid = true;
            }
            if (totaalAantalVerdiendeCookies >= 1000000 && !isQuest8Voltooid)
            {
                CreatieNotificatie("1.000.000 Cookies in totaal gemaakt", "Vanmiddag stond je keuken in het teken " +
                    "van creativiteit en zoete verleiding. Met een scala aan ingrediënten heb je een batch verrukkelijke " +
                    "koekjes gemaakt. De subtiele mix van basis ingrediënten resulteerde in een smaakvolle traktatie die " +
                    "je smaakpapillen deed dansen.");
                isQuest8Voltooid = true;
            }
            if (totaalAantalVerdiendeCookies >= 1000000000 && !isQuest9Voltooid)
            {
                CreatieNotificatie("1.000.000.000 Cookies in totaal gemaakt!", "Deze middag was een ware zoete symfonie," +
                    " waar je een indrukwekkende hoeveelheid koekjes hebt gebakken. De keuken was gevuld met het geluid" +
                    " van knisperend deeg en het aroma van verschillende smaken die samensmolten tot een verrukkelijke" +
                    " harmonie. Jouw overvloed aan koekjes is niet alleen een feest voor de smaakpapillen maar ook een" +
                    " visueel festijn.");
                isQuest9Voltooid = true;
            }
            if (aantalCookieAfbeeldingGeklikt >= 50 && !isQuest10Voltooid)
            {
                CreatieNotificatie("50 klikkende Cookies!", "Je hebt 25 cookies gemaakt door erop te klikken.");
                isQuest10Voltooid = true;
            }
            if (isGoudenCookieGeklikt && !isQuest11Voltooid)
            {
                CreatieNotificatie("Gouden Cookie gevonden!", $"Je hebt {CookieBonus15Min}(15 min. productie) aan cookies gekregen.");
                isQuest11Voltooid = true;
            }

        }

        //Aparte method gemaakt voor de quests rond investeringen sinds de ander gebaseerd zijn op puur cookies.
        //Als de investeringen ook in UpdateCookies() opgeroepen worden krijg je 2x een messagebox.
        // Het in een ander method steken en deze in UpdateInvestering() oproepen verhelpt het probleem
        private void ShowQuestNotificatiesInvesteringen()
        {
            if (aantalInvesteringCursor >= 20 && !isQuest14Voltooid)
            {
                CreatieNotificatie("Megaveel Cursors!", "Je hebt 20 Cursors gekocht.");
                isQuest14Voltooid = true;
            }
            if (aantalInvesteringGrandma >= 25 && !isQuest15Voltooid)
            {
                CreatieNotificatie("Grandma's gaan los!", "Je hebt 25 Grandmas gekocht om koekjes te bakken.");
                isQuest15Voltooid = true;
            }
            if (aantalInvesteringFarm >= 20 && !isQuest16Voltooid)
            {
                CreatieNotificatie("Farms in overvloed!", "Je hebt 20 farms gekocht om koekjes te bakken.");
                isQuest16Voltooid = true;
            }
            if (aantalInvesteringMine >= 15 && !isQuest17Voltooid)
            {
                CreatieNotificatie("Overal mines!", "Je hebt 15 mines gekocht om koekjes te bakken.");
                isQuest17Voltooid = true;
            }
            if (aantalInvesteringFactory >= 10 && !isQuest18Voltooid)
            {
                CreatieNotificatie("Zoveel industrie!", "Je hebt 10 factories gekocht om koekjes te bakken.");
                isQuest18Voltooid = true;
            }
            if (aantalInvesteringBank >= 5 && !isQuest19Voltooid)
            {
                CreatieNotificatie("Banken dynastie", "Je hebt 6 banken gekocht om koekjes te bakken.");
                isQuest19Voltooid = true;
            }
            if (aantalInvesteringTemple >= 3 && !isQuest20Voltooid)
            {
                CreatieNotificatie("Temples", "Je hebt 3 temples gekocht om koekjes te bakken.");
                isQuest20Voltooid = true;
            }
        }

        private void CreatieNotificatie(string titel, string tekst)
        {
            const string NotificationHexColor = "#FF00FF";
            Color backgroundColor = (Color)ColorConverter.ConvertFromString(NotificationHexColor);
            
            Window customNotificatie = new Window
            {
                Title = titel,
                Background = new SolidColorBrush(backgroundColor),
                Width = 300,
                Height = 200,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                
            };
            
            Grid grid = new Grid();
            
            TextBlock bericht = new TextBlock
            {
                Text = tekst,
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(20),
            };
            
            grid.Children.Add(bericht);

            customNotificatie.Content = grid;
            if (Application.Current.MainWindow != null && Application.Current.MainWindow.IsLoaded)
            {
                customNotificatie.Owner = Application.Current.MainWindow;
            }
            customNotificatie.ShowDialog();
            

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
        private void MinuutTimer_Tick(object sender, EventArgs e)
        {
            int randomNummer = randomGoudenCookieGetal.Next(100);

            if (randomNummer <= 30)
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

            double randomX = randomCookiePositie.NextDouble() * (GoudenCookieScherm.ActualWidth - goudenCookie.Width);
            double randomY = randomCookiePositie.NextDouble() * (GoudenCookieScherm.ActualHeight - goudenCookie.Height);


            goudenCookie.MouseLeftButtonDown += goudenCookie_MouseLeftButtonDown;

            Canvas.SetLeft(goudenCookie, randomX);
            Canvas.SetTop(goudenCookie, randomY);

            GoudenCookieScherm.Children.Add(goudenCookie);
        }

        private void goudenCookie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isGoudenCookieGeklikt = true;
            // Verwijdert de afbeelding bij het klikken
            Image clickedImage = sender as Image;
            GoudenCookieScherm.Children.Remove(clickedImage);

            //Haalt de huidige inkomsten per 10ms op en zet dit om naar 15min waard aan inkomsten
            CookieBonus15Min = passieveCookieRatio10ms * 100 * 6 * 15;
            aantalCookies += CookieBonus15Min;
            totaalAantalVerdiendeCookies += CookieBonus15Min;
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
            aantalCookieAfbeeldingGeklikt++;
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
