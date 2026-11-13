
using CleaningProductsApp;

var repository = new ProductRepository();

while (true)
{
    Console.Clear();

    Console.WriteLine("MY CLEANING PRODUCTS REGISTRY");
    Console.WriteLine();

    Console.WriteLine("1 - Add Product");
    Console.WriteLine("2 - View Products");
    Console.WriteLine("3 - Search Product");
    Console.WriteLine("4 - Edit Product");
    Console.WriteLine("5 - Delete Product");
    Console.WriteLine("6 - Exit");

    Console.Write("Choose an option: ");

    var option = Console.ReadLine();

    if (option == "1")
    {
        Console.Write("Enter the name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter the brand: ");
        string brand = Console.ReadLine() ?? "";

        Console.Write("Enter the category: ");
        string category = Console.ReadLine() ?? "";

        Console.Write("Enter the price: ");
        string priceText = Console.ReadLine() ?? "";

        Console.Write("Enter the stock: ");
        string stockText = Console.ReadLine() ?? "";

        if (name == "" || brand == "" || category == "")
        {
            Console.WriteLine("Fields cannot be empty");
        }
        else if (!decimal.TryParse(priceText, out decimal price) || price <= 0)
        {
            Console.WriteLine("Invalid price");
        }
        else if (!int.TryParse(stockText, out int stock) || stock < 0)
        {
            Console.WriteLine("Invalid stock");
        }
        else
        {
            repository.Insert(name, brand, category, price, stock);
            Console.WriteLine("Product added successfully");
        }

        WaitForSpace();
    }
    else if (option == "2")
    {
        Console.WriteLine("PRODUCT LIST");

        repository.Get();

        WaitForSpace();
    }
    else if (option == "3")
    {
        Console.WriteLine("1 Search by ID");
        Console.WriteLine("2 Search by name");
        Console.WriteLine("3 Search by brand");
        Console.WriteLine("4 Search by category");

        Console.Write("Select an option: ");

        if (!int.TryParse(Console.ReadLine(), out int searchOption) ||
            searchOption < 1 || searchOption > 4)
        {
            Console.WriteLine("Invalid option");
        }
        else
        {
            Console.Write("Enter what you want to search: ");
            string data = Console.ReadLine() ?? "";

            repository.Search(searchOption, data);
        }

        WaitForSpace();
    }
    else if (option == "4")
    {
        Console.WriteLine("The registered products are:");

        repository.Get();

        Console.Write("Enter the product ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
        }
        else if (repository.GetName(id) == null)
        {
            Console.WriteLine("Product not found");
        }
        else
        {
            Console.WriteLine("Leave a field empty to keep its current value");

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

            decimal? price = null;
            int? stock = null;

            if (priceText != "")
            {
                if (!decimal.TryParse(priceText, out decimal newPrice) || newPrice <= 0)
                {
                    Console.WriteLine("Invalid price");
                    WaitForSpace();
                    continue;
                }

                price = newPrice;
            }

            if (stockText != "")
            {
                if (!int.TryParse(stockText, out int newStock) || newStock < 0)
                {
                    Console.WriteLine("Invalid stock");
                    WaitForSpace();
                    continue;
                }

                stock = newStock;
            }

            repository.Update(id, name, brand, category, price, stock);

            Console.WriteLine("Product updated successfully");
            Console.WriteLine("The list is now:");

            repository.Get();
        }

        WaitForSpace();
    }
    else if (option == "5")
    {
        Console.WriteLine("The registered products are:");

        repository.Get();

        Console.Write("Enter the ID of the product to delete: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
        }
        else
        {
            string? name = repository.GetName(id);

            if (name == null)
            {
                Console.WriteLine("Product not found");
            }
            else
            {
                Console.WriteLine("Name: " + name);

                Console.Write("Do you want to delete this product? (1 = Yes, 2 = No): ");

                if (Console.ReadLine() == "1")
                {
                    repository.Delete(id);

                    Console.WriteLine("Product deleted successfully");
                    Console.WriteLine("The list is now:");

                    repository.Get();
                }
                else
                {
                    Console.WriteLine("Deletion canceled");
                }
            }
        }

        WaitForSpace();
    }
    else if (option == "6")
    {
        Console.WriteLine("Thank you for using My Cleaning Products Registry");
        break;
    }
    else
    {
        Console.WriteLine("Invalid option");
        WaitForSpace();
    }
}

static void WaitForSpace()
{
    Console.WriteLine();
    Console.WriteLine("Press SPACE to continue...");

    while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
    {
    }
}
