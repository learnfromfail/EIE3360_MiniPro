using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour {
    public backgroundSetting BS;

    public /*static*/ GameObject ArcherATK0;
   public  /*static*/ GameObject ArcherATK1;
   public /*static*/ GameObject ArcherATK2;
    public /*static*/ GameObject CavalryATK0;
    public /*static*/ GameObject CavalryATK1;
    public /*static*/ GameObject CavalryATK2;
    public /*static*/ GameObject InfantryATK0;
    public /*static*/ GameObject InfantryATK1;
    public /*static*/ GameObject InfantryATK2;

    public /*static*/ GameObject DEF0;
    public /*static*/ GameObject DEF1;
    public /*static*/ GameObject DEF2;
    public /*static*/ GameObject DEFRe;

    static GameObject diceObj;

    static Vector2 ATKStartPlacingDicePos = new Vector2(-225, -180);
    static Vector2 DEFStartPlacingDicePos = new Vector2(225, -225);

    public static int ATKDiceResult;
    public static int DEFDiceResult;
    public static int ReATKDiceResult;
    public static int ReDEFDiceResult;
    public static int ReResult;

    // Use this for initialization
    void Start () {
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();
        /*
        ArcherATK0 = GameObject.Find("Dice_ArcherAttack0");
        ArcherATK1 = GameObject.Find("Dice_ArcherAttack1");
        ArcherATK2 = GameObject.Find("Dice_ArcherAttack2");
        CavalryATK0 = GameObject.Find("Dice_CavalryAttack0");
        CavalryATK1 = GameObject.Find("Dice_CavalryAttack1");
        CavalryATK2 = GameObject.Find("Dice_CavalryAttack2");
        InfantryATK0 = GameObject.Find("Dice_InfantryAttack0");
        InfantryATK1 = GameObject.Find("Dice_InfantryAttack1");
        InfantryATK2 = GameObject.Find("Dice_InfantryAttack2");

        DEF0 = GameObject.Find("Dice_Defense0");
        DEF1 = GameObject.Find("Dice_Defense1");
        DEF2 = GameObject.Find("Dice_Defense2");
        DEFRe = GameObject.Find("Dice_DefenseRe");
        */
    }
	
    public IEnumerator RollATK()
    {
        int diceNum = BS.BM.OffenseCharacterATK;
        string type = BS.BM.OffenseCharacterType;

        int offset = 0;
        for (int i = 0; i < diceNum; i++)
        {
            int dice = Random.Range(1,7);

            if (type == "EnglandArcher" || type == "FranceArcher")
            {
                switch (dice)
                {
                    case 1:
                        diceObj = Instantiate(ArcherATK0);
                        break;
                    case 2:
                        diceObj = Instantiate(ArcherATK0);
                        break;
                    case 3:
                        diceObj = Instantiate(ArcherATK1);
                        ATKDiceResult++;
                        break;
                    case 4:
                        diceObj = Instantiate(ArcherATK1);
                        ATKDiceResult++;
                        break;
                    case 5:
                        diceObj = Instantiate(ArcherATK1);
                        ATKDiceResult++;
                        break;
                    case 6:
                        diceObj = Instantiate(ArcherATK2);
                        ATKDiceResult += 2;
                        break;
                }
            }
            else if (type == "EnglandCavalry" || type == "FranceCavalry")
            {
                switch (dice)
                {
                    case 1:
                        diceObj = Instantiate(CavalryATK0);
                        break;
                    case 2:
                        diceObj = Instantiate(CavalryATK0);
                        break;
                    case 3:
                        diceObj = Instantiate(CavalryATK1);
                        ATKDiceResult++;
                        break;
                    case 4:
                        diceObj = Instantiate(CavalryATK1);
                        ATKDiceResult++;
                        break;
                    case 5:
                        diceObj = Instantiate(CavalryATK1);
                        ATKDiceResult++;
                        break;
                    case 6:
                        diceObj = Instantiate(CavalryATK2);
                        ATKDiceResult += 2;
                        break;
                }
            }
            else if (type == "EnglandInfantry" || type == "FranceInfantry")
            {
                switch (dice)
                {
                    case 1:
                        diceObj = Instantiate(InfantryATK0);
                        break;
                    case 2:
                        diceObj = Instantiate(InfantryATK0);
                        break;
                    case 3:
                        diceObj = Instantiate(InfantryATK1);
                        ATKDiceResult++;
                        break;
                    case 4:
                        diceObj = Instantiate(InfantryATK1);
                        ATKDiceResult++;
                        break;
                    case 5:
                        diceObj = Instantiate(InfantryATK1);
                        ATKDiceResult++;
                        break;
                    case 6:
                        diceObj = Instantiate(InfantryATK2);
                        ATKDiceResult += 2;
                        break;
                }
            }
            diceObj.transform.SetParent(GameObject.Find("UI").transform);
            diceObj.transform.localScale = new Vector2(1, 1);
            diceObj.transform.localPosition = new Vector2(ATKStartPlacingDicePos.x + offset, ATKStartPlacingDicePos.y);
            StartCoroutine("DestroyDiceInstant", diceObj);
            yield return new WaitForSeconds(0.1f);
            offset += 60;
        }
    }

    public IEnumerator RollReATK()
    {
        int diceNum = BS.BM.DefenseCharacterATK;
        string type = BS.BM.DefenseCharacterType;

        int offset = 0;
        for (int i = 0; i < diceNum; i++)
        {
            int dice = Random.Range(1, 7);

            if (type == "EnglandArcher" || type == "FranceArcher")
            {
                switch (dice)
                {
                    case 1:
                        diceObj = Instantiate(ArcherATK0);
                        break;
                    case 2:
                        diceObj = Instantiate(ArcherATK0);
                        break;
                    case 3:
                        diceObj = Instantiate(ArcherATK1);
                        ReATKDiceResult++;
                        break;
                    case 4:
                        diceObj = Instantiate(ArcherATK1);
                        ReATKDiceResult++;
                        break;
                    case 5:
                        diceObj = Instantiate(ArcherATK1);
                        ReATKDiceResult++;
                        break;
                    case 6:
                        diceObj = Instantiate(ArcherATK2);
                        ReATKDiceResult += 2;
                        break;
                }
            }
            else if (type == "EnglandCavalry" || type == "FranceCavalry")
            {
                switch (dice)
                {
                    case 1:
                        diceObj = Instantiate(CavalryATK0);
                        break;
                    case 2:
                        diceObj = Instantiate(CavalryATK0);
                        break;
                    case 3:
                        diceObj = Instantiate(CavalryATK1);
                        ReATKDiceResult++;
                        break;
                    case 4:
                        diceObj = Instantiate(CavalryATK1);
                        ReATKDiceResult++;
                        break;
                    case 5:
                        diceObj = Instantiate(CavalryATK1);
                        ReATKDiceResult++;
                        break;
                    case 6:
                        diceObj = Instantiate(CavalryATK2);
                        ReATKDiceResult += 2;
                        break;
                }
            }
            else if (type == "EnglandInfantry" || type == "FranceInfantry")
            {
                switch (dice)
                {
                    case 1:
                        diceObj = Instantiate(InfantryATK0);
                        break;
                    case 2:
                        diceObj = Instantiate(InfantryATK0);
                        break;
                    case 3:
                        diceObj = Instantiate(InfantryATK1);
                        ReATKDiceResult++;
                        break;
                    case 4:
                        diceObj = Instantiate(InfantryATK1);
                        ReATKDiceResult++;
                        break;
                    case 5:
                        diceObj = Instantiate(InfantryATK1);
                        ReATKDiceResult++;
                        break;
                    case 6:
                        diceObj = Instantiate(InfantryATK2);
                        ReATKDiceResult += 2;
                        break;
                }
            }
            diceObj.transform.SetParent(GameObject.Find("UI").transform);
            diceObj.transform.localScale = new Vector2(1, 1);
            diceObj.transform.localPosition = new Vector2(DEFStartPlacingDicePos.x - offset, DEFStartPlacingDicePos.y);
            StartCoroutine("DestroyDiceInstant", diceObj);
            yield return new WaitForSeconds(0.1f);
            offset += 60;
        }
    }

    public IEnumerator RollDEF()
    {
        int diceNum = BS.BM.DefenseCharacterDEF;

        int offset = 0;
        for (int i = 0; i < diceNum; i++)
        {
            int dice = Random.Range(1, 7);
            {
                switch (dice)
                {
                    case 1:
                        diceObj = Instantiate(DEF0);
                        break;
                    case 2:
                        diceObj = Instantiate(DEF0);
                        break;
                    case 3:
                        diceObj = Instantiate(DEF1);
                        DEFDiceResult++;
                        break;
                    case 4:
                        diceObj = Instantiate(DEF1);
                        DEFDiceResult++;
                        break;
                    case 5:
                        diceObj = Instantiate(DEF2);
                        DEFDiceResult += 2;
                        break;
                    case 6:
                        diceObj = Instantiate(DEFRe);
                        ReResult++;
                        break;
                }
            }

            diceObj.transform.SetParent(GameObject.Find("UI").transform);
            diceObj.transform.localScale = new Vector2(1, 1);
            diceObj.transform.localPosition = new Vector2(DEFStartPlacingDicePos.x - offset, DEFStartPlacingDicePos.y);
            StartCoroutine("DestroyDiceInstant", diceObj);
            yield return new WaitForSeconds(0.1f);
            offset += 60;
        }
    }

    public IEnumerator RollReDEF()
    {
        int diceNum = BS.BM.OffenseCharacterDEF;

        int offset = 0;
        for (int i = 0; i < diceNum; i++)
        {
            int dice = Random.Range(1, 7);
            {
                switch (dice)
                {
                    case 1:
                        diceObj = Instantiate(DEF0);
                        break;
                    case 2:
                        diceObj = Instantiate(DEF0);
                        break;
                    case 3:
                        diceObj = Instantiate(DEF1);
                        ReDEFDiceResult++;
                        break;
                    case 4:
                        diceObj = Instantiate(DEF1);
                        ReDEFDiceResult++;
                        break;
                    case 5:
                        diceObj = Instantiate(DEF2);
                        ReDEFDiceResult += 2;
                        break;
                    case 6:
                        diceObj = Instantiate(DEF1);
                        ReDEFDiceResult++;
                        break;
                }
            }

            diceObj.transform.SetParent(GameObject.Find("UI").transform);
            diceObj.transform.localScale = new Vector2(1, 1);
            diceObj.transform.localPosition = new Vector2(ATKStartPlacingDicePos.x + offset, ATKStartPlacingDicePos.y);
            StartCoroutine("DestroyDiceInstant",diceObj);
            yield return new WaitForSeconds(0.1f);
            offset += 60;
        }

        /*
        yield return new WaitForSeconds(1f);
        Debug.Log(ATKDiceResult);
        Debug.Log(DEFDiceResult);
        Debug.Log(ReATKDiceResult);
        Debug.Log(ReDEFDiceResult);
        */
    }

    IEnumerator DestroyDiceInstant(GameObject diceObj)
    {
        yield return new WaitForSeconds(3.5f);
        Destroy(diceObj);
    }
}
