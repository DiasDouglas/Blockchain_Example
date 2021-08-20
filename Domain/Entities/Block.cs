using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public int Nonce { get; set; } = 0;

        public Block(DateTime timestamp, string previousHash)
        {
            Index = 0;
            Timestamp = timestamp;
            PreviousHash = previousHash;
            Hash = CalculateHash();
        }

        /// <summary>
        /// Calculates a hash for the block
        /// </summary>
        /// <returns>A string representing the hash of the block's properties</returns>
        public string CalculateHash()
        {
            SHA256 sha256 = SHA256.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes($"{Timestamp}-{PreviousHash ?? ""}-" +
                $"{JsonConvert.SerializeObject(Transactions)}-{Nonce}");
            byte[] outputBytes = sha256.ComputeHash(inputBytes);

            return Convert.ToBase64String(outputBytes);
        }

        /// <summary>
        /// Tries to find a hash that meets the difficulty parameter (hash should start with a number of leading zeroes,
        /// defined by the difficulty parameter).
        /// </summary>
        /// <param name="difficulty"> The number of leading zeroes that the hash should start with </param>
        public void Mine(int difficulty)
        {
            var leadingZeroes = new string('0', difficulty);

            while (this.Hash == null || this.Hash.Substring(0, difficulty) != leadingZeroes)
            {
                this.Nonce++;
                this.Hash = this.CalculateHash();
            }
        }
    }
}
