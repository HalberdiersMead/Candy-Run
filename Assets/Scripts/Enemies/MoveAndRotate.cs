using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndRotate : MonoBehaviour
{
    //The transform component always allows us to read the position, but also to retrieve the gameObject associated
    public Transform targetTransform;

    //From the game object we can extract the .position
    public GameObject targetObject;

    //Store directly the "point" in our world
    public Vector3 targetPosition;

    //Movement section
    public bool shouldMove = true;
    public float speedPerSecond = 2;
    //Activation Range
    public bool useActivationRange = false;
    public float activationRange = 2.0f;

    //Rotation section
    public bool shouldRotate = true;
    public float compensationAngle=0;

    //Mouse section
    public bool targetMouse = false;

    //Sprite direction
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    private Transform point;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (targetTransform == null)
            targetTransform = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetMouse)
        {
            //I want to acquire Input.mousePosition and I want to project it in the world space
            Vector3 mouseWorldPosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0;
            targetPosition = mouseWorldPosition;
        }
        else if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }
        else if (targetObject != null)
        {
            targetPosition=targetObject.transform.position;
        }
        //Move towards the object
        Vector3 directionVector= targetPosition - transform.position;

        //Cover the entire distance in 1 frame. Not ideal...
        //transform.position+= directionVector;
        //We should apply an appropriate "speed"
        //We are using the magnitude of the vector for determining the speed. So at the beginning is bigger
        //transform.position += directionVector*speedPerSecond*Time.deltaTime;

        //Use Pythagora's to compute the length of the vector
        float distanceToTarget=Vector3.Distance(targetTransform.position, transform.position);
        float distanceToTarget2 = directionVector.magnitude;
       // Normalize a vector
        directionVector.Normalize();
        if (useActivationRange && distanceToTarget2 > activationRange)
        { return; }
        if (shouldMove)
        {
            transform.position += directionVector * speedPerSecond * Time.deltaTime;
        }
        //make the sprite flip if going in a different direction
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        //We discovered that Unity does all the hard math/trig stuff

        if (!shouldRotate)
        {
            return;
        }
        //Rotation Logic
        //Obtain the angle between 0 and 360 in degrees
        //With explicit math functions
        //float angle = Mathf.Atan2(directionVector.y, directionVector.x) * Mathf.Rad2Deg;
        //transform.rotation=Quaternion.Euler(0,0,angle+compensationAngle);

        transform.right = directionVector;
        transform.Rotate(new Vector3(0, 0, compensationAngle));
    }

   
}
