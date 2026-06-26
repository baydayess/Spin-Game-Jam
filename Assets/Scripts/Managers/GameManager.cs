using System;
using System.Collections;
using System.Runtime.CompilerServices;
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
    [field: SerializeField] public int Round { get; private set; } = 0;

    public UnityEvent<EGamePlayState> OnGamePlayStateChanged { get; set; } = new UnityEvent<EGamePlayState>();
    public UnityEvent<EGameState> OnGameStateChanged { get; set; } = new UnityEvent<EGameState>();

    private void Start()
    {
        goToBetScreenButton.SetActive(false);

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
                goToBetScreenButton.SetActive(false);
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