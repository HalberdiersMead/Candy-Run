using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float timeToLive = 5;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("KillObject",timeToLive);  
    }

    void KillObject()
    {
        Destroy(gameObject);
    }

}
