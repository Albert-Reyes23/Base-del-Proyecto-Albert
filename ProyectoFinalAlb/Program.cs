

class Product
{
    public int Id;
    public string Name;
    public string Brand;
    public string Category;
    public decimal Price;
    public int Stock;

    public Product(int id, string name, string brand, string category, decimal price, int stock)
    {
        Id = id;
        Name = name;
        Brand = brand;
        Category = category;
        Price = price;
        Stock = stock;
    }
}

class ProductRegistry
{
    List<Product> products = new List<Product>();
    int newId = 1;

    public void AddProduct()
    {
        Console.WriteLine("ADD PRODUCT");

        Console.Write("Enter name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter brand: ");
        string brand = Console.ReadLine() ?? "";

        Console.Write("Enter category: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("Enter price: ");
        string priceText = Console.ReadLine() ?? "";

        Console.Write("Enter stock: ");
        string stockText = Console.ReadLine() ?? "";

        if (name == "" || brand == "" || category == "")
        {
            Console.WriteLine("Fields cannot be empty");
            return;
        }

        if (!decimal.TryParse(priceText, out decimal price) || price <= 0)
        {
            Console.WriteLine("Invalid price");
            return;
        }

        if (!int.TryParse(stockText, out int stock) || stock < 0)
        {
            Console.WriteLine("Invalid stock");
            return;
        }

        Product product = new Product(newId, name, brand, category, price, stock);

        products.Add(product);

        Console.WriteLine("Product added successfully");
        Console.WriteLine("Product ID: " + newId);

        newId++;
    }

    public void ViewProducts()
    {
        Console.WriteLine("PRODUCT LIST");

        if (products.Count == 0)
        {
            Console.WriteLine("No products available");
            return;
        }

        foreach (Product product in products)
        {
            ShowProduct(product);
        }
    }

    public void SearchProduct()
    {
        Console.WriteLine("SEARCH PRODUCT");
        Console.WriteLine("1 Search by ID");
        Console.WriteLine("2 Search by name");
        Console.WriteLine("3 Search by brand");
        Console.WriteLine("4 Search by category");

        Console.Write("Choose an option: ");

        if (!int.TryParse(Console.ReadLine(), out int option) || option < 1 || option > 4)
        {
            Console.WriteLine("Invalid option");
            return;
        }

        Console.Write("Enter search information: ");
        string search = Console.ReadLine() ?? "";

        bool found = false;

        foreach (Product product in products)
        {
            if ((option == 1 && product.Id.ToString() == search) ||
                (option == 2 && product.Name.ToLower().Contains(search.ToLower())) ||
                (option == 3 && product.Brand.ToLower().Contains(search.ToLower())) ||
                (option == 4 && product.Category.ToLower().Contains(search.ToLower())))
            {
                ShowProduct(product);
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Product not found");
        }
    }

    public void EditProduct()
    {
        Console.WriteLine("EDIT PRODUCT");
        Console.Write("Enter product ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }

        Product product = products.Find(p => p.Id == id);

        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        Console.WriteLine("Leave empty to keep the current value");

        Console.Write("New name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("New brand: ");
        string brand = Console.ReadLine() ?? "";

        Console.Write("New category: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("New price: ");
        string priceText = Console.ReadLine() ?? "";

        Console.Write("New stock: ");
        string stockText = Console.ReadLine() ?? "";

        decimal price = product.Price;
        int stock = product.Stock;

        if (priceText != "" && (!decimal.TryParse(priceText, out price) || price <= 0))
        {
            Console.WriteLine("Invalid price");
            return;
        }

        if (stockText != "" && (!int.TryParse(stockText, out stock) || stock < 0))
        {
            Console.WriteLine("Invalid stock");
            return;
        }

        if (name != "")
            product.Name = name;

        if (brand != "")
            product.Brand = brand;

        if (category != "")
            product.Category = category;

        product.Price = price;
        product.Stock = stock;

        Console.WriteLine("Product updated successfully");
    }

    public void DeleteProduct()
    {
        Console.WriteLine("DELETE PRODUCT");
        Console.Write("Enter product ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }

        Product product = products.Find(p => p.Id == id);

        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        Console.WriteLine("Product: " + product.Name);
        Console.Write("Delete this product? (1 = Yes, 2 = No): ");

        if (!int.TryParse(Console.ReadLine(), out int option))
        {
            Console.WriteLine("Invalid option");
            return;
        }

        if (option == 1)
        {
            products.Remove(product);
            Console.WriteLine("Product deleted successfully");
        }
        else
        {
            Console.WriteLine("Deletion canceled");
        }
    }

    void ShowProduct(Product product)
    {
        Console.WriteLine("----------------");
        Console.WriteLine("ID: " + product.Id);
        Console.WriteLine("Name: " + product.Name);
        Console.WriteLine("Brand: " + product.Brand);
        Console.WriteLine("Category: " + product.Category);
        Console.WriteLine("Price: " + product.Price);
        Console.WriteLine("Stock: " + product.Stock);
    }
}

class Program
{
    static void Main()
    {
        ProductRegistry registry = new ProductRegistry();

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("MY CLEANING PRODUCTS REGISTRY");
            Console.WriteLine("1 Add Product");
            Console.WriteLine("2 View Products");
            Console.WriteLine("3 Search Product");
            Console.WriteLine("4 Edit Product");
            Console.WriteLine("5 Delete Product");
            Console.WriteLine("6 Exit");

            Console.Write("Choose an option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("You must enter a number");
                continue;
            }

            switch (option)
            {
                case 1:
                    registry.AddProduct();
                    break;

                case 2:
                    registry.ViewProducts();
                    break;

                case 3:
                    registry.SearchProduct();
                    break;

                case 4:
                    registry.EditProduct();
                    break;

                case 5:
                    registry.DeleteProduct();
                    break;

                case 6:
                    running = false;
                    Console.WriteLine("Thank you for using My Cleaning Products Registry");
                    break;

                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
    }
}
