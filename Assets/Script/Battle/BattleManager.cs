using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    //Data from last scene
    public GameObject BattleCamera;

    public /*static*/ string OffenseCharacterType;
    public /*static*/ string DefenseCharacterType;

    public /*static*/ int OffenseCharacterATK ;
    public /*static*/ int OffenseCharacterDEF ;

    public /*static*/ int DefenseCharacterATK ;
    public /*static*/ int DefenseCharacterDEF ;

    public /*static*/ int OffenseCharacterMaxHP ;
    public /*static*/ int OffenseCharacterCurrentHP ;

    public /*static*/ int DefenseCharacterMaxHP ;
    public /*static*/ int DefenseCharacterCurrentHP ;

    public /*static*/ bool rangeATK;

    //Use to define two fighting characters

    public  /*static*/ GameObject player1;
    public /*static*/ GameObject player2;

    public Character p1atker;
    public Character p2defer;

    public int attackTurn;
    public GameObject OffenseCharacter;
    public GameObject DefenseCharacter;

    public GameObject EnglandArcher;
    public GameObject EnglandCavalry;
    public GameObject EnglandInfantry;
    public GameObject FranceArcher;
    public GameObject FranceCavalry;
    public GameObject FranceInfantry;

    public bool IsFighting = false;
    public GameObject fightingUI;

    void Start () {
    }
    
    void Update()
    {
        
    }


    public void StartFight(GameObject attacker, GameObject defender)
    {
        //Use for initialization, get data from last scene
        //Now just sub something into it
        DiceManager.SetValuesZero();
        IsFighting = true;
        fightingUI.SetActive(true);
        Camera.main.gameObject.SetActive(false);
        BattleCamera.SetActive(true);
        checkSoliderType(attacker.GetComponent<Character>(), defender.GetComponent<Character>());
  
        //Declare 2 characters and play animation
        player1 = Instantiate(OffenseCharacter, GameObject.Find("positionP1").transform.position, Quaternion.Euler(new Vector3(0, 90, 0))) as GameObject;
        player2 = Instantiate(DefenseCharacter, GameObject.Find("positionP2").transform.position, Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;

        player1.transform.localScale = new Vector3(30, 30, 30);
        player2.transform.localScale = new Vector3(30, 30, 30);

        player1.GetComponent<BattleAnimation>().playerId = 1;
        player2.GetComponent<BattleAnimation>().playerId = 2;

        if (OffenseCharacterType == "EnglandArcher" || OffenseCharacterType == "FranceArcher")
        {
            GameObject.Find("BattleManager").GetComponent<HealthManager>().AssignHPvaluesFromMap();
            player1.GetComponent<BattleAnimation>().StartCoroutine("ArcherAttack");
            Debug.Log("here11111");
        }
        if (OffenseCharacterType == "EnglandCavalry" || OffenseCharacterType == "FranceCavalry")
        {
            GameObject.Find("BattleManager").GetComponent<HealthManager>().AssignHPvaluesFromMap();
            player1.GetComponent<BattleAnimation>().StartCoroutine("CavalryAttack");
            Debug.Log("here12222");
        }
        if (OffenseCharacterType == "EnglandInfantry" || OffenseCharacterType == "FranceInfantry")
        {
            GameObject.Find("BattleManager").GetComponent<HealthManager>().AssignHPvaluesFromMap();
            player1.GetComponent<BattleAnimation>().StartCoroutine("InfantryAttack");
            Debug.Log("here1222233333");
        }

        if (!rangeATK)
        {
            if (DefenseCharacterType == "EnglandArcher" || DefenseCharacterType == "FranceArcher")
            {
                GameObject.Find("BattleManager").GetComponent<HealthManager>().AssignHPvaluesFromMap();
                player2.GetComponent<BattleAnimation>().StartCoroutine("ArcherRetaliate");
                Debug.Log("here44444");
            }
            if (DefenseCharacterType == "EnglandCavalry" || DefenseCharacterType == "FranceCavalry")
            {
                GameObject.Find("BattleManager").GetComponent<HealthManager>().AssignHPvaluesFromMap();
                player2.GetComponent<BattleAnimation>().StartCoroutine("CavalryRetaliate");
                Debug.Log("here5555");
            }
            if (DefenseCharacterType == "EnglandInfantry" || DefenseCharacterType == "FranceInfantry")
            {
                GameObject.Find("BattleManager").GetComponent<HealthManager>().AssignHPvaluesFromMap();
                player2.GetComponent<BattleAnimation>().StartCoroutine("InfantryRetaliate");
                Debug.Log("here66666");
            }
        }
    }

    public void checkSoliderType(Character p1, Character p2)
    {
        if (p1.career == 0)
        {
            OffenseCharacter = EnglandCavalry; OffenseCharacterType = "EnglandCavalry";
        }
        if (p1.career == 1)
        {
            OffenseCharacter = EnglandInfantry; OffenseCharacterType = "EnglandInfantry";
        }
        if (p1.career == 2)
        {
            OffenseCharacter = EnglandArcher; OffenseCharacterType = "EnglandArcher";
        }

        if (p2.career == 0)
        {
            DefenseCharacter = FranceCavalry; DefenseCharacterType = "FranceCavalry";
        }
        if (p2.career == 1)
        {
            DefenseCharacter = FranceInfantry; DefenseCharacterType = "FranceInfantry";
        }
        if (p2.career == 2)
        {
            DefenseCharacter = FranceArcher; DefenseCharacterType = "FranceArcher";
        }
        p1atker = p1;
        p2defer = p2;
        OffenseCharacterATK = p1.attack;
        OffenseCharacterDEF = p1.defense;
        DefenseCharacterATK = p2.attack;
        DefenseCharacterDEF = p2.defense;



        OffenseCharacterMaxHP = p1.Hp;
        OffenseCharacterCurrentHP = p1.Hp;
        DefenseCharacterMaxHP = p2.Hp;
        DefenseCharacterCurrentHP = p2.Hp;

        gameObject.GetComponent<HealthManager>().UpdateTextValue(p1,p2);


        if ((p1.career == 2 && p2.career == 0) || (p1.career == 2 && p2.career == 1))
            rangeATK = true;
        else
            rangeATK = false;
    }
}
