using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

public class GridBasedMovement : MonoBehaviour//, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum BlockShape {
        Horizontal,
        Vertical,
        Square
    }

    public enum VeerDirection {
        Up,
        Down,
        Left,
        Right,
        None
    }

    //public AudioSource BlockElement;
    //public AudioClip BlockGrab;
    //public AudioClip BlockDrop;

    bool Selected;
    public VeerDirection BlockVeer;
    public BlockShape BlockDirection;
    Vector3 mousePos;
    private float BlockPosX, BlockPosY;
    float slideSpeed;
    public float FieldLength, FieldWidth;
    public Vector2 xPos;
    public Vector2 yPos;
    public GameObject areaSize;
    public GameObject pivot;

    public bool Grabbed = false;
    private Rigidbody2D BlockRB;
    private BoxCollider2D BlockCollider;
    private RaycastHit2D GrabPoint;

    // Start is called before the first frame update
    void Start()
    {
        
        //BlockElement = GetComponent<AudioSource>();
        BlockRB = GetComponent<Rigidbody2D>();
        BlockCollider = GetComponent<BoxCollider2D>();
        objectPos = transform.position;
        LoadSoundConfig();
    }   

    // Update is called once per frame
    void Update()
    {       
        GrabABlock();
    }

    private Vector3 objectPos;

    public void HorizontalMovement() {
        //print("Horizonal Movement");
        print("Mouse Position:" + mousePos.x);
        Debug.Log("Position" + transform.position.x);
        //this.gameObject.transform.position = new Vector3(mousePos.x, gameObject.transform.position.y, 0);
        //gameObject.transform.position = new Vector3(Mathf.Clamp(mousePos.x, xPos.x, xPos.y), gameObject.transform.position.y, 0);
        
        BlockRB.MovePosition(new Vector2(Mathf.Clamp(mousePos.x, xPos.x, xPos.y), BlockRB.position.y));
    }

    public void VerticalMovement()
    {
        //this.gameObject.transform.position = new Vector3(gameObject.transform.position.x, mousePos.y, 0);
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Clamp(mousePos.y, yPos.x, yPos.y), 0);
        BlockRB.MovePosition(new Vector2(BlockRB.position.x, Mathf.Clamp(mousePos.y, yPos.x, yPos.y)));
        //gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, transform.position, Mathf.Clamp(mousePos.y, -FieldWidth, FieldWidth));
    }

    /*public void OctaMovement()
    {
        this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
    }*/

    private void GrabABlock()
    {
        if (Grabbed == true)
        {
            Vector2 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            
            //Selected = true;
            //Debug.Log("blockGrab");
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);          
            BlockPosX = mousePos.x - transform.position.x;
            BlockPosY = mousePos.y - transform.position.y;
            switch (BlockDirection)
            {
                case BlockShape.Horizontal:
                    //print("Horizontal Case");
                    transform.Translate(CursorPos);
                    HorizontalMovement();
                    break;
                case BlockShape.Vertical:
                    VerticalMovement();
                    break;
                case BlockShape.Square:
                    //OctaMovement();
                    break;
            }
        }
        /*if (Grabbed == true) {
            Vector3 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 MousePos2D = new Vector2(CursorPos.x, CursorPos.y);
            RaycastHit2D Grab = Physics2D.Raycast(MousePos2D, gameObject.transform.position);
            if (Grab.collider != null)
            {
                Debug.Log("You Grabbed A Block");
                switch (BlockDirection)
                {
                    case BlockShape.Horizontal:
                        HorizontalMovement();
                       // BlockRB.transform.position = new Vector3(gameObject.transform.position.x, Grab.transform.position.y, 0);
                        break;
                    case BlockShape.Vertical:
                        //BlockRB.transform.position = new Vector3(gameObject.transform.position.x, Grab.transform.position.y, 0);
                        VerticalMovement();
                        break;
                    case BlockShape.Square:
                        OctaMovement();
                        break;
                }
            }
        }*/

        //Ray mousePosRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //transform.position = new Vector2();
        //BlockPosY = mousePos.y - transform.position.y;
        //transform.position = new Vector2(mousePos.x, mousePos.y);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "TileMap") {
            Grabbed = false;
            gameObject.transform.position = transform.position;
        }
    }

    private void LateUpdate()
    {
        mousePos.x = Mathf.Floor(gameObject.transform.position.x / 1f) * 1f;
        mousePos.y = Mathf.Floor(gameObject.transform.position.y / 1f) * 1f;
        //pivot.transform.position = mousePos;
    }

    

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            Grabbed = true;
           // BlockElement.PlayOneShot(BlockGrab);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Grabbed = false;
           // BlockElement.PlayOneShot(BlockDrop);
        }
    }

    public void LoadSoundConfig()
    {
        if (PlayerPrefs.HasKey("SFXValue"))
        {
            //BlockElement.volume = PlayerPrefs.GetFloat("SFXValue");
        }
    }

    /*public void OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        /*print("terst");
        gameObject.transform.position = eventData.position;
        if (Grabbed == true)
        {
            Vector2 CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Selected = true;
            Debug.Log("blockGrab");
            mousePos = eventData.position;
            //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //BlockPosX = mousePos.x - transform.position.x;
            //BlockPosY = mousePos.y - transform.position.y;
            switch (BlockDirection)
            {
                case BlockShape.Horizontal:
                    //transform.position = new Vector3(eventData.position.x, gameObject.transform.position.y, 0);
                    HorizontalMovement();
                    break;
                case BlockShape.Vertical:
                    //transform.position = new Vector3(gameObject.transform.position.x, eventData.position.y, 0);
                    VerticalMovement();
                    break;
                case BlockShape.Square:
                    OctaMovement();
                    break;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }*/
}
