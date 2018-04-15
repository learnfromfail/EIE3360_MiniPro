using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceClass : MonoBehaviour {

    int result;
    string prefabName;
    public Vector3 velocity;
    Rigidbody rb;
    bool check = false;
    Vector3 diceVelocity;
    bool started = false;
    GameObject[] diceList;

    void Start () {
        
	}
	
	void Update () {
        

        if (started == true && rb.velocity.x == 0f && rb.velocity.y == 0f && rb.velocity.z == 0f && check == false)
        {
            float S1 = transform.GetChild(0).gameObject.transform.position.y;
            float S2 = transform.GetChild(1).gameObject.transform.position.y;
            float S3 = transform.GetChild(2).gameObject.transform.position.y;
            float S4 = transform.GetChild(3).gameObject.transform.position.y;
            float S5 = transform.GetChild(4).gameObject.transform.position.y;
            float S6 = transform.GetChild(5).gameObject.transform.position.y;
            float maxValue = (Mathf.Max(S1, S2, S3, S4, S5, S6));
            if (maxValue == S1)
            {
                Debug.Log(string.Concat(this.name, ": 1"));
                result = 1;
            }
            else if (maxValue == S2)
            {
                Debug.Log(string.Concat(this.name, ": 2"));
                result = 2;
            }
            else if (maxValue == S3)
            {
                Debug.Log(string.Concat(this.name, ": 3"));
                result = 3;
            }
            else if (maxValue == S4)
            {
                Debug.Log(string.Concat(this.name, ": 4"));
                result = 4;
            }
            else if (maxValue == S5)
            {
                Debug.Log(string.Concat(this.name, ": 5"));
                result = 5;
            }
            else if (maxValue == S6)
            {
                Debug.Log(string.Concat(this.name, ": 6"));
                result = 6;
            }
            check = true;
        }
    }

    public void InitDice(string name, float force, char side)
    {
        float dirX = Random.Range(0, 1000);
        float dirY = Random.Range(0, 1000);
        float dirZ = Random.Range(0, 1000);
        prefabName = name;
        this.tag = "Dice";
        result = 0;
        this.rb = GameObject.Find(prefabName).GetComponent<Rigidbody>();
        if (side == 'A')
        {
            GameObject.Find(prefabName).GetComponent<Rigidbody>().AddForce(transform.right * force);
        }else if (side == 'D')
        {
            GameObject.Find(prefabName).GetComponent<Rigidbody>().AddForce(-transform.right * force);
        }
        
        GameObject.Find(prefabName).GetComponent<Rigidbody>().AddTorque(dirX, dirY, dirZ);
        started = true;
    }
}