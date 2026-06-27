using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{


    [SerializeField] private AudioClip audio;

    [SerializeField] private List<AudioSource> audioPlayers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (audioPlayers.Count > 0)
        {
            for (int i = 0; i < audioPlayers.Count; i++)
            {
                if (!audioPlayers[i].isPlaying)
                {
                    Destroy(audioPlayers[i]);
                    audioPlayers.RemoveAt(i);
                }
            }
        }
    }

    public void PlayAudio()
    {

        AudioSource audioPlayer = gameObject.AddComponent<AudioSource>();
        audioPlayer.loop = false;
        audioPlayer.pitch = Random.Range(0.9f, 1.1f);
        audioPlayer.PlayOneShot(audio);
        audioPlayers.Add(audioPlayer);
    }
}
