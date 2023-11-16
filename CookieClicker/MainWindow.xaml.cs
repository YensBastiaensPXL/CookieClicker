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
        long aantalCookies = 0;
        DispatcherTimer passieveCookiesGenerated;
        public MainWindow()
        {
            InitializeComponent();
            UpdateCookies();
            
            
        }
        private void BtnClickCookie(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            aantalCookies++;
            UpdateCookies();
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
        private void UpdateCookies()
        {
            aantalCookiesTxt.Content = $"{aantalCookies} cookies";
        }
    }
}
