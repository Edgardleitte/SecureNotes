// Services/AuthService.cs
using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

public class AuthService
{
    private const string ConnectionString = "Data Source=database.sqlite;Version=3;";

    public static bool RegisterUser(string username, string email, string password)
    {
        if (!ValidationHelper.IsValidUsername(username) || !ValidationHelper.IsValidEmail(email))
            return false;

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            string query = "INSERT INTO Users (Username, Email, PasswordHash, Role) VALUES (@Username, @Email, @PasswordHash, 'User')";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PasswordHash", hashedPassword);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
