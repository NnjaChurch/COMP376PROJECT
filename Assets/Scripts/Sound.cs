using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
	[SerializeField] AudioSource audio_source;

	// Start is called before the first frame update
	void Start() {
		DontDestroyOnLoad(gameObject);
		if (!audio_source.isPlaying) {
			audio_source.Play();
		}
	}

	// Update is called once per frame
	void Update() {
		if (!audio_source.isPlaying) {
			Destroy(this.gameObject);
		}
	}
}
