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

    public UnityEvent<EGamePlayState> OnGamePlayStateChanged { get; set; } = new UnityEvent<EGamePlayState>();
    public UnityEvent<EGameState> OnGameStateChanged { get; set; } = new UnityEvent<EGameState>();

    private void Start()
    {
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
                break;
            case EGamePlayState.Roulette:
                break;
            case EGamePlayState.Shop:
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
}