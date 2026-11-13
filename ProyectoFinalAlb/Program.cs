
using System;
using System.Collections.Generic;

class Contact
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public string Address { get; private set; }

    public Contact(int id, string name, string phone, string email, string address)
    {
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
        Address = address;
    }

    public void Edit(string name, string phone, string email, string address)
    {
        if (name != "")
            Name = name;

        if (phone != "")
            Phone = phone;

        if (email != "")
            Email = email;

        if (address != "")
            Address = address;
    }

    public void Show()
    {
        Console.WriteLine("----------------------");
        Console.WriteLine("ID " + Id);
        Console.WriteLine("Name " + Name);
        Console.WriteLine("Phone " + Phone);
        Console.WriteLine("Email " + Email);
        Console.WriteLine("Address " + Address);
    }
}

class ContactBook
{
    private List<Contact> contacts = new List<Contact>();
    private int newId = 1;

    public void AddContact()
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
            Contact contact = new Contact(newId, name, phone, email, address);

            contacts.Add(contact);

            Console.WriteLine("Contact added successfully");
            Console.WriteLine("Contact ID " + newId);

            newId++;
        }
    }

    public void ViewContacts()
    {
        Console.WriteLine("CONTACT LIST");

        if (contacts.Count == 0)
        {
            Console.WriteLine("There are no contacts");
        }
        else
        {
            foreach (Contact contact in contacts)
            {
                contact.Show();
            }
        }
    }

    public void SearchContact()
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

        foreach (Contact contact in contacts)
        {
            bool matches = false;

            if (option == 1 && contact.Id.ToString() == data)
                matches = true;

            if (option == 2 && contact.Name.ToLower().Contains(data.ToLower()))
                matches = true;

            if (option == 3 && contact.Phone.Contains(data))
                matches = true;

            if (option == 4 && contact.Email.ToLower().Contains(data.ToLower()))
                matches = true;

            if (matches)
            {
                contact.Show();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Contact not found");
        }
    }

    public void EditContact()
    {
        Console.WriteLine("EDIT CONTACT");

        Console.Write("Enter the contact ID ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }

        Contact selected = null;

        foreach (Contact contact in contacts)
        {
            if (contact.Id == id)
            {
                selected = contact;
                break;
            }
        }

        if (selected == null)
        {
            Console.WriteLine("Contact not found");
            return;
        }

        Console.Write("New name ");
        string name = Console.ReadLine() ?? "";

        Console.Write("New phone number ");
        string phone = Console.ReadLine() ?? "";

        Console.Write("New email ");
        string email = Console.ReadLine() ?? "";

        Console.Write("New address ");
        string address = Console.ReadLine() ?? "";

        selected.Edit(name, phone, email, address);

        Console.WriteLine("Contact updated successfully");
    }

    public void DeleteContact()
    {
        Console.WriteLine("DELETE CONTACT");

        Console.Write("Enter the contact ID ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID");
            return;
        }

        Contact selected = null;

        foreach (Contact contact in contacts)
        {
            if (contact.Id == id)
            {
                selected = contact;
                break;
            }
        }

        if (selected == null)
        {
            Console.WriteLine("Contact not found");
            return;
        }

        Console.WriteLine("Name " + selected.Name);

        Console.Write("Do you want to delete this contact? (1 = Yes, 2 = No) ");

        if (!int.TryParse(Console.ReadLine(), out int option))
        {
            Console.WriteLine("Invalid option");
            return;
        }

        if (option == 1)
        {
            contacts.Remove(selected);
            Console.WriteLine("Contact deleted successfully");
        }
        else
        {
            Console.WriteLine("Deletion canceled");
        }
    }
}

class Program
{
    static void Main()
    {
        ContactBook book = new ContactBook();

        bool running = true;

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
                    book.AddContact();
                    break;

                case 2:
                    book.ViewContacts();
                    break;

                case 3:
                    book.SearchContact();
                    break;

                case 4:
                    book.EditContact();
                    break;

                case 5:
                    book.DeleteContact();
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
    }
}
