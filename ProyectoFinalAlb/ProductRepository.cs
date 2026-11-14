

namespace CleaningProductsApp
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Brand { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class ProductRepository
    {
        public void Insert(string name, string brand, string category, decimal price, int stock)
        {
            using var db = new MyConnection();

            var product = new Product
            {
                Name = name,
                Brand = brand,
                Category = category,
                Price = price,
                Stock = stock
            };

            db.Products.Add(product);
            db.SaveChanges();
        }

        public void Get()
        {
            using var db = new MyConnection();

            var products = db.Products.ToList();

            foreach (var product in products)
            {
                ShowProduct(product);
            }

            if (products.Count == 0)
                Console.WriteLine("There are no products");
        }

        public void Search(int option, string data)
        {
            using var db = new MyConnection();

            var products = db.Products.AsQueryable();

            if (option == 1 && int.TryParse(data, out int id))
                products = products.Where(p => p.Id == id);
            else if (option == 2)
                products = products.Where(p => p.Name.Contains(data));
            else if (option == 3)
                products = products.Where(p => p.Brand.Contains(data));
            else if (option == 4)
                products = products.Where(p => p.Category.Contains(data));

            var result = products.ToList();

            foreach (var product in result)
            {
                ShowProduct(product);
            }

            if (result.Count == 0)
                Console.WriteLine("There are no products");
        }

        public void Update(int id, string name, string brand, string category, decimal? price, int? stock)
        {
            using var db = new MyConnection();

            var product = db.Products.Find(id);

            if (product == null)
                return;

            if (name != "") product.Name = name;
            if (brand != "") product.Brand = brand;
            if (category != "") product.Category = category;
            if (price.HasValue) product.Price = price.Value;
            if (stock.HasValue) product.Stock = stock.Value;

            db.SaveChanges();
        }

        public void Delete(int id)
        {
            using var db = new MyConnection();

            var product = db.Products.Find(id);

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public string? GetName(int id)
        {
            using var db = new MyConnection();

            return db.Products
                .Where(p => p.Id == id)
                .Select(p => p.Name)
                .FirstOrDefault();
        }

        private void ShowProduct(Product product)
        {
            Console.WriteLine("----------------------");
            Console.WriteLine("ID: " + product.Id);
            Console.WriteLine("Name: " + product.Name);
            Console.WriteLine("Brand: " + product.Brand);
            Console.WriteLine("Category: " + product.Category);
            Console.WriteLine("Price: " + product.Price);
            Console.WriteLine("Stock: " + product.Stock);
        }
    }
}
