using UnityEngine;

public class FireWork : MonoBehaviour
{
    [SerializeField]private ParticleSystem particle;
    [SerializeField] private AudioSource audioPlayer;
    [SerializeField] private AudioClip[] audios;
    private float time = 0;
    private float maxTime;
    private bool finished = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxTime = Random.Range(1f, 3f);

        var main = particle.main;
        main.startDelay = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!particle.isPlaying) Destroy(gameObject);
        time += Time.deltaTime;

        audioPlayer.volume = 1 - time + maxTime;

        if(time >= maxTime && !finished)
        {
            audioPlayer.clip = audios[Random.Range(0, audios.Length - 1)];
            audioPlayer.pitch = Random.Range(0.8f, 1.2f);
            audioPlayer.Play();
            finished = true;
        }
    }
}
