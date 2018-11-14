namespace ReadingList.Domain.Services.Interfaces
{
    public interface IEncryptionAlgorithm
    {
        string Execute(string normalString);
    }
}