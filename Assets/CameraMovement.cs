using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform Mario;
    private float previousMarioPositionX;

    public float offset = 4f;

    
    // Start is called before the first frame update
    void Start()
    {
        Mario = GameObject.Find("Mario").transform;
        previousMarioPositionX = Mario.position.x;
        this.transform.position = new Vector3(Mario.transform.position.x + offset,
                this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mario.transform.position.x > previousMarioPositionX)
        {
            this.transform.position = new Vector3(Mario.transform.position.x + offset, 
                this.transform.position.y, this.transform.position.z);
            previousMarioPositionX = Mario.transform.position.x;
        }
    }
}
