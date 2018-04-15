using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {
    public backgroundSetting BS;

    public Image player1HealthBar;
   public Image player2HealthBar;

    public Text player1HealthText;
    public Text player2HealthText;

    int player1MaxHP;
    int player1CurrentHP;
    int player2MaxHP;
    int player2CurrentHP;

    int player1CurrentShowingHP;
    int player2CurrentShowingHP;

    // Use this for initialization
    void Start () {
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();
        //    player1HealthBar = GameObject.Find("Player1HealthBar").GetComponent<Image>();
        //   player1HealthText = GameObject.Find("Player1HealthText").GetComponent<Text>();

        //   player2HealthBar = GameObject.Find("Player2HealthBar").GetComponent<Image>();
        //   player2HealthText = GameObject.Find("Player2HealthText").GetComponent<Text>();
        /*
        player1MaxHP = BS.BM.OffenseCharacterMaxHP;
        player1CurrentHP = BS.BM.OffenseCharacterCurrentHP;

        player2MaxHP = BS.BM.DefenseCharacterMaxHP;
        player2CurrentHP = BS.BM.DefenseCharacterCurrentHP;

        player1HealthText.text = player1MaxHP.ToString();
        player1CurrentShowingHP = player1MaxHP;

        player2HealthText.text = player2MaxHP.ToString();
        player2CurrentShowingHP = player2MaxHP;
        */
    }

    public void Damage(int playerId, int damage)
    {
        if (playerId == 1)
        {
            player1CurrentHP -= damage;
            StartCoroutine("P1LerpHealthBar");
            StartCoroutine("P1LerpHealthText", damage);
        }
        else if (playerId == 2)
        {
            player2CurrentHP -= damage;
            StartCoroutine("P2LerpHealthBar");
            StartCoroutine("P2LerpHealthText", damage);
        }
    }

    IEnumerator P1LerpHealthBar()
    {
        float targetPosition = (float)player1CurrentHP / (float)player1MaxHP;
        while (player1HealthBar.fillAmount > targetPosition)
        {
            player1HealthBar.fillAmount -= 0.01f;
            player1HealthBar.color = Color.Lerp(Color.red, Color.green, player1HealthBar.fillAmount);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator P1LerpHealthText(int damage)
    {
        while (player1HealthText.text != (player1CurrentHP).ToString())
        {
            player1HealthText.text = (player1CurrentShowingHP - 1).ToString();
            player1CurrentShowingHP--;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator P2LerpHealthBar()
    {
        float targetPosition = (float)player2CurrentHP / (float)player2MaxHP;
        while (player2HealthBar.fillAmount > targetPosition)
        {
            player2HealthBar.fillAmount -= 0.01f;
            player2HealthBar.color = Color.Lerp(Color.red, Color.green, player2HealthBar.fillAmount);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator P2LerpHealthText(int damage)
    {
        while (player2HealthText.text != (player2CurrentHP).ToString())
        {
            player2HealthText.text = (player2CurrentShowingHP - 1).ToString();
            player2CurrentShowingHP--;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
