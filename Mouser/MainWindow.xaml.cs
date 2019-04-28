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
        private bool initDone = false;

        public MainWindow()
        {
            InitializeComponent();
            SensitivitySlider.Value = optionHandler.GetSensitivity();
            MousePrecisionCheckbox.IsChecked = optionHandler.GetMousePrecision();
            ClickSpeedSlider.Value = optionHandler.GetDoubleClick();
            ScrollSpeedSlider.Value = optionHandler.GetScrollSpeed();
            initDone = true;
        }
        private void ChangeMouseSensitivity(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initDone)
            optionHandler.ChangeMouseSensitivity(e.NewValue);
        }
        private void ToggleMousePrecision(object sender, RoutedEventArgs e)
        {
            if (initDone)
            optionHandler.ChangeMousePrecision((bool)MousePrecisionCheckbox.IsChecked);
        }
        private void ChangeMouseClickSpeed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initDone)
            optionHandler.ChangeMouseClickSpeed(e.NewValue);
        }
        private void ChangeScrollSpeed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (initDone)
            optionHandler.ChangeScrollSpeed(e.NewValue);
        }
    }
}
