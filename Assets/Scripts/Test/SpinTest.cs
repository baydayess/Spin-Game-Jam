using UnityEngine;

public class SpinTest : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField ]private float speed = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 rotation = transform.eulerAngles;
        // rotation.y += 200f * Time.deltaTime;
        // transform.eulerAngles = rotation;
    }

    void FixedUpdate()
    {
        rb.angularVelocity = Vector3.up * speed;
        if (speed >= 0)
        {
            speed -= Time.fixedDeltaTime;
        }
        else
        {
            speed = 0;
        }
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(0, 200 * Time.fixedDeltaTime, 0));
    }
}
