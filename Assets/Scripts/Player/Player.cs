using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, ISingleton
{
    private int current_Money;

    public List<Item> Inventory {get; set;}

    private ESlotColor[] numbers;

    private Dictionary<int, int[]> bets;

    private int[] multiplier_bets;

    private int[] amount_bets;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Confirm_Bet(int value)
    {
        for (int i = 0; i < bets.Count; i++)
        {
            for (int y = 0; i < bets[i].Length; i++)
            {
                if(bets[i][y] == value)
                {
                    Pay_Out(i);
                    break;
                }
            }
        }
    }

    void Pay_Out(int bet_index)
    {
        current_Money += amount_bets[bet_index] * multiplier_bets[bet_index];
    }
}
