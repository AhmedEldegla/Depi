namespace DEPI.Domain.Common.Interfaces;

public interface IPasswordService
{
    (string Hash, string Salt) HashPassword(string plainText);
    bool VerifyPassword(string plainText, string hash, string salt);
    string GenerateResetToken();
}
