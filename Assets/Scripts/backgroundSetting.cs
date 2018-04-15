using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class backgroundSetting : MonoBehaviour
{
    GameObject UnitG;
    GameObject BackGround;
    public List<GameObject> AllUnit;
    public GameObject[,] AllUnits;
    public int width;
    public int height;
    int Xscale; int Yscale; int Zscale;
    // Use this for initialization
    public MenuController mc;

    public List<GameObject> Characters;
    public List<GameObject> Enemys;
    public List<GameObject> Companions;

    public int noOfCharacters;
    public int round = 0;
    static int ContinueLoop = 0;
    int[] rankList;
    int[] initialVal;
    static bool IsSetUp = false;

    public Text turntext,broadtext;
    public List<int> BattleOrder;

    int breakLoop = 0;
    bool hasChild = false;
    public int switchTurn = 0;

    public Camera main_camera;
    public BattleManager BM;

    void Start()
    {
        BM = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        main_camera = Camera.main;

        AllUnit = new List<GameObject>();
        BattleOrder = new List<int>();

        AllUnits = new GameObject[width + 1, height + 1];

        BackGround = GameObject.Find("Background");
        Xscale = (int)BackGround.transform.localScale.x;
        Yscale = (int)BackGround.transform.localScale.y;
        Zscale = (int)BackGround.transform.localScale.z;
        UnitG = GameObject.Find("UnitGround");

        //UnitG.transform.position = new Vector3((BackGround.transform.position.x) * Xscale, (BackGround.transform.position.y) * Yscale, (BackGround.transform.position.z + 5) * Zscale);
        // StartCoroutine("WaitForTime");
        SetupGround();

        //turntext = GameObject.Find("TurnText").GetComponent<Text>();
        //broadtext = GameObject.Find("broadText").GetComponent<Text>();

        Camera.main.gameObject.GetComponent<CameraMovement>().setFly(Characters[RankWhoseTurn(round + 1)].transform.position);//should use a objective
        mc = GetComponent<MenuController>();
    }
    void Update()
    {

    }
    public void Restart()
    {
        foreach(GameObject ug in AllUnit)
        {
            ug.gameObject.GetComponent<Renderer>().material.color = ug.GetComponent<UnitGround>().orig;//this.gameObject.GetComponent<Renderer>().material.color = orig;
        }
    }

    public void UpdateCharacter()
    {
        noOfCharacters = Characters.Count;
    }

    IEnumerator WaitForTime()
    {
        SetupGround();
        yield return new WaitForSeconds(1);
    }
    void SetupGround()
    {
        for (int i = 0; i <= width; i++)
            for (int ii = height; ii >= 0; ii--)
            {
                GameObject newUG = Instantiate(UnitG, new Vector3((BackGround.transform.position.x - 10 + i) * Xscale / 2, (BackGround.transform.position.y) * Yscale,
                     (BackGround.transform.position.z + 10 - ii) * Zscale / 2), Quaternion.identity, BackGround.transform);
                newUG.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                newUG.GetComponent<UnitGround>().coordinateX = i; newUG.GetComponent<UnitGround>().coordinateY = ii;
                if (i >= (width / 3) && (ii >= (height / 3) && ii <= (height / 2)))
                    newUG.GetComponent<UnitGround>().setGroundType(2); // set obstacle
                                                                       //    if ((i >= (width / 3) && i <= (width / 2)) && (ii >= (height / 9) && (ii <= height / 4)))
                                                                       //       newUG.GetComponent<UnitGround>().setGroundType(2); // set mud
                                                                       //  if ((i >= (width / 3) && i <= (width / 2)) && (ii >= (11) && (ii <= 17)))
                                                                       //      newUG.GetComponent<UnitGround>().setGroundType(2); // set mud
                newUG.name = "X" + i + ",Y" + ii;
                AllUnit.Add(newUG);
                AllUnits[i, ii] = newUG;
            }
        setNeighbourGround();
        for (int i = 0; i < Characters.Count; i++)
        {
            Character thisChar = Characters[i].GetComponent<Character>();
            AllUnits[thisChar.currentX, thisChar.currentY].GetComponent<UnitGround>().beingStepped = true;
        }
    }

    public void setNeighbourGround() // this function fails to work
    {
        foreach (GameObject Unit in AllUnit)
        {
             UnitGround myUnit = Unit.GetComponent<UnitGround>();//////////
            myUnit.setNeighb();

        }
        
    }
    GameObject getCertainUnit(int Cx, int Cy)
    {
        foreach (GameObject Unit in AllUnit)
        {
            if (Unit.name == ("X" + Cx + ",Y" + Cy))
                return Unit;
        }
        return new GameObject();
    }

    public void ChangeTurnText(int CharID)
    {
        turntext.text = round+" "+ Characters[CharID].GetComponent<Character>().CharName + "s'turn "+ Characters[CharID].GetComponent<Character>().speed;
        broadtext.text = Characters[0].GetComponent<Character>().CharName+": "+rankList[0]+" "+
                         Characters[1].GetComponent<Character>().CharName+": "+rankList[1]+" "+
                         Characters[2].GetComponent<Character>().CharName + ": " + rankList[2] + " "+
                         Characters[3].GetComponent<Character>().CharName + ": " + rankList[3];
    }

    public void SetupRank()
    {
        rankList = new int[Characters.Count];
        initialVal = new int[Characters.Count];
        int endPoint = 100;
        for (int i=0;i<Characters.Count;i++){
            rankList[i] = endPoint / Characters[i].GetComponent<Character>().speed -1 ;  //10  25  20  50
            initialVal[i] = endPoint / Characters[i].GetComponent<Character>().speed -1;//=> 10 4 5 2 ==> 9 3 4 1
            //Debug.Log("Ch"+i+": "+ initialVal[i]+" .");
        }
         
    }
    int FoundTimes = 0;

    public int RankWhoseTurn(int round)
    {
        if (IsSetUp == false) {
            SetupRank();
            IsSetUp = true;
        }

        List<List<int>> theComing3Array = new List<List<int>>();
        Queue<Queue<int>> sorted3Array = new Queue<Queue<int>>();
        List<int> noOfSameThisTurn = new List<int>();

        bool Found3 = false;
        string messa = "";

        while (Found3 == false)
        {
            for (int Starts = 0; Starts < rankList.Length; Starts++)
            {
                rankList[Starts] -= 1;
                if (rankList[Starts] == 0)
                {
                    rankList[Starts] += initialVal[Starts] + 1;
                    int marks = Starts;
                    noOfSameThisTurn.Add(marks);
                    messa += marks + " ";
                    Characters[Starts].GetComponent<Character>().noOfMove++;
                }
                if (Starts == rankList.Length - 1 && noOfSameThisTurn.Count != 0)// when at last with at least one and cannot find
                {
                    FoundTimes++;
                    theComing3Array.Add(noOfSameThisTurn);
                    if (FoundTimes < 2) //not 3 array
                    {
                        messa += " ,";
                        Starts = -1;
                        noOfSameThisTurn = new List<int>();
                        continue;
                    }
                    else
                    {
                        Found3 = true;
                        break;
                    }
                }
            }
        }
        //Debug.Log("We have added: " + messa);
        sorted3Array = SetOrder(theComing3Array);//refresh every turn
        Become1DArray(sorted3Array);
        FoundTimes--;
        //Debug.Log(round - 1 + " battle Turn: " + BattleOrder[round - 1]);
        //ChangeTurnText(BattleOrder[round - 1]);

        return BattleOrder[round-1];
    }

    string ListEle = "Turn Order: ";

    public /*List<int>*/ void Become1DArray (Queue<Queue<int>> Array2D)
    {
        foreach (Queue<int> Arr2D in Array2D)
        {
            foreach (int element in Arr2D)
            {
                BattleOrder.Add(element);
                ListEle += " " + element;
            }
        }
        //Debug.Log(ListEle); // show the turn order
    }

    public Queue<Queue<int>> SetOrder(List<List<int>> Array3)
    {
        Queue<Queue<int>> returnArrayQueue = new Queue<Queue<int>>();
        for (int i = 0; i < Array3.Count; i++)
        {
            returnArrayQueue.Enqueue(ReturnASortedArray(Array3[i]));
        }
        return returnArrayQueue;
    }
    
    public Queue<int> ReturnASortedArray(List<int> theSameTimeArray) //sort the array when the same speed rise!!
    {
        Queue<int> returnArray = new Queue<int>();
       
        for (int startFrom = 0; startFrom < theSameTimeArray.Count; startFrom++)
        {                                           //1:(3),(1),(3),(2,3) ||2 :(2,3)
            int temp = 10;
            int max = 99;
            for (int inn = 0 + startFrom; inn < theSameTimeArray.Count; inn++)
            {
                if (Characters[theSameTimeArray[inn]].GetComponent<Character>().noOfMove < max)
                {
                    max = Characters[theSameTimeArray[inn]].GetComponent<Character>().noOfMove;
                    temp = theSameTimeArray[inn];
                }
                if (inn == theSameTimeArray.Count - 1)
                {
                    returnArray.Enqueue(temp);
                   // Debug.Log(startFrom + " layers temp Ch: " + temp + " has added.");
                }
            }
        }
        return returnArray;
    }

    public void chooseMove()//show movable range 
    {
        List<UnitGround> MoveRange = new List<UnitGround>();
        Character currentCharacter = Characters[RankWhoseTurn(round + 1)].GetComponent<Character>();
        MoveRange = getNaturalRange(currentCharacter.movingAbility, currentCharacter.currentX, currentCharacter.currentY);
        foreach (UnitGround ug in MoveRange)
        {
            if (ug.groundType == 0)
                ug.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        }
    }
    public List<UnitGround> getNaturalRange(int range,int startX,int startY)
    {
        List<UnitGround> NaturalRange = new List<UnitGround>();
        List<UnitGround> ConsideredUG = new List<UnitGround>();
        List<UnitGround> returnRange = new List<UnitGround>();

        UnitGround startXY = AllUnits[startX, startY].GetComponent<UnitGround>();

        startXY.gameObject.GetComponent<Renderer>().material.color = Color.cyan;
        NaturalRange.Add(startXY);//NaturalRange is the list to consider their neighb
        ConsideredUG.Add(startXY);//ConsideredUG means to object to reconsider the considered unitGround
        List<UnitGround>  SpareList = new List<UnitGround>();//help to minimise the range 
        for (int i = 0; i < range; i++)//the -1 comes from the above foreach
        {
            foreach (UnitGround possibleUG in NaturalRange)
            {
                foreach (UnitGround FurtherPossibleUG in possibleUG.neighbourUnit)
                {
                    if ((FurtherPossibleUG.groundType == 0 && (SpareList.Contains(FurtherPossibleUG) == false)) && (ConsideredUG.Contains(FurtherPossibleUG) == false))
                    {
                        if ((mc.chooseAttack == true))
                        {
                            SpareList.Add(FurtherPossibleUG);
                            returnRange.Add(FurtherPossibleUG);
                        }
                        else if ((mc.chooseMove == true) && (FurtherPossibleUG.beingStepped == true))
                        {

                        }
                        else
                        {
                            SpareList.Add(FurtherPossibleUG);
                            returnRange.Add(FurtherPossibleUG);
                        }
                    }
                }
                ConsideredUG.Add(possibleUG);//avoid double add
            }
            NaturalRange = new List<UnitGround>(SpareList);
            SpareList.Clear();
        }
        return returnRange;
    }

    public void chooseAttack() //show the attack Range
    {
        foreach (UnitGround ug in GetAttackRange())
        {
            if(ug.groundType == 0)
                ug.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        //AI part
        if (Characters[RankWhoseTurn(round + 1)].GetComponent<Character>().IsCompanion == false)
            Characters[RankWhoseTurn(round + 1)].GetComponent<Enemy>().IsGoNext2 = true; ;
    }
    
    public List<UnitGround> GetAttackRange()
    {
        List<UnitGround> AttackRange = new List<UnitGround>();
        Character currentCharacter = Characters[RankWhoseTurn(round + 1)].GetComponent<Character>();
        UnitGround UnitBeingStepped = AllUnits[currentCharacter.currentX, currentCharacter.currentY].GetComponent<UnitGround>();
        if (currentCharacter.career == 0)
        { //cavalry
            AttackRange = new List<UnitGround>(AllUnits[currentCharacter.currentX, currentCharacter.currentY].GetComponent<UnitGround>().neighbourUnit);
        }
        if (currentCharacter.career == 1) //infantry
        {
            AttackRange = new List<UnitGround>(AllUnits[currentCharacter.currentX, currentCharacter.currentY].GetComponent<UnitGround>().neighbourUnit);
            if (currentCharacter.currentX - 1 > 0 && currentCharacter.currentY - 1 > 0)
                AttackRange.Add(AllUnits[currentCharacter.currentX - 1, currentCharacter.currentY - 1].GetComponent<UnitGround>());
            if (currentCharacter.currentX - 1 > 0 && currentCharacter.currentY + 1 < height)
                AttackRange.Add(AllUnits[currentCharacter.currentX - 1, currentCharacter.currentY + 1].GetComponent<UnitGround>());
            if (currentCharacter.currentX + 1 < width && currentCharacter.currentY - 1 > 0)
                AttackRange.Add(AllUnits[currentCharacter.currentX + 1, currentCharacter.currentY - 1].GetComponent<UnitGround>());
            if (currentCharacter.currentX + 1 < width && currentCharacter.currentY + 1 < height)
                AttackRange.Add(AllUnits[currentCharacter.currentX + 1, currentCharacter.currentY + 1].GetComponent<UnitGround>());
        }
        if (currentCharacter.career == 2) //archer
        {
            AttackRange = getNaturalRange(2, currentCharacter.currentX, currentCharacter.currentY);
            foreach (UnitGround ug in AllUnits[currentCharacter.currentX, currentCharacter.currentY].GetComponent<UnitGround>().neighbourUnit)
            {
                AttackRange.Remove(ug);
            }
        }
        return AttackRange;
    }
    
    public void chooseState()
    {
        foreach(GameObject each in Characters)
            AllUnits[each.GetComponent<Character>().currentX, each.GetComponent<Character>().currentY].GetComponent<Renderer>().material.color = Color.white;
    }
}
