using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerAutoMovement : MonoBehaviour
{
    public enum StartDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum FilmReel {
        Forward,
        Backward
    }

    public FilmReel BGAni;
    public StartDirection MoveDirection;
    public float Speed;
    public Rigidbody2D PlayerRB;
    private BoxCollider2D PlayerCollider;
    private Animator PlayerSprite;
    private Animator Background;

    public bool GameStarted = false;
    public bool GameFinished = false;
    public bool IsTalking = false;
    bool IsCollided = false;
    private GridBasedMovement Obstacle;

    public bool KeyCollected;
    public NPCDialogue DialogueContent;
    public GameObject DialoguePanel;
    public DialogueBox PlayerDialogue;

    [TextArea(2,5)]
    public string PlayerMonologueIntro;
    [TextArea(2, 5)]
    public string PlayerMonologueEnding;
    private Camera PlayerCamera;
    //public Tilemap
    public GameObject[] Field;
    int FieldIndex = 0;
    public string[] Collectibles;
    int CollectiblesCount;
    int MaxCollectibles;

    private AudioSource InteractionSFX;
    public AudioClip DoorSFX;

    

    // Start is called before the first frame update
    void Start()
    {
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
        for (i = 0; i < Field.Length; i++) {
            Field[i].SetActive(false);
        }
        
        Field[0].SetActive(true);
        MaxCollectibles = Collectibles.Length;
        PlayerCamera.transform.position = new Vector3(Field[0].transform.position.x, Field[0].transform.position.y, PlayerCamera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (GameStarted == true)
        {
            StartingMovement();

            Speed = 2;
            PlayerSprite.SetInteger("Speed", 2);
            PlayerStatus();

        }
        else if (GameFinished == true) {
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

    }

    private void PlayerStatus()
    {       
        if ((IsTalking == true && DialoguePanel.gameObject.activeSelf) || (IsCollided == true || Obstacle.Grabbed == true))
        {
            Speed = 0;
            PlayerSprite.SetFloat("Speed", Speed);
            //return;
        }
        else {
            Speed = 2;
            PlayerSprite.SetFloat("Speed", Speed);
        }

        
    }

    void StartingMovement() {
        PlayerRB.velocity = Vector2.up * Speed;
        switch (MoveDirection)
        {
            case StartDirection.Up:
                MovementUp();             
                break;
            case StartDirection.Down:
                MovementDown();
                break;
            case StartDirection.Left:
                MovementLeft();
                break;
            case StartDirection.Right:
                MovementRight();
                break;
        }       
    }

    /*void FieldDisplay() {

        foreach (GameObject CurrentField in Field)
        {
            CurrentField.SetActive(false);
        }
        Field[FieldIndex].SetActive(true);
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GridBasedMovement>())
        {
            Obstacle = collision.gameObject.GetComponent<GridBasedMovement>();
            IsCollided = true;
        }

        if (collision.gameObject.CompareTag("Ending"))
        {
            InteractionSFX.PlayOneShot(DoorSFX);
            GameFinished = true;
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(13);
            }
        }
        if (collision.gameObject.CompareTag("Up"))
        {
            Speed = 0;
            MoveDirection = StartDirection.Up;
        }
        else if (collision.gameObject.CompareTag("Down"))
        {
            Speed = 0;
            MoveDirection = StartDirection.Down;
        }
        else if (collision.gameObject.CompareTag("Left"))
        {
            Speed = 0;
            MoveDirection = StartDirection.Left;
        }
        else if (collision.gameObject.CompareTag("Right"))
        {
            Speed = 0;
            MoveDirection = StartDirection.Right;
        }
        else {
            Speed = 2;
            MoveDirection = StartDirection.Up;
        }
        
        for (int i = 0; i < Collectibles.Length; i++)
        {
            if (collision.gameObject.name == Collectibles[i])
            {
                CollectiblesCount += 1;
                Destroy(collision.gameObject);
            }
        }
    
        if (collision.gameObject.CompareTag("KeyItem"))
        {
            KeyCollected = true;
            Destroy(collision.gameObject);
            PlayerSprite.SetInteger("Speed", 2);
            Destroy(GameObject.FindGameObjectWithTag("Locked Door"));
            
        }

        /*if (collision.gameObject.CompareTag("Locked Door") && KeyCollected == true)
        {
            
            PlayerSprite.SetInteger("Speed", 2);
        }*/
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<GridBasedMovement>())
        {
            IsCollided = false;
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {            
        if (collision.gameObject.CompareTag("Transition"))
        {
            Speed = 0;
            FieldIndex++;
            Debug.Log(FieldIndex);
            //  PlayerCamera.transform.position = new Vector3(Field[FieldIndex].transform.position.x, Field[FieldIndex].transform.position.y, PlayerCamera.transform.position.z);
            Field[FieldIndex-1].SetActive(false);
            Field[FieldIndex].SetActive(true);            
            print(PlayerCamera.transform.position);
        }
        PlayerCamera.transform.position = new Vector3(Field[FieldIndex].transform.position.x, Field[FieldIndex].transform.position.y, PlayerCamera.transform.position.z);

        

        if (collision.gameObject.CompareTag("Dialogue"))
        {
            PlayerDialogue.CurrNPC = collision.gameObject.GetComponent<NPCDialogue>();
            IdentifyCharacter();
            print(IsTalking);
            //PlayerDialogue.DialogueIndex = 0;           
        }
        //Destroy(collision.gameObject);


        if (collision.gameObject.CompareTag("Goal") && CollectiblesCount == MaxCollectibles) {
            InteractionSFX.PlayOneShot(DoorSFX);
            GameFinished = true;
            KeyCollected = false;
        }
    }

    public void IdentifyCharacter() {
        //NameText = DialogueSpot.CharDialogue.Name;
        //DialogueSpot.CharDialogue.CharLines;
        //EndDialogue();
        // DialogueIndex = 0;
        //DialoguePanel.SetActive(false);
        //PlayerStatus.IsTalking = false;
        //PlayerStatus.PlayerRB.ve
        // close dialogue box
        DialoguePanel.SetActive(true);
        IsTalking = true;
        


        PlayerDialogue.gameObject.GetComponent<DialogueBox>();

        //PlayerDialogue.TurnOnDialogueBox(PlayerDialogue.CharDialogue);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    //movement Direction

    private void MovementRight()
    {
        //PlayerRB.velocity = Vector3.right * Speed;
        if (MoveDirection == StartDirection.Right) {
            PlayerSprite.SetBool("Up", false);
            PlayerSprite.SetBool("Down", false);
            PlayerSprite.SetBool("Left", false);
            PlayerSprite.SetBool("Right", true);
        }
    }

    private void MovementLeft()
    {
        //PlayerRB.velocity = Vector3.left * Speed;
        if (MoveDirection == StartDirection.Left)
        {
            PlayerSprite.SetBool("Up", false);
            PlayerSprite.SetBool("Down", false);
            PlayerSprite.SetBool("Left", true);
            PlayerSprite.SetBool("Right", false);
        }            
    }

    private void MovementDown()
    {
        //PlayerRB.velocity = Vector3.down * Speed;
        if (MoveDirection == StartDirection.Down)
        {
            PlayerSprite.SetBool("Up", false);
            PlayerSprite.SetBool("Down", true);
            PlayerSprite.SetBool("Left", false);
            PlayerSprite.SetBool("Right", false);
        }
    }

    private void MovementUp()
    {
        //PlayerRB.velocity = Vector3.up * Speed;
        if (MoveDirection == StartDirection.Up)
        {
            PlayerSprite.SetBool("Up", true);
            PlayerSprite.SetBool("Down", false);
            PlayerSprite.SetBool("Left", false);
            PlayerSprite.SetBool("Right", false);
        }
    }
    
    //Widget

    

    public void FinishGame() {

    }

}
