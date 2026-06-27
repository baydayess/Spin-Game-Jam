using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum EGameState
{
    MainMenu,
    Gameplay,
    GameOver
}

public enum EGamePlayState
{
    BetScreen,
    Roulette,
    Shop
}

public class GameManager : Singleton<GameManager>
{
    [field:SerializeField] public EGameState GameState { get; private set; }
    private EGameState prevGameState;
    [field: SerializeField] public EGamePlayState GamePlayState { get; private set; }
    private EGamePlayState prevGamePlayState = EGamePlayState.Shop;

    [SerializeField] private GameObject goToBetScreenButton;

    [field: SerializeField] public int Quota { get; private set; } = 200;
    [SerializeField] private TextMeshProUGUI quotaText;
    [SerializeField] private TextMeshProUGUI currentQuotaText;
    [field: SerializeField] public int Round { get; private set; } = 0;
    [SerializeField] private TextMeshProUGUI roundText;

    public UnityEvent<EGamePlayState> OnGamePlayStateChanged { get; set; } = new UnityEvent<EGamePlayState>();
    public UnityEvent<EGameState> OnGameStateChanged { get; set; } = new UnityEvent<EGameState>();

    private void Start()
    {
        goToBetScreenButton.SetActive(false);
        quotaText.text = $"{Quota}$";
        OnGamePlayStateChanged.AddListener(GamePlayStateChanged);
        OnGameStateChanged.AddListener(GameStateChanged);
    }

    private void LateUpdate()
    {
        if (GamePlayState != prevGamePlayState)
        {
            prevGamePlayState = GamePlayState;
            OnGamePlayStateChanged.Invoke(GamePlayState);
        }

        if (GameState != prevGameState)
        {
            prevGameState = GameState;
            OnGameStateChanged.Invoke(GameState);
        }
    }

    private void GamePlayStateChanged(EGamePlayState state)
    {
        switch (state)
        {
            case EGamePlayState.BetScreen:
                goToBetScreenButton.SetActive(false);

                if(Player.Instance.Check_Money() <= 0) 
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

                if (Shop.Instance.IsShopOpen) 
                {
                    Shop.Instance.CloseShop();
                    Player.Instance.ResetQuota();
                    Round++;
                    IncreaseQuota();
                    Player.Instance.CurrentRolls = Player.Instance.maxRolls;
                }
                else if(Player.Instance.CurrentRolls <= 0 && Player.Instance.currentQuota >= Quota)
                {
                    GamePlayState = EGamePlayState.Shop;
                    OnGamePlayStateChanged.Invoke(GamePlayState);
                }
                else if(Player.Instance.CurrentRolls <= 0 && Player.Instance.currentQuota <= Quota)
                {
                    GameState = EGameState.GameOver;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

                if (Player.Instance.currentQuota >= Quota) currentQuotaText.color = Color.green;
                else currentQuotaText.color = Color.red;
                currentQuotaText.text = $"{Player.Instance.currentQuota}$";
                break;
            case EGamePlayState.Roulette:
                Player.Instance.CurrentRolls--;
                break;
            case EGamePlayState.Shop:
                goToBetScreenButton.SetActive(true);
                Shop.Instance.OpenShop();
                break;
        }
    }

    private void IncreaseQuota()
    {
        Quota = (int) (200 + (1000 * Round * Mathf.Pow(Random.Range(1f, 2f), Round)));
        quotaText.text = $"{Quota}$";
        roundText.text = $"Round {Round}$";
    }

    private void GameStateChanged(EGameState state)
    {
        switch (state)
        {
            case EGameState.MainMenu:
                break;
            case EGameState.Gameplay:
                break;
            case EGameState.GameOver:
                break;
        }
    }
    
    public void ChangeGamePlayState(int state)
    {
        GamePlayState = (EGamePlayState) state;
    }
}