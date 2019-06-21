using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject LookAtPoint;

    public float Distance;
    public float Speed;
    public bool autoSpin;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        var movementVector = Vector3.zero;

        bool goUp = Input.GetKey(KeyCode.UpArrow);
        bool goDown = Input.GetKey(KeyCode.DownArrow);
        bool goLeft = Input.GetKey(KeyCode.LeftArrow);
        bool goRight = Input.GetKey(KeyCode.RightArrow);

        if (autoSpin)
        {
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = true;
        }

        if (goUp != goDown)
        {
            if (!(transform.position.y > 40) && goUp)
            {
                movementVector += transform.up;
            }
            else if (!(transform.position.y < -40) && goDown)
            {
                movementVector += -transform.up;
            }
        }
        if (goLeft != goRight)
        {
            movementVector += (goRight) ? transform.right : -transform.right;
        }

        transform.position += movementVector * Time.deltaTime * ((autoSpin) ?  Speed / 16 : Speed);
        
        var x2 = transform.position.x;
        var y2 = transform.position.y;
        var z2 = transform.position.z;

        var x1 = LookAtPoint.transform.position.x;
        var y1 = LookAtPoint.transform.position.y;
        var z1 = LookAtPoint.transform.position.z;

        var vectorFromLookAt = new Vector3(((x2 - x1)), ((y2 - y1)), ((z2 - z1)));
        var normalizedVector = Vector3.Normalize(vectorFromLookAt);
        var multipliedVector = normalizedVector * Distance;
        transform.position = multipliedVector;
        transform.LookAt(LookAtPoint.transform);
    }
}
