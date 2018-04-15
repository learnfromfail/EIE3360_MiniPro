using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour {
    public bool NeedChangePos = false;
    public bool NeedChangePos2 = false;
    public Vector3 targetHead = new Vector3();
    public Vector3 targetItself = new Vector3();
    public Vector3 statePos = new Vector3();
    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        if (NeedChangePos==true)
            Fly();
      //  if (NeedChangePos2 == true)
      //      freeCamera(statePos);
    }
    public void Fly()
    {
        GameObject.Find("EventSystem").GetComponent<HPbarHandler>().UpdateHPbarRotation();
        //this.transform.LookAt(direction);
        this.transform.position = Vector3.Lerp(this.transform.position, targetHead, 0.05f);
        this.transform.LookAt(targetItself);
        if (Vector3.Distance(this.transform.position, targetHead) < 0.01)
            NeedChangePos = false;
    }
    public void setFly(Vector3 Tar_Pos)
    {
        targetItself = Tar_Pos;
        targetHead = new Vector3(Tar_Pos.x -5, Tar_Pos.y+20 , Tar_Pos.z -5 );// Tar_Pos;
        this.transform.LookAt(Tar_Pos);
        NeedChangePos = true;
    }
    
    public void freeCamera(Vector3 unitGroundPos)
    {
        Camera.main.transform.position = Vector3.Lerp(this.transform.position,new Vector3(unitGroundPos.x, 20, unitGroundPos.y),0.2f);
        //Camera.main.transform.LookAt(unitGroundPos);
        if (Vector3.Distance(this.transform.position, unitGroundPos) < 10)
            NeedChangePos2 = false;
    }

}
/*
if (targetItself != Vector3.zero)
{
    Debug.Log("changing direction");
    Vector3 direction = targetItself - this.transform.position;
    //Quaternion toRotation = Quaternion.FromToRotation(transform.forward, direction);
    //   transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.1f * Time.time);

    targetItself = Vector3.zero;
}
*/
