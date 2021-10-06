using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeterminosPlayer2 : MonoBehaviour
{
 
    //  int roundedX;
    //  public int roundedY;
    public static int height = 12;
    public static int width = 6;
    float previousTime;
    public float fallTime = 0.8f;
    bool moveable = true;
    bool canSpawn = true;
    private float nextUpdate = .2f;
    public float countDown = 2f;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    public float dragDistance;  //minimum distance for a swipe to be registered
    public static Transform[,] grid2 = new Transform[width, height];
    public bool test;
    public bool drag;

    private float fingerInitialPositionX;
    private float fingerMovedPositionX;
    private float fingerHeldPositionX;
    private float fingerEndPositionX;

    private float fingerInitialPositionY;
    private float fingerMovedPositionY;
    private float fingerHeldPositionY;
    private float fingerEndPositionY;

    private bool swipeUpOn;
    private bool swipeDownOn;
    private bool swipeRightOn;
    private bool swipeLeftOn;

    [Space]
    [Header("Effects")]
    public GameObject DestoryEffect;
    public GameObject PlacedEffect;


    private void Start()
    {
        dragDistance = Screen.height * 15 / 200;
        swipeUpOn = false;
        swipeDownOn = false;
        swipeRightOn = false;
        swipeLeftOn = false;
    }

    void Update()
    {
        int roundedX2 = Mathf.RoundToInt(transform.position.x);
        int roundedY2 = Mathf.RoundToInt(transform.position.y);
        if (moveable)
        {
            /* if (Input.touchCount == 1) // user is touching the screen with a single touch
             {
                 Touch touch = Input.GetTouch(0); // get the touch
                 if (touch.phase == TouchPhase.Began) //check for the first touch
                 {
                     fp = touch.position;
                     lp = touch.position;
                 }
                 else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                 {
                     lp = touch.position;
                 }
                 else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
                 {
                     lp = touch.position;  //last touch position. Ommitted if you use list

                     //Check if drag distance is greater than 20% of the screen height
                     if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                     {//It's a drag
                      //check if the drag is vertical or horizontal
                         if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                         {   //If the horizontal movement is greater than the vertical movement...
                             if ((lp.x > fp.x))  //If the movement was to the right)
                             {   //Right swipe
                                 transform.position += new Vector3(1, 0, 0);
                             }
                             else
                             {   //Left swipe
                                 transform.position -= new Vector3(1, 0, 0);
                             }
                         }
                         else
                         {   //the vertical movement is greater than the horizontal movement
                             if (lp.y > fp.y)  //If the movement was up
                             {   //Up swipe
                                 Debug.Log("Up Swipe");
                             }
                             else
                             {   //Down swipe
                               //  drag = true;
                             }
                         }
                     }

                 }

             }*/
            foreach (Touch FingerTouch in Input.touches) //get touches
            {
                //do things when touch has just begun
                if (FingerTouch.phase == TouchPhase.Began)
                {
                    fingerInitialPositionX = FingerTouch.position.x; //get initial X position of touch
                    fingerInitialPositionY = FingerTouch.position.y; //get initial Y position of touch
                 //   Debug.Log("Touch initiated");
                }

                //do things as soon as finger is moved (and make it not repeat theinformation every frame)
                else if (FingerTouch.phase == TouchPhase.Moved) //do things as soon as finger is moved
                {
                    fingerMovedPositionX = FingerTouch.position.x; //get the new X position of touch
                    fingerMovedPositionY = FingerTouch.position.y; //get the new Y position of touch

                    //first case - finger is moved right, movement is predominantly horizontal (x axis)
                    if (fingerMovedPositionX > fingerInitialPositionX && Mathf.Abs(fingerMovedPositionX - fingerInitialPositionX) > Mathf.Abs(fingerMovedPositionY - fingerInitialPositionY))
                    {
                        //swipe right
                        if (swipeRightOn == false && swipeLeftOn == false && swipeUpOn == false && swipeDownOn == false) //make it so you can't initiate a new swipe after one has already bin initiated
                        {
                            //initiate stuff on swipe right
                            swipeRightOn = true;
                          //  Debug.Log("Swipe right initiated");
                            FindObjectOfType<AudioManager>().Play("Move");
                            transform.position += new Vector3(1, 0, 0);
                            if (!ValidMove())
                            {
                                transform.position -= new Vector3(1, 0, 0);
                            }
                        }
                    }
                    //second case - finger is moved left, movement is predominantly horizontal (x axis)
                    else if (fingerMovedPositionX < fingerInitialPositionX && Mathf.Abs(fingerMovedPositionX - fingerInitialPositionX) > Mathf.Abs(fingerMovedPositionY - fingerInitialPositionY))
                    {
                        //swipe left
                        if (swipeRightOn == false && swipeLeftOn == false && swipeUpOn == false && swipeDownOn == false)
                        {
                            //initiate stuff on swipe left
                            swipeLeftOn = true;
                           // Debug.Log("Swipe left initiated");
                            FindObjectOfType<AudioManager>().Play("Move");
                            transform.position -= new Vector3(1, 0, 0);
                            if (!ValidMove())
                            {
                                transform.position += new Vector3(1, 0, 0);
                            }
                        }
                    }

                    //third case - finger is moved up, movement predominantly vertical (y axis)
                    else if (fingerMovedPositionY > fingerInitialPositionY && Mathf.Abs(fingerMovedPositionX - fingerInitialPositionX) < Mathf.Abs(fingerMovedPositionY - fingerInitialPositionY))
                    {
                        //swipe up
                        if (swipeRightOn == false && swipeLeftOn == false && swipeUpOn == false && swipeDownOn == false)
                        {
                            //initiate stuff on swipe up
                            swipeUpOn = true;
                          //  Debug.Log("Swipe up initiated");
                        }
                    }

                    //fourth case - finger is moved down, movement predominantly vertical (y axis)
                    else if (fingerMovedPositionY < fingerInitialPositionY && Mathf.Abs(fingerMovedPositionX - fingerInitialPositionX) < Mathf.Abs(fingerMovedPositionY - fingerInitialPositionY))
                    {
                        //swipe down
                        if (swipeRightOn == false && swipeLeftOn == false && swipeUpOn == false && swipeDownOn == false)
                        {
                            //initiate stuff on swipe down
                            swipeDownOn = true;
                          //  Debug.Log("Swipe down initiated");
                            drag = true;
                        }
                    }
                }

                //do things when touch has ended
                else if (FingerTouch.phase == TouchPhase.Ended)
                {
                    fingerEndPositionX = FingerTouch.position.x; //get the X position at the end, you may not need it unless you make gestures such as right and then left
                    fingerEndPositionY = FingerTouch.position.y; //get the Y position at the end, you may not need it unless you make gestures such as down and then up

                    //now reset all booleans and do stuff at the end of all swipes - like despawning shields on release etc.
                    if (swipeRightOn == true)
                    {
                        swipeRightOn = false;
                      //  Debug.Log("Swipe right released");
                    }
                    else if (swipeLeftOn == true)
                    {
                        swipeLeftOn = false;
                        //Debug.Log("Swipe left released");
                    }
                    else if (swipeUpOn == true)
                    {
                        swipeUpOn = false;
                     //   Debug.Log("Swipe up released");
                    }
                    else if (swipeDownOn == true)
                    {
                        swipeDownOn = false;
                      //  Debug.Log("Swipe down released");
                        drag = false;
                    }
                }

                //else statement which makes it so you can hold down a swipe and keep things activated etc.
                else
                {
                    //get current position of touch
                    fingerHeldPositionX = FingerTouch.position.x;
                    fingerHeldPositionY = FingerTouch.position.y;

                    if (swipeRightOn == true)
                    {
                        //swipe right is held
                       // Debug.Log("Swipe right is held");
                    }
                    else if (swipeLeftOn == true)
                    {
                        //swipe left is held
                      //  Debug.Log("Swipe left is held");
                    }
                    else if (swipeUpOn == true)
                    {
                        //swipe up is held
                       // Debug.Log("Swipe up is held");
                    }
                    else if (swipeDownOn == true)
                    {
                        //swipe down is held
                        //Debug.Log("Swipe down is held");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position += new Vector3(-1, 0, 0);
                }
            }

            if (Time.time - previousTime > (drag ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    if (grid2[roundedX2, 11] != null)
                    {
                        LogicManager.intance.canSpawn = false;
                        LogicManager.intance.LastMovePlayer();
                        if (countDown <= 0)
                        {
                            Debug.Log("You Lost!");
                            // Time.timeScale = 0;
                            //  FindObjectOfType<BlockSpawnerPlayer2>().enabled = false;
                            
                        }
                    }
                    else
                    {
                        countDown = 2;
                    }
                    transform.position -= new Vector3(0, -1, 0);
                    AddToGird();
                    if (GetComponent<PowerBlocksPlayer2>())
                        GetComponent<PowerBlocksPlayer2>().isFallen = true;
                    else if (GetComponent<TeterminoBlockPlayer2>())
                        GetComponent<TeterminoBlockPlayer2>().isFallen = true;
                    moveable = false;
                    // this.enabled = false;
                    if(canSpawn)
                    BlockSpawnerPlayer2.blockSpawner.NewBlock();
                }
                previousTime = Time.time;
            }
        }

      //  if(grid2[roundedX2,roundedY2+1] != gameObject)
        {
            //grid2[roundedX2, roundedY2 + 1] = null;
        }
       // test = ValidMove();

       
        /* if (!moveable && grid[roundedX2, roundedY2 - 1] == null)
         {
             Debug.Log("Working " + roundedX2 + " ," + roundedY2);
             //  grid[roundedX, roundedY+1] = null;
             transform.position += new Vector3(0, -1, 0);
             if (!ValidMove())
             {
                 grid[roundedX2, roundedY2] = null;
                 transform.position -= new Vector3(0, -1, 0);
                 AddToGird();
             }

         }
        if (ValidMove()&& !moveable)
        {
            GetComponent<SpriteRenderer>().color = Color.cyan;
            Debug.Log("We Can Move");
        }*/
        if (!moveable)
        {
         
            
            //   previousTime = Time.time;
            if (Time.time >= nextUpdate)
            {
              //  Debug.Log(Time.time + ">=" + nextUpdate);
                // Change the next update (current second+1)
                nextUpdate = Time.time + 0.2f;
                // Call your fonction
                UpdateEverySecond();
            }


        }
      
    }
    void UpdateEverySecond()
    {
        if (ValidMove())
        {
            grid2[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y)+1] = null;
        }
        transform.position += new Vector3(0, -1, 0);
        if (!ValidMove())
        {
            // Debug.Log("Working");
            /*if (grid2[roundedX2, roundedY2 + 1] != null)
            {
               // Debug.Log("Happening" + roundedX2 + "," + roundedY2);
                // grid[roundedY2,roundedY2+1].GetComponent<Teterminos>()
              //  grid2[roundedX2, roundedY2 + 1] = null;
            }
           */
            // grid2[roundedX2, roundedY2] = null;
            transform.position -= new Vector3(0, -1, 0);
            AddToGird();

        }
    }

    bool ValidMove()
    {

        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);


        if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
        {
            return false;
        }
        if (grid2[roundedX, roundedY] != null)
            return false;


        return true;
    }
    void AddToGird()
    {
      //  Instantiate(PlacedEffect, transform.position, Quaternion.identity);
        int roundedX = Mathf.RoundToInt(transform.position.x);
        int roundedY = Mathf.RoundToInt(transform.position.y);

        grid2[roundedX, roundedY] = transform;
    }

    private void OnDestroy()
    {
        Instantiate(DestoryEffect, transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("BlockBreak");
    }
}
