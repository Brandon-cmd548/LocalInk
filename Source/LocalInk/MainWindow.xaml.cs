using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace LocalInk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtOutput.Text.Length == 0)
                {
                    MessageBox.Show("Nothing to copy :)");
                    return;
                }

                Clipboard.SetText(txtOutput.Text);

                lblConfirmCopy.Content = "Copied!";
                await Task.Delay(1500);
                lblConfirmCopy.Content = "";
            }
            catch
            {
                MessageBox.Show("Fatal error occured at btnCopy_Click");
            }
        }

        private void btnRegenerate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("This works");
            }catch
            {
                MessageBox.Show("Fatal error occured at btnRegenerate_Click");
            }
        }
    }
}
