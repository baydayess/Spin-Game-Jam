using System.Collections.Generic;
using UnityEngine;

public class Bet_Red : Bet_Button
{
    private Stack<GameObject> Chips = new();
    [SerializeField] private GameObject chip;

    private void Start()
    {
        GameManager.Instance.OnGamePlayStateChanged.AddListener(ClearChips);
    }
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
        if (bet.amount_bets.ContainsKey(1))
        {
            PlaceChips(bet.amount_bets[1]);
        }
    }

    private void PlaceChips(float amount)
    {
        int number = (int)amount / 50;

        number -= Chips.Count;

        if (Chips.Count == 0)
        {
            GameObject newChip = Instantiate(chip);
            Transform pointA = transform;
            Transform pointB = newChip.transform.Find("Bottom");

            newChip.transform.position += pointA.position - pointB.position;
            Chips.Push(newChip);
            number -= 1;
        }
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
}
