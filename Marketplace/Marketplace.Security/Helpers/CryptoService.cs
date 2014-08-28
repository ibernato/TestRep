using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Security.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Crypto service that uses PBKDF2 function implemented by <see cref="Rfc2898DeriveBytes"/> class.
    /// It uses different random salt for each new computation.
    /// </summary>
    internal class CryptoService
    {
        public CryptoService()
        {
            HashIterations = 5000;
            SaltSize = 16;  // in bytes
        }

        public int HashIterations { get; set; }

        public int SaltSize { get; set; }

        public string PlainText { get; set; }

        public string HashedText { get; private set; }

        public string Salt { get; private set; }

        /// <summary>
        /// Computes hash of the text set in PlainText property, and use the result to set HashedText property.
        /// </summary>
        /// <returns>hash of the text set in PlainText property</returns>
        public string Compute()
        {
            if (string.IsNullOrEmpty(PlainText)) throw new InvalidOperationException("null text");

            //if there is no salt, generate one
            if (string.IsNullOrEmpty(Salt))
                generateSalt();

            HashedText = calculateHash(HashIterations);

            return HashedText;
        }

        /// <summary>
        /// Computes hash of the given text and set is to HashedText property.
        /// </summary>
        /// <param name="textToHash">text to hash</param>
        /// <returns>hash of the given text</returns>
        public string Compute(string textToHash)
        {
            PlainText = textToHash;
            //generate salt
            generateSalt();
            //compute hash
            Compute();
            return HashedText;
        }

        /// <summary>
        /// Computes hash of the given text, using the given salt size and given number of iterations.
        /// </summary>
        /// <param name="textToHash">text to hash</param>
        /// <param name="saltSize">size of the salt used by hash algorithm</param>
        /// <param name="hashIterations">number of iterations used by hash algorithm</param>
        /// <returns>hash of the given text</returns>
        public string Compute(string textToHash, int saltSize, int hashIterations)
        {
            PlainText = textToHash;
            HashIterations = hashIterations;
            SaltSize = saltSize;
            //generate salt
            generateSalt();
            //compute hash
            Compute();
            return HashedText;
        }

        /// <summary>
        /// Computes hash of the given text, using the given salt.
        /// </summary>
        /// <param name="textToHash">text to hash</param>
        /// <param name="salt">salt to be used by the hash algoritm</param>
        /// <returns>hash of the given text</returns>
        public string Compute(string textToHash, string salt)
        {
            PlainText = textToHash;
            Salt = salt;
            //expand salt
            expandSalt();
            Compute();
            return HashedText;
        }

        /// <summary>
        /// Calculates time spent by the algoritm to produce hash. Used for testing purposes.
        /// </summary>
        /// <param name="iteration">number of iterations used by hash algoritm</param>
        /// <returns></returns>
        public int GetElapsedTimeForIteration(int iteration)
        {
            var sw = new Stopwatch();
            sw.Start();
            calculateHash(iteration);
            return (int)sw.ElapsedMilliseconds;

        }

        private string calculateHash(int iteration)
        {
            //convert salt into a byte array
            byte[] saltBytes = Encoding.UTF8.GetBytes(Salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(PlainText, saltBytes, iteration))
            {
                var key = pbkdf2.GetBytes(64);
                return Convert.ToBase64String(key);
            }
        }

        private void generateSalt()
        {
            if (SaltSize < 1)
            {
                throw new InvalidOperationException(string.Format("invalid size {0}", SaltSize));
            }

            var rand = RandomNumberGenerator.Create();
            var ret = new byte[SaltSize];

            rand.GetBytes(ret);

            //assign the generated salt in the format of {iterations}.{salt}
            Salt = string.Format("{0}.{1}", HashIterations, Convert.ToBase64String(ret));
        }

        private void expandSalt()
        {
            try
            {
                //get position of the dot that splits the string
                var i = Salt.IndexOf('.');

                //Get hash iteration from the first index
                HashIterations = int.Parse(Salt.Substring(0, i), System.Globalization.NumberStyles.Number);
            }
            catch (Exception ex)
            {
                throw new FormatException("wrong format", ex);
            }
        }

    }

}
