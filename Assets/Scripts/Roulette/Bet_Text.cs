using TMPro;
using UnityEngine;

public class Bet_Text : MonoBehaviour
{

    public float amount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<TextMeshPro>().text = amount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Camera.main.transform);
        //transform.Rotate(0, 180, 0);
    }
}
