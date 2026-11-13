using Microsoft.Data.SqlClient;
using System;

namespace ContactBookApp
{
    public class ContactRepository
    {
        public void Get()
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = "SELECT Id, Name, Phone, Email, Address FROM contacts";
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            ShowRows(reader);
        }

        public void Insert(string name, string phone, string email, string address)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = @"INSERT INTO contacts (Name, Phone, Email, Address)
                          VALUES (@Name, @Phone, @Email, @Address)";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Address", address);

            command.ExecuteNonQuery();
        }

      
        public void Update(int id, string name, string phone, string email, string address)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = @"UPDATE contacts SET
                            Name    = COALESCE(NULLIF(@Name, ''), Name),
                            Phone   = COALESCE(NULLIF(@Phone, ''), Phone),
                            Email   = COALESCE(NULLIF(@Email, ''), Email),
                            Address = COALESCE(NULLIF(@Address, ''), Address)
                          WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Phone", phone);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Address", address);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = "DELETE FROM contacts WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

       
        public string? GetName(int id)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = "SELECT Name FROM contacts WHERE Id = @Id";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteScalar() as string;
        }

        
        public void Search(int option, string data)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            string where = option switch
            {
                1 => "CAST(Id AS NVARCHAR(10)) = @Data",
                2 => "Name LIKE '%' + @Data + '%'",
                3 => "Phone LIKE '%' + @Data + '%'",
                _ => "Email LIKE '%' + @Data + '%'"
            };

            var query = "SELECT Id, Name, Phone, Email, Address FROM contacts WHERE " + where;
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Data", data);

            using var reader = command.ExecuteReader();
            ShowRows(reader);
        }

        private void ShowRows(SqlDataReader reader)
        {
            bool any = false;

            while (reader.Read())
            {
                any = true;
                Console.WriteLine("----------------------");
                Console.WriteLine("ID " + reader.GetInt32(0));
                Console.WriteLine("Name " + reader.GetString(1));
                Console.WriteLine("Phone " + reader.GetString(2));
                Console.WriteLine("Email " + reader.GetString(3));
                Console.WriteLine("Address " + reader.GetString(4));
            }

            if (!any)
                Console.WriteLine("There are no contacts");
        }
    }
}