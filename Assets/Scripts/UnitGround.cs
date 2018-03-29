﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGround : MonoBehaviour {
    public static backgroundSetting BS;
    public static MenuController MC;
    public int coordinateX;
    public int coordinateY;
    public int groundType = 0; //0 is grassground //1 is mudd //2 obstacle
    public List<UnitGround> neighbourUnit;
    public int noOfNeigh = 0;
    public bool considered = false;
    // Use this for initialization
    

    public Color orig;

    //battlehandling
    int Zero = 0;

    void Start () {
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();
        MC = GameObject.Find("EventSystem").GetComponent<MenuController>();
        orig = this.gameObject.GetComponent<Renderer>().material.color;
    }
    
    void Update () {
		
	}
    void OnMouseDown()
    {

    }
   public void OnMouseUp()
    {
        
        if (MC.chooseMove == true)
        {
            if (this.gameObject.GetComponent<Renderer>().material.color != Color.cyan)
                return;
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
            BS.round++;
            BS.Characters[BS.RankWhoseTurn(BS.round)].GetComponent<Character>().Move(coordinateX, coordinateY);
            MC.chooseMove = false;
            BS.Restart();
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

    public void setNeighb()
    {
        this.neighbourUnit = new List<UnitGround>();
        if (coordinateX - 1 >= 0)
            this.neighbourUnit.Add(BS.AllUnits[coordinateX - 1, coordinateY].GetComponent<UnitGround>());

        if (coordinateX + 1 <= 20) 
            this.neighbourUnit.Add(BS.AllUnits[coordinateX + 1, coordinateY].GetComponent<UnitGround>());

        if (coordinateY - 1 >= 0) 
            this.neighbourUnit.Add(BS.AllUnits[coordinateX, coordinateY-1].GetComponent<UnitGround>());

        if (coordinateY + 1 <= 20)
            this.neighbourUnit.Add(BS.AllUnits[coordinateX, coordinateY + 1].GetComponent<UnitGround>());

        noOfNeigh = this.neighbourUnit.Count;
    }
}