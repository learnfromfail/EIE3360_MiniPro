using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    AudioClip FranceTheme;
    AudioClip EnglandTheme;

    // Use this for initialization
    void Start () {
        AudioSource audio = GameObject.Find("EnglandTheme").GetComponent<AudioSource>();
        audio.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
