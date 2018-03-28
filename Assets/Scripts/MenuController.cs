using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuController : MonoBehaviour {


    public Character WhoCurrentInTurn;
    public List<GameObject> buttonsStored = new List<GameObject>();
    public List<GameObject> buttonsShowed = new List<GameObject>();
    public List<Vector3> ButtonPosition = new List<Vector3>();

    public string[] buttonName = new string[]{ "Move", "Attack", "Skill", "Wait" };
    bool IsCompletedReveal = false;
    int Distancelength = 200;
    float MoveSpeed = 1.0f;
    public GameObject ParentCanvas;
    Vector3 tempPosition;
    int fre = 0;
    int timesPress = 0;
    bool IsArrived = true;

     void Start()
    {
        Start1();
    }
    void Start1 () {
        ParentCanvas = GameObject.Find("MenuCanvas");
        int nameNo = 0;
        buttonsShowed.Clear();
        fre = 0;
        foreach (GameObject button in buttonsStored)
        {
           GameObject but =  Instantiate(button);
            
            but.name = "button No"+nameNo++;
            but.transform.SetParent(ParentCanvas.transform);
            but.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
            but.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
            buttonsShowed.Add(but);
            tempPosition = new Vector3(but.transform.position.x, but.transform.position.y);
            Debug.Log("tempPosit: " + tempPosition);
                //but.transform.parent.position;
        }
        for (int i = 0; i < buttonName.Length; i++)
            ParentCanvas.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = buttonName[i];
        IsCompletedReveal = false;

        ///handling text

    }
    void CheckKeyDown()
    {
        if (IsArrived == true)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                timesPress++;
                IsArrived = false;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                timesPress += 3;
                IsArrived = false;
            }
        }
        if (IsArrived == false)
        {
            Vector3 brt0;
            for (int i = 0; i < buttonsShowed.Count; i++)
            {
                brt0 = buttonsShowed[i].GetComponent<RectTransform>().transform.position;
                buttonsShowed[i].GetComponent<RectTransform>().transform.position = Vector3.Lerp(brt0, ButtonPosition[((timesPress % 4) + i)% 4], MoveSpeed * Time.deltaTime + 0.2f);
                if (Vector3.Distance(brt0, ButtonPosition[((timesPress % 4) + i) % 4]) <= 5)
                {
                    IsArrived = true;
                }
            }
        }
        
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
                rt.sizeDelta = new Vector2(100,30);
        //    Debug.Log(button.name+": this distance is: "+Vector2.Distance(rt.position, targetPos)+", target position: "+ targetPos+", with current"+rt.position);
            i++;
            if (Vector2.Distance(rt.position, targetPos) >=200f) {
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

}
