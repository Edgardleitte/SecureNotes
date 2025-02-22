// Helpers/ValidationHelper.cs
using System;
using System.Linq;
using System.Text.RegularExpressions;

public static class ValidationHelper
{
    public static bool IsValidUsername(string input)
    {
        return !string.IsNullOrEmpty(input) && input.All(c => char.IsLetterOrDigit(c));
    }

    public static bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public static bool IsValidNoteContent(string content)
    {
        return !Regex.IsMatch(content, "<.*?>"); // Prevents XSS
    }
}
