using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSetup : MonoBehaviour, ITileCheckable<Vector2>
{
    //public sprites and variables
    public int grid_size;
    public GameObject objTemp;
    public Sprite letterA;
    public Sprite letterB;
    public Sprite letterC;
    public Sprite letterD;
    public Sprite letterE;
    public Sprite letterF;
    public Sprite letterG;
    public Sprite letterH;
    public Sprite letterI;
    public Sprite letterJ;
    public Sprite letterK;
    public Sprite letterL;
    public Sprite letterM;
    public Sprite letterN;
    public Sprite letterO;
    public Sprite letterP;
    public Sprite letterQ;
    public Sprite letterR;
    public Sprite letterS;
    public Sprite letterT;
    public Sprite letterU;
    public Sprite letterV;
    public Sprite letterW;
    public Sprite letterX;
    public Sprite letterY;
    public Sprite letterZ;
    public Sprite letterWild;
    public static GameObject[] playerLetters = new GameObject[8];

    //private variables, not needed to be present in editor
    private Vector3 vectTemp;
    private static readonly Vector3 LETTER_SCALE = new Vector3(0.6f, 0.6f, 0.6f);
    private Vector3[] playerLetterPos = new Vector3[8];
    private static GameObject[] opponentLetters = new GameObject[8];
    private static Dictionary<int, Sprite> myLetterDict = new Dictionary<int, Sprite>();
    private int numSelectP;
    private int numSelectO;
    private int localLetter;
    private Vector3 mouse_position_raw;
    private Vector2 location_grab;
    private Vector2 mouse_position_game = new Vector2(0f, 0f);
    private Vector2 direction = Vector2.zero;
    private RaycastHit2D obj_hit;
    private Rigidbody2D obj_grab = null;

    //Enum values for object layer values
    enum OBJECT_LAYERS
    {
        Dragging = 8,
        Normal = 9,
        Enemy = 10
    };

    //Enum values for sprite sorting layer values
    enum SORTING_LAYERS
    {
        Default,
        Background,
        Pieces,
        Player,
        Dragging
    };

    //**********************************************************************
    //*  Returns a number to represent the level after a "random" drawing  *
    //**********************************************************************
    private int grabLetterCategory()
    {
        int localVal = Random.Range(0,15);

        //~4/16 chance for level one
        if (localVal < 4)
        {
            return 1;
        }
        //~4/16 chance for level two
        else if (localVal < 8)
        {
            return 2;
        }
        //~3/16 chance for level three
        else if (localVal < 11)
        {
            return 3;
        }
        //~2/16 chance for level four
        else if (localVal < 13)
        {
            return 4;
        }
        //~1/16 chance for level five
        else if (localVal < 14)
        {
            return 5;
        }
        //~1/16 chance for level six
        else if (localVal < 15)
        {
            return 6;
        }
        //~1/16 chance for level seven
        else
        {
            return 7;
        }
    }

    //******************************************************************************
    //*  Returns a "random" number given the level range that was selected before  *
    //******************************************************************************
    private int randomLetter(int numSelect)
    {
        switch (numSelect)
        {
            case 0:
                return Random.Range(0, 4);
            case 1:
                return Random.Range(5, 9);
            case 2:
                return Random.Range(10, 11);
            case 3:
                return Random.Range(12, 15);
            case 4:
                return Random.Range(16, 20);
            case 5:
                return 21;
            case 6:
                return Random.Range(22, 23);
            case 7:
                return Random.Range(24, 26);
            default:
                return 0;
        }
    }

    //Initialization
    void Start ()
    {
        //1 point vowels; will always get two (not including wild card)
        myLetterDict.Add(0, letterA);
        myLetterDict.Add(1, letterE);
        myLetterDict.Add(2, letterI);
        myLetterDict.Add(3, letterO);
        myLetterDict.Add(4, letterU);

        //1 point consonant
        myLetterDict.Add(5, letterL);
        myLetterDict.Add(6, letterN);
        myLetterDict.Add(7, letterR);
        myLetterDict.Add(8, letterT);
        myLetterDict.Add(9, letterS);

        //2 point consonant
        myLetterDict.Add(10, letterD);
        myLetterDict.Add(11, letterG);

        //3 point consonant
        myLetterDict.Add(12, letterB);
        myLetterDict.Add(13, letterC);
        myLetterDict.Add(14, letterM);
        myLetterDict.Add(15, letterP);

        //4 point consonant
        myLetterDict.Add(16, letterF);       
        myLetterDict.Add(17, letterH);
        myLetterDict.Add(18, letterV);
        myLetterDict.Add(19, letterW);
        myLetterDict.Add(20, letterY);

        //5 point consonant
        myLetterDict.Add(21, letterK);

        //8 point consonant
        myLetterDict.Add(22, letterJ);
        myLetterDict.Add(23, letterX);

        //10 point consonant
        myLetterDict.Add(24, letterQ);   
        myLetterDict.Add(25, letterZ);
        //Wild Card
        myLetterDict.Add(26, letterWild);


        objTemp.SetActive(true);
        numSelectP = Random.Range(0, 8);
        localLetter = randomLetter(numSelectP);
        objTemp.GetComponent<SpriteRenderer>().sprite = myLetterDict[localLetter];

        playerLetterPos[0] = new Vector3(-20f, 4.62f, 0f);
        playerLetterPos[1] = new Vector3(-15f, 4.62f, 0f);
        playerLetterPos[2] = new Vector3(-20f, 1.12f, 0f);
        playerLetterPos[3] = new Vector3(-15f, 1.12f, 0f);
        playerLetterPos[4] = new Vector3(-20f, -2.38f, 0f);
        playerLetterPos[5] = new Vector3(-15f, -2.38f, 0f);
        playerLetterPos[6] = new Vector3(-20f, -5.88f, 0f);
        playerLetterPos[7] = new Vector3(-15f, -5.88f, 0f);

        for (int i = 0; i < 8; i++)
        {
            //Letter level select
            if (i < 2)
            {
                numSelectP = 0;
                numSelectO = 0;
            }
            else
            {
                numSelectP = grabLetterCategory();
                numSelectO = grabLetterCategory();
            }

            //Player letter
            localLetter = randomLetter(numSelectP);
            playerLetters[i] = Instantiate(objTemp, playerLetterPos[i] , Quaternion.identity);
            playerLetters[i].GetComponent<SpriteRenderer>().sprite = myLetterDict[localLetter];
            playerLetters[i].transform.localScale = LETTER_SCALE;

            //Opponent letter
            localLetter = randomLetter(numSelectO);
            opponentLetters[i] = Instantiate(objTemp, playerLetterPos[i], Quaternion.identity);
            vectTemp = playerLetterPos[i];
            vectTemp.x *= -1;
            opponentLetters[i].transform.position = vectTemp;
            opponentLetters[i].GetComponent<SpriteRenderer>().sprite = myLetterDict[localLetter];
            opponentLetters[i].transform.localScale = LETTER_SCALE;
            opponentLetters[i].layer = (int)OBJECT_LAYERS.Enemy;
        }
    }


    //********************************************
    //*  Drag operation performed during Update  *
    //********************************************
    private void dragObject()
    {
        if (obj_grab != null)
        {
            mouse_position_raw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_position_game.x = mouse_position_raw.x;
            mouse_position_game.y = mouse_position_raw.y;
            obj_grab.position = mouse_position_game;
        }
    }


    //*************************************************
    //*  Detects Mouse-Down Input and Sets RigidBody  *
    //*************************************************
    private void detectObjectWithMouse()
    {
        if (Input.GetMouseButton(0))
        {
            mouse_position_raw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_position_game.x = mouse_position_raw.x;
            mouse_position_game.y = mouse_position_raw.y;

            if (obj_grab == null && Input.GetMouseButtonDown(0))
            {
                obj_hit = Physics2D.Raycast(mouse_position_game, direction);
                if (obj_hit)
                {
                    location_grab.x = obj_hit.transform.gameObject.transform.position.x;
                    location_grab.y = obj_hit.transform.gameObject.transform.position.y;
                }
            }

            if (obj_hit.collider != null)
            {
                if (obj_hit.transform.gameObject.layer != (int)OBJECT_LAYERS.Enemy)
                {
                    obj_grab = obj_hit.collider.GetComponent<Rigidbody2D>();
                    obj_hit.transform.gameObject.layer = (int)OBJECT_LAYERS.Dragging;
                    obj_hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)SORTING_LAYERS.Dragging;
                }
                else
                {
                    obj_grab = null;
                }
            }
            return;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (obj_hit.collider != null && obj_hit.transform.gameObject.layer != (int)OBJECT_LAYERS.Enemy)
            {
                obj_hit.transform.gameObject.layer = (int)OBJECT_LAYERS.Normal;
                obj_hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)SORTING_LAYERS.Pieces;
                obj_hit.transform.gameObject.transform.position = tileCheck(mouse_position_game,location_grab, grid_size);
            }
        }

        obj_grab = null;
    }


    //*****************************************************************
    //*  Iterates through game grid to find the tile with the minimum *
    //*****************************************************************
    public Vector2 tileCheck(Vector2 current_location, Vector2 dest_location, int grid_size)
    {
        if (current_location.x < -10.79 || current_location.x > 10.93 || current_location.y > 11.14 || current_location.y < -10.76)
        {
            Debug.Log("Hopefully this always works");
            return dest_location;
        }

        float min_dot_product = float.MaxValue;
        float dot_product;
        float offset = 3.62f;
        Vector2 temp = new Vector3(-5.45f, 5.5f);
        float x_start = temp.x;
        Vector2 final_values = new Vector2(0f, 0f);

        for (int i = 0; i < grid_size; i++)
        {
            for (int j = 0; j < grid_size; j++)
            {
                dot_product = findDistance(current_location, temp);
                min_dot_product = Mathf.Min(dot_product, min_dot_product);
                if (dot_product == min_dot_product)
                {
                    final_values.x = temp.x;
                    final_values.y = temp.y;
                }
                temp.x += offset;
            }
            temp.x = x_start;
            temp.y -= offset;
        }

        if (min_dot_product > offset)
        {
            return dest_location;
        }

        return final_values;
    }


    //*****************************************************************************
    //*  Computes the distance between two vectors by calculating the hypotenuse  *
    //*****************************************************************************
    public float findDistance(Vector2 v1, Vector2 v2)
    {
        return (v1 - v2).sqrMagnitude;
    }


    // Update is called once per frame
    void Update ()
    {
        detectObjectWithMouse();
    }

    void FixedUpdate()
    {
        dragObject();
    }
}
