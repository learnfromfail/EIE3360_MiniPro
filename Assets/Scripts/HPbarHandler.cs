using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbarHandler : MonoBehaviour {
    public backgroundSetting BS;
    public GameObject HpBarPrefab;
    public List<GameObject> AllHpbars;

	void Start () {
        AllHpbars = new List<GameObject>();
        BS = GameObject.Find("EventSystem").GetComponent<backgroundSetting>();
        
        StartCoroutine("SetupInCorrectSequence");
    }

    IEnumerator SetupInCorrectSequence()
    {
        int fromZero = 0;
        while (true)
        {   if (fromZero == 2)
                SetupHPbar();
            if (fromZero == 4)
                break;
            fromZero++;
            Debug.Log("from0: "+fromZero);
            yield return new WaitForSeconds(1);

        }
    }


    void SetupHPbar()
    {
        foreach(GameObject charGO in BS.Characters)
        {
            Vector3 HPbarPosition = new Vector3(/*charGO.transform.position.x*/0, 2, 0);
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, charGO.transform.position - Camera.main.transform.position);
            GameObject CharGoHpBar = Instantiate(HpBarPrefab, HPbarPosition, toRotation, charGO.transform);
            AllHpbars.Add(CharGoHpBar);
            CharGoHpBar.GetComponent<RectTransform>().localPosition = new Vector3(0,2,0);
            CharGoHpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
            CharGoHpBar.name = charGO.name+" Hp Bar";
            CharGoHpBar.transform.LookAt(Camera.main.transform);
            CharGoHpBar.transform.GetChild(0).GetChild(0).GetComponent<Slider>().maxValue = charGO.GetComponent<Character>().HpMax;
            CharGoHpBar.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = charGO.GetComponent<Character>().HpMax;
            Debug.Log(CharGoHpBar.name+" is");
        }    
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateHPbarRotation()
    {
        foreach(GameObject hpbar in AllHpbars)
        {
            hpbar.transform.LookAt(Camera.main.transform);
        }
    }
}
