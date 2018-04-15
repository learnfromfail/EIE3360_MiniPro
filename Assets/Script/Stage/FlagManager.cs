using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour {

    void OnMouseDown()
    {
        if (!ShowStageInfo.enteredSelection) {
            switch (name)
            {
                case "EnglandFlag":
                    ShowStageInfo.showStageInfo(0);
                    break;
                case "FranceFlag":
                    ShowStageInfo.showStageInfo(1);
                    break;
            }
        }
        
    }
}
