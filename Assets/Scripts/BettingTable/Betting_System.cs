using System.Collections.Generic;
using UnityEngine;

public class Betting_System : MonoBehaviour
{
    [SerializeField] private Dictionary<int, ESlotColor> current_Bet;


    public float betting_amount { get; set; } = 50;

    public Dictionary<int, List<int>> bets { get; set; } = new();
    public Dictionary<int, float> multiplier_bets { get; set; } = new();
    public Dictionary<int, float> amount_bets { get; set; } = new();

    private void Start()
    {
        GameManager.Instance.OnGamePlayStateChanged.AddListener(Remove_Bets);
    }

    public void add_bet(List<int> bet, float mult, int bet_index)
    {
        bets[bet_index] = bet;
        multiplier_bets[bet_index] = mult;
        if(amount_bets.ContainsKey(bet_index))
        {
            amount_bets[bet_index] += betting_amount;
            return;
        }
        amount_bets[bet_index] = betting_amount;
    }

    public bool bet_reds(ESlotColor color, int number)
    {
        if (color == ESlotColor.red)
        {
            return true;
        }
        return false;
    }

    public bool bet_blacks(ESlotColor color, int number)
    {
        if (color == ESlotColor.black)
        {
            return true;
        }
        return false;
    }

    public bool bet_even(ESlotColor color, int number)
    {
        if (number % 2 == 0)
        {
            return true;
        }
        return false;
    }

    public bool bet_odd(ESlotColor color, int number)
    {
        if (number % 2 == 0)
        {
            return false;
        }
        return true;
    }

    public bool bet_first_12(ESlotColor color, int number)
    {
        if (number <= 12 && number != 0)
        {
            return true;
        }
        return false;
    }
    public bool bet_second_12(ESlotColor color, int number)
    {
        if (number is > 12 and <= 24)
        {
            return true;
        }
        return false;
    }
    
    public bool bet_third_12(ESlotColor color, int number)
    {
        if (number is > 24 and <= 36)
        {
            return true;
        }
        return false;
    }

    public bool bet_number(ESlotColor color, int number)
    {
        if (number is > 24 and <= 36)
        {
            return true;
        }
        return false;
    }

    private void Remove_Bets(EGamePlayState state)
    {
        if (GameManager.Instance.GamePlayState != EGamePlayState.BetScreen) return;

        bets.Clear();
        multiplier_bets.Clear();
        amount_bets.Clear();
    }
}
