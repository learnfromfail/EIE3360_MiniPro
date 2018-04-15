using UnityEngine;
using System.Collections;

public class MoveMap : MonoBehaviour
{
    float dragSpeed = 0.05f;
    private Vector3 dragOrigin;

    bool cameraDragging = true;

    float outerLeft = -0.5f;
    float outerRight = 0.5f;
    float outerUp = 0.5f;
    float outerDown = -0.75f;


    void Update()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float left = Screen.width * 0.2f;
        float right = Screen.width - (Screen.width * 0.2f);
        float up = Screen.height * 0.2f;
        float down = Screen.height - (Screen.height * 0.2f);

        if (mousePosition.x < left)
        {
            cameraDragging = true;
        }
        else if (mousePosition.x > right)
        {
            cameraDragging = true;
        }
        if (mousePosition.y < up)
        {
            cameraDragging = true;
        }
        else if (mousePosition.y > down)
        {
            cameraDragging = true;
        }

        if (cameraDragging && !ShowStageInfo.enteredSelection)
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

            
            if (move.x > 0f && move.z > 0f)
            {
                if (this.transform.position.x < outerRight && this.transform.position.z < outerUp)
                {
                    transform.Translate(move, Space.World);
                }
            }
            else if (move.x > 0f && move.z < 0f)
            {
                if (this.transform.position.x < outerRight && this.transform.position.z > outerDown)
                {
                    transform.Translate(move, Space.World);
                }
            }
            else if (move.x < 0f && move.z > 0f)
            {
                if (this.transform.position.x > outerLeft && this.transform.position.z < outerUp)
                {
                    transform.Translate(move, Space.World);
                }
            }
            else if (move.x < 0f && move.z < 0f)
            {
                if (this.transform.position.x > outerLeft && this.transform.position.z > outerDown)
                {
                    transform.Translate(move, Space.World);
                }
            }
        }
    }
}