using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myMovement : MonoBehaviour
{
    public float myJump;
    public float jump_offset;
    public Vector2 myActionStart = new Vector2(0f, 0f);
    public int gridWidth = 7;
    public GameObject the_other_obj;
    private Vector2 direction = Vector2.zero;
    private Vector2 obj_hit;
    private Vector3 myCurrPosition;
    private Vector3 cursor_size5 = new Vector3(0.6f, 0.6f, 0.6f);
    private Vector3 cursor_size4 = new Vector3(0.7f, 0.7f, 0.7f);
    private Vector3 cursor_size3 = new Vector3(0.8f, 0.8f, 0.8f);
    private Vector3 cursor_size2 = new Vector3(0.9f, 0.9f, 0.9f);
    private Vector3 cursor_size1 = new Vector3(1f, 1f, 1f);
    private int last_cursor_size = 1;
    private bool cursor_direction = false;//false = down, true = up
    private bool left_or_right = false;//false = left, true = right
    private bool enter_pressed1 = false;
    private int frames = 0;
    private float thresholdR;
    private float thresholdL;
    private float thresholdU;
    private float thresholdD;
    private GameObject thisCursor;
    private GameObject selected;
    private myMovement the_other_script;


    void Start()
    {
        if (string.Equals(gameObject.name, "myCursor"))
        {
            //gameObject.SetActive(false);

            switch (gridWidth)
            {
                case 7:
                    thresholdR = 10.83f;
                    thresholdL = -10.81f;
                    break;
                case 4:
                    thresholdR = 5.3f;
                    thresholdL = -5.4f;
                    thresholdU = 5.1f;
                    thresholdD = -5.1f;
                    //transform.Translate(-20f, 4.6f, 0f);
                    break;
            }
        }
        else
        {
            switch (gridWidth)
            {
                case 7:
                    break;
                case 4:
                    thresholdR = -20f;
                    thresholdL = -21f;
                    thresholdU = 4f;
                    thresholdD = -5f;
                    //transform.Translate(-20f, 4.6f, 0f);
                    break;
            }
        }
        cursor_direction = false;
        the_other_script = the_other_obj.GetComponent<myMovement>();
    }


    void Update()
    {
        if (string.Equals(gameObject.name, "myCursor"))
        {
            cursorMovement();
        }
        if (string.Equals(gameObject.name, "Arrow") && !the_other_obj.activeSelf)
        {
            cursorMovement();
        }
    }


    //***********************************************************
    //*  Code for the cursors that the users use for selecting  *
    //***********************************************************
    void cursorMovement()
    {
        myCurrPosition = transform.position;

        if (myCurrPosition.x < thresholdR && (Input.GetKeyDown("d") == true || Input.GetKeyDown("right") == true))
        {
            transform.Translate(myJump + jump_offset, 0, 0);
            left_or_right = true;
        }
        else if (myCurrPosition.x > thresholdL && (Input.GetKeyDown("a") == true || Input.GetKeyDown("left") == true))
        {
            transform.Translate((myJump + jump_offset) * -1, 0, 0);
            left_or_right = false;
        }
        else if (myCurrPosition.y < thresholdU && (Input.GetKeyDown("w") == true || Input.GetKeyDown("up") == true))
        {
            transform.Translate(0, myJump, 0);
        }
        else if (myCurrPosition.y > thresholdD && (Input.GetKeyDown("s") == true || Input.GetKeyDown("down") == true))
        {
            transform.Translate(0, myJump * -1, 0);
        }
        else if ((Input.GetKeyDown("enter") == true) || (Input.GetKeyDown("return") == true) || (Input.GetKeyDown("e") == true))
        {
            if (string.Equals(gameObject.name, "Arrow") && !enter_pressed1)
            {
                enter_pressed1 = true;
                the_other_obj.SetActive(true);

                if (myCurrPosition.x >= -18.3)
                {
                    transform.position = new Vector3(-17.4f, myCurrPosition.y, 0);
                }
                else
                {
                    transform.position = new Vector3(-22.4f, myCurrPosition.y, 0);
                }

                selected = pieceGrabber(myCurrPosition);
                the_other_obj.GetComponent<SpriteRenderer>().sprite = selected.GetComponent<SpriteRenderer>().sprite;
                selected.SetActive(false);
            }
            else if (string.Equals(gameObject.name, "myCursor"))
            {
                GameObject new_obj = Instantiate(gameObject, myCurrPosition, Quaternion.identity);
                new_obj.transform.localScale = cursor_size5;
                gameObject.SetActive(false);
                the_other_script.enter_pressed1 = false;
            }
        }
        else
        {
            if (string.Equals(gameObject.name, "myCursor") && frames % 12 == 0)
            {
                idleAnimationGrid();
            }
            else if (string.Equals(gameObject.name, "Arrow") && frames % 6 == 0 && !enter_pressed1)
            {
                Debug.Log("9");
                idleAnimationPiece();
            }
            if (frames == 60)
            {
                frames = 0;
            }
            else
            {
                frames++;
            }
        }
    }


    //************************************************************************************
    //*  Slightly moves piece-selection cursor side-to-side to create an idle animation  *
    //************************************************************************************
    void idleAnimationPiece()
    {
        if (cursor_direction)
        {
            transform.Translate(0.25f, 0, 0);

            if ((myCurrPosition.x > -21.9 && !left_or_right) || (myCurrPosition.x > -16.9 && left_or_right))
            {
                //transform.Translate(-0.25f,0, 0);
                cursor_direction = false;
            }
        }
        else
        {
            transform.Translate(-0.25f, 0, 0);

            if ((myCurrPosition.x < -22.9 && !left_or_right) || (myCurrPosition.x < -17.9 && left_or_right))
            {
                transform.Translate(0.25f, 0, 0);
                cursor_direction = true;
            }
        }
    }


    //****************************************************************
    //*  Resizes grid-selection cursor to create an idle animation  *
    //****************************************************************
    void idleAnimationGrid()
    {
        if (cursor_direction)
        {
            switch (last_cursor_size)
            {
                case 1:
                    //Debug.Log(cursor_size2);
                    transform.localScale = cursor_size2;
                    cursor_direction = false;
                    last_cursor_size = 9;
                    break;
                case 9:
                    //Debug.Log(cursor_size1);
                    transform.localScale = cursor_size1;
                    last_cursor_size = 1;
                    break;
                case 8:
                    //Debug.Log(cursor_size2);
                    transform.localScale = cursor_size2;
                    last_cursor_size = 9;
                    break;
                case 7:
                    //Debug.Log(cursor_size3);
                    transform.localScale = cursor_size3;
                    last_cursor_size = 8;
                    break;
                case 6:
                    //Debug.Log(cursor_size4);
                    transform.localScale = cursor_size4;
                    last_cursor_size = 7;
                    break;
            }
        }
        else
        {
            switch (last_cursor_size)
            {
                case 1:
                    //Debug.Log(cursor_size2);
                    transform.localScale = cursor_size2;
                    last_cursor_size = 9;
                    break;
                case 9:
                    //Debug.Log(cursor_size3);
                    transform.localScale = cursor_size3;
                    last_cursor_size = 8;
                    break;
                case 8:
                    //Debug.Log(cursor_size4);
                    transform.localScale = cursor_size4;
                    last_cursor_size = 7;
                    break;
                case 7:
                    //Debug.Log(cursor_size5);
                    transform.localScale = cursor_size5;
                    last_cursor_size = 6;
                    break;
                case 6:
                    //Debug.Log(cursor_size4);
                    transform.localScale = cursor_size4;
                    cursor_direction = true;
                    last_cursor_size = 7;
                    break;
            }
        }
    }


    //***************************************************************
    //*  Finds the object piece the cursor is currently pointing to *
    //***************************************************************
    private GameObject pieceGrabber(Vector2 current_location)
    {
        Vector2 final_values = new Vector2(0f, 0f);

        if (left_or_right)
        {
            final_values.x = -15f;
        }
        else
        {
            final_values.x = -20f;
        }

        final_values.y = current_location.y;

        return Physics2D.Raycast(final_values, direction).transform.gameObject;
    }
}