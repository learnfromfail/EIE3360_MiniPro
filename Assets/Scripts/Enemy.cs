using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    public string AIstatus = "Attacker"; //waiter //HP recoverer 
    public bool IsGoNext = false;
    public bool IsGoNext2 = false;
    public bool IsGoNext3 = false;
    public bool IsGoNext4 = false;

    public GameObject WeakestOne;
    // Use this for initialization
    void Start () {
        menuc = GameObject.Find("EventSystem").GetComponent<MenuController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AImove()
    {
        if(AIstatus == "Attacker")
        StartCoroutine("AttackerAI");
    }

    IEnumerator AttackerAI()
    {
        menuc.setAllButDisappeared();
        int secondsToWait = 0;
        while (true)
        {
            if (secondsToWait == 0)
            {
                menuc.ClickMove();
            }
            if (secondsToWait == 1)//Move
            {
                BS.AllUnits[AIGettingCertainNode().coordinateX, AIGettingCertainNode().coordinateY].GetComponent<UnitGround>().AIonClickMoveToHere();
            }
            
            if (IsGoNext)//Attack or Not
            {
                IsGoNext = false;
                if (AICheckAttackable())
                {
                    menuc.ClickAttack(); //hold the red grid for 1 sec
                    secondsToWait = 0;
                }
                else
                {
                    IsGoNext4 = true; //end turn directly
                }
            }
            if (IsGoNext2 && (secondsToWait ==1)) //choose 2 to 3// time diff = 1sec 
            {
                IsGoNext2 = false;
                WeakestOne.GetComponent<Character>().OnMouseDown();
                // IsGoNext3 = true;
                break;
            }
            if ((IsGoNext3  && (secondsToWait == 1))|| IsGoNext4) // AI pressing next turn
            {
               IsGoNext4 = false; IsGoNext3 = false;
               menuc.ClickWait();
                break;
            }

            secondsToWait++;
            Debug.Log("secondsToWait" + secondsToWait);
            if (secondsToWait == 100) {
                break;
            }

            yield return new WaitForSeconds(0.5f);

        }
    }
    
    public UnitGround AIGettingCertainNode()
    {
        GetWeakestTarget();
        float MaxDistance = 99999f;
        UnitGround TargetGrid = new UnitGround();
        currentX = this.gameObject.GetComponent<Character>().currentX;
        currentY = this.gameObject.GetComponent<Character>().currentY;
        foreach (UnitGround memberGrid in BS.getNaturalRange(movingAbility, currentX, currentY))
        {
            if (Vector3.Distance(memberGrid.transform.position, WeakestOne.transform.position) < MaxDistance)
            {
                MaxDistance = Vector3.Distance(memberGrid.transform.position, WeakestOne.transform.position);
                TargetGrid = memberGrid;
               // Debug.Log("where2 X:" + memberGrid.coordinateX +",Y: "+ memberGrid.coordinateY);
            }
        }
        Debug.Log("where3" + TargetGrid.coordinateX + TargetGrid.coordinateY);

        return TargetGrid;
    }

    public bool AICheckAttackable()
    {
        foreach (UnitGround ug in BS.GetAttackRange())
        {
            if ((ug.coordinateX == WeakestOne.GetComponent<Character>().currentX) && (ug.coordinateY == WeakestOne.GetComponent<Character>().currentY))
            {
                return true;
            }
        }
        return false;
    }

    public GameObject GetWeakestTarget()
    {
        float MaxHP = 999f;
       // WeakestOne = new GameObject();
        foreach (GameObject Playertarget in BS.Companions)
        {
            if (Playertarget.GetComponent<Character>().Hp < MaxHP)
            {
                MaxHP = Playertarget.GetComponent<Character>().Hp;
                WeakestOne = Playertarget;
            }
        }
        return WeakestOne;
    }
}
