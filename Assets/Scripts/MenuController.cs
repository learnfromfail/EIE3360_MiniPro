﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {
    public backgroundSetting BS;

    public Character WhoCurrentInTurn;
    public List<GameObject> buttonsStored = new List<GameObject>();
    public List<GameObject> buttonsShowed = new List<GameObject>();
    public List<Vector3> ButtonPosition = new List<Vector3>();

    public GameObject BackBut; // drag and drop => therefore can set True And false
    public GameObject stateCanvas; // Start( GameObject.Find()), therefore must be set Active to be found

    string[] buttonName = new string[]{ "Move", "Attack", "Skill","State", "Wait" };
    bool IsCompletedReveal = false;
    int Distancelength = 200;
    float MoveSpeed = 1.0f;
    public GameObject ParentCanvas;
    Vector3 tempPosition;
    int fre = 0;
    int timesPress = 0;
    bool IsArrived = true;

    public bool chooseMove = false;
    public bool chooseAttack = false;
    public bool chooseSkill = false;
    public bool chooseState = false;
    public bool chooseWait = false;

    int stateNumber = 0;
    void Start()
    {
        Start1();
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();
    }

    void Start1 () {
        ParentCanvas = GameObject.Find("MenuCanvas");
        int nameNo = 0;
        buttonsShowed.Clear();
        fre = 0;
        foreach (GameObject button in buttonsStored)
        {
          //  GameObject but =  Instantiate(button, new Vector3(1000f,1000f,200f), Quaternion.identity);
            GameObject but = Instantiate(button);
            but.name = "button No"+nameNo++;
            but.transform.SetParent(ParentCanvas.transform);
            but.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
            but.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
            buttonsShowed.Add(but);
            tempPosition = new Vector3(but.transform.position.x, but.transform.position.y);
            //Debug.Log("tempPosit: " + tempPosition);
                //but.transform.parent.position;
        }
        for (int i = 0; i < buttonName.Length; i++)
        {
            ParentCanvas.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = buttonName[i];
            if (i == 0)
                ParentCanvas.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(ClickMove);
            if (i == 1)
                ParentCanvas.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(ClickAttack);
            if (i == 2) //skill?
                ParentCanvas.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(ClickAttack);
            if (i == 3)
                ParentCanvas.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(ClickState);
            if (i == 4)
                ParentCanvas.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(ClickWait);
        }
        IsCompletedReveal = false;

        ///handling text
    }
    void CheckKeyDown()
    {
        if (IsArrived == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                timesPress++;// CN
                IsArrived = false;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                timesPress += buttonsShowed.Count -1 ; // CN
                IsArrived = false;
            }
        }
        if (IsArrived == false)
        {
            Vector3 brt0;
            for (int i = 0; i < buttonsShowed.Count; i++)
            {
                brt0 = buttonsShowed[i].GetComponent<RectTransform>().transform.position;
                buttonsShowed[i].GetComponent<RectTransform>().transform.position = Vector3.Lerp(brt0, ButtonPosition[((timesPress % /*4*/ 5) + i) % /*4*/ 5], MoveSpeed * Time.deltaTime + 0.2f);
                if (Vector2.Distance(brt0, ButtonPosition[((timesPress % /*4*/ buttonsShowed.Count) + i) %/* 4*/buttonsShowed.Count]) <= 5)
                {
                    IsArrived = true;
                }
            }
        }
        if (Input.GetMouseButton(1))//right key
            goBack();
    }
 
    void Update () {
        if (IsCompletedReveal == false)
        {
            SetButtonAndSlide();
        }
        CheckKeyDown();
    }

    void SetButtonAndSlide()
    {
        int i = 0;
        foreach(GameObject button in buttonsShowed)
        {
            RectTransform rt = button.GetComponent<RectTransform>();
            float targetAngle =  (360 / buttonsStored.Count) * i;
            Vector3 targetPos = tempPosition + Vector3.up * Distancelength;
            targetPos = RotatePointAroundPivot(targetPos, button.transform.position, targetAngle);
        //    Debug.Log(button.name + " tempPosition:" + button.transform.position);
            rt.position = Vector3.Lerp(rt.position, targetPos, MoveSpeed * Time.deltaTime);
        //    if (button == buttonsShowed[2])
       //         buttonsShowed[0].GetComponent<RectTransform>().position= new Vector3(button.transform.position.x, Mathf.Abs(button.GetComponent<RectTransform>().position.y));
          //      rt.sizeDelta = new Vector2(40, 40);
          //  else
                rt.sizeDelta = new Vector2(70,30);
        //    Debug.Log(button.name+": this distance is: "+Vector2.Distance(rt.position, targetPos)+", target position: "+ targetPos+", with current"+rt.position);
            i++;
            if (Vector2.Distance(rt.position, targetPos) >=300f) {
                fre++;
                if (fre == buttonsShowed.Count - 1) { 
                    Debug.Log("Finished: "+ Vector2.Distance(rt.position, targetPos)+",i am button: "+button.name+" at: "+button.transform.position+", rt po: "+ rt.position);
                    IsCompletedReveal = true; //last three may suffer
                    savePosition();
                    return;
                }
            }
            
        }
    }
    public void ReGenerateNenu()
    {
        fre = 0;
        foreach (GameObject button in buttonsShowed)
        {
            button.transform.position = new Vector3(226, 158);
        }
        IsCompletedReveal = false;
        tempPosition =  Vector3.zero;
        SetButtonAndSlide();
    }

    Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle)
    {
        Vector3 targetPoint = point - pivot;
        targetPoint = Quaternion.Euler(0, 0, angle) * targetPoint;                                                       
        targetPoint += pivot;
        return targetPoint;
    }

    void savePosition()
    {
            foreach(GameObject Button in buttonsShowed)
        {
            ButtonPosition.Add(new Vector3(Button.transform.position.x, Button.transform.position.y));
        }
    }

    public void Reset()
    {
        if (this.enabled == false)
        {
            this.enabled = true;
            Start1();
        }
        else
        if (this.enabled == true)
        {
            Debug.Log(this.gameObject.name);
            Destroy(GameObject.Find("MenuCanvas").transform.GetChild(0).gameObject);
            Destroy(GameObject.Find("MenuCanvas").transform.GetChild(1).gameObject);
            Destroy(GameObject.Find("MenuCanvas").transform.GetChild(2).gameObject);
            Destroy(GameObject.Find("MenuCanvas").transform.GetChild(3).gameObject);
            this.enabled = false;
        }

    }

    public void OnClick()
    {
        Debug.Log(this.gameObject.name);
    }
    
    public void ClickMove()
    {
        BackBut.SetActive(true);
        setAllButDisappeared();
        chooseMove = true;
        BS.chooseMove();
    }

    public void ClickState()
    {
        BackBut.SetActive(true);
        setAllButDisappeared();
        chooseState = true;
        BS.chooseState(); //white all the stepped grid
        UpdateDetailsAndShowState(BS.RankWhoseTurn(BS.round + 1));
        stateCanvas.SetActive(true);
    }

    public void ClickWait()
    {
        StartCoroutine("ClickWaitSupport");
    }

    IEnumerator ClickWaitSupport()
    {
        setAllButDisappeared();
        int secondsToWait = 0;
        while (true)
        {
            if (secondsToWait == 1)
            {
                BS.round++;
                Camera.main.gameObject.GetComponent<CameraMovement>().setFly(BS.Characters[BS.RankWhoseTurn(BS.round + 1)].transform.position);
                goBack();
                //AI part
                if (BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Character>().IsCompanion == false)
                    BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Enemy>().AImove();
                break;
            }
            secondsToWait++;
            yield return new WaitForSeconds(1);

        }
    }

    public void ClickAttack()
    {
        BackBut.SetActive(true);
        setAllButDisappeared();
        chooseAttack = true;
        BS.chooseAttack();
    }

    public void goBack()
    {
        BackBut.SetActive(false);
        stateCanvas.SetActive(false);//for clicking state
        setAllBoolFalse();
        stateNumber = 0;
        foreach (GameObject but in buttonsShowed)
            but.SetActive(true);
        BS.Restart();

    }
    public void setAllButDisappeared()
    {
        foreach (GameObject but in buttonsShowed)
            but.SetActive(false);
    }
    public void setAllBoolFalse()
    {
         chooseMove = false;
         chooseAttack = false;
         chooseSkill = false;
         chooseState = false;
         chooseWait = false;
    }

    public void UpdateDetailsAndShowState(int who)
    {
        Character thisCharacter = BS.Characters[who].GetComponent<Character>();
        for(int i = 0;i< stateCanvas.transform.GetChild(0).childCount ; i++)
        {
            if (i == 0)//picture
                continue;
            else if (i == 1) //name
                stateCanvas.transform.GetChild(0).GetChild(i).GetComponentInChildren<Text>().text = "Name : " + thisCharacter.CharName;
            else
                stateCanvas.transform.GetChild(0).GetChild(i).GetComponentInChildren<Text>().text = Character.StateName[i - 2] + ": " + thisCharacter.StateAllDetail[i-2];
        }
    }

    public void ChangeContentOfState()
    {
        stateNumber++; 
        Camera.main.gameObject.GetComponent<CameraMovement>().setFly(BS.Characters[stateNumber% BS.Characters.Count].transform.position);//should use a objective
        UpdateDetailsAndShowState(stateNumber % BS.Characters.Count);
    }
}
