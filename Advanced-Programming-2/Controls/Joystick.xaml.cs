using Advanced_Programming_2.ViewModel;
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

namespace Advanced_Programming_2.Controls
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        JoystickVM vm;
        public Joystick()
        { 
            InitializeComponent();
        }
        public void setViewModel(JoystickVM vm)
        {
            this.vm = vm;
            DataContext = vm;
        }
    }
}
