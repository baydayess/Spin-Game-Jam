using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    private void Start()
    {
        StartCoroutine(WaitToChangeCamera());
    }

    private IEnumerator WaitToChangeCamera()
    {
        while (true) 
        {
            yield return new WaitForSeconds(3);
            CameraState.Instance.ChangeCameraDirection(EGamePlayState.Shop);
            yield return new WaitForSeconds(3);
            CameraState.Instance.ChangeCameraDirection(EGamePlayState.BetScreen);
            yield return new WaitForSeconds(3);
            CameraState.Instance.ChangeCameraDirection(EGamePlayState.Roulette);
        }
    }
}