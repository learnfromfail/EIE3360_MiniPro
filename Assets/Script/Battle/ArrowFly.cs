using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowFly : MonoBehaviour {

    public void Fly(int playerId)
    {
        StartCoroutine("FlyProcess", playerId);
    }

    IEnumerator FlyProcess(int playerId)
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 17; i++)
        {
            if(playerId == 1)
            {
                this.gameObject.transform.Translate(0, 0.5f, 0);
            }
            else
            {
                this.gameObject.transform.Translate(0, -0.5f, 0);
            }
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(this.gameObject);
    }
}
