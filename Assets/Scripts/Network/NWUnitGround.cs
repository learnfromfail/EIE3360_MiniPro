using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NWUnitGround : NetworkBehaviour
{
    public static NWbackgroundSetting BS;
    public static NWMenuController MC;
    public int coordinateX;
    public int coordinateY;
    public int groundType = 0; //0 is grassground //1 is mudd //2 obstacle //
    public List<NWUnitGround> neighbourUnit;
    public int noOfNeigh = 0;
    public bool considered = false;
    public bool beingStepped = false;
    // Use this for initialization

    public static bool notblockAnyUI = true;
    public Color orig;

    //battlehandling
    int Zero = 0;

    void Start () {
        BS = GameObject.Find("EventSystem").GetComponent<NWbackgroundSetting>();
        MC = GameObject.Find("EventSystem").GetComponent<NWMenuController>();
        orig = this.gameObject.GetComponent<Renderer>().material.color;
    }
    
    void Update () {
		
	}
    void OnMouseDown()
    {

    }
   public void OnMouseUp()
    {
        if (notblockAnyUI)
            if (beingStepped == false ||(MC.chooseAttack == true && beingStepped == true))
            {
                if (MC.chooseMove == true)
                {
                    if (this.gameObject.GetComponent<Renderer>().material.color != Color.cyan)
                        return;
                    this.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    //BS.round++;
                    BS.Characters[BS.RankWhoseTurn(BS.round+1)].GetComponent<NWCharacter>().Move(coordinateX, coordinateY);
                    MC.chooseMove = false;
                    MC.setAllButDisappeared();
                    MC.BackBut.SetActive(false);
                    BS.Restart();
                    //MC.goBack();
                }
            }
            else {
                Debug.Log("SomeOne Occupied");
            }
    }

    public void setGroundType(int type)
    {
        switch (type)
        {
            case 1:
                groundType = 1;
                this.gameObject.GetComponent<Renderer>().material.color = Color.black;
                orig = this.gameObject.GetComponent<Renderer>().material.color;
                //= Resources.Load("/Materials/New Material", typeoff(Material)) as Material;
                break;
            case 2:
                groundType = 2;
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
                orig = this.gameObject.GetComponent<Renderer>().material.color;
                break;
            default:
                groundType = 0;
        break;
        }
    }

    [ServerCallback]
    public void setNeighb()
    {
        this.neighbourUnit = new List<NWUnitGround>();
        if (coordinateX - 1 >= 0)
            this.neighbourUnit.Add(BS.AllUnits[coordinateX - 1, coordinateY].GetComponent<NWUnitGround>());

        if (coordinateX + 1 <= BS.width) 
            this.neighbourUnit.Add(BS.AllUnits[coordinateX + 1, coordinateY].GetComponent<NWUnitGround>());

        if (coordinateY - 1 >= 0) 
            this.neighbourUnit.Add(BS.AllUnits[coordinateX, coordinateY-1].GetComponent<NWUnitGround>());

        if (coordinateY + 1 <= BS.height)
            this.neighbourUnit.Add(BS.AllUnits[coordinateX, coordinateY + 1].GetComponent<NWUnitGround>());

        noOfNeigh = this.neighbourUnit.Count;
    }
}
