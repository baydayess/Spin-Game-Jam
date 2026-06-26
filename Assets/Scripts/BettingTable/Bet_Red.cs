using System.Collections.Generic;
using UnityEngine;

public class Bet_Red : Bet_Button
{
    override public void Press()
    {
        if (GameManager.Instance.GamePlayState != EGamePlayState.BetScreen) return;

        ESlotColor[] numbers = FindFirstObjectByType<Player>().numbers;
        Betting_System bet = FindFirstObjectByType<Betting_System>();
        List<int> final_bet = new List<int>();

        for (int i = 0; i < numbers.Length; i++)
        {
            if (bet.bet_reds(numbers[i], i))
            {
                final_bet.Add(i);
            }
        }
        bet.add_bet(final_bet, 2, 1);
    }
}
