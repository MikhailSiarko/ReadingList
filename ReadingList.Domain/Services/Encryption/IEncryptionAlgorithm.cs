namespace ReadingList.Domain.Services.Encryption
{
    public interface IEncryptionAlgorithm
    {
        string Execute(string normalString);
    }
}