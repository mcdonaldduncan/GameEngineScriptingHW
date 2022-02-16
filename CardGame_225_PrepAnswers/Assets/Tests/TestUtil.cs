using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Tests
{
    class TestUtil
    {
        static public void ResetCards(Dealer dealer)
        {
            Card initCard = dealer.deck.First();
            var allSprites = dealer.allSprites;
            Sprite topCard = initCard.sprite;
            for (int i = 0; i < allSprites.Count; i++)
            {
                allSprites[i].sprite = topCard;
            }
        }
    }
}
