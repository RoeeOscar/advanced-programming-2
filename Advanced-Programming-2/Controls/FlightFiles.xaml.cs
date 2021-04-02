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
using System.IO;
using Microsoft.Win32;
using Advanced_Programming_2.ViewModel;


namespace Advanced_Programming_2.Controls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FlightFiles : UserControl
    {
        FlightFilesVM vm;
        string CSVFileName, XMLFileName;
        string tempCSVFileName, tempXMLFileName;

        public FlightFiles()
        {
            InitializeComponent();
            vm = new FlightFilesVM();
            // Bind the new data source to the myText TextBlock control's Text dependency property.
        }

        // Open file dialog for the CSV File.
        private void V_OpenCSVFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (.csv)|*.csv";
            openFileDialog.InitialDirectory = "c:\\";

            if (openFileDialog.ShowDialog() == true)
            {
                    tempCSVFileName = openFileDialog.FileName;
                V_OpenCSVFileTextBox.Text = openFileDialog.SafeFileName;
            }
        }

        // Open file dialog for the XML File.
        private void V_OpenXMLFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (.xml)|*.xml";
            openFileDialog.InitialDirectory = "c:\\";

            if (openFileDialog.ShowDialog() == true)
            {
                tempXMLFileName = openFileDialog.FileName;
                V_OpenXMLFileTextBox.Text = openFileDialog.SafeFileName;
            }
        }

        // Accepting the files.
        public bool AcceptFlightFiles()
        {
            if ((tempCSVFileName == null) || (tempXMLFileName == null))
            {
                return false;
            }
            else
            {
                CSVFileName = tempCSVFileName;
                XMLFileName = tempXMLFileName;
                vm.VM_csvFileName = CSVFileName;
                vm.VM_xmlFileName = XMLFileName;
                return true;
            }
        }
    }
}
