using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour {

    private Vector3 mouseDownPosition;
    private Vector3 cameraDownPosition;
    public float cursorSensitivity = 2.5f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        const int orthographicSizeMin = 100;
        const int orthographicSizeMax = 1000;


        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
 {
            Camera.main.orthographicSize = Camera.main.orthographicSize - 100;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
 {
            Camera.main.orthographicSize = Camera.main.orthographicSize + 100;
        }
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthographicSizeMin, orthographicSizeMax);


        if(Input.GetMouseButtonDown(0))
        {
            cameraDownPosition = transform.position;
            mouseDownPosition = Input.mousePosition;
        }

        if(Input.GetMouseButton(0))
        {
            Vector3 mouseOffsetPosition = -(Input.mousePosition - mouseDownPosition) * Camera.main.orthographicSize / 1000 * cursorSensitivity;
            transform.position = cameraDownPosition + mouseOffsetPosition;
        }

    }
}
