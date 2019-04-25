using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using MahApps.Metro.Controls;

namespace Mouser
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private OptionHandler optionHandler = new OptionHandler();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ChangeMouseSensitivity(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            optionHandler.ChangeMouseSensitivity(Convert.ToUInt32(e.NewValue));
        }
        private void ChangeMouseClickSpeed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            optionHandler.ChangeMouseClickSpeed(Convert.ToUInt32(e.NewValue));
        }
        private void ChangeScrollSpeed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            optionHandler.ChangeScrollSpeed(Convert.ToUInt32(e.NewValue));
        }
    }
}
