using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlacement : MonoBehaviour
{
    private Dictionary<string,GameObject> letters_used_this_turn;
    public float highlight_offset = 0.62f;
    public float origin_x = -0.34f;
    public float origin_y = 4.34f;

    /*
    enum SORTING_LAYERS {
        Default,
        Background,
        Board,
        Letter,
        Bomb,
        Dragging,
        UI
    };
    */

    private GameObject draggedObject;

    //~~UNITY: Start is called before the first frame update
    void Start()
    {
        letters_used_this_turn = new Dictionary<string, GameObject>();
    }


    //**********************
    //*  JEREMY FUNCTIONS  *
    //**********************

    //~~JEREMY: Adds letters used in a given turn to custom dictionary. (Index=object name, Value=object)
    public void addLettersUsedThisTurn(GameObject currentLetter) {
        letters_used_this_turn.Add(currentLetter.name,currentLetter);
    }

    //~~JEREMY: For space complexity efficiency, returns only a list of object names added to table
    private List<string> getKeysToActiveLetters() {
        return new List<string>(this.letters_used_this_turn.Keys);
    }

    //~~JEREMY: Adds letters to custom dictionary. Indexes of letters is determined by class of frequency (Key=index of letter,Value=sprite)
    public void AddLetterSum() {
        int sum = 0;
        Debug.Log("Buttton-->" + String.Join(", ", getKeysToActiveLetters()));
    }
}
