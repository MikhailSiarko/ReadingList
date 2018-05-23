namespace ReadingList.Domain.Services.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IEncryptionAlgorithm _encryptionAlgorithm;

        public EncryptionService(IEncryptionAlgorithm encryptionAlgorithm)
        {
            _encryptionAlgorithm = encryptionAlgorithm;
        }

        public string Encrypt(string normalString)
        {
            return _encryptionAlgorithm.Execute(normalString);
        }
    }
}