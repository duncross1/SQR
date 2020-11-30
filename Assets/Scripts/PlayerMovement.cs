using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;

    public Sprite[] playerSprites = new Sprite[3];

    public GameObject[] linePrefabs = new GameObject[3];
    public GameObject currentLine;
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public List<Vector2> playerPostions;

    public LevelLoader levelLoader;

    public float linePointDiff = 0.3f;

    int colour = 0; //0 = red, 1 = green, 2 = blue
    int previousColour = 0;

    Vector2 movement;
    //Timer
    public float time;
    bool playerMoved = false;
    public Animator timerAnimator;
    public bool timerOn;

    //obstacle checks
    bool touchingHole = false;


    void Start()
    {
        Debug.Log("B");
        CreateLine();
    }

    void CreateLine()
    {
       
        currentLine = Instantiate(linePrefabs[colour], new Vector3(0, 0, -1), Quaternion.identity);
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        edgeCollider = currentLine.GetComponent<EdgeCollider2D>();
        playerPostions.Clear();
        Vector3 linepos = new Vector3(rb.position.x, rb.position.y, 1);
        playerPostions.Add(rb.position);
        playerPostions.Add(rb.position);
        lineRenderer.SetPosition(0, new Vector3(playerPostions[0].x, playerPostions[0].y, 1));
        lineRenderer.SetPosition(1, new Vector3(playerPostions[1].x, playerPostions[1].y, 1));
        edgeCollider.points = playerPostions.ToArray();
    }

    void UpdateLine(Vector2 newPlayerPos)
    {
        playerPostions.Add(newPlayerPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(newPlayerPos.x, newPlayerPos.y, 1));
        edgeCollider.points = playerPostions.ToArray();
    }

    void Update()
    {

        if (cam == null)
        {
            cam = Camera.main;
        }

        
        
        

        Vector2 tempPlayerPos = rb.position;

        if (previousColour == colour)
        {
            if (Vector2.Distance(tempPlayerPos, playerPostions[playerPostions.Count - 1]) > linePointDiff)
            {
                UpdateLine(tempPlayerPos);
            }
        }
        else
        {
            previousColour = colour;
            CreateLine();
        }


        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        /*TIMER*/
        if (timerOn == true)
        {
            //If player moves or switches colour start the timer
            if (movement.x != 0 || movement.y != 0 || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
            {
                timerAnimator.SetTrigger("Start");
                playerMoved = true;
            }

        }


        if (timerOn == true)
        {
            if (playerMoved == true)
            {
                time += Time.deltaTime;
                if (time >= 10)
                {
                    walkSpeed = 0;
                    levelLoader.loadCurrentLevel();
                }
            }
        }




        //Movement
        rb.MovePosition(rb.position + movement * walkSpeed * Time.fixedDeltaTime);

        //If you take 2 vectors and subtract them you are going to get a vector that points to the other
        // Vector2 lookDir = mousePos - rb.position;
        //Atan2 is a math function used to calculate angles between 2 vectors, Rad2Deg converts radions to degrees
        // float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        // rb.rotation = angle;

        //Colour swapping
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (colour == 0)
            {
                colour = 2;
            }
            else
            {
                colour -= 1;
            }
            GetComponent<SpriteRenderer>().sprite = playerSprites[colour];
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (colour == 2)
            {
                colour = 0;
            }
            else
            {
                colour += 1;
            }
            GetComponent<SpriteRenderer>().sprite = playerSprites[colour];
        }



        


        //DEBUG
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }



    /*Collsions*/
    //Triggered every frame while inside of trigger
    void OnTriggerStay2D(Collider2D collision)
    {
        //WALLS
        //Purple Walls
        if (collision.gameObject.tag == "PurpleWall")
        {
            walkSpeed = 0;
            levelLoader.loadCurrentLevel();
        }
        //Red Walls
        if (collision.gameObject.tag == "RedWall")
        {
            if (colour != 0)
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
        }
        //Green Walls
        if (collision.gameObject.tag == "GreenWall")
        {
            if (colour != 1)
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
        }
        //Blue Walls
        if (collision.gameObject.tag == "BlueWall")
        {
            if (colour != 2)
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
        }
        //Obstacle Wall
        if (collision.gameObject.tag == "ObstacleWall")
        {
            if (touchingHole == false)
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
        }
    }

    //Triggered when entering a trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        //LINES
        //Red Line
        if (collision.gameObject.tag == "RedLine")
        {
            if (colour != 0)
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
        }
        //Green Line
        if (collision.gameObject.tag == "GreenLine")
        {
            if (colour != 1)
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
        }
        //Blue Line
        if (collision.gameObject.tag == "BlueLine")
        {
            if (colour != 2)
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
        }

        //OBSTACLES
        //FinishLine
        if (collision.gameObject.tag == "LevelFinish")
        {
            walkSpeed = 0;
            levelLoader.loadNextLevel();
        }
        //Keys
        if (collision.gameObject.tag == "Key1" || collision.gameObject.tag == "Key2")
        {
            Destroy(collision.gameObject);
        }
        //Colour Switchers
        if (collision.gameObject.tag == "ColourRandomizer")
        {
            if (Random.value < 0.5f)
            {
                colour = colour + 1;
                if(colour == 3)
                {
                    colour = 0;
                }
            } 
            else
            {
                colour = colour - 1;
                if (colour == -1)
                {
                    colour = 2;
                }
            }
            GetComponent<SpriteRenderer>().sprite = playerSprites[colour];
        }
        //Moving Holes
        if (collision.gameObject.tag == "RedMovingHole")
        {
            if (colour == 0)
            {
                touchingHole = true;
            }
            else
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
        }
        if (collision.gameObject.tag == "GreenMovingHole")
        {
            if(colour == 1)
            {
                touchingHole = true;
            }
            else
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }
            
        }
        if (collision.gameObject.tag == "BlueMovingHole")
        {
            if (colour == 2)
            {
                touchingHole = true;
            }
            else
            {
                walkSpeed = 0;
                levelLoader.loadCurrentLevel();
            }

        }

        
    }

    //Triggered when leaving a trigger
    void OnTriggerExit2D(Collider2D collision)
    {
        //Moving Holes
        if (collision.gameObject.tag == "RedMovingHole")
        {
            touchingHole = false;
        }
        if (collision.gameObject.tag == "GreenMovingHole")
        {
            touchingHole = false;
        }
        if (collision.gameObject.tag == "BlueMovingHole")
        {
            touchingHole = false;
        }
    }
}
