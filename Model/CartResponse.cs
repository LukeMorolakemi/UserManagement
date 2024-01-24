using Newtonsoft.Json;
using static UserManagement.ApiResource;

namespace UserManagement.Model
{









    public class CartResponse

    {



        public async Task<Cart> CartsAsync(int id)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://dummyjson.com/carts/{id}");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();


            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    var myDeserializedClass = JsonConvert.DeserializeObject<Cart>(content);

                    return myDeserializedClass;
                }
            }


            return null;

        }


        public async Task<Root> AllCartsAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://dummyjson.com/carts/");

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();


            if (response.Content != null)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {

                    var myDeserializedClass = JsonConvert.DeserializeObject<Root>(content);

                    return myDeserializedClass;

                }
            }


            return null;

        }
    }
    public class Cart
        {
            public int id { get; set; }
            public List<Product> products { get; set; }
            public int total { get; set; }
            public int discountedTotal { get; set; }
            public int userId { get; set; }
            public int totalProducts { get; set; }
            public int totalQuantity { get; set; }
        }

        public class Product
        {
            public int id { get; set; }
            public string title { get; set; }
            public int price { get; set; }
            public int quantity { get; set; }
            public int total { get; set; }
            public double discountPercentage { get; set; }
            public int discountedPrice { get; set; }
            public string thumbnail { get; set; }
        }

        public class Root
        {
            public List<Cart> carts { get; set; }
            public int total { get; set; }
            public int skip { get; set; }
            public int limit { get; set; }
        }

    }

