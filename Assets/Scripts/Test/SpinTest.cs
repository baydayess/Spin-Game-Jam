using UnityEngine;

public class SpinTest : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]private float speed = 10;
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
        }
    }
}
