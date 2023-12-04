using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public float speed=100f;
    public float jumpPower = 50f;
    private float lastY;
    public int score { get; private set; }
    public TMP_Text scoreTxt;
    public Vector3 respawnPoint { get; private set; }

    //Keep player data
    public static Player Instance;

    //particle system
    public ParticleSystem dust;
    //jumping bools
    private bool isJumping = false;
    private bool canDoubleJump = false;
    private bool isDoubleJumping = false;
    Rigidbody2D rb;
    public AudioSource bonk;
    //animation
    Animator anim;


    string sceneName; 
   

    private void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastY = transform.position.y;
        respawnPoint = transform.position;
        bonk = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        } else
        {
            score = 0;
        }
        scoreTxt.text = "Score: " + score.ToString();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //horizontal movement

        float isFalling = (transform.position.y - lastY) * Time.deltaTime;
        lastY = transform.position.y;

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        Jump();
        //sprite flip
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(3,3,1);
        }
        //will look left if moving left
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-3,3,1);
        }

       

        //animation booleans
        anim.SetBool("Walking", horizontalInput != 0);
        anim.SetBool("IdlePlayer", horizontalInput == 0);
        anim.SetBool("JumpingPlayer", isFalling > lastY && isJumping==true);
        anim.SetBool("FallingPlayer", isFalling < lastY && isJumping == true);

        //if they fall past certain threshold gameover
        if (lastY <= -10)
        {
            transform.position = respawnPoint;
        }


    }

    public void ChangeScore(int scoreValue)
    {
        score += scoreValue;
        scoreTxt.text = "Score: " + score.ToString();
    }
    //jumping method


    private void Jump()
    {
        //jumping
        if (Input.GetButtonDown("Jump")&&(isJumping==false))
        {
            MakeDust();
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
        }
        // if player is jumping, they have picked up the powerup, and they havent double jumped, they can double jump
        if(Input.GetButtonDown("Jump"))
        {
            if(canDoubleJump == true && (isDoubleJumping == false||isJumping==true))
            {
                MakeDust();
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                isDoubleJumping = true;
            }
                
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            bonk.Play();
        }
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            if (canDoubleJump == true)
            {
                isDoubleJumping = false;
            }
            
        }
        if (collision.gameObject.tag == "WeakSpot")
        {
            ChangeScore(200);
        }
    }
    // sets double jump to true on pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // allows for double jump on pickup
        if (collision.tag=="Pickup")
        {
            canDoubleJump = true;
            ChangeScore(100);
        }
        if (collision.tag == "collectable")
        {
            ChangeScore(50);
        }
        //change checkpoint
        if (collision.tag == "Checkpoint")
        {
            //attempt to gain points only when you cross the checkpoint once
            //if (respawnPoint != transform.position)
            //{
            //    ChangeScore(500);
            //}
            respawnPoint = transform.position;
            
        }
        //if you finish lvl 1 you get sent to the next level, if lvl 2 you go to the level select
        if (collision.tag == "Finish")
        {
            ChangeScore(1000);
            PlayerPrefs.SetInt("Score", score);
            // attempt to make individual high score per level.
            //Debug.Log(PlayerPrefs.GetInt("previousHealth"));
            //if (PlayerPrefs.HasKey(sceneName))
            //{
            //    if (score > PlayerPrefs.GetInt(sceneName))
            //    {
            //        PlayerPrefs.SetInt(sceneName, score);
            //    }
            //}
            //else
            //{
            //    PlayerPrefs.SetInt(sceneName, score);
            //}
            //Debug.Log(PlayerPrefs.GetInt(sceneName));
            if (sceneName == "LevelOne")
            {
                canDoubleJump = false;
                rb.transform.position = new Vector2(-6, -1);
                SceneManager.LoadScene("LevelTwo");
            }
            if (sceneName == "LevelTwo")
            {
                canDoubleJump = false;
                SceneManager.LoadScene("LevelSelect");
                Destroy(this.gameObject);
                return;
            }
            
        }
    }

    //jumping particle effect
    void MakeDust()
    {
        dust.Play();
    }
}
