using Market.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Market.View
{
    /// <summary>
    /// Логика взаимодействия для MarketWindow.xaml
    /// </summary>
    public partial class MarketWindow : Window
    {
        public MarketWindow()
        {
            InitializeComponent();

            MarketWindowViewModel vm = new MarketWindowViewModel();

            DataContext = vm;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow.DragMove();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
    }
}
