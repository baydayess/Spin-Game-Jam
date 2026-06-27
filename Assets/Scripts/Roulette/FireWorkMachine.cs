using System.Collections.Generic;
using UnityEngine;

public class FireWorkMachine : MonoBehaviour
{
    [SerializeField ]private GameObject FireWork;
    [SerializeField] private GameObject Chip;

    private Stack<GameObject> Chips = new();

    public void StartFireWorks(float amount)
    {
        int finalAmount = (int)amount / 100;

        if(finalAmount > 1000) finalAmount = 1000;

        for (int i = 0; i < finalAmount; i++)
        {
            GameObject spawned = Instantiate(FireWork);
            spawned.transform.position = transform.position + new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);
        }
    }

    public void PlaceChips(float amount)
    {
        int finalAmount = (int)amount / 50;

        if (finalAmount > 10000) finalAmount = 10000;

        for (int i = 0; i < finalAmount; i++)
        {
            GameObject spawned = Instantiate(Chip);
            spawned.transform.position = transform.position + new Vector3(Random.Range(-15, 15), Random.Range(10, 30), 0);
        }
    }
}
