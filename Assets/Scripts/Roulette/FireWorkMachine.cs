using System.Collections.Generic;
using UnityEngine;

public class FireWorkMachine : MonoBehaviour
{
    [SerializeField ]private GameObject FireWork;
    [SerializeField] private GameObject Chip;

    private Stack<GameObject> Chips = new();

    private int chipsAmount;
    private float chipTimer = 0;
    private void Update()
    {
        chipTimer += Time.deltaTime;
        if (chipsAmount > 0 && chipTimer >= 0.05)
        {
            chipTimer = 0;
            chipsAmount--;
            GameObject spawned = Instantiate(Chip);
            spawned.transform.position = transform.position + new Vector3(Random.Range(-50, 100), Random.Range(10, 30), Random.Range(0, 20));
            Chips.Push(spawned);
        }
    }

    public void StartFireWorks(float amount)
    {
        int finalAmount = (int)amount / 100;

        if(finalAmount > 1000) finalAmount = 1000;

        for (int i = 0; i < finalAmount; i++)
        {
            GameObject spawned = Instantiate(FireWork);
            spawned.transform.position = transform.position + new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);
        }
        PlaceChips(amount);
    }

    public void PlaceChips(float amount)
    {
        chipsAmount = (int)amount / 50;

        if (chipsAmount > 10000) chipsAmount = 10000;
    }
}
