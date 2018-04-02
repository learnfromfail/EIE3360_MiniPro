using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class minor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse was clicked over a UI element
            if (EventSystem.current.IsPointerOverGameObject())
            {
                UnitGround.notblockAnyUI = false;
                Debug.Log("Clicked on the UI");
            }
            else
            {

                UnitGround.notblockAnyUI = true;
            }

        }
    }
}
