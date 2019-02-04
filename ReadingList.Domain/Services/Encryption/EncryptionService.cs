using ReadingList.Domain.Services.Interfaces;

namespace ReadingList.Domain.Services.Encryption
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IEncryptionAlgorithm _encryptionAlgorithm;

        public EncryptionService(IEncryptionAlgorithm encryptionAlgorithm)
        {
            _encryptionAlgorithm = encryptionAlgorithm;
        }

        public EncryptionService()
        {
            _encryptionAlgorithm = new DefaultEncryptionAlgorithm();
        }

        public string Encrypt(string normalString)
        {
            return _encryptionAlgorithm.Execute(normalString);
        }
    }
}