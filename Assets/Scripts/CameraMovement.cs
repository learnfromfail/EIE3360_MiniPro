using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public bool NeedChangePos = false;
    public Vector3 targetHead = new Vector3();
    public Vector3 targetItself = new Vector3();
    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        if (NeedChangePos==true)
            Fly();
        if (targetItself != Vector3.zero)
        {
            Vector3 direction = targetItself - this.transform.position;
            //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
            //   transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f * Time.time);
            this.transform.LookAt(targetItself);
        }
    }
    public void Fly()
    {
        //this.transform.LookAt(direction);
        this.transform.position = Vector3.Lerp(this.transform.position, targetHead, 0.05f);

        if (Vector3.Distance(this.transform.position, targetHead) < 0.01)
            NeedChangePos = false;
    }
    public void setFly(Vector3 Tar_Pos)
    {
        targetItself = Tar_Pos;
        targetHead = new Vector3(Tar_Pos.x -5, Tar_Pos.y+10 , Tar_Pos.z -5 );// Tar_Pos;
        this.transform.LookAt(Tar_Pos);
        NeedChangePos = true;
    }
    
}
