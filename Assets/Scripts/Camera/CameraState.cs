using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraState : Singleton<CameraState>
{
    [SerializeField] private Animator anim;

    private void Start()
    {
        GameManager.Instance.OnGamePlayStateChanged.AddListener(ChangeCameraDirection);
    }

    public void ChangeCameraDirection(EGamePlayState state)
    {
        switch (state)
        {
            case EGamePlayState.Shop:
                anim.SetTrigger("Shop");
                break;
            case EGamePlayState.BetScreen:
                anim.SetTrigger("BetScreen");
                break;
            case EGamePlayState.Roulette:
                anim.SetTrigger("Roulette"); 
                break;
        }
    }
}