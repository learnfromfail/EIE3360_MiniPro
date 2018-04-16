using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public int currentX;
    public int currentY;
    public static backgroundSetting BS;

    public string CharName;
    public bool IsCompanion = true; //false will be enemy
    public int career; //cavalry=0 infantry=1  archer=2 

    public int level ;
    public int expRequired ;
    public int attack ;
    public int defense ;
    public int HpMax ;
    public int Hp ;
    public int SpMax ;
    public int Sp ;
    public int speed;//turn base order
    public int movingAbility;// number of grid each turn
    public int[] StateAllDetail;
    public static string[] StateName = new string[] { "Level", "HpMax", "HP", "SpMax", "SP" , "Attack", "Defense", "Speed", "Moving Ability" };

    public bool UpPosition = false;
    public UnitGround UpdateUnit;

    public int noOfMove;
    public MenuController menuc;
    //
    public GameObject UImenu;
    //
    public CharacterAnimator ModelMovement;
	void Start () {
        StateAllDetail = new int[] { level,  HpMax , Hp, SpMax, Sp, attack, defense, speed, movingAbility };
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();
            BS.Characters.Add(this.gameObject);
        if (IsCompanion)
            BS.Companions.Add(this.gameObject);
        if (!IsCompanion)
            BS.Enemys.Add(this.gameObject);
        BS.UpdateCharacter();
        this.gameObject.transform.position = ChangeV2toV3(ChangeCoordinateTofloat(currentX, currentY));     
        menuc = GameObject.Find("EventSystem").GetComponent<MenuController>();

    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
            touches();

        if (UpPosition == true)
        {
            Vector3 targetPos = ChangeV2toV3(ChangeCoordinateTofloat(UpdateUnit.coordinateX, UpdateUnit.coordinateY));
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
            if (Vector3.Distance(transform.position, targetPos) < 1)
                UpPosition = false;
        }

    }
    private void OnMouseOver()
    {
        
    }
    private void OnMouseExit()
    {

    }
    public void touches()
    {
        menuc.enabled = true;
    }

    public void Move(int x,int y)
    {   
        //
        ModelMovement.MoveAnimation(true);
        //
        int[] parms = new int[4] { currentX, currentY, x, y };
        StartCoroutine("DrawLine",parms);
    }

    Vector3 ChangeV2toV3(Vector2 v2 )
    {
        return new Vector3(2*v2.x, 1f, 2*v2.y);
    }

    Vector2 ChangeCoordinateTofloat(int x, int y)
    {
        return new Vector2((float)(x * 0.5 - 5), (float)(y * -0.5)+5);
    }
      IEnumerator DrawLine(int[] par  )
    {
        Vector3 start = ChangeV2toV3(ChangeCoordinateTofloat(par[0], par[1])); Vector3 end = ChangeV2toV3(ChangeCoordinateTofloat(par[2], par[3]));
        Debug.DrawLine(start, end, Color.green, 2.0f);
        //float dist = 1000;
        int timesLoop = 0;
        
        List<UnitGround> unvisitedNeighbour = new List<UnitGround>();
        List<UnitGround> path = new List<UnitGround>();
        List<UnitGround> visitedUGs = new List<UnitGround>();

        bool IsReached = false;
        UnitGround nextUG = BS.AllUnits[par[0], par[1]].GetComponent<UnitGround>();
        path.Add(nextUG);//start point
        visitedUGs.Add(nextUG);
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
                  Debug.Log("Now Look at Character" + BS.RankWhoseTurn(BS.round + 1));
                    break;
                }
                if (ug.groundType == 2|| ug.beingStepped == true)
                {
                    noOfObstacle++;
                    continue;
                }

                if (visitedUGs.Contains(ug) == false)
                {
                    if (unvisitedNeighbour.Contains(ug) == false)
                    {
                        unvisitedNeighbour.Add(ug);
                    }
                }
                else
                {
                    noOfVisited++;
                    continue;//prevent looping in visitedPath
                }
                float newDist = Vector3.Distance(ChangeV2toV3(ChangeCoordinateTofloat(ug.gameObject.GetComponent<UnitGround>().coordinateX, ug.gameObject.GetComponent<UnitGround>().coordinateY)), end);
                if (newDist < oldDist)
                {
                    oldDist = newDist; nextUG = ug;
                }

            }

            if ( ((noOfVisited + noOfObstacle) != nextUG.noOfNeigh)) {
                path.Add(nextUG);
                visitedUGs.Add(nextUG);
                unvisitedNeighbour.Remove(nextUG);
                //
                UpPosition = true;
                UpdateUnit = nextUG;
               // this.gameObject.transform.position = ChangeV2toV3(ChangeCoordinateTofloat(nextUG.coordinateX, nextUG.coordinateY));
                //
                yield return new WaitForSeconds(0.5f);
                if (IsReached == true)
                {
                    BS.AllUnits[par[0], par[1]].GetComponent<UnitGround>().beingStepped = false;
                    nextUG.beingStepped = true;
                    if (IsCompanion)
                    {
                        //
                        ModelMovement.MoveAnimation(false);
                        //
                        menuc.goBack();
                        menuc.buttonsShowed[0].SetActive(false);
                    }
                    else
                    {
                       //AI part
                        GetComponent<Enemy>().IsGoNext = true;
                    }
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
        }
    }

    public void OnMouseDown()//Attack people
    {
        if(menuc.chooseAttack == true && (BS.AllUnits[currentX,currentY].GetComponent<Renderer>().material.color == Color.red))
        {
            Character Attacker = BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Character>();
            Character Suffer = this;
            //
            BS.BM.StartFight(Attacker.gameObject, Suffer.gameObject);
            BS.mc.BackBut.SetActive(false);
            //
            //int minus = Attacker.attack - Suffer.defense;
            //Debug.Log("Hp: " + (Attacker.attack - Suffer.defense));
            //Suffer.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Slider>().value -=  minus;
            menuc.chooseAttack = false;
         //   if (BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Character>().IsCompanion == true)
         //   {
          //      BS.Restart();
          //      menuc.ClickWait();
         //   }
        }
    }



}
