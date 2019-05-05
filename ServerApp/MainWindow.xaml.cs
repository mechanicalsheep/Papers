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
        Page currentPage;
        bool output=true;
        //List<string> output;
        public MainWindow()
        {
            InitializeComponent();
            //output = new List<string>();
           devices = new pg_Devices();
           home = new pg_home();
            pages = new List<Page>();

            home.Tag = "home";
            devices.Tag = "devices";

            pages.Add(home);
            pages.Add(devices);

            //TODO: ADD PAGE TAGS THEN ADD THEM TO LIST, SEE IF YOU CAN'T KEEP TRACK OF PAGE AND BUTTON WITH THE SAME TAGS.
            //currentPage = new pg_home();
            currentPage = new pg_Devices();
            currentPage.IsEnabled = false;
            MainFrame.Navigate(currentPage);
            //btn_Home.IsEnabled = false;

        }

        public void writeline(string message)
        {
            lb.Items.Add(message);
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
           // Page tempPage = pages.First(x => x.Tag == button.Tag);

               // writeline($"Button.Tag={button.Tag}");
            foreach(var page in pages)
            {
                //writeline($"page.tag= {page.Tag}");
               // writeline($"is {button.Tag} == {page.Tag} ?");
                if (page.Tag.Equals( button.Tag))
                {
                    currentPage.IsEnabled = true;
                    currentPage = page;
                    
                    MainFrame.Navigate(currentPage);
                    //label.Content = "Found it!";
                   //writeline("Found it!");
                }
            }
            //MainFrame.Navigate();
            //Console.WriteLine(tempPage.Tag);
           //label.Content=pages.Where(x=> x.Tag==button.Tag).ToString();

        }
        private void Btn_Devices_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void Btn_Home_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(home);
        }

        private void Btn_output_Click(object sender, RoutedEventArgs e)
        {
            output = !output;
            // lb.IsEnabled = output;
            if (output == false)
                lb.Visibility = Visibility.Hidden;
            else
                lb.Visibility = Visibility.Visible;
        }
    }
}
