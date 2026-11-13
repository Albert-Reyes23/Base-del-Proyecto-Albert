
using CleaningProductsRegistry;
using Microsoft.Data.SqlClient;
using System;

namespace CleaningProductsApp
{
    public class ProductRepository
    {
        public void Get()
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = "SELECT Id, Name, Brand, Category, Price, Stock FROM Products";

            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();

            ShowRows(reader);
        }

        public void Insert(string name, string brand, string category, decimal price, int stock)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = @"INSERT INTO Products (Name, Brand, Category, Price, Stock)
                          VALUES (@Name, @Brand, @Category, @Price, @Stock)";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Brand", brand);
            command.Parameters.AddWithValue("@Category", category);
            command.Parameters.AddWithValue("@Price", price);
            command.Parameters.AddWithValue("@Stock", stock);

            command.ExecuteNonQuery();
        }

        public void Update(int id, string name, string brand, string category, decimal? price, int? stock)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = @"UPDATE Products SET
                          Name = COALESCE(NULLIF(@Name, ''), Name),
                          Brand = COALESCE(NULLIF(@Brand, ''), Brand),
                          Category = COALESCE(NULLIF(@Category, ''), Category),
                          Price = COALESCE(@Price, Price),
                          Stock = COALESCE(@Stock, Stock)
                          WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Brand", brand);
            command.Parameters.AddWithValue("@Category", category);
            command.Parameters.AddWithValue("@Price", (object?)price ?? DBNull.Value);
            command.Parameters.AddWithValue("@Stock", (object?)stock ?? DBNull.Value);

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = "DELETE FROM Products WHERE Id = @Id";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            command.ExecuteNonQuery();
        }

        public string? GetName(int id)
        {
            using var connection = new SqlConnection(MyConnection.connection);
            connection.Open();

            var query = "SELECT Name FROM Products WHERE Id = @Id";

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
                3 => "Brand LIKE '%' + @Data + '%'",
                _ => "Category LIKE '%' + @Data + '%'"
            };

            var query = "SELECT Id, Name, Brand, Category, Price, Stock FROM Products WHERE " + where;

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Data", data);

            using var reader = command.ExecuteReader();

            ShowRows(reader);
        }

        private void ShowRows(SqlDataReader reader)
        {
            bool found = false;

            while (reader.Read())
            {
                found = true;

                Console.WriteLine("----------------------");
                Console.WriteLine("ID: " + reader.GetInt32(0));
                Console.WriteLine("Name: " + reader.GetString(1));
                Console.WriteLine("Brand: " + reader.GetString(2));
                Console.WriteLine("Category: " + reader.GetString(3));
                Console.WriteLine("Price: " + reader.GetDecimal(4));
                Console.WriteLine("Stock: " + reader.GetInt32(5));
            }

            if (!found)
                Console.WriteLine("There are no products");
        }
    }
}
