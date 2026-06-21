using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 gravity;
    
    private Rigidbody rb;

    private Slot_Color current_Color;

    private int current_Number;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity += gravity * Time.fixedDeltaTime;
    }

    public void select_info(Slot_Color color, int number)
    {
        current_Color = color;
        current_Number = number;
    }
}
