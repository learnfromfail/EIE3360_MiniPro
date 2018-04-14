using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class NWCharacter : NetworkBehaviour
{
    public int currentX;
    public int currentY;
    public static NWbackgroundSetting BS;

    public string CharName;
    public int playerID = 0; //network
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
    public NWMenuController menuc;
    public GameObject UImenu;
    //
    public Camera playerCamera;
    public AudioListener playerAudioListener;


    void Start () {
        BS = GameObject.Find("EventSystem").GetComponent<NWbackgroundSetting>();
        InitializeData();
        BS.Characters.Add(this.gameObject);
        BS.UpdateCharacter();
      
        this.gameObject.transform.position = ChangeV2toV3(ChangeCoordinateTofloat(currentX, currentY));
        
        menuc = GameObject.Find("EventSystem").GetComponent<NWMenuController>();
   
    }

    void InitializeData()
    {
        playerID = BS.PlayerNumber;
        if (isLocalPlayer==true)
            gameObject.name = "MyCharacter";
        else
        {
            gameObject.name = "Character " + BS.PlayerNumber;
            BS.PlayerNumber++;
        }
        career = (int)Random.Range(0.0f, 2.0f);
        level = (int)Random.Range(0.0f, 2.0f);
         attack = (int)Random.Range(0.0f, 2.0f);
         defense = (int)Random.Range(0.0f, 2.0f);
        HpMax = (int)Random.Range(0.0f, 10.0f);
         Hp = HpMax;
        SpMax = (int)Random.Range(0.0f, 10.0f);
        Sp = (int)Random.Range(0.0f, 10.0f);
         speed = 50;
         movingAbility = 5;

        ////////// set initial position
        if (playerID == 0)
        {
            currentX = 7;
            currentY = 7;
        }
        else 
        {
            currentX = 5 + 2 * playerID;
            currentY = 5 + 2 * playerID;
        }
        //////////

       

        if (isLocalPlayer == false)
        {
            playerCamera = this.transform.GetComponentInChildren<Camera>();
            //playerCamera = this.transform.parent.GetComponentInChildren<Camera>();
            playerCamera.transform.LookAt(this.gameObject.transform);
            playerAudioListener = GetComponent<AudioListener>();

            if (playerCamera)
            {
                playerCamera.enabled = false;
            }
            if (playerAudioListener)
            {
                playerAudioListener.enabled = false;
            }
        }
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
        // this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, new Vector3(x,1,y), 0.2f);  // each NWUnitGround

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
        
        List<NWUnitGround> unvisitedNeighbour = new List<NWUnitGround>();
        List<NWUnitGround> path = new List<NWUnitGround>();
        List<NWUnitGround> visitedUGs = new List<NWUnitGround>();

        bool IsReached = false;
        NWUnitGround nextUG = BS.AllUnits[par[0], par[1]].GetComponent<NWUnitGround>();
        //NWUnitGround previousUG = BS.AllUnits[myX, myY].GetComponent<NWUnitGround>();//prevent repetition

        path.Add(nextUG);//start point
        visitedUGs.Add(nextUG);
       // unvisitedNeighbour = nextUG.neighbourUnit;

        //   foreach (NWUnitGround ugRange in unvisitedNeighbour)
        //  {
        while (IsReached == false) {
            int noOfObstacle = 0, noOfVisited = 0;
            float oldDist = Mathf.Infinity;
            foreach (NWUnitGround ug in nextUG.neighbourUnit)
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
                    */
                    ///////////////////////////////3 29
                    
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
                float newDist = Vector3.Distance(ChangeV2toV3(ChangeCoordinateTofloat(ug.gameObject.GetComponent<NWUnitGround>().coordinateX, ug.gameObject.GetComponent<NWUnitGround>().coordinateY)), end);
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
                //yield return new WaitForSeconds(1f);
                visitedUGs.Add(nextUG);
                unvisitedNeighbour.Remove(nextUG);
              //  nextUG.gameObject.GetComponent<Renderer>().material.color = Color.green;
                this.gameObject.transform.position = ChangeV2toV3(ChangeCoordinateTofloat(nextUG.coordinateX, nextUG.coordinateY));
                yield return new WaitForSeconds(1f);
                if (IsReached == true)
                {
                    BS.AllUnits[par[0], par[1]].GetComponent<NWUnitGround>().beingStepped = false;
                    nextUG.beingStepped = true;

                    menuc.goBack();
                    menuc.buttonsShowed[0].SetActive(false);
                    //LocalPlayerCamera.gameObject.GetComponent<CameraMovement>().setFly(BS.Characters[BS.RankWhoseTurn(BS.round + 1)].transform.position);
                    
                
                }
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

    public void OnMouseDown()//Atack people
    {
        if(menuc.chooseAttack == true && (BS.AllUnits[currentX,currentY].GetComponent<Renderer>().material.color == Color.red))
        {
            NWCharacter Attacker = BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<NWCharacter>();
            NWCharacter Suffer = this.GetComponent<NWCharacter>();
            /*****
             * place battle animation
             * *****/
            int minus = Attacker.attack - Suffer.defense;
            Debug.Log("Hp: " + (Attacker.attack - Suffer.defense));
            Suffer.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value -=  minus;

            menuc.chooseAttack = false;
            BS.Restart();
            menuc.ClickWait();
        }
    }
}
