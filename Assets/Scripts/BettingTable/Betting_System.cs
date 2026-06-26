using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Betting_System : MonoBehaviour
{
    [SerializeField] private Dictionary<int, ESlotColor> current_Bet;

    public float betting_amount { get; set; } = 50;
    [field:SerializeField] public TextMeshProUGUI betAmountText { get; set; }

    public Dictionary<int, List<int>> bets { get; set; } = new();
    public Dictionary<int, float> multiplier_bets { get; set; } = new();
    public Dictionary<int, float> amount_bets { get; set; } = new();

    [SerializeField] private AudioClip[] audios;

    [SerializeField] private List<AudioSource> audioPlayers;

    private void Start()
    {
        GameManager.Instance.OnGamePlayStateChanged.AddListener(Remove_Bets);
        betAmountText.text = betting_amount.ToString();
    }

    public void AddBetAmount(int amount)
    {
        float amountBetted = 0;
        foreach (var amountbet in amount_bets)
        {
            amountBetted += amountbet.Value;
        }
        float finalBet = amountBetted + betting_amount + amount;
        if (amount < 0)
        {
            finalBet = amount;
        }
        if (finalBet > Player.Instance.current_Money) 
        {
            betting_amount = Player.Instance.current_Money - amountBetted;
            betAmountText.text = betting_amount.ToString();
            return;
        }
        
        betting_amount += amount;
        betAmountText.text = betting_amount.ToString();
    }

    public void AllInBet()
    {
        float amountBetted = 0;
        foreach (var amountbet in amount_bets)
        {
            amountBetted += amountbet.Value;
        }
        betting_amount = Player.Instance.current_Money - amountBetted;
        betAmountText.text = betting_amount.ToString();
    }

    public void ClearBet()
    {
        betting_amount = 0;
        betAmountText.text = betting_amount.ToString();
    }

    public void add_bet(List<int> bet, float mult, int bet_index)
    {
        float amountBetted = 0;
        foreach (var amountbet in amount_bets)
        {
            amountBetted += amountbet.Value;
        }
        amountBetted += betting_amount;
        if (amountBetted > Player.Instance.current_Money) return;


        bets[bet_index] = bet;
        multiplier_bets[bet_index] = mult;

        if(amount_bets.ContainsKey(bet_index))
        {
            if(betting_amount != 0)
            {
                PlayAudio();
            }
            amount_bets[bet_index] += betting_amount;
            if (amount_bets[bet_index] <= 0)
            {
                bets.Remove(bet_index);
                multiplier_bets.Remove(bet_index);
                amount_bets.Remove(bet_index);
                return;
            }
            return;
        }
        amount_bets[bet_index] = betting_amount;
        if (amount_bets[bet_index] <= 0)
        {
            bets.Remove(bet_index);
            multiplier_bets.Remove(bet_index);
            amount_bets.Remove(bet_index);
            return;
        }
        PlayAudio();
    }

    private void Update()
    {
        if (audioPlayers.Count > 0)
        {
            for(int i = 0; i< audioPlayers.Count; i++)
            {
                if (!audioPlayers[i].isPlaying)
                {
                    Destroy(audioPlayers[i]);
                    audioPlayers.RemoveAt(i);
                }
            }
        }
    }

    private void PlayAudio()
    {
        AudioSource audioPlayer = gameObject.AddComponent<AudioSource>();
        audioPlayer.loop = false;
        audioPlayer.pitch = Random.Range(0.8f, 1.2f);
        audioPlayer.PlayOneShot(audios[Random.Range(0, audios.Length)]);
        audioPlayers.Add(audioPlayer);
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
