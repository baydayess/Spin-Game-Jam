using System;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private enum Color
    {
        black,
        red,
        green
    }
    
    [SerializeField] private ESlotColor color;

    [SerializeField] private int number;
    
    //[SerializeField] private GameObject roulette;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.GetComponent<Ball>();
        if(ball)
            ball.select_info(color, number);
    }

    void ontriggerEnter(Collider other)
    {
        
    }
}
