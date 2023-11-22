using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject objectToFollow;

    public float lerpPerSecond = 0.5f;
    public bool lockYAxis = false;
    // Update is called once per frame
    void Update()
    {
        Vector3 objPosition = objectToFollow.transform.position;
        objPosition.z = transform.position.z;
        if (lockYAxis)
        {
            objPosition.y = transform.position.y;
        }
        //What if we want to smooth things?
        //Interpolation
        //objPosition = Vector3.Lerp(transform.position, objPosition, lerpPerSecond * Time.deltaTime);
        //transform.position = objPosition;
        transform.position=Vector3.Lerp(transform.position, objPosition, lerpPerSecond*Time.deltaTime);
    }
}
