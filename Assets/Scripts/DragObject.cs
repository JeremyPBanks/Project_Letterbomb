using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class DragObject : MonoBehaviour
{
	enum SORTING_LAYERS {
        Default,
        Background,
        Board,
        Letter,
        Bomb,
        Hovered,
        Dragging,
        UI
    };

    public float highlight_offset = 0.62f;
    public float origin_x = -0.34f;
	public float origin_y = 4.34f;
	public float offscreen_x = -0.34f;
	public float offscreen_y = 4.34f;
	public int value = 0;
	public GameObject highlightObj;
    private bool over_board = false;
	private bool hovering = false;
	private BoxCollider2D box_collider;
	private Collider2D[] overlapped_objects;
	private Vector3 mouse_position_raw;
	private Vector3 offset;
	private Vector3 highlightLocation;
	private Vector3 prevScale = new Vector3(1.0f,1.0f,0);
	public static bool dragging = false;


	//~~UNITY: Start is called before the first frame update
	void Start() {
		box_collider = gameObject.GetComponent<BoxCollider2D>();
	}

	//~~UNITY: Called ONCE per frame
	void Update() {
		if (dragging && over_board) {
			highlightLocation = getHighlightLocation(transform.position.x,transform.position.y);
			highlightObj.transform.position = highlightLocation;
		}
	}

	//~~UNITY: Called ONCE when the mouse enters the Collider
	void OnMouseEnter() {
		Debug.Log("STATE CHANGE:::::Enter");
		if (!dragging) {
			hovering = true;
			//Debug.Log("Enter; "+ dragging +"-->"+ this.GetInstanceID());
			GetComponent<SpriteRenderer>().sortingOrder = (int)SORTING_LAYERS.Hovered;
			//transform.localScale = prevScale
			transform.localScale = new Vector3(1.2f,1.2f,0);
		}
    }

    //~~UNITY: Called ONCE when the mouse is no longer on the Collider
    void OnMouseExit() {
    	Debug.Log("STATE CHANGE:::::Exit");
    	if (!dragging && hovering) {
	    	//Debug.Log("Off; "+ dragging +"--> "+ this.GetInstanceID());
	    	hovering = false;
	    	GetComponent<SpriteRenderer>().sortingOrder = (int)SORTING_LAYERS.Letter;
	    	transform.localScale = prevScale;
	    }
    }

    //~~UNITY: Called when the mouse is down over the Collider
	void OnMouseDown() {
		Debug.Log("STATE CHANGE:::::Down");
		dragging = true;
		//Debug.Log("MouseDown; "+ dragging +"-->"+ this.GetInstanceID());
		GetComponent<SpriteRenderer>().sortingOrder = (int)SORTING_LAYERS.Dragging;
		mouse_position_raw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mouse_position_raw.z));
		 prevScale = new Vector3(0.8f,0.8f,0);
		 transform.localScale = prevScale;
	}

	//~~UNITY: Called when the user has released the mouse button
	void OnMouseUp() {
		Debug.Log("STATE CHANGE:::::Up");
		GetComponent<SpriteRenderer>().sortingOrder = (int)SORTING_LAYERS.Letter;
		if (over_board && dragging) {
			over_board = false;
			dragging = false;
			transform.position = highlightLocation;
			prevScale = new Vector3(0.5f,0.5f,0);
			transform.localScale = prevScale;
			Debug.Log("Piece Placement: (" + highlightLocation.x + "," + highlightLocation.y + ")");
		}
		else {
			prevScale = new Vector3(1.0f,1.0f,0);
			transform.localScale = prevScale;
			Debug.Log("Boolean Value: over_board(" + over_board + ")\tdragging(" + dragging + ")");
		}
		highlightObj.transform.position = new Vector3(offscreen_x,offscreen_y,0);
		
		//Debug.Log("MouseUp; "+ dragging +"-->"+ this.GetInstanceID());
	}

	//~~UNITY: Called when the user has clicked on a Collider and is still holding down the mouse
	void OnMouseDrag() {
		Debug.Log("STATE CHANGE:::::Drag:::" + value);
		hovering = false;
		Vector3 mouse_position_current = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mouse_position_raw.z);
		Vector3 mouse_position_new = Camera.main.ScreenToWorldPoint(mouse_position_current) + offset;
		transform.position = mouse_position_new;
		overlapped_objects = Physics2D.OverlapAreaAll(box_collider.bounds.min, box_collider.bounds.max);

		//Detect if over another object, which is the board *right side of screen
		if (overlapped_objects.Length > 1 && transform.position.x > 0 && !over_board) {
			//Debug.Log(string.Format("Found {0} overlapping object(s)", overlapped_objects.Length));
			over_board = true;
		}
		else if ((overlapped_objects.Length <= 1 || transform.position.x <= 0) && over_board) {
			over_board = false;
		}
	}

	Vector3 getHighlightLocation(float x,float y) {
		float nearestSquare_X = nearestCoordinate(x, origin_x);
		//Debug.Log("New X-->" + nearestSquare_X);
		float nearestSquare_Y = nearestCoordinate(y, 0);
		//Debug.Log("New Y-->" + nearestSquare_Y);

		return new Vector3 (nearestSquare_X,nearestSquare_Y,0f);
	}


	float nearestCoordinate(float value, float origin)
	{
		//Debug.Log("Initial Value-->" + value);
	    float roundedValue =(Mathf.Floor((value / highlight_offset)) * highlight_offset) + origin;
	    return roundedValue;
	}
}