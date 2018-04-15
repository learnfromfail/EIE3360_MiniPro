using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStageInfo : MonoBehaviour {

    public static GameObject[,] stageElement = new GameObject[2,5];
    /* stageElement[,] Index:
    stageElement[][0] <- StageContainer
    stageElement[][1] <- FlagBack
    stageElement[][2] <- Pillar
    stageElement[][3] <- Flag
    stageElement[][4] <- Information
    */
    public static ShowStageInfo StageInfoManager;

    static int selectedStage;
    public static bool enteredSelection = false;

    void Awake()
    {
        StageInfoManager = this;
    }

    void Start()
    {
        //Put Stage elements into array

        //Battle of Crecy
        stageElement[0,0] = GameObject.Find("Crecy");
        stageElement[0,1] = GameObject.Find("EdwardFlagBack");
        stageElement[0,2] = GameObject.Find("CrecyPillar");
        stageElement[0,3] = GameObject.Find("EdwardFlag");
        stageElement[0,4] = GameObject.Find("CrecyInfo");

        //Siege of Orleans
        stageElement[1,0] = GameObject.Find("Orleans");
        stageElement[1,1] = GameObject.Find("JoanFlagBack");
        stageElement[1,2] = GameObject.Find("OrleansPillar");
        stageElement[1,3] = GameObject.Find("JoanFlag");
        stageElement[1,4] = GameObject.Find("OrleansInfo");

    }

    public static void showStageInfo(int stageNum)
    {
        enteredSelection = true;
        selectedStage = stageNum;
        GameObject.Find("Public").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("ButtonsLayer").transform.localScale = new Vector3(1, 1, 1);
        stageElement[stageNum,0].transform.localScale = new Vector3(1, 1, 1);
        StageInfoManager.StartCoroutine("showFlag", stageNum);
    }

    IEnumerator showFlag(int stageNum)
    {
        for (int i = 0; i < 20; i++)
        {
            stageElement[stageNum, 1].transform.localPosition = new Vector2(stageElement[stageNum, 1].transform.localPosition.x, stageElement[stageNum, 1].transform.localPosition.y-22.5f);
            stageElement[stageNum, 3].transform.localPosition = new Vector2(stageElement[stageNum, 3].transform.localPosition.x, stageElement[stageNum, 3].transform.localPosition.y-22.5f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    //Back and Start Button

    public void backButton()
    {
        GameObject.Find("ButtonsLayer").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Public").transform.localScale = new Vector3(0, 0, 0);
        stageElement[selectedStage, 0].transform.localScale = new Vector3(0, 0, 0);
        stageElement[selectedStage, 1].transform.localPosition = new Vector2(0, 450);
        stageElement[selectedStage, 3].transform.localPosition = new Vector2(0, 450);
        selectedStage = -1; //Nothing
        enteredSelection = false;
    }

    public void startButton()
    {

    } 
}

