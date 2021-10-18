using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManagement : MonoBehaviour
{
	//~~Present in editor
	public GameObject objTemp;
	public Sprite letterA_spr;
    public Sprite letterB_spr;
    public Sprite letterC_spr;
    public Sprite letterD_spr;
    public Sprite letterE_spr;
    public Sprite letterF_spr;
    public Sprite letterG_spr;
    public Sprite letterH_spr;
    public Sprite letterI_spr;
    public Sprite letterJ_spr;
    public Sprite letterK_spr;
    public Sprite letterL_spr;
    public Sprite letterM_spr;
    public Sprite letterN_spr;
    public Sprite letterO_spr;
    public Sprite letterP_spr;
    public Sprite letterQ_spr;
    public Sprite letterR_spr;
    public Sprite letterS_spr;
    public Sprite letterT_spr;
    public Sprite letterU_spr;
    public Sprite letterV_spr;
    public Sprite letterW_spr;
    public Sprite letterX_spr;
    public Sprite letterY_spr;
    public Sprite letterZ_spr;
    //public Sprite bomb;
    //public static GameObject[] playerLetters = new GameObject[8];


    //~~Not present in editor
    private static Dictionary<int, Letter> myLetterDict = new Dictionary<int, Letter>();
    private static GameObject[] playerLetters = new GameObject[8];
    private RaycastHit2D obj_hit;
    private Vector2 location_grab;
    private Vector2 mouse_position_active = new Vector2(0f, 0f);
    private Vector2 direction = Vector2.zero;
    private Vector3 mouse_position_raw;


    


    //~Enum values for sprite sorting layer values
    enum SORTING_LAYERS
    {
        Default,
        Background,
        Board,
        Letter,
        Bomb,
        Hovered,
        Dragging,
        UI
    };

    //~Enum values for object layer values
    enum OBJECT_LAYERS
    {
        Bomb = 9,
        Dragging = 10,
        Letter = 11,
        Board = 15
    };






    //*********************
    //*  UNITY FUNCTIONS  *
    //*********************

    //~~UNITY: Start is called before the first frame update
    void Start()
    {
        addToDictionary();
        letterObjectCreation();
    }

    //~~UNITY: Update is called once per frame
    /*void Update()
    {
        clickableElement();
        //clickToDrag();
    }*/

    //~~UNITY: For physics, can run any number of frames due to the physics engine
    /*void FixedUpdate()
    {
        clickableElement();
        dragObject();
    }*/









    //**********************
    //*  JEREMY FUNCTIONS  *
    //**********************


    //~~JEREMY: Adds letters to custom dictionary. Indexes of letters is determined by class of frequency (Key=index of letter,Value=sprite)
    void addToDictionary()
    {
    	 //1 point vowels; will always get two (not including wild card)
        myLetterDict.Add(0, new Letter(letterA_spr,'A',1));
        myLetterDict.Add(1, new Letter(letterE_spr,'E',1));
        myLetterDict.Add(2, new Letter(letterI_spr,'I',1));
        myLetterDict.Add(3, new Letter(letterO_spr,'O',1));
        myLetterDict.Add(4, new Letter(letterU_spr,'U',1));

        //1 point consonant
        myLetterDict.Add(5, new Letter(letterL_spr,'L',1));
        myLetterDict.Add(6, new Letter(letterN_spr,'N',1));
        myLetterDict.Add(7, new Letter(letterR_spr,'R',1));
        myLetterDict.Add(8, new Letter(letterT_spr,'T',1));
        myLetterDict.Add(9, new Letter(letterS_spr,'S',1));

        //2 point consonant
        myLetterDict.Add(10, new Letter(letterD_spr,'D',2));
        myLetterDict.Add(11, new Letter(letterG_spr,'G',2));

        //3 point consonant
        myLetterDict.Add(12, new Letter(letterB_spr,'B',3));
        myLetterDict.Add(13, new Letter(letterC_spr,'C',3));
        myLetterDict.Add(14, new Letter(letterM_spr,'M',3));
        myLetterDict.Add(15, new Letter(letterP_spr,'P',3));

        //4 point consonant
        myLetterDict.Add(16, new Letter(letterF_spr,'F',4));       
        myLetterDict.Add(17, new Letter(letterH_spr,'H',4));
        myLetterDict.Add(18, new Letter(letterV_spr,'V',4));
        myLetterDict.Add(19, new Letter(letterW_spr,'W',4));
        myLetterDict.Add(20, new Letter(letterY_spr,'Y',4));

        //5 point consonant
        myLetterDict.Add(21, new Letter(letterK_spr,'K',5));

        //8 point consonant
        myLetterDict.Add(22, new Letter(letterJ_spr,'J',8));
        myLetterDict.Add(23, new Letter(letterX_spr,'X',8));

        //10 point consonant
        myLetterDict.Add(24, new Letter(letterQ_spr,'Q',10));
        myLetterDict.Add(25, new Letter(letterA_spr,'Z',10));

        //Bomb
        ///myLetterDict.Add(26, bomb);
    }

    //~~JEREMY: Creates each individual player letter-object from a base empty/temporary object; letters are assigned in the playerLetters array for future reference
    void letterObjectCreation()
    {
    	Vector3[] playerLetterPos = new Vector3[7];
    	int numSelectP = 0;

        playerLetterPos[0] = new Vector3(-6.5f,3f,0f);
        playerLetterPos[1] = new Vector3(-3.5f,3f,0f);//-15f, 4.62f, 0f);
        playerLetterPos[2] = new Vector3(-6.5f,1f,0f);//-20f, 1.12f, 0f);
        playerLetterPos[3] = new Vector3(-3.5f,1f,0f);//-15f, 1.12f, 0f);
        playerLetterPos[4] = new Vector3(-6.5f,-1f,0f);//-20f, -2.38f, 0f);
        playerLetterPos[5] = new Vector3(-3.5f,-1f,0f);//-15f, -2.38f, 0f);
        playerLetterPos[6] = new Vector3(-5f,-3f,0f);//-20f, -5.88f, 0f);

        for (int i = 0; i < 7; i++)
        {
            //Letter level select
            if (i < 2)
            {
                numSelectP = 0;
            }
            else
            {
                numSelectP = grabLetterCategory();
            }

            //Player letter
            int localLetter = randomLetter(numSelectP);
            playerLetters[i] = Instantiate(objTemp, playerLetterPos[i] , Quaternion.identity);
            playerLetters[i].name = "UserLetter-" + i;
            playerLetters[i].GetComponent<DragObject>().value = myLetterDict[localLetter].letter_value;
            playerLetters[i].GetComponent<SpriteRenderer>().sprite = myLetterDict[localLetter].letter_spr;
            /*Debug.Log(playerLetters[i].GetComponent<SpriteRenderer>().sprite.bounds.extents.x);
            Debug.Log(playerLetters[i].GetComponent<SpriteRenderer>().sprite.bounds.extents.y);
            Debug.Log("/n");*/
        }
    }

    //~~JEREMY: Returns a random letter index based on the letter's class
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
                return Random.Range(24, 25);//Change once we get bomb
            default:
                return 0;
        }
    }

    //~~JEREMY: Returns a random letter class based on frequency of use
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
}