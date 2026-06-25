using System.Collections.Generic;
using UnityEngine;

public class Bet_Black : Bet_Button
{
    override public void Press()
    {
        ESlotColor[] numbers = FindFirstObjectByType<Player>().numbers;
        Betting_System bet = FindFirstObjectByType<Betting_System>();
        List<int> final_bet = new List<int>();

        for (int i = 0; i < numbers.Length; i++)
        {
            if (bet.bet_blacks(numbers[i], i))
            {
                final_bet.Add(i);
            }
        }
        bet.add_bet(final_bet, 2, 0);
    }
}
