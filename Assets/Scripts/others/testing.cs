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
        //Camera.main.gameObject.GetComponent<CameraMovement>().setFly(gL[times].transform.position);
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

 
        //    {
        //        debug.log("where0");
        //        foreach (unitground furtherpossibleug in possibleug.neighbourunit)
        //        {
        //            if ((furtherpossibleug.groundtype == 0 && (NaturalRange.contains(furtherpossibleug) == false)) && (consideredug.contains(furtherpossibleug) == false))
        //            {
        //                NaturalRange.add(furtherpossibleug);
        //                debug.log(++addnum);
        //                furtherpossibleug.gameobject.getcomponent<renderer>().material.color = color.cyan;
        //            }
        //        }
        //        NaturalRange.remove(possibleug);
        //        consideredug.add(possibleug);
        //        debug.log("where1");
        //    }
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


/*
 *using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public int currentX;
    public int currentY;
    public static backgroundSetting BS;

    public string CharName;
    public int career; //cavalry=0 infantry=1  archer=2 

    public int level = 1;
    public int attack = 3;
    public int defense = 1;
    public int HpMax = 10;
    public int Hp = 10;
    public int SpMax = 10;
    public int Sp = 10;
    public int speed;//turn base order
    public int movingAbility;// number of grid each turn

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
                    /*
                    for(int i =0; i < 10; i++)
                    {
                        StartCoroutine("WaitForTime2");
                    }


Debug.Log("Now Look at Character" + BS.RankWhoseTurn(BS.round + 1));
                    break;
                }
                // Debug.Log("name"+ ug.gameObject.name);
                if (ug.groundType == 2|| ug.beingStepped == true)
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

                if (newDist<oldDist)
                {
                    oldDist = newDist; nextUG = ug;
                    // Debug.Log("Add: " + ug.gameObject.name);
                }
                else if(newDist==oldDist)//debug
                {
                   // nextUG; ug;
                }
            }

            if (((noOfVisited + noOfObstacle) != nextUG.noOfNeigh) /*&& ((noOfVisited + noOfObstacle)!=nextUG.noOfNeigh-1)) {
                path.Add(nextUG);//2nd pt and afterwards
                //yield return new WaitForSeconds(1f);
                visitedUGs.Add(nextUG);
                unvisitedNeighbour.Remove(nextUG);
              //  nextUG.gameObject.GetComponent<Renderer>().material.color = Color.green;
                this.gameObject.transform.position = ChangeV2toV3(ChangeCoordinateTofloat(nextUG.coordinateX, nextUG.coordinateY));
                yield return new WaitForSeconds(1f);
                if (IsReached == true)
                {
                    BS.AllUnits[par[0], par[1]].GetComponent<UnitGround>().beingStepped = false;
                    nextUG.beingStepped = true;

                    menuc.goBack();
                    menuc.buttonsShowed[0].SetActive(false);
                    //Camera.main.gameObject.GetComponent<CameraMovement>().setFly(BS.Characters[BS.RankWhoseTurn(BS.round + 1)].transform.position);
                    
                
                }
                    currentX = nextUG.coordinateX; currentY = nextUG.coordinateY;
            }
            else 
            {
                visitedUGs.Add(nextUG);
                path.Remove(nextUG);
                nextUG.gameObject.GetComponent<Renderer>().material.color = nextUG.orig;
                nextUG = path[path.Count - 1];
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

        }
    }

    public void OnMouseDown()//Atack people
{
    if (menuc.chooseAttack == true && (BS.AllUnits[currentX, currentY].GetComponent<Renderer>().material.color == Color.red))
    {
        Character Attacker = BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Character>();
        Character Suffer = this;
        /*****
         * place battle animation

        int minus = Attacker.attack - Suffer.defense;
        Debug.Log("Hp: " + (Attacker.attack - Suffer.defense));
        Suffer.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value -= minus;

        menuc.chooseAttack = false;
        BS.Restart();
        menuc.ClickWait();
    }
}
}

 */