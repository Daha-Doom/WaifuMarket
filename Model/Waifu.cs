using Market.MVVM;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Market.ViewModel;

namespace Market.Model
{
    public class Waifu : ViewModelBase
    {
        public int waifuID { get; set; }
        private string waifuName, waifuCup, waifuType;
        private float waifuPrice;
        public string WaifuName { get => waifuName; set => waifuName = value; }
        public string WaifuCup { get => waifuCup; set => waifuCup = value; }
        public float WaifuPrice { get => waifuPrice; set => waifuPrice = value; }
        public string WaifuType { get => waifuType; set => waifuType = value; }

        private BitmapImage waifuImg;
        public BitmapImage WaifuImg { get => waifuImg; set => waifuImg = value; }

        public Waifu(int waifuID, string waifuName, string waifuCup, float waifuPrice, string waifuType, byte[] waifuImgByte)
        {
            this.waifuID = waifuID;
            this.WaifuName = waifuName;
            this.WaifuCup = waifuCup;
            this.WaifuType = waifuType;
            this.WaifuPrice = waifuPrice;
            waifuImg = new BitmapImage();
            waifuImg = BlobToImage(waifuImgByte);

            CountProduct = 1;
        }

        private BitmapImage BlobToImage(byte[] bitImg)
        {
            using (var ms = new System.IO.MemoryStream(bitImg))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        public static CartManager cartManager = new CartManager();

        public RelayCommand MinusCommand => new RelayCommand(execute => CountMinus());
        public RelayCommand PlusCommand => new RelayCommand(execute => CountPluse());
        public RelayCommand AddToCartCommand => new RelayCommand(execute => AddToCart());
        public RelayCommand RemoveFromCartCommand => new RelayCommand(execute => RemoveFromCart());

        private int countProduct;
        public int CountProduct
        {
            get => countProduct;
            set
            {
                countProduct = value;
                OnPropertyChanged();
            }
        }

        private int oneCount;
        public int OneCount
        {
            get => oneCount;
            set
            {
                oneCount = value;
                OnPropertyChanged();
            }
        }

        private float totalPrice;
        public float TotalPrice
        {
            get => totalPrice;
            set
            {
                totalPrice = value;
                OnPropertyChanged();
            }
        }

        private void CountMinus()
        {
            if (CountProduct > 1)
            {
                CountProduct -= 1;
            }
        }

        private void CountPluse()
        {
            CountProduct += 1;
        }

        private void AddToCart()
        {
            cartManager.AddToCart(waifuID, CountProduct);
            OneCount = cartManager.OneCount(waifuID);
        }

        private void RemoveFromCart()
        {
            cartManager.RemoveFromCart(waifuID);
        }

        public float GetTotalPrice()
        {
            TotalPrice = cartManager.TotalPrice(waifuID, waifuPrice);
            return cartManager.TotalPrice(waifuID, waifuPrice);
        }
    }
}
