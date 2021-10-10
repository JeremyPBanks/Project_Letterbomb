using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPlacement : MonoBehaviour
{
	enum SORTING_LAYERS {
        Default,
        Background,
        Board,
        Letter,
        Bomb,
        Dragging,
        UI
    };

    private GameObject draggedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
