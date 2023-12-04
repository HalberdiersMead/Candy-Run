using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    //patrol points
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    public bool horizontal = false;
    public bool vertical = true;
    private Transform currentPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
        currentPoint = pointB.transform;
        
        //set beginning paramaters


    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal == true)
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }
        }
        if (vertical == true)
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(0, speed);
            }
            else
            {
                rb.velocity = new Vector2(0, -speed);
            }
        }

        if(Vector2.Distance(transform.position, currentPoint.position)< 0.5f && currentPoint == pointB.transform)
        {
            
            currentPoint = pointA.transform;
            
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            
            currentPoint = pointB.transform;
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
