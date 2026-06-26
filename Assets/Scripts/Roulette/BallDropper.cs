using UnityEngine;

public class BallDropper : MonoBehaviour
{
    bool start = false;

    int amountOfBalls = 1;

    float time = 0;

    [SerializeField]GameObject ball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.OnGamePlayStateChanged.AddListener(StartDrop);
        amountOfBalls = Player.Instance.ballAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if(amountOfBalls > 0)
        {
            if (start == true && time >= 0.3f)
            {
                Drop_Ball();
                time = 0;
                amountOfBalls--;
            }
            time += Time.deltaTime;
        }
    }

    void StartDrop(EGamePlayState state)
    {
        start = true;
        amountOfBalls = Player.Instance.ballAmount;
        time = 0.3f;
    }

    void Drop_Ball()
    {
        GameObject newBall = Instantiate(ball);
        newBall.transform.position = transform.position;
    }
}
