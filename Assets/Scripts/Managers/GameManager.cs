using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public enum EGameState
{
    MainMenu,
    Pause,
    Gameplay
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

    public UnityEvent<EGamePlayState> OnGamePlayStateChanged { get; set; } = new UnityEvent<EGamePlayState>();
    public UnityEvent<EGameState> OnGameStateChanged { get; set; } = new UnityEvent<EGameState>();

    private void Start()
    {
        goToBetScreenButton.SetActive(false);
        seeRouletteButton.SetActive(false);

        prevGamePlayState = GamePlayState;
        prevGameState = GameState;
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
                Shop.Instance.CloseShop();
                goToBetScreenButton.SetActive(false);
                seeRouletteButton.SetActive(true);
                break;
            case EGamePlayState.Roulette:
                seeRouletteButton.SetActive(false);
                checkShopButton.SetActive(true);
                break;
            case EGamePlayState.Shop:
                checkShopButton.SetActive(false);
                goToBetScreenButton.SetActive(true);
                Shop.Instance.OpenShop();
                break;
        }
    }

    private void GameStateChanged(EGameState state)
    {
        switch (state)
        {
            case EGameState.MainMenu:
                break;
            case EGameState.Gameplay:
                break;
            case EGameState.Pause:
                break;
        }
    }
    
    public void ChangeGamePlayState(int state)
    {
        GamePlayState = (EGamePlayState) state;
    }
}