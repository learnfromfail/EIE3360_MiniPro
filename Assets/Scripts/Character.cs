using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
    public int currentX;
    public int currentY;
    public static backgroundSetting BS;

    public string CharName;
    public int movingPower;
    public int noOfMove;
    public MenuController menuc;
    //
    public GameObject UImenu;

	void Start () {
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();

        BS.Characters.Add(this.gameObject);
        BS.UpdateCharacter();
      
        this.gameObject.transform.position = ChangeV2toV3(ChangeCoordinateTofloat(currentX, currentY));

        menuc = GameObject.Find("EventSystem").GetComponent<MenuController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
            touches();

    }
    private void OnMouseOver()
    {
        
    }
    private void OnMouseExit()
    {
       // menuc.enabled = false;
    }
    public void touches()
    {
        menuc.enabled = true;
    }

    public void Move(int x,int y)
    {
        //draw a path to
        //DrawLine(currentX, currentY, x, y);
        int[] parms = new int[4] { currentX, currentY, x, y };
        StartCoroutine("DrawLine",parms);
        // this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(x,1,y), 0.2f);  // each unitGround

    }
    Vector3 ChangeV2toV3(Vector2 v2 )
    {
        return new Vector3(2*v2.x, 1f, 2*v2.y); //2* is the world coordinate since these at parent
    }

    Vector2 ChangeCoordinateTofloat(int x, int y)
    {
        return new Vector2((float)(x * 0.5 - 5), (float)(y * -0.5)+5);
    }

    IEnumerator WaitForTime2()
    {
        //SetupGround();
        yield return new WaitForSeconds(1);
    }

    //DrawLine(int myX, int myY, int toX, int toY)
      IEnumerator DrawLine(int[] par  )
    {
        Vector3 start = ChangeV2toV3(ChangeCoordinateTofloat(par[0], par[1])); Vector3 end = ChangeV2toV3(ChangeCoordinateTofloat(par[2], par[3]));
      //  Debug.Log("start: " + start+", end: "+end+" at ("+toX+", "+toY+")");
      //  Debug.Log("start2"+this.gameObject.transform.TransformPoint(this.gameObject.transform.position));
        Debug.DrawLine(start, end, Color.green, 2.0f);

        // int diffX = toX - myX;int diffY = toY - myY; // other alogiritm right line , may not suitable in 4 dir movement
        float dist = 1000;
        int timesLoop = 0;
        
        List<UnitGround> unvisitedNeighbour = new List<UnitGround>();
        List<UnitGround> path = new List<UnitGround>();
        List<UnitGround> visitedUGs = new List<UnitGround>();

        bool IsReached = false;
        UnitGround nextUG = BS.AllUnits[par[0], par[1]].GetComponent<UnitGround>();
        //UnitGround previousUG = BS.AllUnits[myX, myY].GetComponent<UnitGround>();//prevent repetition

        path.Add(nextUG);//start point
        visitedUGs.Add(nextUG);
       // unvisitedNeighbour = nextUG.neighbourUnit;

        //   foreach (UnitGround ugRange in unvisitedNeighbour)
        //  {
        while (IsReached == false) {
            int noOfObstacle = 0, noOfVisited = 0;
            float oldDist = Mathf.Infinity;
            foreach (UnitGround ug in nextUG.neighbourUnit)
            {
                timesLoop++;
                if ((ug.coordinateX == par[2] && ug.coordinateY == par[3]) || timesLoop > 2000) {
                    Debug.Log("times: " + timesLoop + ", to " + end + " from " + ug.gameObject.transform.position);
                    IsReached = true;
                    nextUG = ug;
                    break;
                }
                // Debug.Log("name"+ ug.gameObject.name);
                if (ug.groundType == 2)
                {
                    // unvisitedNeighbour.Remove(ug);
                    noOfObstacle++;
                    continue;
                }

                if (visitedUGs.Contains(ug) == false)
                {
                    if (unvisitedNeighbour.Contains(ug) == false)
                    {
                        unvisitedNeighbour.Add(ug);//3
                       // ug.gameObject.GetComponent<Renderer>().material.color = Color.grey;//represent neighbour
                    }
                    else
                    {
                      //  continue;//prevent repeating adding unvis neighb
                    }
                }
                else
                {
                    noOfVisited++;
                    continue;//prevent looping in visitedPath
                }
                float newDist = Vector3.Distance(ChangeV2toV3(ChangeCoordinateTofloat(ug.gameObject.GetComponent<UnitGround>().coordinateX, ug.gameObject.GetComponent<UnitGround>().coordinateY)), end);
                // Debug.Log(ug.gameObject.name+"  starts at" + ug.gameObject.transform.TransformPoint(ug.gameObject.transform.position));

                if (newDist < oldDist)
                {
                    oldDist = newDist; nextUG = ug;
                    // Debug.Log("Add: " + ug.gameObject.name);
                }
                else if(newDist==oldDist)//debug
                {
                   // nextUG; ug;
                }
            }
            if ( ((noOfVisited + noOfObstacle) != nextUG.noOfNeigh) /*&& ((noOfVisited + noOfObstacle)!=nextUG.noOfNeigh-1)*/) {
                path.Add(nextUG);//2nd pt and afterwards
                yield return new WaitForSeconds(0.0001f);
                visitedUGs.Add(nextUG);
                unvisitedNeighbour.Remove(nextUG);
              //  nextUG.gameObject.GetComponent<Renderer>().material.color = Color.green;
                this.gameObject.transform.position = ChangeV2toV3(ChangeCoordinateTofloat(nextUG.coordinateX, nextUG.coordinateY));
                currentX = nextUG.coordinateX; currentY = nextUG.coordinateY;
            }
            else 
            {
                visitedUGs.Add(nextUG);
                path.Remove(nextUG);
                nextUG.gameObject.GetComponent<Renderer>().material.color = nextUG.orig;
                nextUG = path[path.Count-1];
            }
            timesLoop++;
            /*
            string name = "";
            for (int iii = 0; iii < path.Count; iii++)
            {
                name +=iii+" order: "+ path[iii].gameObject.name+"("+ path[iii].noOfNeigh+ ")"+ (noOfVisited + noOfObstacle)+ ", ";
                if(iii==path.Count-1)
                    Debug.Log(name);
            }
            */
        }
    }

    private void OnMouseDown()
    {
        
    }
    /*
    UnitGround compareResult(UnitGround ug1, UnitGround ug2)
    {
        float dis = Mathf.Infinity;
        foreach(UnitGround ugSon in ug1.neighbourUnit)
        {
            float newDist = Vector3.Distance(ChangeV2toV3(ChangeCoordinateTofloat
                (ug.gameObject.GetComponent<UnitGround>().coordinateX, ug.gameObject.GetComponent<UnitGround>().coordinateY)), end);

        }
    }
    */
}
