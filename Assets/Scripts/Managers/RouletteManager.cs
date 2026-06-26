using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class RouletteManager : MonoBehaviour
{
    public UnityEvent roulette_Stoped;

    private List<Ball> balls;

    private bool finished = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roulette_Stoped.AddListener(Roulette_Finished);
    }

    void Update()
    {
        if (finished && Check_Balls())
        {
            Count_Points();
        }
    }
    
    void Roulette_Finished()   
    {
        balls = FindObjectsByType<Ball>(0).ToList();
        finished = true;
    }


    void Count_Points()
    {
        List<Ball> destroy_balls = new();
        foreach (var ball in balls)
        {
            print("Number: " + ball.GetNumber() + " " + ball.GetColor());
            destroy_balls.Add(ball);
            Player.Instance.Confirm_Bet(ball.GetNumber());
        }
        foreach (var ball in destroy_balls)
        {
            balls.Remove(ball);
            Destroy(ball.gameObject);
        }
        finished = false;
        GameManager.Instance.ChangeGamePlayState((int) EGamePlayState.BetScreen);
    }

    bool Check_Balls()
    {
        foreach (var ball in balls)
        {
            if (!ball.finished)
            {
                return false;
            }
        }
        return true;
    }
}
