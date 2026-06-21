using UnityEngine;

public class SpinTest : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]private float speed = 10;
    
    [SerializeField]private RouletteManager rouletteManager;

    private bool stoped = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = transform.eulerAngles;
        rotation.y += speed * Time.deltaTime;
        transform.eulerAngles = rotation;
    }

    void FixedUpdate()
    {
        if (speed > 0)
        {
            speed -= Time.fixedDeltaTime * 50;
            //rb.angularVelocity = Vector3.up * speed;
        
        }
        else
        {
            speed = 0;
            if (!stoped && rouletteManager)
            {
                rouletteManager.roulette_Stoped.Invoke();
                stoped = true;
            }
        }
    }
}
