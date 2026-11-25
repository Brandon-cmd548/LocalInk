using LocalInk.Services;
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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace LocalInk
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
        }

        private const int HOTKEY_ID = 9000;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private HwndSource _source;

        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_SHIFT = 0x0004;

        private const uint VK_R = 0x52;

        private RewriteService rewriteService = new RewriteService();

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var windowHandle = new WindowInteropHelper(this).Handle;

            _source = HwndSource.FromHwnd(windowHandle);

            _source?.AddHook(HwndHook);

            RegisterHotKey(windowHandle, HOTKEY_ID, MOD_CONTROL | MOD_SHIFT,VK_R);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            if (msg == WM_HOTKEY)
            {
                int id = wParam.ToInt32();
                if (id == HOTKEY_ID)
                {
                    string clipboardText = Clipboard.GetText();
                    txtOutput.Text = clipboardText;
                    Show();
                    Activate();
                    handled = true;
                }
            }
            return IntPtr.Zero;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var windowHandle = new WindowInteropHelper(this).Handle;

            UnregisterHotKey(windowHandle, HOTKEY_ID);
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

                btnCopy.IsEnabled = false;
                btnCopy.Foreground = Brushes.Green;
                btnCopy.Content = "Copied!";

                await Task.Delay(1500);

                btnCopy.Foreground = Brushes.Black;
                btnCopy.Content = "Copy";
                btnCopy.IsEnabled = true;
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
                string InputText = Clipboard.GetText();

                txtOutput.Text = rewriteService.RewriteText(InputText, "Professional");
            }catch
            {
                MessageBox.Show("Fatal error occured at btnRegenerate_Click");
            }
        }
    }
}
