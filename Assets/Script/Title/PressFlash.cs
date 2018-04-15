using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressFlash : MonoBehaviour {

    public Image PressToStart;
    Color c;

	// Use this for initialization
	void Start () {
        c = PressToStart.color;
        StartCoroutine("Flash");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Flash()
    {
        for (int i = 0; i < 25; i++)
        {
            c.a -= 0.04f;
            PressToStart.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        for (int i = 0; i < 25; i++)
        {
            c.a += 0.04f;
            PressToStart.color = c;
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine("Flash");
    }
}
