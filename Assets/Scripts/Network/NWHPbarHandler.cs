using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NWHPbarHandler : NetworkBehaviour
{
    public NWbackgroundSetting BS;
    public GameObject HpBarPrefab;
    public List<GameObject> AllHpbars;
    public Camera LocalPlayerCamera; 

	void Start () {
        AllHpbars = new List<GameObject>();
        BS = GameObject.Find("EventSystem").GetComponent<NWbackgroundSetting>();
        
        StartCoroutine("SetupInCorrectSequence");
    }

    IEnumerator SetupInCorrectSequence()
    {
        int fromZero = 0;
        while (true)
        {   if (fromZero == 2)
            {
                LocalPlayerCamera = GameObject.Find("MyCharacter").GetComponentInChildren<Camera>();
                CmdSetupHPbar();
            }
            if (fromZero == 4)
                break;

            fromZero++;
            Debug.Log("from0: "+fromZero);
            yield return new WaitForSeconds(1);

        }
    }
    [Command]
    void CmdSetupHPbar()
    {
        foreach(GameObject charGO in BS.Characters)
        {
            Vector3 HPbarPosition = new Vector3(/*charGO.transform.position.x*/0, 2, 0);
            Quaternion toRotation = Quaternion.FromToRotation(transform.forward, charGO.transform.position - LocalPlayerCamera.transform.position);
            GameObject CharGoHpBar = Instantiate(HpBarPrefab, HPbarPosition, toRotation, charGO.transform);
            AllHpbars.Add(CharGoHpBar);
            CharGoHpBar.GetComponent<RectTransform>().localPosition = new Vector3(0,2,0);
            CharGoHpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(1, 1);
            CharGoHpBar.name = charGO.name+" Hp Bar";
            CharGoHpBar.transform.LookAt(LocalPlayerCamera.transform);
            CharGoHpBar.transform.GetChild(0).GetChild(0).GetComponent<Slider>().maxValue = charGO.GetComponent<NWCharacter>().HpMax;
            CharGoHpBar.transform.GetChild(0).GetChild(0).GetComponent<Slider>().value = charGO.GetComponent<NWCharacter>().HpMax;
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
            hpbar.transform.LookAt(LocalPlayerCamera.transform);
        }
    }
}
