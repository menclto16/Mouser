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
        private FileHandler fileHandler = new FileHandler();
        private APIHandler apiHandler = new APIHandler();
        private List<MouseProfile> profiles = new List<MouseProfile>();
        private bool initDone = false;

        public MainWindow()
        {
            InitializeComponent();

            this.Topmost = true;

            ApplicationToken.Content = "apptoken: " + fileHandler.GetToken();
            
            Task.Run(() => apiHandler.GetDataFromDB()).Wait();

            if (fileHandler.ReadFile() == null | fileHandler.ReadFile().Count == 0)
            {
                MouseProfile mouseProfile = new MouseProfile();
                mouseProfile.Name = "Initial Profile";
                mouseProfile.Sensitivity = optionHandler.GetSensitivity();
                mouseProfile.MousePrecision = optionHandler.GetMousePrecision();
                mouseProfile.DoubleClickSpeed = optionHandler.GetDoubleClick();
                mouseProfile.ScrollSpeed = optionHandler.GetScrollSpeed();
                profiles.Add(mouseProfile);
                ProfilesCombobox.Items.Add(profiles[0]);
            }
            else
            {
                profiles = fileHandler.ReadFile();
                fillCombobox();
            }

            ProfilesCombobox.SelectedIndex = 0;
            updateControls();

            initDone = true;
        }
        private void updateControls()
        {
            int selectedProfile = ProfilesCombobox.SelectedIndex;
            SensitivitySlider.Value = profiles[selectedProfile].Sensitivity;
            MousePrecisionCheckbox.IsChecked = profiles[selectedProfile].MousePrecision;
            ClickSpeedSlider.Value = profiles[selectedProfile].DoubleClickSpeed;
            ScrollSpeedSlider.Value = profiles[selectedProfile].ScrollSpeed;
        }
        private void fillCombobox()
        {
            ProfilesCombobox.Items.Clear();

            foreach (var profile in profiles)
            {
                ProfilesCombobox.Items.Add(profile);
            }
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
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (profiles.Find(x => x.Name.Contains(NewProfileTextbox.Text)) == null)
            {
                MouseProfile mouseProfile = new MouseProfile();
                mouseProfile.Name = NewProfileTextbox.Text;
                mouseProfile.Sensitivity = (int)SensitivitySlider.Value;
                mouseProfile.MousePrecision = (bool)MousePrecisionCheckbox.IsChecked;
                mouseProfile.DoubleClickSpeed = (int)ClickSpeedSlider.Value;
                mouseProfile.ScrollSpeed = (int)ScrollSpeedSlider.Value;
                profiles.Add(mouseProfile);

                fillCombobox();

                NewProfileTextbox.Text = "";
                ProfilesCombobox.SelectedIndex = ProfilesCombobox.Items.Count - 1;
            }
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = ProfilesCombobox.SelectedIndex;
            profiles[selectedIndex].Sensitivity = (int)SensitivitySlider.Value;
            profiles[selectedIndex].MousePrecision = (bool)MousePrecisionCheckbox.IsChecked;
            profiles[selectedIndex].DoubleClickSpeed = (int)ClickSpeedSlider.Value;
            profiles[selectedIndex].ScrollSpeed = (int)ScrollSpeedSlider.Value;

            fileHandler.SaveFile(profiles);
            apiHandler.SaveDataToDB();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProfilesCombobox.SelectedIndex != -1)
            updateControls();
        }
    }
}
