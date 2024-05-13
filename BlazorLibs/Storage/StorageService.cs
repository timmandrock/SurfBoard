using MyBlazorShop.Libraries.Services.Product.Models;
using MyBlazorShop.Libraries.Services.ShoppingCart.Models;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyBlazorShop.Libraries.Services.Storage
{
    /// <summary>
    /// Stores the data used for the application.
    /// </summary>
    /// 
    public class SurfBoard
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public double length { get; set; }
        public double width { get; set; }
        public double thickness { get; set; }
        public double volume { get; set; }
        public double price { get; set; }
        public string equipment { get; set; }
        public string rowversion { get; set; }
        public string imageUrl { get; set; }
    }

    public class StorageService : IStorageService
    {

        /// <summary>
        /// Stores a list of products.
        /// </summary>
        public IList<ProductModel> Products { get; private set; }

        /// <summary>
        /// Stores the shopping cart.        
        /// </summary>
        public ShoppingCartModel ShoppingCart { get; private set; }

        /// <summary>
        ///  Constructs a storage service.
        /// </summary>
        public StorageService()
        {
            Products = new List<ProductModel>();
            ShoppingCart = new ShoppingCartModel();

            // Store a list of all the products for the online shop.

            //Task<List<SurfBoard>> task = GetBoards();

            /*
            AddProduct(new ProductModel("SURFBOARD-1", "The Minilog", 24, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-2", "The Wide Glider", 28, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-3", "The Golden Ratio", 20, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-4", "Mahi Mahi", 30, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-5", "The Emerald Glider", 36, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-6", "The Bomb", 18, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-7", "Surfboard 7", 22, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-8", "Surfboard 8", 40, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-9", "Surfboard 9", 28, "surfboard1.jpg"));
            AddProduct(new ProductModel("SURFBOARD-10", "Surfboard 10", 18, "surfboard1.jpg"));
            */
        }

        /// <summary>
        /// Adds a product to the storage.
        /// </summary>
        /// <param name="productModel">The <see cref="ProductModel"/> type to be added.</param>
        private void AddProduct(ProductModel productModel)
        {
            if (!Products.Any(p => p.Sku == productModel.Sku))
            {
                Products.Add(productModel);
            }
        }

        public async Task<List<ProductModel>> GetAll()
        {
            //API
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://localhost:7207/Surfboard/surfboards");

            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            //string responseBody = "[\r\n  {\r\n    \"id\": 1,\r\n    \"name\": \"The Minilog\",\r\n    \"type\": \"Shortboard\",\r\n    \"length\": 6,\r\n    \"width\": 21,\r\n    \"thickness\": 2.75,\r\n    \"volume\": 38.8,\r\n    \"price\": 565,\r\n    \"equipment\": \"\",\r\n    \"rowversion\": \"AAAAAAAAB9E=\",\r\n    \"imageUrl\": \"https://kite-prod.b-cdn.net/13342-large_default/naish-hover-ascend-carbon-s26-2021-surf-foilboard.jpg\"\r\n  },\r\n  {\r\n    \"id\": 2,\r\n    \"name\": \"The Wide Glider\",\r\n    \"type\": \"Funboard\",\r\n    \"length\": 7.1,\r\n    \"width\": 21.75,\r\n    \"thickness\": 2.75,\r\n    \"volume\": 43.22,\r\n    \"price\": 685,\r\n    \"equipment\": \"\",\r\n    \"rowversion\": \"AAAAAAAAB9I=\",\r\n    \"imageUrl\": \"https://kite-prod.b-cdn.net/13969-large_default/armstrong-wing-surf-foilboard-2022.jpg\"\r\n  },\r\n  {\r\n    \"id\": 3,\r\n    \"name\": \"The Golden Ratio\",\r\n    \"type\": \"Funboard\",\r\n    \"length\": 6.3,\r\n    \"width\": 20.75,\r\n    \"thickness\": 2.9,\r\n    \"volume\": 43.22,\r\n    \"price\": 695,\r\n    \"equipment\": \"Paddel\",\r\n    \"rowversion\": \"AAAAAAAAB9M=\",\r\n    \"imageUrl\": \"https://kite-prod.b-cdn.net/10803-large_default/rrd-longsup-9-8-lte-y25-2020-sup.jpg\"\r\n  },\r\n  {\r\n    \"id\": 4,\r\n    \"name\": \"Mahi Mahi\",\r\n    \"type\": \"Fish\",\r\n    \"length\": 5.4,\r\n    \"width\": 20.75,\r\n    \"thickness\": 2.3,\r\n    \"volume\": 29.39,\r\n    \"price\": 545,\r\n    \"equipment\": \"\",\r\n    \"rowversion\": \"AAAAAAAAB9Q=\",\r\n    \"imageUrl\": \"https://kite-prod.b-cdn.net/18422-thickbox_default/north-charge-2023-kite-surfboard.jpg\"\r\n  },\r\n  {\r\n    \"id\": 5,\r\n    \"name\": \"The Emerald Glider\",\r\n    \"type\": \"Longboard\",\r\n    \"length\": 9.2,\r\n    \"width\": 22.8,\r\n    \"thickness\": 2.8,\r\n    \"volume\": 65.4,\r\n    \"price\": 895,\r\n    \"equipment\": \"\",\r\n    \"rowversion\": \"AAAAAAAAB9U=\",\r\n    \"imageUrl\": \"https://kite-prod.b-cdn.net/14807-thickbox_default/duotone-whip-d-lab-2022-kite-surfboard.jpg\"\r\n  },\r\n  {\r\n    \"id\": 6,\r\n    \"name\": \"The Bomb\",\r\n    \"type\": \"Longboard\",\r\n    \"length\": 5.5,\r\n    \"width\": 21,\r\n    \"thickness\": 2.5,\r\n    \"volume\": 33.7,\r\n    \"price\": 645,\r\n    \"equipment\": \"\",\r\n    \"rowversion\": \"AAAAAAAAB9Y=\",\r\n    \"imageUrl\": \"https://kite-prod.b-cdn.net/18574-thickbox_default/f-one-mitu-pro-flex-2023-kite-surfboard.jpg\"\r\n  }\r\n]";

            List<SurfBoard> boards = JsonSerializer.Deserialize<List<SurfBoard>>(responseBody);

            List<ProductModel> products = new List<ProductModel>();
            foreach (SurfBoard b in boards)
            {
                products.Add(new ProductModel(b.name.ToUpper(), b.name, (decimal)b.price, b.imageUrl));

                if(Products.FirstOrDefault(p => p.Name == b.name) == null)
                {
                    Products.Add(new ProductModel(b.name.ToUpper(), b.name, (decimal)b.price, b.imageUrl));
                }
            }

            products.AddRange(Products);
            
            return products;
        }

        public async void AddAll(List<ProductModel> products)
        {
            products.AddRange(Products);
        }
    }
}
