using ContactBookApp;

var repository = new ContactRepository();

while (true)
{
    Console.Clear();
    Console.WriteLine("MY CONTACT BOOK");

    Console.WriteLine("1 - Add Contact");
    Console.WriteLine("2 - View Contacts");
    Console.WriteLine("3 - Search Contact");
    Console.WriteLine("4 - Edit Contact");
    Console.WriteLine("5 - Delete Contact");
    Console.WriteLine("6 - Exit");
    Console.Write("Choose an option: ");

    var option = Console.ReadLine();

    if (option == "1")
    {
        Console.Write("Enter the name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Enter the phone number: ");
        string phone = Console.ReadLine() ?? "";
        Console.Write("Enter the email: ");
        string email = Console.ReadLine() ?? "";
        Console.Write("Enter the address: ");
        string address = Console.ReadLine() ?? "";

        if (name == "" || phone == "" || email == "" || address == "")
        {
            Console.WriteLine("Fields cannot be empty");
        }
        else
        {
            repository.Insert(name, phone, email, address);
            Console.WriteLine("Contact added successfully");
        }
        WaitForSpace();
    }
    else if (option == "2")
    {
        Console.WriteLine("CONTACT LIST");
        repository.Get();
        WaitForSpace();
    }
    else if (option == "3")
    {
        Console.WriteLine("1 Search by ID");
        Console.WriteLine("2 Search by name");
        Console.WriteLine("3 Search by phone");
        Console.WriteLine("4 Search by email");
        Console.Write("Select an option: ");

        if (!int.TryParse(Console.ReadLine(), out int searchOption) || searchOption < 1 || searchOption > 4)
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
        Console.WriteLine("The registered contacts are:");
        repository.Get();

        Console.Write("Enter the contact ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
        }
        else if (repository.GetName(id) == null)
        {
            Console.WriteLine("Contact not found");
        }
        else
        {
            Console.WriteLine("(Leave a field empty to keep its current value)");
            Console.Write("New name: ");
            string name = Console.ReadLine() ?? "";
            Console.Write("New phone number: ");
            string phone = Console.ReadLine() ?? "";
            Console.Write("New email: ");
            string email = Console.ReadLine() ?? "";
            Console.Write("New address: ");
            string address = Console.ReadLine() ?? "";

            repository.Update(id, name, phone, email, address);

            Console.WriteLine("Contact updated successfully");
            Console.WriteLine("The list is now:");
            repository.Get();
        }
        WaitForSpace();
    }
    else if (option == "5")
    {
        Console.WriteLine("The registered contacts are:");
        repository.Get();

        Console.Write("Enter the ID of the contact to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
        }
        else
        {
            string? name = repository.GetName(id);

            if (name == null)
            {
                Console.WriteLine("Contact not found");
            }
            else
            {
                Console.WriteLine("Name " + name);
                Console.Write("Do you want to delete this contact? (1 = Yes, 2 = No): ");

                if (Console.ReadLine() == "1")
                {
                    repository.Delete(id);
                    Console.WriteLine("Contact deleted successfully");
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
        Console.WriteLine("Thank you for using My Contact Book");
        break;
    }
}

static void WaitForSpace()
{
    Console.WriteLine();
    Console.WriteLine("Press SPACE to continue...");

    // ReadKey(true) no muestra la tecla en pantalla
    while (Console.ReadKey(true).Key != ConsoleKey.Spacebar)
    {
    }
}