namespace ContactBookApp
{
    public static class MyConnection
    {
        public static string connection =>
            "server = localhost; Database = ContactBook; Integrated Security = true; TrustServerCertificate = True;";
    }
}