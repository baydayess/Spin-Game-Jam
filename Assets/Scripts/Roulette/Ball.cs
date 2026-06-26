using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 gravity;
    
    private Rigidbody rb;

    private ESlotColor current_Color;

    public bool finished = false;
    
    private int current_Number;

    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForceAtPosition(new Vector3(Random.Range(1, -1), 0, Random.Range(1, -1)) * 90, transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.linearVelocity += gravity * Time.fixedDeltaTime;
        check_if_stoped();
    }

    public void select_info(ESlotColor color, int number)
    {
        current_Color = color;
        current_Number = number;
    }

    public ESlotColor GetColor()
    {
        return current_Color;
    }

    public int GetNumber()
    {
        return current_Number;
    }

    void check_if_stoped()
    {
        if (rb.linearVelocity.magnitude < 0.5f)
        {
            timer += Time.fixedDeltaTime;
            if (timer >= 1f)
            {
                finished = true;
            }
        }
        else
        {
            timer = 0;
        }
    }
}
