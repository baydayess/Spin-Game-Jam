using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : Singleton<Player>
{
    [field: SerializeField] public float current_Money { get; private set; } = 500;

    [field: SerializeField] public TextMeshProUGUI money_text { get;  set; }

    [field: SerializeField] public int ballAmount { get; set; } = 1;

    [field:SerializeField] public List<Item> Inventory {get; set;}

    [field: SerializeField] public ESlotColor[] numbers { get; set; }
    [field: SerializeField] public int maxRolls { get; private set; } = 3;
    [field: SerializeField] public int CurrentRolls { get; set; } = 3;

    [field: SerializeField] public float currentQuota { get; private set; } = 0;

    public Dictionary<int, List<int>> bets { get; set; } = new();
    public Dictionary<int, float> multiplier_bets { get; set; } = new();
    public Dictionary<int, float> amount_bets { get; set; } = new();

    private float item_multiplier = 1;

    private UnityEvent money_changed = new UnityEvent();

    private Outline outlineObj;

    [SerializeField] public GameObject textObj;
    private GameObject currentTextObj;

    [SerializeField] private GameObject moneyChanged;
    [SerializeField] private GameObject Canvas;

    void Start()
    {
        money_changed.AddListener(Update_Money);
        money_changed.Invoke();
        GameManager.Instance.OnGamePlayStateChanged.AddListener(Remove_Bets);
    }
    
    void Update()
    {
        Mouse_Select();
    }

    public void Confirm_Bet(int value)
    {
        foreach (var bet in bets)
        {
            for (int y = 0; y < bet.Value.Count; y++)
            {
                if(bet.Value[y] == value)
                {
                    Check_Items(value);
                    Pay_Out(bet.Key);
                    break;
                }
            }
        }
    }

    void Check_Items(int number)
    {
        foreach (Item item in Inventory)
        {
            item_multiplier += item.GetEffect(number, numbers[number]);
        }
    }

    void Pay_Out(int bet_index)
    {
        float moneyEarned = amount_bets[bet_index] * multiplier_bets[bet_index] * item_multiplier;
        current_Money += moneyEarned;
        currentQuota += moneyEarned;
        GameObject text = Instantiate(moneyChanged, Canvas.transform);
        text.GetComponent<TextMeshProUGUI>().text = "+" + moneyEarned;
        item_multiplier = 1;
        money_changed.Invoke();
    }

    void Mouse_Select()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Bet_Button button = hit.collider.GetComponent<Bet_Button>();
            if (button != null)
            {
                if (!currentTextObj)
                {
                    float amount = button.Return_Bet();
                    if (amount > 0)
                    {
                        currentTextObj = Instantiate(textObj, hit.transform.position + new Vector3(0, 0, 6.5f), Quaternion.identity);
                        currentTextObj.GetComponent<Bet_Text>().amount = amount;
                        //currentTextObj.transform.position = button.transform.position;
                    }
                }
                if (Mouse.current.leftButton.wasPressedThisFrame)
                    button.Press();
            }
            else
            {
                if (currentTextObj) Destroy(currentTextObj.gameObject);
            }

            Outline outline = hit.collider.GetComponent<Outline>();
            if (outline != null)
            {
                if (outlineObj != null && outlineObj != outline) 
                {
                    outlineObj.OutlineColor = Color.white;
                    outlineObj.enabled = false;
                    if (currentTextObj) Destroy(currentTextObj.gameObject);
                }

                outlineObj = outline;
                outline.enabled = true;
                
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    outline.OutlineColor = Color.red;
                    if (currentTextObj) Destroy(currentTextObj.gameObject);
                }
                else if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    outline.OutlineColor = Color.white;
                    if (!currentTextObj)
                    {
                        float amount = button.Return_Bet();
                        if (amount > 0)
                        {
                            currentTextObj = Instantiate(textObj, hit.transform.position + new Vector3(0,0,6.5f), Quaternion.identity);
                            currentTextObj.GetComponent<Bet_Text>().amount = amount;
                            //currentTextObj.transform.position = button.transform.position;
                        }
                    }
                }
            }
        }
        else
        {
            if (currentTextObj) Destroy(currentTextObj.gameObject);
            if (outlineObj != null)
            {
                outlineObj.OutlineColor = Color.white;
                outlineObj.enabled = false;
                outlineObj = null;
            }
        }
    }

    public void Remove_Money(float amount)
    {
        current_Money -= amount;
        currentQuota -= amount;
        GameObject text = Instantiate(moneyChanged, Canvas.transform);
        text.GetComponent<TextMeshProUGUI>().color = Color.red;
        text.GetComponent<TextMeshProUGUI>().text = "-" + amount;
        money_changed.Invoke();
    }

    public float Check_Money()
    {
        return current_Money;
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
    
    public void ResetQuota()
    {
        currentQuota = 0;
    }
}
