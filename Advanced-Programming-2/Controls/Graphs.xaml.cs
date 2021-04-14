using Advanced_Programming_2.ViewModel;
using Microsoft.Win32;
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
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : UserControl
    {
        GraphsVM vm;
        public Graphs()
        {
            InitializeComponent();
        }
        public void setViewModel(GraphsVM vm)
        {
            this.vm = vm;
            DataContext = vm;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AnomaliesList.SelectedItem = -1;
            vm.changeGraph(listBox.SelectedItem.ToString());
        }

        private void V_LoadDLL_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "DLL Files (.dll)|*.dll";
            openFileDialog.InitialDirectory = "c:\\";

            if (openFileDialog.ShowDialog() == true)
            {
                vm.loadDLL(openFileDialog.FileName);
            }
        }

        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (AnomaliesList.SelectedItem != null)
            {
                vm.changeCurrentTime((int)AnomaliesList.SelectedItem / 10);
            }
        }
    }
}
