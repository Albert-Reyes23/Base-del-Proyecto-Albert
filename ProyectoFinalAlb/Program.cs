

Dictionary<int, string> names = new Dictionary<int, string>();
Dictionary<int, string> brands = new Dictionary<int, string>();
Dictionary<int, string> categories = new Dictionary<int, string>();
Dictionary<int, decimal> prices = new Dictionary<int, decimal>();
List<int> stocks = new List<int>();
List<int> ids = new List<int>();

bool running = true;
int newId = 1;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("1 Add Product");
    Console.WriteLine("2 View Products");
    Console.WriteLine("3 Search Product");
    Console.WriteLine("4 Edit Product");
    Console.WriteLine("5 Delete Product");
    Console.WriteLine("6 Exit");
    Console.Write("Choose an option ");

    if (!int.TryParse(Console.ReadLine(), out int option))
    {
        Console.WriteLine("You must enter a number");
        continue;
    }

    switch (option)
    {
        case 1:
            AddProduct(names, brands, categories, prices, stocks, ids, ref newId);
            break;
        case 2:
            ViewProducts(names, brands, categories, prices, stocks, ids);
            break;
        case 3:
            SearchProduct(names, brands, categories, prices, stocks, ids);
            break;
        case 4:
            EditProduct(names, brands, categories, prices, stocks, ids);
            break;
        case 5:
            DeleteProduct(names, brands, categories, prices, stocks, ids);
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

static void AddProduct(Dictionary<int, string> names, Dictionary<int, string> brands, Dictionary<int, string> categories, Dictionary<int, decimal> prices, List<int> stocks, List<int> ids, ref int newId)
{
    Console.WriteLine("ADD PRODUCT");

    Console.Write("Enter the name ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Enter the brand ");
    string brand = Console.ReadLine() ?? "";

    Console.Write("Enter the category ");
    string category = Console.ReadLine() ?? "";

    Console.Write("Enter the price ");
    string priceText = Console.ReadLine() ?? "";

    Console.Write("Enter the stock ");
    string stockText = Console.ReadLine() ?? "";

    if (name == "" || brand == "" || category == "")
    {
        Console.WriteLine("Fields cannot be empty");
    }
    else if (!decimal.TryParse(priceText, out decimal price) || price <= 0)
    {
        Console.WriteLine("The price must be a number greater than 0");
    }
    else if (!int.TryParse(stockText, out int stock) || stock < 0)
    {
        Console.WriteLine("The stock must be a whole number of 0 or more");
    }
    else
    {
        names.Add(newId, name);
        brands.Add(newId, brand);
        categories.Add(newId, category);
        prices.Add(newId, price);
        stocks.Add(stock);
        ids.Add(newId);

        Console.WriteLine("Product added successfully");
        Console.WriteLine("Product ID " + newId);
        newId++;
    }
}

static void ViewProducts(Dictionary<int, string> names, Dictionary<int, string> brands, Dictionary<int, string> categories, Dictionary<int, decimal> prices, List<int> stocks, List<int> ids)
{
    Console.WriteLine("PRODUCT LIST");

    if (ids.Count == 0)
    {
        Console.WriteLine("There are no products");
    }
    else
    {
        foreach (int id in ids)
        {
            int position = ids.IndexOf(id);

            Console.WriteLine("----------------");
            Console.WriteLine("ID " + id);
            Console.WriteLine("Name " + names[id]);
            Console.WriteLine("Brand " + brands[id]);
            Console.WriteLine("Category " + categories[id]);
            Console.WriteLine("Price " + prices[id]);
            Console.WriteLine("Stock " + stocks[position]);
        }
    }
}

static void SearchProduct(Dictionary<int, string> names, Dictionary<int, string> brands, Dictionary<int, string> categories, Dictionary<int, decimal> prices, List<int> stocks, List<int> ids)
{
    Console.WriteLine("SEARCH PRODUCT");
    Console.WriteLine("1 Search by ID");
    Console.WriteLine("2 Search by name");
    Console.WriteLine("3 Search by brand");
    Console.WriteLine("4 Search by category");
    Console.Write("Select an option ");

    if (!int.TryParse(Console.ReadLine(), out int option))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    if (option < 1 || option > 4)
    {
        Console.WriteLine("Invalid option");
        return;
    }

    Console.Write("Enter what you want to search ");
    string data = Console.ReadLine() ?? "";
    bool found = false;

    foreach (int id in ids)
    {
        bool matches = false;

        if (option == 1 && id.ToString() == data)
            matches = true;
        if (option == 2 && names[id].ToLower().Contains(data.ToLower()))
            matches = true;
        if (option == 3 && brands[id].ToLower().Contains(data.ToLower()))
            matches = true;
        if (option == 4 && categories[id].ToLower().Contains(data.ToLower()))
            matches = true;

        if (matches)
        {
            int position = ids.IndexOf(id);
            Console.WriteLine("----------------");
            Console.WriteLine("ID " + id);
            Console.WriteLine("Name " + names[id]);
            Console.WriteLine("Brand " + brands[id]);
            Console.WriteLine("Category " + categories[id]);
            Console.WriteLine("Price " + prices[id]);
            Console.WriteLine("Stock " + stocks[position]);
            found = true;
        }
    }

    if (!found)
    {
        Console.WriteLine("Product not found");
    }
}

static void EditProduct(Dictionary<int, string> names, Dictionary<int, string> brands, Dictionary<int, string> categories, Dictionary<int, decimal> prices, List<int> stocks, List<int> ids)
{
    Console.WriteLine("EDIT PRODUCT");
    Console.Write("Enter the product ID ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid ID");
        return;
    }

    if (!names.ContainsKey(id))
    {
        Console.WriteLine("Product not found");
        return;
    }

    int position = ids.IndexOf(id);

    Console.WriteLine("Leave a field empty to keep the current value");
    Console.Write("New name ");
    string name = Console.ReadLine() ?? "";
    Console.Write("New brand ");
    string brand = Console.ReadLine() ?? "";
    Console.Write("New category ");
    string category = Console.ReadLine() ?? "";
    Console.Write("New price ");
    string priceText = Console.ReadLine() ?? "";
    Console.Write("New stock ");
    string stockText = Console.ReadLine() ?? "";

    decimal price = prices[id];
    int stock = stocks[position];

    if (priceText != "" && (!decimal.TryParse(priceText, out price) || price <= 0))
    {
        Console.WriteLine("Invalid price, no changes were made");
        return;
    }

    if (stockText != "" && (!int.TryParse(stockText, out stock) || stock < 0))
    {
        Console.WriteLine("Invalid stock, no changes were made");
        return;
    }

    if (name != "")
        names[id] = name;
    if (brand != "")
        brands[id] = brand;
    if (category != "")
        categories[id] = category;

    prices[id] = price;
    stocks[position] = stock;

    Console.WriteLine("Product updated successfully");
}

static void DeleteProduct(Dictionary<int, string> names, Dictionary<int, string> brands, Dictionary<int, string> categories, Dictionary<int, decimal> prices, List<int> stocks, List<int> ids)
{
    Console.WriteLine("DELETE PRODUCT");
    Console.Write("Enter the product ID ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid ID");
        return;
    }

    if (!names.ContainsKey(id))
    {
        Console.WriteLine("Product not found");
        return;
    }

    Console.WriteLine("Name " + names[id]);
    Console.Write("Do you want to delete this product? (1 = Yes, 2 = No) ");

    if (!int.TryParse(Console.ReadLine(), out int option))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    if (option == 1)
    {
        int position = ids.IndexOf(id);
        names.Remove(id);
        brands.Remove(id);
        categories.Remove(id);
        prices.Remove(id);
        stocks.RemoveAt(position);
        ids.Remove(id);
        Console.WriteLine("Product deleted successfully");
    }
    else
    {
        Console.WriteLine("Deletion canceled");
    }
}