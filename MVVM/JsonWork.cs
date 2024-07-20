using Market.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Market.MVVM
{
    class JsonWork
    {
        private string saveFile = "Data//CartSave.json";

        public void SaveJson(List<CartItem> carts)
        {
            //var SaveFile = File.Exists(saveFile) ? JsonConvert.DeserializeObject<List<Cart>>(File.ReadAllText(saveFile)) : new List<Cart>();

            File.WriteAllText(saveFile, JsonConvert.SerializeObject(carts));
        }

        public List<CartItem> LoadJson()
        {
            if (File.Exists(saveFile))
            {
                string json = File.ReadAllText(saveFile);

                return JsonConvert.DeserializeObject<List<CartItem>>(json);
            }
            return new List<CartItem>();
        }
    }
}
