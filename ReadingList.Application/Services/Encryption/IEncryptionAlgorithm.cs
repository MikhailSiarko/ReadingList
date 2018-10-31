namespace ReadingList.Application.Services.Encryption
{
    public interface IEncryptionAlgorithm
    {
        string Execute(string normalString);
    }
}