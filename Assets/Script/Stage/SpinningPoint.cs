using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPoint : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(1,0,1));
    }
}
