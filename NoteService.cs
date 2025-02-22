// Services/NoteService.cs
public class NoteService
{
    public static bool AddNote(string username, string content)
    {
        if (!ValidationHelper.IsValidNoteContent(content))
            return false;

        int userId = GetUserId(username);
        if (userId == -1)
            return false;

        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            string query = "INSERT INTO Notes (UserID, Content) VALUES (@UserID, @Content)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@UserID", userId);
                command.Parameters.AddWithValue("@Content", content);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }

    private static int GetUserId(string username)
    {
        using (var connection = new SQLiteConnection(ConnectionString))
        {
            connection.Open();
            string query = "SELECT UserID FROM Users WHERE Username = @Username";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                object result = command.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : -1;
            }
        }
    }
}
