using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimation : MonoBehaviour {
    public backgroundSetting BS;
    public int playerId;
    public int anotherPlayerId;
    string[] playerArray = { null, "player1", "player2" };

    GameObject slashParticle;
    GameObject TempSlashParticle;
    GameObject arrow;

    Rigidbody rb;
    Animator anim;
    public bool isAttacking;
    public bool retaliate;

    Vector3 hitOffset = new Vector3(0, 1, 0);

    // Use this for initialization
    void Start () {
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        slashParticle = GameObject.Find("SparkParticles");
        arrow = GameObject.Find("arrow");
    }
	
	// Update is called once per frame
	void Update () {

    }

    /*
    ##########
    Infatry
    ##########
    */

    public IEnumerator InfantryAttack()
    {
        //Initialize player relationship
        if (playerId == 1)
        {
            anotherPlayerId = 2;
        }
        else if (playerId == 2)
        {
            anotherPlayerId = 1;
        }

        GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn = playerId;
        //Ready

        yield return new WaitForSeconds(0.5f);
        if (retaliate)
        {
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollReATK");
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollReDEF");
        }
        else
        {
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollATK");
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollDEF");
        }
        
        yield return new WaitForSeconds(0.5f);
        //Run to the front of enemy
        anim.SetBool("Run", true);
        for (int i = 0; i < 50; i++)
        {
            transform.Translate(0, 0, 3 * 0.01f * 5.0f);
            yield return new WaitForSeconds(0.01f);
        }
        anim.SetBool("Run", false);
        //Attack!!!
        anim.SetTrigger("Attack");

        if (playerId == 1)
        {
            yield return new WaitForSeconds(0.5f);
            DealDamage();
            /*BattleManager*/  BS.BM.player2.GetComponent<Animator>().SetTrigger("Impact");
            TempSlashParticle = Instantiate(slashParticle, /*BattleManager*/  BS.BM.player2.transform.position + hitOffset, Quaternion.identity);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            DealDamage();
            /*BattleManager*/  BS.BM.player1.GetComponent<Animator>().SetTrigger("Impact");
            TempSlashParticle = Instantiate(slashParticle, /*BattleManager*/  BS.BM.player1.transform.position + hitOffset, Quaternion.identity);
        }
        TempSlashParticle.GetComponent<ParticleSystem>().Play();
        
        //Wait for the end of attack animation
        yield return new WaitForSeconds(0.6f);
        //Run!!!
        for (int i = 0; i < 20; i++)
        {
            transform.Rotate(0, 9f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        anim.SetBool("Run", true);
        for (int i = 0; i < 50; i++)
        {
            transform.Translate(0, 0, 3 * 0.01f * 5.0f);
            yield return new WaitForSeconds(0.01f);
        }
        //Prepare to defence
        anim.SetBool("Run", false);
        for (int i = 0; i < 20; i++)
        {
            transform.Rotate(0, 9f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        //Change to enemy's attack round
        GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn = anotherPlayerId;
        Destroy(TempSlashParticle);
        TuneCamera();

    }

    public IEnumerator InfantryRetaliate()
    {
        while(GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn != playerId)
        {
            yield return new WaitForSeconds(0.5f);
        }
        retaliate = true;
        StartCoroutine("InfantryAttack");
    }

    /*
    ##########
    Cavalry
    ##########
    */

    public IEnumerator CavalryAttack()
    {
        //Initialize player relationship
        if (playerId == 1)
        {
            anotherPlayerId = 2;
        }
        else if (playerId == 2)
        {
            anotherPlayerId = 1;
        }

        GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn = playerId;
        //Ready
        yield return new WaitForSeconds(0.5f);
        if (retaliate)
        {
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollReATK");
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollReDEF");
        }
        else
        {
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollATK");
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollDEF");
        }

        yield return new WaitForSeconds(0.5f);
        //Run to the front of enemy
        anim.SetBool("Run", true);
        for (int i = 0; i < 75; i++)
        {
            transform.Translate(0, 0, 3 * 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        anim.SetBool("Run", false);
        //Attack!!!
        anim.SetTrigger("Attack");

        if (playerId == 1)
        {
            yield return new WaitForSeconds(1.25f);
            DealDamage();
            /*BattleManager*/  /*BS.BM.player2*/
            BS.BM.player2.GetComponent<Animator>().SetTrigger("Impact");
            TempSlashParticle = Instantiate(slashParticle, /*BattleManager*/  /*BS.BM.player2*/BS.BM.player2.transform.position + hitOffset, Quaternion.identity);
        }
        else
        {
            yield return new WaitForSeconds(1.25f);
            DealDamage();
            /*BattleManager*/  BS.BM.player1.GetComponent<Animator>().SetTrigger("Impact");
            TempSlashParticle = Instantiate(slashParticle, /*BattleManager*/  BS.BM.player1.transform.position + hitOffset, Quaternion.identity);
        }
        TempSlashParticle.GetComponent<ParticleSystem>().Play();

        //Wait for the end of attack animation
        yield return new WaitForSeconds(1.75f);
        //Run!!!
        anim.SetBool("Run", true);
        for (int i = 0; i < 75; i++)
        {
            transform.Translate(0, 0, -3 * 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
        anim.SetBool("Run", false);
        //Change to enemy's attack round
        GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn = anotherPlayerId;
        Destroy(TempSlashParticle);
        TuneCamera();

    }

    public IEnumerator CavalryRetaliate()
    {
        while (GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn != playerId)
        {
            yield return new WaitForSeconds(0.5f);
        }
        retaliate = true;
        StartCoroutine("CavalryAttack");
    }

    /*
    ##########
    Archer
    ##########
    */

    public IEnumerator ArcherAttack()
    {
        //Initialize player relationship
        if (playerId == 1)
        {
            anotherPlayerId = 2;
        }
        else if (playerId == 2)
        {
            anotherPlayerId = 1;
        }

        GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn = playerId;
        //Ready
        yield return new WaitForSeconds(0.5f);
        if (retaliate)
        {
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollReATK");
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollReDEF");
        }
        else
        {
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollATK");
            GameObject.Find("BattleManager").GetComponent<DiceManager>().StartCoroutine("RollDEF");
        }

        yield return new WaitForSeconds(0.5f);
        
        //Attack!!!
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(2.5f);

        if (playerId == 1)
        {
            Vector3 arrowSpawnLocation = new Vector3(/*BattleManager*/  BS.BM.player1.transform.position.x + 1f, /*BattleManager*/  BS.BM.player1.transform.position.y + 1.9f, /*BattleManager*/  BS.BM.player1.transform.position.z - 0.15f);
            GameObject arrowInst = Instantiate(arrow, arrowSpawnLocation, Quaternion.Euler(new Vector3(0, 0, -90)));
            arrowInst.GetComponent<ArrowFly>().Fly(1);
            yield return new WaitForSeconds(0.7f);
            DealDamage();
            /*BattleManager*/  BS.BM.player2.GetComponent<Animator>().SetTrigger("Impact");
            TempSlashParticle = Instantiate(slashParticle, /*BattleManager*/  BS.BM.player2.transform.position + hitOffset, Quaternion.identity);
        }
        else
        {
            Vector3 arrowSpawnLocation = new Vector3(/*BattleManager*/  BS.BM.player2.transform.position.x - 1f, /*BattleManager*/  BS.BM.player2.transform.position.y + 1.9f, /*BattleManager*/  BS.BM.player2.transform.position.z + 0.15f);
            GameObject arrowInst = Instantiate(arrow, arrowSpawnLocation, Quaternion.Euler(new Vector3(0, 0, -90)));
            arrowInst.GetComponent<ArrowFly>().Fly(2);
            yield return new WaitForSeconds(0.7f);
            DealDamage();
            /*BattleManager*/  BS.BM.player1.GetComponent<Animator>().SetTrigger("Impact");
            TempSlashParticle = Instantiate(slashParticle, /*BattleManager*/  BS.BM.player1.transform.position + hitOffset, Quaternion.identity);
        }
        TempSlashParticle.GetComponent<ParticleSystem>().Play();

        //Wait for the end of attack animation
        yield return new WaitForSeconds(1.75f);

        //Change to enemy's attack round
        GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn = anotherPlayerId;
        Destroy(TempSlashParticle);
        TuneCamera();
    }

    public IEnumerator ArcherRetaliate()
    {
        while (GameObject.Find("BattleManager").GetComponent<BattleManager>().attackTurn != playerId)
        {
            yield return new WaitForSeconds(0.5f);
        }
        retaliate = true;
        StartCoroutine("ArcherAttack");
    }

    /*
    ##########
    Calculate Damage
    ##########
    */

    public void DealDamage(){
       
        int damage;
        if (playerId == 1)
        {
            damage = DiceManager.ATKDiceResult - DiceManager.DEFDiceResult;
        }
        else {
            damage = DiceManager.ReATKDiceResult - DiceManager.ReDEFDiceResult;
        }

        if (damage < 0)
            damage = 0;

        if (DiceManager.ReResult == 0) {
            GameObject.Find("BattleManager").GetComponent<HealthManager>().Damage(anotherPlayerId, damage);
        }
        else
        {
            GameObject.Find("BattleManager").GetComponent<HealthManager>().Damage(anotherPlayerId, damage);
            GameObject.Find("BattleManager").GetComponent<HealthManager>().Damage(playerId, DiceManager.ReResult);
        }  
    }

    public void TuneCamera()
    {
        if (BS.BM.rangeATK == true)
        {
            retaliate = false;
            if (BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Character>().IsCompanion == false)
                BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Enemy>().IsGoNext3 = true;
            else
            {
                BS.Restart();
                BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Character>().menuc.ClickWait();
            }
            BS.main_camera.gameObject.SetActive(true);
            BS.BM.BattleCamera.SetActive(false);
            BS.BM.fightingUI.SetActive(false);
            Destroy(BS.BM.player1);
            Destroy(BS.BM.player2);
        }else if (retaliate)
            {
                retaliate = false;
                if (BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Character>().IsCompanion == false)
                    BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Enemy>().IsGoNext3 = true;
                else
                {
                    BS.Restart();
                    BS.Characters[BS.RankWhoseTurn(BS.round + 1)].GetComponent<Character>().menuc.ClickWait();
                }
                BS.main_camera.gameObject.SetActive(true);
                BS.BM.BattleCamera.SetActive(false);
                BS.BM.fightingUI.SetActive(false);
                Destroy(BS.BM.player1);
                Destroy(BS.BM.player2);
            }
    }
}
