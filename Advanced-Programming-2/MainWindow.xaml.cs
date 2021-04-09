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
using Advanced_Programming_2.Model;
using Advanced_Programming_2.ViewModel;
using Advanced_Programming_2.Controls;
 

namespace Advanced_Programming_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IFlightAnalysisModel model;
        public MainWindow()
        {
            InitializeComponent();
            model = new FlightAnalysisModel();
            FlightFilesVM flightFilesVM = new FlightFilesVM(model);
            FlightFiles flightFilesView = new FlightFiles();
            FlightFilesControl.setViewModel(flightFilesVM);
            MediaPlayerVM mediaPlayerVM = new MediaPlayerVM(model);
            Controls.MediaPlayer MediaPlayerView = new Controls.MediaPlayer();
            MediaPlayerControl1.setViewModel(mediaPlayerVM);
            MediaPlayerControl2.setViewModel(mediaPlayerVM);
            DataControlVM dataControlVM = new DataControlVM(model);
            DataControl dataControlView = new DataControl();
            DataControl1.setViewModel(dataControlVM);
            JoystickVM joystickVM = new JoystickVM(model);
            Joystick joystick = new Joystick();
            JoystickControl.setViewModel(joystickVM);
            GraphsVM graphsVM = new GraphsVM(model);
            Graphs graphs = new Graphs();
            GraphsControl.setViewModel(graphsVM);
        }

        private void AcceptFiles_Click(object sender, RoutedEventArgs e)
        {
            if (FlightFilesControl.AcceptFlightFiles())
            {
                V_TabControl.SelectedIndex = 1;
            }
            else
            {
                MessageBox.Show("You must load both files.", "Error!");
            }
                
        }

        private void Welcome_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
