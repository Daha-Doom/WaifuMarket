using Market.Model;
using Market.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Market.ViewModel
{
    class CartWindowViewModel: ViewModelBase
    {
        Window window;
        public ObservableCollection<Waifu> waifus { get; set; }
        public ObservableCollection<Waifu> existWaifu { get; set; }

        private readonly CartManager cartManager;

        public RelayCommand CloseCommand => new RelayCommand(execute => CloseApp());
        public RelayCommand MinimizeCommand => new RelayCommand(execute => MinimizeApp());
        public RelayCommand MaximizeCommand => new RelayCommand(execute => MaximizeApp());
        public RelayCommand ByingCommand => new RelayCommand(execute => Bying());

        private int totalCountProduct;
        public int TotalCountProduct
        {
            get { return totalCountProduct; }
            set
            {
                totalCountProduct = value;
                OnPropertyChanged();
            }
        }

        private float totalPrice;
        public float TotalPrice
        {
            get { return totalPrice; }
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }

        public CartWindowViewModel(ObservableCollection<Waifu> waifus, Window window)
        {
            existWaifu = new ObservableCollection<Waifu>();
            this.waifus = waifus; cartManager = Waifu.cartManager;

            cartManager.CartChanged += CartManager_CartChanged;

            TotalCountProduct = cartManager.GetCartItems().Sum(item => item.Count);

            ExistWaifu();
            CountTotalPrice();
            this.window = window;
        }

        private void CartManager_CartChanged(object sender, EventArgs e)
        {
            TotalCountProduct = cartManager.GetCartItems().Sum(item => item.Count);
            ExistWaifu();
            CountTotalPrice();
            OnPropertyChanged();
        }

        private void ExistWaifu()
        {
            existWaifu.Clear();

            foreach (var waifu in waifus)
            {
                if (cartManager.GetCartItems().Any(item => item.Id == waifu.waifuID))
                    existWaifu.Add(waifu);
            }
        }

        private void CountTotalPrice()
        {
            TotalPrice = 0;

            foreach (var waifu in existWaifu)
                TotalPrice += waifu.GetTotalPrice();
        }

        private void CloseApp()
        {
            window.Close();
        }

        private void MaximizeApp()
        {
            if (window.WindowState == WindowState.Normal)
                window.WindowState = WindowState.Maximized;
            else
                window.WindowState = WindowState.Normal;
        }

        private void MinimizeApp()
        {
            window.WindowState = WindowState.Minimized;
        }

        private void Bying()
        {
            if (existWaifu.Count > 0)
                MessageBox.Show("Поздравляем с покупкой!");
            else
                MessageBox.Show("В корзине нет товаров!");
        }
    }
}
