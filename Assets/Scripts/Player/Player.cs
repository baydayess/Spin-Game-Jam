using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Player : Singleton<Player>
{
    private float current_Money = 500;

    [SerializeField] TextMeshProUGUI money_text;

    [field: SerializeField] public int ballAmount { get; set; } = 1;

    [field:SerializeField] public List<Item> Inventory {get; set;}

    [field: SerializeField] public ESlotColor[] numbers { get; set; }

    public Dictionary<int, List<int>> bets { get; set; } = new();
    public Dictionary<int, float> multiplier_bets { get; set; } = new();
    public Dictionary<int, float> amount_bets { get; set; } = new();

    private float item_multiplier = 1;

    private UnityEvent money_changed = new UnityEvent();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        money_changed.AddListener(Update_Money);
        money_changed.Invoke();
        GameManager.Instance.OnGamePlayStateChanged.AddListener(Remove_Bets);
    }

    // Update is called once per frame
    void Update()
    {
        Mouse_Select();
    }

    public void Confirm_Bet(int value)
    {
        for (int i = 0; i < bets.Count; i++)
        {
            for (int y = 0; y < bets[i].Count; y++)
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
        money_changed.Invoke();
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

    public void Remove_Money(float amount)
    {
        current_Money -= amount;
        money_changed.Invoke();
    }

    private void Update_Money()
    {
        money_text.text = "Money: " + current_Money;
    }

    private void Remove_Bets(EGamePlayState state)
    {
        if (GameManager.Instance.GamePlayState != EGamePlayState.BetScreen) return;

        bets.Clear();
        multiplier_bets.Clear();
        amount_bets.Clear();
    }
}
