using System.Collections.Generic;
using UnityEngine;

public class Betting_System : MonoBehaviour
{
    [SerializeField] private Dictionary<int, ESlotColor> current_Bet;

    bool bet_reds(ESlotColor color, int number)
    {
        if (color == ESlotColor.red)
        {
            return true;
        }
        return false;
    }    
    
    bool bet_blacks(ESlotColor color, int number)
    {
        if (color == ESlotColor.black)
        {
            return true;
        }
        return false;
    }
    
    bool bet_even(ESlotColor color, int number)
    {
        if (number % 2 == 0)
        {
            return true;
        }
        return false;
    }
    
    bool bet_odd(ESlotColor color, int number)
    {
        if (number % 2 == 0)
        {
            return false;
        }
        return true;
    }

    bool bet_first_12(ESlotColor color, int number)
    {
        if (number <= 12 && number != 0)
        {
            return true;
        }
        return false;
    }
    bool bet_second_12(ESlotColor color, int number)
    {
        if (number is > 12 and <= 24)
        {
            return true;
        }
        return false;
    }
    
    bool bet_third_12(ESlotColor color, int number)
    {
        if (number is > 24 and <= 36)
        {
            return true;
        }
        return false;
    }
}
