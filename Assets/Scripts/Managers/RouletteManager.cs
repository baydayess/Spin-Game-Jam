using UnityEngine;
using UnityEngine.Events;

public class RouletteManager : MonoBehaviour
{
    public UnityEvent roulette_Stoped;

    private Ball[] balls;

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
        balls = FindObjectsByType<Ball>(0);
        finished = true;
    }


    void Count_Points()
    {
        foreach (var ball in balls)
        {
            print("Number: " + ball.GetNumber() + " " + ball.GetColor());
        }
        finished = false;
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
