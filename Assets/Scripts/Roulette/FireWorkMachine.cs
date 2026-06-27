using UnityEngine;

public class FireWorkMachine : MonoBehaviour
{
    [SerializeField ]private GameObject FireWork;
    

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
}
