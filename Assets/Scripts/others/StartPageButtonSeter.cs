using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPageButtonSeter : MonoBehaviour {
    public GameObject LoadedLobby;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadAScene() {
        SceneManager.LoadScene("Lobby");
    }
}
