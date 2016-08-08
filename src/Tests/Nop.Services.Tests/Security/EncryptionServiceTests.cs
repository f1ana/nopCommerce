﻿using Nop.Core.Domain.Security;
using Nop.Services.Security;
using Nop.Tests;
using NUnit.Framework;

namespace Nop.Services.Tests.Security
{
    [TestFixture]
    public class EncryptionServiceTests : ServiceTest
    {
        private IEncryptionService _encryptionService;
        private SecuritySettings _securitySettings;

        [SetUp]
        public new void SetUp() 
        {
            _securitySettings = new SecuritySettings
            {
                EncryptionKey = "273ece6f97dd844d",
                SaltKeySize = 16
            };
            _encryptionService = new EncryptionService(_securitySettings);
        }

        [Test]
        public void Can_hash() 
        {
            string password = "MyLittleSecret";
            var saltKey = "salt1";
            var hashedPassword = _encryptionService.CreatePasswordHash(password, saltKey);
            hashedPassword.ShouldEqual("A07A9638CCE93E48E3F26B37EF7BDF979B8124D6");
        }

        [Test]
        public void Can_hash_SHA256() {
            string password = "MyLittleSecret";
            var saltKey = "salt1";
            var hashedPassword = _encryptionService.CreatePasswordHash(password, saltKey, "SHA256");
            hashedPassword.ShouldEqual("4506D65FDB6F3A8CF97278AB7C5C62DEC35EADD474BE1E6243776691D56E1B27F71C1D9085B26BD7513BED89822204D6B8FCBD6E665D46558C48F56D21B2A293");
        }


        [Test]
        public void Can_hash_SHA512() {
            string password = "MyLittleSecret";
            var saltKey = "salt1";
            var hashedPassword = _encryptionService.CreatePasswordHash(password, saltKey, "SHA512");
            hashedPassword.ShouldEqual("4506D65FDB6F3A8CF97278AB7C5C62DEC35EADD474BE1E6243776691D56E1B27F71C1D9085B26BD7513BED89822204D6B8FCBD6E665D46558C48F56D21B2A293");
        }

        [Test]
        public void Can_encrypt_and_decrypt_default() 
        {
            var password = "MyLittleSecret";
            string encryptedPassword = _encryptionService.EncryptText(password);
            var decryptedPassword = _encryptionService.DecryptText(encryptedPassword);
            decryptedPassword.ShouldEqual(password);
        }

        [Test]
        public void Can_encrypt_and_decrypt_3DESExplicit() {
            _securitySettings = new SecuritySettings {
                EncryptionKey = "273ece6f97dd844d",
                SaltKeySize = 16,
                EncryptionFormat = EncryptionFormat.TripleDes
            };
            _encryptionService = new EncryptionService(_securitySettings);

            var password = "MyLittleSecret";
            string encryptedPassword = _encryptionService.EncryptText(password);
            var decryptedPassword = _encryptionService.DecryptText(encryptedPassword);
            decryptedPassword.ShouldEqual(password);
        }

        [Test]
        public void Can_encrypt_and_decrypt_AES128() {
            _securitySettings = new SecuritySettings {
                EncryptionKey = "273ece6f97dd844d273ece6f97dd844d",
                SaltKeySize = 16,
                EncryptionFormat = EncryptionFormat.Aes128
            };
            _encryptionService = new EncryptionService(_securitySettings);

            var password = "MyLittleSecret";
            string encryptedPassword = _encryptionService.EncryptText(password);
            var decryptedPassword = _encryptionService.DecryptText(encryptedPassword);
            decryptedPassword.ShouldEqual(password);
        }
        
        [Test]
        public void Can_encrypt_and_decrypt_AES256() {
            _securitySettings = new SecuritySettings {
                EncryptionKey = "273ece6f97dd844d273ece6f97dd844d",
                SaltKeySize = 16,
                EncryptionFormat = EncryptionFormat.Aes256
            };
            _encryptionService = new EncryptionService(_securitySettings);

            var password = "MyLittleSecret";
            string encryptedPassword = _encryptionService.EncryptText(password);
            var decryptedPassword = _encryptionService.DecryptText(encryptedPassword);
            decryptedPassword.ShouldEqual(password);
        }

    }
}
