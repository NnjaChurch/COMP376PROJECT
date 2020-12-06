using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {
	[SerializeField] float DURATION;
	[SerializeField] AudioSource audio;


	// Start is called before the first frame update
	void Start() {
		if (!audio.isPlaying) {
			audio.Play();
		}
	}

	// Update is called once per frame
	void Update() {
		if(!audio.isPlaying) {
			Destroy(this.gameObject);
		}
	}
}
