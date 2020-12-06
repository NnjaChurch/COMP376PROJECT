using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] float DURATION;
    [SerializeField] AudioSource audio;

    float deathTime;

    // Start is called before the first frame update
    void Start()
    {
        deathTime = Time.time + DURATION;
        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= deathTime)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
