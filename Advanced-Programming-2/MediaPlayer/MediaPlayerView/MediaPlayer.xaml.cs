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
using System.Threading;

namespace Advanced_Programming_2.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class MediaPlayer : UserControl
    {
           
        double speed;
        public MediaPlayer()
        {
            InitializeComponent();
            speed = 1;

        }

        private void V_ForwardButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void V_BackwardButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void PlayPauseButton_Click(object sender, RoutedEventArgs e)
        {


        }
    

        private void V_StopButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
