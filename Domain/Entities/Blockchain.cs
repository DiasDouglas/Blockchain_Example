using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Blockchain
    {
        public IList<Block> Chain { get; set; }
        public int ProofOfWorkDifficulty { get; set; } = 2;
        public IList<Transaction> PendingTransactions { get; set; }

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }

        /// <summary>
        /// Initializes the collection properties in the blockchain
        /// </summary>
        public void InitializeChain()
        {
            Chain = new List<Block>();
            PendingTransactions = new List<Transaction>();
        }

        /// <summary>
        /// Creates the first block in the blockchain (with the previous hash null)
        /// </summary>
        /// <returns>A Block representing the Genesis Block of a blockchain</returns>
        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.UtcNow, null);
        }

        /// <summary>
        /// Adds the first block to the blockchain
        /// </summary>
        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        /// <summary>
        /// Searches and returns the last block on the blockchain
        /// </summary>
        /// <returns>A Block, representing the last block in the blockchain</returns>
        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        /// <summary>
        /// Adds the block passed as a parameter to the blockchain
        /// </summary>
        /// <param name="block">The block to be added</param>
        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();

            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();
            block.Mine(this.ProofOfWorkDifficulty);
            Chain.Add(block);
        }

        /// <summary>
        /// Validates the blockchain
        /// </summary>
        /// <returns>A boolean, with the value indicating if the blockchain is valid or not</returns>
        public bool IsValid()
        {
            for (int i = 0; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.PreviousHash != previousBlock.Hash)
                    return false;

                if (currentBlock.Hash != currentBlock.CalculateHash())
                    return false;

            }

            return true;
        }
    }
}
