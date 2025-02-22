// Tests/SecurityTests.cs
using NUnit.Framework;

[TestFixture]
public class SecurityTests
{
    [Test]
    public void TestSQLInjection()
    {
        string maliciousInput = "'; DROP TABLE Users; --";
        Assert.IsFalse(AuthService.RegisterUser(maliciousInput, "test@example.com", "password123"));
    }

    [Test]
    public void TestXSSPrevention()
    {
        string xssAttempt = "<script>alert('Hacked');</script>";
        Assert.IsFalse(ValidationHelper.IsValidNoteContent(xssAttempt));
    }

    [Test]
    public void TestValidLogin()
    {
        AuthService.RegisterUser("secureUser", "user@example.com", "SecurePass1!");
        string token = AuthService.LoginUser("secureUser", "SecurePass1!");
        Assert.IsNotNull(token);
    }
}
