using Market.MVVM;
using Market.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market.Model
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Count { get; set; }
    }

    public class CartManager
    {
        JsonWork jsonWork = new JsonWork();

        private List<CartItem> cartItems = new List<CartItem>();

        public event EventHandler CartChanged;

        public void OnCartChanged()
        {
            CartChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddToCart(int itemId, int itemCount)
        {
            var existingItem = cartItems.FirstOrDefault(item => item.Id == itemId);
            if (existingItem != null)
            {
                existingItem.Count += itemCount;
            }
            else
            {
                cartItems.Add(new CartItem { Id = itemId, Count = itemCount });
            }

            OnCartChanged();
        }

        public void RemoveFromCart(int itemId)
        {
            var itemToRemove = cartItems.FirstOrDefault(item => item.Id == itemId);
            if (itemToRemove != null)
            {
                cartItems.Remove(itemToRemove);

                OnCartChanged();
            }
        }

        public List<CartItem> GetCartItems()
        {
            if (cartItems != null)
                return new List<CartItem>(cartItems);
            else return cartItems = new List<CartItem>();
        }

        public int GetCountItem()
        {
            return cartItems.Sum(item => item.Count);
        }

        public float TotalPrice(int id, float price)
        {
            if (cartItems.Any(item => item.Id == id))
                return cartItems.FirstOrDefault(item => item.Id == id).Count * price;
            else return 0;
        }

        public int OneCount(int id)
        {
            var item = cartItems.FirstOrDefault(item => item.Id == id);
            return item.Count;
        }

        public void SaveCart()
        {
            jsonWork.SaveJson(cartItems);
        }

        public void LoadCart()
        {
            cartItems = jsonWork.LoadJson();
            OnCartChanged();
        }
    }
}
