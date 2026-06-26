using UnityEngine;
using UnityEngine.Audio;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector3 gravity;

    [SerializeField] private AudioSource audioPlayer;

    [SerializeField] private AudioClip[] audios;

    private float audioTimer = 0;

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
        audioTimer += Time.fixedDeltaTime;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (audioTimer <= 0.2f || collision.gameObject.layer == 6) return;

        audioPlayer.clip = audios[Random.Range(0, audios.Length - 1)];
        audioPlayer.pitch = Random.Range(0.8f, 1.2f);
        audioPlayer.volume = rb.linearVelocity.magnitude / 20;
        audioPlayer.Play();
        audioTimer = 0;
    }
}
