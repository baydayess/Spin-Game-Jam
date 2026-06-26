using System.Collections.Generic;
using UnityEngine;

public class Bet_Number : Bet_Button
{
    private Stack<GameObject> Chips = new();
    [SerializeField] private GameObject chip;

    private void Start()
    {
        GameManager.Instance.OnGamePlayStateChanged.AddListener(ClearChips);
    }

    [SerializeField] private int number;
    override public void Press()
    {
        if (GameManager.Instance.GamePlayState != EGamePlayState.BetScreen) return;

        ESlotColor[] numbers = FindFirstObjectByType<Player>().numbers;
        Betting_System bet = FindFirstObjectByType<Betting_System>();
        List<int> final_bet = new List<int>();

        final_bet.Add(number);
        bet.add_bet(final_bet, 36, 20 + number);
        if (bet.amount_bets.ContainsKey(20 + number))
        {
            PlaceChips(bet.amount_bets[20 + number]);
        }
        else
        {
            ClearChips(EGamePlayState.BetScreen);
        }
    }

    private void PlaceChips(float amount)
    {
        int number = (int)amount / 50;

        number -= Chips.Count;

        if (number < 0)
        {
            while (number < 0)
            {
                Destroy(Chips.Peek().gameObject);
                Chips.Pop();
                number++;
            }
            return;
        }

        if (Chips.Count == 0)
        {
            GameObject newChip = Instantiate(chip);
            Transform pointA = transform;
            Transform pointB = newChip.transform.Find("Bottom");

            newChip.transform.position += pointA.position - pointB.position;
            Chips.Push(newChip);
            number -= 1;
        }

        if (number > 5000) number = 5000;

        for (int i = 0; i < number; i++)
        {
            GameObject newChip = Instantiate(chip);
            Transform pointA = Chips.Peek().transform.Find("Top");
            Transform pointB = newChip.transform.Find("Bottom");

            newChip.transform.position += pointA.position - pointB.position;
            Chips.Push(newChip);
        }
    }

    private void ClearChips(EGamePlayState state)
    {
        if (GameManager.Instance.GamePlayState != EGamePlayState.BetScreen) return;
        while (Chips.Count > 0)
        {
            Destroy(Chips.Peek().gameObject);
            Chips.Pop();
        }
    }

    override public float Return_Bet()
    {
        Betting_System bet = FindFirstObjectByType<Betting_System>();
        return bet.GetMyBet(20 + number);
    }
}
