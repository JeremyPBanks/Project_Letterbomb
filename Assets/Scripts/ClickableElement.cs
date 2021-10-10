  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClickableElement<T>
{
    void clickableElement();
    float findDistance(Vector2 v1, Vector2 v2);
}



	//~~JEREMY: Detects if mouse is over a clickable element
    //TODO:::Limit Raycast via condition if possible for efficiency
    //public void clickableElement()
    //{
    	/*mouse_position_raw = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    	mouse_position_active.x = mouse_position_raw.x;
        mouse_position_active.y = mouse_position_raw.y;

		obj_hit = Physics2D.Raycast(mouse_position_active, direction);
        if (obj_hit)
        {
        	if (justHovered && hoveredID != obj_hit.transform.gameObject.GetInstanceID())
        	{
	        	justHovered = false;
	        	hoveredObj.transform.localScale -= new Vector3(0.04f,0.04f,0);
	        	hoveredObj = null;
        	}

        	hoveredObj = obj_hit.transform.gameObject;
        	hoveredID = hoveredObj.GetInstanceID();
            location_grab.x = hoveredObj.transform.position.x;
            location_grab.y = hoveredObj.transform.position.y;
            
            if (obj_hit.collider != null && !justHovered)
	        {
	        	justHovered = true;
	        	obj_grab = obj_hit.collider.GetComponent<Rigidbody2D>();
	            hoveredObj.transform.localScale += new Vector3(0.04f,0.04f,0);
	        }
        }
        else if (hoveredObj)
        {
            //Debug.Log("???????");
        	justHovered = false;
        	hoveredObj.transform.localScale -= new Vector3(0.04f,0.04f,0);
        	hoveredObj = null;
        }*/
        
    //}

	//~~JEREMY: Computes the distance between two vectors by calculating the hypotenuse
    /*public float findDistance(Vector2 v1, Vector2 v2) {
        return (v1 - v2).sqrMagnitude;
    }*/
