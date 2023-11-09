using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject player;
    private Rigidbody2D rb;
    //find if player is within activation range to prevent unnessisary firing
    public Transform targetTransform;
    public Vector3 targetPosition;
    //time delay
    public float delayBetweenSpawns = 2.0f;
    //spawning sound effect
    public AudioSource soundEffect;
    //can spawn
    bool canSpawn = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        soundEffect = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (prefabToSpawn == null)
        {
            Debug.Log("Missing projectile prefab");
        }
        if (targetTransform == null)
            targetTransform = GameObject.FindWithTag("Player").transform;
        Vector3 direction = player.transform.position - transform.position;
    }

    private void Update()
    {
        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
        }
        else if (player != null)
        {
            targetPosition = player.transform.position;
        }
        //if player is not in range, it will not fire
        Vector3 directionVector = targetPosition - transform.position;
        float distanceToTarget2 = directionVector.magnitude;
        if (GetComponent<MoveAndRotate>().useActivationRange && distanceToTarget2 > GetComponent<MoveAndRotate>().activationRange)
        { return; }
        if (canSpawn)
        {
            soundEffect.Play();
            GameObject spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            //Retrieve the correct rotation at spawn time, and we are done
            MoveAndRotate moveAndRotate=GetComponent<MoveAndRotate>();
            if (moveAndRotate != null )
            {
                spawnedObject.transform.Rotate(new Vector3(0, 0, -moveAndRotate.compensationAngle));
            }
            canSpawn = false;
            Invoke("EnableSpawn", delayBetweenSpawns);
        }
    }

    void EnableSpawn()
    { canSpawn = true; }
}
