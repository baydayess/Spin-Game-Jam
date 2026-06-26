using System;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
    private EGamePlayState prevGamePlayState;

    [SerializeField] private GameObject seeRouletteButton;
    [SerializeField] private GameObject checkShopButton;
    [SerializeField] private GameObject goToBetScreenButton;

    [field: SerializeField] public int Quota { get; private set; } = 200;
    [field: SerializeField] public int Round { get; private set; } = 0;

    public UnityEvent<EGamePlayState> OnGamePlayStateChanged { get; set; } = new UnityEvent<EGamePlayState>();
    public UnityEvent<EGameState> OnGameStateChanged { get; set; } = new UnityEvent<EGameState>();

    private void Start()
    {
        goToBetScreenButton.SetActive(false);
        seeRouletteButton.SetActive(false);

        OnGamePlayStateChanged.AddListener(GamePlayStateChanged);
        OnGameStateChanged.AddListener(GameStateChanged);
    }

    private void LateUpdate()
    {
        if (GamePlayState != prevGamePlayState)
        {
            OnGamePlayStateChanged.Invoke(GamePlayState);
            prevGamePlayState = GamePlayState;
        }

        if (GameState != prevGameState)
        {
            OnGameStateChanged.Invoke(GameState);
            prevGameState = GameState;
        }
    }

    private void GamePlayStateChanged(EGamePlayState state)
    {
        switch (state)
        {
            case EGamePlayState.BetScreen:
                if (Shop.Instance.IsShopOpen) 
                { 
                    Shop.Instance.CloseShop();
                    IncreaseQuota();
                    Player.Instance.CurrentRolls = Player.Instance.maxRolls;
                    Round++;
                }
                else if(Player.Instance.CurrentRolls <= 0 && Player.Instance.currentQuota >= Quota)
                {
                    GamePlayState = EGamePlayState.Shop;
                }
                else if(Player.Instance.CurrentRolls <= 0 && Player.Instance.currentQuota <= Quota)
                {
                    GameState = EGameState.GameOver;
                }
                goToBetScreenButton.SetActive(false);
                seeRouletteButton.SetActive(true);
                break;
            case EGamePlayState.Roulette:
                Player.Instance.CurrentRolls--;
                seeRouletteButton.SetActive(false);
                checkShopButton.SetActive(true);
                break;
            case EGamePlayState.Shop:
                Player.Instance.ResetQuota();
                checkShopButton.SetActive(false);
                goToBetScreenButton.SetActive(true);
                Shop.Instance.OpenShop();
                break;
        }
    }

    private void IncreaseQuota()
    {
        Quota = (int) (200 + (500 * Round * Mathf.Pow(Random.Range(0.8f, 0.5f), Round - 1)));
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