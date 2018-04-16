using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAnimator : MonoBehaviour {
    public static backgroundSetting BS;

    public GameObject EnglandArcher;
    public GameObject EnglandCavalry;
    public GameObject EnglandInfantry;
    public GameObject FranceArcher;
    public GameObject FranceCavalry;
    public GameObject FranceInfantry;

    public GameObject Model;
    // Use this for initialization
    void Start () {
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();
        Initalize();
        GetComponent<Character>().ModelMovement = GetComponent<CharacterAnimator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Initalize()
    {//   Model = new GameObject();

        if (gameObject.GetComponent<Character>().career == 0)
        {
         Model = Instantiate(EnglandCavalry,transform.position, Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;
        }else if(gameObject.GetComponent<Character>().career == 1)
        {
        Model = Instantiate(EnglandInfantry, transform.position, Quaternion.Euler(new Vector3(0, -90, 0))) as GameObject;
        }
        else // ==2
        {
            Model = Instantiate(EnglandArcher, transform.position, Quaternion.Euler(new Vector3(0, -90, 0)));
            Model.transform.SetParent(this.gameObject.transform);
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
    public void MoveAnimation(bool trueOrFalse)
    {
        Model.GetComponent<Animator>().SetBool("Run", trueOrFalse);
    }
}
