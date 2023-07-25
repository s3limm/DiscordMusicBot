using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordMusicBot.External_Classes
{
    public class CardBuilder
    {
        public int[] cardNumbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };
        public string[] cardSuits = { "Karo", "Maça", "Kupa", "Sinek" };

        public int SelectedNumber { get; internal set; }
        public string SelectedCard { get; internal set; }
        
        public CardBuilder(Random Random)
        {
            int numberIndex = Random.Next(0,this.cardNumbers.Length - 1);
            int suitIndex = Random.Next(0, this.cardSuits.Length - 1);

            this.SelectedNumber = this.cardNumbers.ElementAt(numberIndex);
            this.SelectedCard = this.cardSuits.ElementAt(suitIndex) + (" ") + this.cardNumbers.ElementAt(numberIndex);
        }




    }
}
