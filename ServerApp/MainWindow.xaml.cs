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
using System.Windows.Media.Animation;

namespace ServerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       // Frame frame = new Frame();
        pg_Devices devices;
        pg_home home;
        List<Page> pages;
        public MainWindow()
        {
            InitializeComponent();
           devices = new pg_Devices();
           home = new pg_home();
            pages = new List<Page>();

            //TODO: ADD PAGE TAGGS THEN ADD THEM TO LIST, SEE IF YOU CAN'T KEEP TRACK OF PAGE AND BUTTON WITH THE SAME TAGS.
            MainFrame.Navigate(home);
            //btn_Home.IsEnabled = false;

        }
        private void btnLeftMenuHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbHideLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
        }

       

        private void btnLeftMenuShow_Click(object sender, RoutedEventArgs e)
        {
            ShowHideMenu("sbShowLeftMenu", btnLeftMenuHide, btnLeftMenuShow, pnlLeftMenu);
        }

        private void ShowHideMenu(string Storyboard, Button btnHide, Button btnShow, StackPanel pnl)
        {
            Storyboard sb = Resources[Storyboard] as Storyboard;
            sb.Begin(pnl);

            if (Storyboard.Contains("Show"))
            {
                btnHide.Visibility = System.Windows.Visibility.Visible;
                btnShow.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (Storyboard.Contains("Hide"))
            {
                btnHide.Visibility = System.Windows.Visibility.Hidden;
                btnShow.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void navTo(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            MainFrame.Navigate(button.Tag);
        }
        private void Btn_Devices_Click(object sender, RoutedEventArgs e)
        {
           MainFrame.Navigate()
            MainFrame.Navigate(devices);
            

        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
