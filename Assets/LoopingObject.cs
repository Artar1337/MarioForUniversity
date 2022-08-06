using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingObject : MonoBehaviour
{
    public float startPosition = -30f;
    public float endPosition = 230f;
    public float movingSpeed = 0.3f;
    
    // Update is called once per frame (physics events)
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x + movingSpeed * Time.deltaTime, this.transform.position.y);
        if (this.transform.position.x > endPosition)
        {
            this.transform.position = new Vector3(startPosition, this.transform.position.y);
        }
    }
}
