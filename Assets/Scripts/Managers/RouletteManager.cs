using UnityEngine;
using UnityEngine.Events;

public class RouletteManager : MonoBehaviour
{
    public UnityEvent roulette_Stoped;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        roulette_Stoped.AddListener(Roulette_Finished);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Roulette_Finished()   
    {
        
    }
}
