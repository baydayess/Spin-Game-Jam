using UnityEngine;

public class SpinTest : MonoBehaviour
{
    private Rigidbody[] rbs;

    [SerializeField] private float speed = 10;
    
    [SerializeField] private RouletteManager rouletteManager;

    private bool stoped = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 rotation = transform.eulerAngles;
        //rotation.y += speed * Time.deltaTime;
        //transform.eulerAngles = rotation;
    }

    void FixedUpdate()
    {
        if (speed > 0)
        {
            speed -= Time.fixedDeltaTime * 50;
            Quaternion deltaRotation = Quaternion.Euler(0f, 0f, speed * Time.fixedDeltaTime);

            foreach (Rigidbody rb in rbs)
                rb.MoveRotation(rb.rotation * deltaRotation);
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
