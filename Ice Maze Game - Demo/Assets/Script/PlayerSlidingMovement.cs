using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSlidingMovement : MonoBehaviour
{
    public enum StartDirection
    {
        Up,
        Down,
        Left,
        Right,
        Stop
    }
    public enum FilmReel
    {
        Forward,
        Backward
    }

   
    public StartDirection MoveDirection;
    int FieldIndex;
    public float Speed;
    private float returnSpeed;
    public Rigidbody2D PlayerRB;
    private BoxCollider2D PlayerCollider;
    public RaycastHit2D Up, Down, Left, Right;
    public LayerMask WhatAreTheObjects;

    public FilmReel BGAni;
    private Animator PlayerSprite;
    private Animator Background;

    public bool GameStarted = false;
    public bool GameFinished = false;
    public bool IsTalking = false;
    public bool IsMoving, SlideUp, SlideDown, SlideLeft, SlideRight;

    public NPCDialogue DialogueContent;
    public GameObject DialoguePanel;
    public DialogueBox PlayerDialogue;

    [TextArea(2, 5)]
    public string PlayerMonologueIntro;
    [TextArea(2, 5)]
    public string PlayerMonologueEnding;

    private Camera PlayerCamera;
    public GameObject[] Field;
    public bool Ending;
    public bool KeyCollected;
    public string[] Collectibles;
    int CollectiblesCount;
    int MaxCollectibles;

    private AudioSource InteractionSFX;
    public AudioClip DoorSFX;
    // Start is called before the first frame update
    void Start()
    {
        returnSpeed = Speed;
        InteractionSFX = GetComponent<AudioSource>();
        PlayerSprite = GetComponent<Animator>();
        DialoguePanel.SetActive(false);
        Background = GameObject.Find("UIBGFilmReel").GetComponent<Animator>();

        Speed = 0;
        int i = 0;

        PlayerDialogue = GetComponent<DialogueBox>();
        PlayerRB = gameObject.GetComponent<Rigidbody2D>();
        PlayerCollider = gameObject.GetComponent<BoxCollider2D>();
        PlayerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        for (i = 0; i < Field.Length; i++)
        {
            Field[i].SetActive(false);
        }
        Field[0].SetActive(true);
        MaxCollectibles = Collectibles.Length;
        PlayerCamera.transform.position = new Vector3(Field[0].transform.position.x, Field[0].transform.position.y, PlayerCamera.transform.position.z);
         
    }

    // Update is called once per frame
    void Update()
    {
        /*Up = Physics2D.Raycast(gameObject.transform.position - new Vector3(0, 0.25f, 0), Vector2.up, 0.2f, WhatAreTheObjects);       
        Down = Physics2D.Raycast(gameObject.transform.position - new Vector3(0, 0.25f, 0), Vector2.down, 0.5f, WhatAreTheObjects);
        Left = Physics2D.Raycast(gameObject.transform.position - new Vector3(0,0.25f, 0), Vector2.left, 0.5f, WhatAreTheObjects);
        Right = Physics2D.Raycast(gameObject.transform.position - new Vector3(0, 0.25f, 0), Vector2.right, 0.5f, WhatAreTheObjects);*/

        Up = Physics2D.BoxCast(gameObject.transform.position + new Vector3(0, 0.4f, 0), new Vector2(0.6f, 0.1f), 0, Vector2.up, 0, WhatAreTheObjects);
        Down = Physics2D.BoxCast(gameObject.transform.position + new Vector3(0, -0.6f, 0), new Vector2(0.6f, 0.1f), 0, Vector2.down, 0, WhatAreTheObjects);
        Left = Physics2D.BoxCast(gameObject.transform.position + new Vector3(-0.4f, 0, 0), new Vector2(0.2f, 0.1f), 0, Vector2.left, 0, WhatAreTheObjects);
        Right = Physics2D.BoxCast(gameObject.transform.position + new Vector3(0.4f, 0, 0), new Vector2(0.2f, 0.1f), 0, Vector2.right, 0, WhatAreTheObjects);
        
        if (GameStarted == true)
        {      
            Speed = returnSpeed;
            PlayerSprite.SetInteger("Speed", 2);
            if (IsMoving == false || MoveDirection == StartDirection.Stop)
            {
                StartingMovement();
                
            }
        }
        else if (GameFinished == true)
        {
            Speed = 0;
        }

        
        switch (BGAni)
         {
             case FilmReel.Forward:
                 Background.SetBool("Up", true);
                 Background.SetBool("Down", false);
                 break;
             case FilmReel.Backward:
                 Background.SetBool("Up", false);
                 Background.SetBool("Down", true);
                 break;
         }
        AnimationManager();
       
        PlayerEye();
        //Gizmos.DrawWireCube(new Vector3(0, 0.4f, 0), new Vector2(0.6f, 0.1f));
    }

    private void FixedUpdate()
    {
     
        switch (MoveDirection) {
            case StartDirection.Up:
                MoveUp();
                break;
            case StartDirection.Down:
                MoveDown();
                break;
            case StartDirection.Left:
                MoveLeft();
                break;
            case StartDirection.Right:
                MoveRight();
                break;
            case StartDirection.Stop:
                Speed = 0;
                break;
        }
    }

    private void PlayerStatus()
    {
        if ((IsTalking == true && DialoguePanel.gameObject.activeSelf) || MoveDirection == StartDirection.Stop)
        {
            Speed = 0;
            PlayerSprite.SetFloat("Speed", Speed);
            //return;
        }
        else
        {
            Speed = 2;
            PlayerSprite.SetFloat("Speed", Speed);
        }


    }

    void PlayerEye() {
        if (Up.collider != null)
        {
            SlideUp = true;
        }
        else {
            SlideUp = false;
        }
        if (Right.collider != null)
        {
            SlideRight = true;
        }
        else
        {
            SlideRight = false;
        }
        if (Left.collider != null)
        {
            SlideLeft = true;
        }
        else
        {
            SlideLeft = false;
        }
        if (Down.collider != null)
        {
            SlideDown = true;
        }
        else
        {
            SlideDown = false;
        }
    }

    private void StartingMovement()
    {
        if (MoveDirection == StartDirection.Stop) {
            if (Input.GetKeyDown(KeyCode.UpArrow) && Up.collider == null)
            {
                MoveDirection = StartDirection.Up;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && Down.collider == null)
            {
                MoveDirection = StartDirection.Down;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Left.collider == null)
            {
                MoveDirection = StartDirection.Left;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && Right.collider == null)
            {
                MoveDirection = StartDirection.Right;
            }
            Speed = returnSpeed;         
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Locked Door")) {
            MoveDirection = StartDirection.Stop;
            //IsMoving = false;
            Speed = 0;
        }

        if (collision.gameObject.CompareTag("KeyItem"))
        {
            KeyCollected = true;
            Destroy(collision.gameObject);
            PlayerSprite.SetInteger("Speed", 2);
            Destroy(GameObject.FindGameObjectWithTag("Locked Door"));

        }

        if (collision.gameObject.CompareTag("Ending"))
        {
            Debug.Log("Game Complete");
            InteractionSFX.PlayOneShot(DoorSFX);
            Ending= true;
            KeyCollected = false;
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(13);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //if()
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Transition"))
        {
            Speed = 0;
            FieldIndex++;
            Debug.Log(FieldIndex);
            PlayerCamera.transform.position = new Vector3(Field[FieldIndex].transform.position.x, Field[FieldIndex].transform.position.y, PlayerCamera.transform.position.z);
            Field[FieldIndex - 1].SetActive(false);
            Field[FieldIndex].SetActive(true);
            print(PlayerCamera.transform.position);
        }
        //PlayerCamera.transform.position = new Vector3(Field[FieldIndex].transform.position.x, Field[FieldIndex].transform.position.y, PlayerCamera.transform.position.z);

        if (collision.gameObject.CompareTag("Goal") && CollectiblesCount == MaxCollectibles)
        {
            Debug.Log("Level Cleared");
            InteractionSFX.PlayOneShot(DoorSFX);
            GameFinished = true;
            KeyCollected = false;
           
        }
        

        if (collision.gameObject.CompareTag("Dialogue"))
        {
            PlayerDialogue.CurrNPC = collision.gameObject.GetComponent<NPCDialogue>();
            IdentifyCharacter();
            print(IsTalking);
            MoveDirection = StartDirection.Stop;
            PlayerRB.velocity = Vector2.zero;
        }
    }

    private void MoveRight()
    {
        PlayerRB.velocity = Vector2.right * Speed;
    }

    private void MoveLeft()
    {
        PlayerRB.velocity = Vector2.left * Speed;       
    }

    private void MoveDown()
    {
        PlayerRB.velocity = Vector2.down * Speed;       
    }

    private void MoveUp()
    {
        PlayerRB.velocity = Vector2.up * Speed;        
    }

    public void IdentifyCharacter()
    {       
        DialoguePanel.SetActive(true);
        IsTalking = true;
        PlayerDialogue.gameObject.GetComponent<DialogueBox>();
    }

    private void AnimationManager() {
        if (MoveDirection == StartDirection.Stop)
        {
            PlayerSprite.SetFloat("Speed", 0);
        }
        else {
            if (MoveDirection == StartDirection.Right)
            {
                PlayerSprite.SetBool("Up", false);
                PlayerSprite.SetBool("Down", false);
                PlayerSprite.SetBool("Left", false);
                PlayerSprite.SetBool("Right", true);
            }
            else if (MoveDirection == StartDirection.Left)
            {
                PlayerSprite.SetBool("Up", false);
                PlayerSprite.SetBool("Down", false);
                PlayerSprite.SetBool("Left", true);
                PlayerSprite.SetBool("Right", false);
            }
            else if (MoveDirection == StartDirection.Down)
            {
                PlayerSprite.SetBool("Up", false);
                PlayerSprite.SetBool("Down", true);
                PlayerSprite.SetBool("Left", false);
                PlayerSprite.SetBool("Right", false);
            }
            else if (MoveDirection == StartDirection.Up)
            {
                PlayerSprite.SetBool("Up", true);
                PlayerSprite.SetBool("Down", false);
                PlayerSprite.SetBool("Left", false);
                PlayerSprite.SetBool("Right", false);
            }
            PlayerSprite.SetFloat("Speed", returnSpeed);
        }

        
        

    }
}
