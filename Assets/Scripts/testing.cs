using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class testing : MonoBehaviour {
    Queue<int> helloQ;
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;
    public int times = 0;
    List<GameObject> gL;
    // Use this for initialization
    void Start () {

        gL = new List<GameObject>();
        gL.Add(g1); gL.Add(g2); gL.Add(g3); gL.Add(g4);
        Camera.main.gameObject.GetComponent<CameraMovement>().setFly(gL[times].transform.position);
        /*
         * 
        helloQ = new Queue<int>();
        helloQ.Enqueue(1);
        helloQ.Enqueue(9);
      //  Debug.Log(helloQ.First<int>());
      //  Debug.Log(helloQ.ElementAt<int>(0));
        helloQ.Dequeue();
        //  Debug.Log(helloQ.First<int>());
        int j = 0;
        for(int i = 0; i < 3; i++)
        {   j++;
        //    Debug.Log(i + " ");
            if (i == 2)
                i = -1;
            if (j == 20)
                break;
        }
        //Debug.Log("");
        /*
             breakLoop++;
            if (breakLoop > 10)
            {
                Debug.Log("Fuck");
                breakLoop = 0;
                break;
            }
         */
    }

    // Update is called once per frame
    void Update () {
		
	}
    //in background setting
    /*
for (int i = myUnit.coordinateX - 1; i <= myUnit.coordinateX + 1; i++)
{
    if (i <= 20 && i >= 0)
    {
        if (i != myUnit.coordinateX)
        {
            //myUnit.neighbourUnit.Add(AllUnits[i, myUnit.coordinateY].GetComponent<UnitGround>());
        }
        if (i == myUnit.coordinateX)
        {
            for (int ii = myUnit.coordinateY - 1; ii <= myUnit.coordinateY + 1; ii++)
            {
                if ((ii <= 20 && ii >= 0) && ii != myUnit.coordinateY)
                {
                    //Unit.GetComponent<UnitGround>().neighbourUnit.Add(AllUnits[i, ii].GetComponent<UnitGround>());
                }
            }
        }
    }
}
*/
   public void OnMouseDown()
    {
        Debug.Log("time:"+ times++);
        Camera.main.gameObject.GetComponent<CameraMovement>().setFly(gL[(times)% gL.Count].transform.position);
    }

    public void nMouseUp()
    {
        Debug.Log("time:" + times++);
        Camera.main.gameObject.GetComponent<CameraMovement>().setFly(gL[(times) % gL.Count].transform.position);
    }
}
