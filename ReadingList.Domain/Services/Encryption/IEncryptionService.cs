namespace ReadingList.Domain.Services.Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string normalString);
    }
}