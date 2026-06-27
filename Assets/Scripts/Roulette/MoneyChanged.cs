using TMPro;
using UnityEngine;

public class MoneyChanged : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mesh;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position -= new Vector3(0, Time.deltaTime * 25, 0);
        Color c = mesh.color;
        c.a -= Time.deltaTime/3;
        mesh.color = c;
        if (c.a <= 0) Destroy(gameObject);
    }
}
