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

namespace SunTaiLibrary.TestClient
{
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void ShowMessageToast_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageToast.Show("I'm message toast, time is " + DateTime.Now.ToString());
        }

        private void ToastControl_FlyOut(object sender, RoutedEventArgs e)
        {
            //var ele = sender as Control;
            //var ep = ele.Parent as Panel;
            //ep.Children.Remove(ele);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void AddToCorrectGrid_Click(object sender, RoutedEventArgs e)
        {
            CorrectGrid1.Children.Add(new Button { Content = "A" });
        }

        private void RemoveFromCorrectGrid_Click(object sender, RoutedEventArgs e)
        {
            if (CorrectGrid1.Children.Count > 0)
            {
                CorrectGrid1.Children.RemoveAt(0);
            }
        }
    }
}