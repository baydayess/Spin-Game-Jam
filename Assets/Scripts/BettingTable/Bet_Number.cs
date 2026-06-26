using System.Collections.Generic;
using UnityEngine;

public class Bet_Number : Bet_Button
{
    [SerializeField] private int number;
    override public void Press()
    {
        if (GameManager.Instance.GamePlayState != EGamePlayState.BetScreen) return;

        ESlotColor[] numbers = FindFirstObjectByType<Player>().numbers;
        Betting_System bet = FindFirstObjectByType<Betting_System>();
        List<int> final_bet = new List<int>();

        final_bet.Add(number);
        bet.add_bet(final_bet, 36, 20 + number);
    }
}
