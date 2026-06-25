using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, ISingleton
{
    private float current_Money;

    public List<Item> Inventory {get; set;}

    [field: SerializeField] public ESlotColor[] numbers { get; set; }

    public Dictionary<int, List<int>> bets { get; set; } = new();
    public List<float> multiplier_bets { get; set; } = new();
    public List<float> amount_bets { get; set; } = new();

    private float item_multiplier = 1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mouse_Select();
    }

    void Confirm_Bet(int value)
    {
        for (int i = 0; i < bets.Count; i++)
        {
            for (int y = 0; i < bets[i].Count; i++)
            {
                if(bets[i][y] == value)
                {
                    Pay_Out(i);
                    break;
                }
            }
        }
    }

    void Check_Items(int number)
    {
        foreach (Item item in Inventory)
        {
            //item_multiplier += item.GetEffect(number, numbers[number])
        }
    }

    void Pay_Out(int bet_index)
    {
        current_Money += amount_bets[bet_index] * multiplier_bets[bet_index] * item_multiplier;
        item_multiplier = 1;
    }

    void Mouse_Select()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Bet_Button button = hit.collider.GetComponent<Bet_Button>();
                if (button != null)
                {
                    button.Press();
                }
            }
        }
    }
}
