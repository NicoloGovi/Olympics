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
using Olympics.ViewModels;

namespace Olympics
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = new MainWindowViewModel();
            this.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm.nextPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm.previousPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            vm.LastPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            vm.FirstPage();
        }
    }
}
