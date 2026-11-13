
using System;
using System.Collections.Generic;

Dictionary<int, string> names = new Dictionary<int, string>();
Dictionary<int, string> phones = new Dictionary<int, string>();
Dictionary<int, string> emails = new Dictionary<int, string>();
List<string> addresses = new List<string>();
List<int> ids = new List<int>();

bool running = true;
int newId = 1;

while (running)
{
    Console.WriteLine();
    Console.WriteLine("1 Add Contact");
    Console.WriteLine("2 View Contacts");
    Console.WriteLine("3 Search Contact");
    Console.WriteLine("4 Edit Contact");
    Console.WriteLine("5 Delete Contact");
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
            AddContact(names, phones, emails, addresses, ids, ref newId);
            break;
        case 2:
            ViewContacts(names, phones, emails, addresses, ids);
            break;
        case 3:
            SearchContact(names, phones, emails, addresses, ids);
            break;
        case 4:
            EditContact(names, phones, emails, addresses, ids);
            break;
        case 5:
            DeleteContact(names, phones, emails, addresses, ids);
            break;
        case 6:
            running = false;
            Console.WriteLine("Thank you for using My Contact Book");
            break;
        default:
            Console.WriteLine("Invalid option");
            break;
    }
}

static void AddContact(Dictionary<int, string> names, Dictionary<int, string> phones, Dictionary<int, string> emails, List<string> addresses, List<int> ids, ref int newId)
{
    Console.WriteLine("ADD CONTACT");

    Console.Write("Enter the name ");
    string name = Console.ReadLine() ?? "";

    Console.Write("Enter the phone number ");
    string phone = Console.ReadLine() ?? "";

    Console.Write("Enter the email ");
    string email = Console.ReadLine() ?? "";

    Console.Write("Enter the address ");
    string address = Console.ReadLine() ?? "";

    if (name == "" || phone == "" || email == "" || address == "")
    {
        Console.WriteLine("Fields cannot be empty");
    }
    else
    {
        names.Add(newId, name);
        phones.Add(newId, phone);
        emails.Add(newId, email);
        addresses.Add(address);
        ids.Add(newId);

        Console.WriteLine("Contact added successfully");
        Console.WriteLine("Contact ID " + newId);
        newId++;
    }
}

static void ViewContacts(Dictionary<int, string> names, Dictionary<int, string> phones, Dictionary<int, string> emails, List<string> addresses, List<int> ids)
{
    Console.WriteLine("CONTACT LIST");

    if (ids.Count == 0)
    {
        Console.WriteLine("There are no contacts");
    }
    else
    {
        foreach (int id in ids)
        {
            int position = ids.IndexOf(id);

            Console.WriteLine("----------------");
            Console.WriteLine("ID " + id);
            Console.WriteLine("Name " + names[id]);
            Console.WriteLine("Phone " + phones[id]);
            Console.WriteLine("Email " + emails[id]);
            Console.WriteLine("Address " + addresses[position]);
        }
    }
}

static void SearchContact(Dictionary<int, string> names, Dictionary<int, string> phones, Dictionary<int, string> emails, List<string> addresses, List<int> ids)
{
    Console.WriteLine("SEARCH CONTACT");
    Console.WriteLine("1 Search by ID");
    Console.WriteLine("2 Search by name");
    Console.WriteLine("3 Search by phone");
    Console.WriteLine("4 Search by email");
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
        if (option == 3 && phones[id].Contains(data))
            matches = true;
        if (option == 4 && emails[id].ToLower().Contains(data.ToLower()))
            matches = true;

        if (matches)
        {
            int position = ids.IndexOf(id);
            Console.WriteLine("----------------");
            Console.WriteLine("ID " + id);
            Console.WriteLine("Name " + names[id]);
            Console.WriteLine("Phone " + phones[id]);
            Console.WriteLine("Email " + emails[id]);
            Console.WriteLine("Address " + addresses[position]);
            found = true;
        }
    }

    if (!found)
    {
        Console.WriteLine("Contact not found");
    }
}

static void EditContact(Dictionary<int, string> names, Dictionary<int, string> phones, Dictionary<int, string> emails, List<string> addresses, List<int> ids)
{
    Console.WriteLine("EDIT CONTACT");
    Console.Write("Enter the contact ID ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid ID");
        return;
    }

    if (!names.ContainsKey(id))
    {
        Console.WriteLine("Contact not found");
        return;
    }

    int position = ids.IndexOf(id);

    Console.Write("New name ");
    string name = Console.ReadLine() ?? "";
    Console.Write("New phone number ");
    string phone = Console.ReadLine() ?? "";
    Console.Write("New email ");
    string email = Console.ReadLine() ?? "";
    Console.Write("New address ");
    string address = Console.ReadLine() ?? "";

    if (name != "")
        names[id] = name;
    if (phone != "")
        phones[id] = phone;
    if (email != "")
        emails[id] = email;
    if (address != "")
        addresses[position] = address;

    Console.WriteLine("Contact updated successfully");
}

static void DeleteContact(Dictionary<int, string> names, Dictionary<int, string> phones, Dictionary<int, string> emails, List<string> addresses, List<int> ids)
{
    Console.WriteLine("DELETE CONTACT");
    Console.Write("Enter the contact ID ");

    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid ID");
        return;
    }

    if (!names.ContainsKey(id))
    {
        Console.WriteLine("Contact not found");
        return;
    }

    Console.WriteLine("Name " + names[id]);
    Console.Write("Do you want to delete this contact? (1 = Yes, 2 = No) ");

    if (!int.TryParse(Console.ReadLine(), out int option))
    {
        Console.WriteLine("Invalid option");
        return;
    }

    if (option == 1)
    {
        int position = ids.IndexOf(id);
        names.Remove(id);
        phones.Remove(id);
        emails.Remove(id);
        addresses.RemoveAt(position);
        ids.Remove(id);
        Console.WriteLine("Contact deleted successfully");
    }
    else
    {
        Console.WriteLine("Deletion canceled");
    }
}
