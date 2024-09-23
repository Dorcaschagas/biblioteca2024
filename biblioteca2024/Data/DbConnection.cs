using System.Data.SQLite;


namespace biblioteca2024{
    public class DbConnection{
        private static SQLiteConnection sqLiteConnection;
        private static String path = Directory.GetCurrentDirectory() + "\\biblioteca2024.db";

        public static SQLiteConnection GetConnection()
        {
            sqLiteConnection = new SQLiteConnection("Data source=" + path);
            sqLiteConnection.Open();
            return sqLiteConnection;
        }
    }
}