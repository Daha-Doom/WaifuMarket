using Market.Model;
using Market.MVVM;
using Market.View;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Market.ViewModel
{
    internal class MarketWindowViewModel: ViewModelBase
    {
        public ObservableCollection<Waifu> waifus {  get; set; }

        DataBaseConnection connection = new DataBaseConnection();

        private readonly CartManager cartManager;

        public RelayCommand CloseCommand => new RelayCommand(execute => CloseApp());
        public RelayCommand MinimizeCommand => new RelayCommand(execute => MinimizeApp());
        public RelayCommand MaximizeCommand => new RelayCommand(execute => MaximizeApp());
        public RelayCommand CartCommand => new RelayCommand(execute => CartWindow());

        private int countProductInCart;
        public int CountProductInCart
        {
            get { return countProductInCart; }
            set
            {
                countProductInCart = value;
                OnPropertyChanged();
            }
        }

        public MarketWindowViewModel() 
        {
            waifus = new ObservableCollection<Waifu>();
            connection.ReadData(waifus);

            cartManager = Waifu.cartManager;
            cartManager.CartChanged += CartManager_CartChanged;
            cartManager.LoadCart();
        }

        private void CartManager_CartChanged(object sender, EventArgs e)
        {
            CountProductInCart = cartManager.GetCartItems().Sum(item => item.Count);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[0-9]+$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void CloseApp()
        {
            cartManager.SaveCart();

            Application.Current.Shutdown();
        }

        private void MaximizeApp()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Normal)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            else
                Application.Current.MainWindow.WindowState = WindowState.Normal;
        }

        private void MinimizeApp()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void CartWindow()
        {
            CartWindow cartWindow = new CartWindow(waifus);
            cartWindow.ShowDialog();
        }
    }
}
