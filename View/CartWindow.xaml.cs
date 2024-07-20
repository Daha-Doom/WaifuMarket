using Market.Model;
using Market.ViewModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Market.View
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow(ObservableCollection<Waifu> waifus)
        {
            InitializeComponent();

            CartWindowViewModel vm = new CartWindowViewModel(waifus, this);

            DataContext = vm;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
