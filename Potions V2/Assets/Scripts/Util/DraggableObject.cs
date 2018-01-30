﻿
using UnityEngine;

public class DraggableObject : MonoBehaviour {


    public bool draggable = true;

    [SerializeField]
    private GameObject pickupEffect;

  

    [SerializeField]
    private GameObject destroyEffect;

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        if (draggable)
        {
         
            if (pickupEffect != null)
            {
                GameObject clone = Instantiate(pickupEffect, this.transform.position, transform.rotation);
            }
            else
            {
                Debug.Log("Missing pickupEffect on " + this.gameObject.name);
            }

            AudioManager.instance.PlaySound("IngredientPickup", this.transform.position);
            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }


    }

    void OnMouseDrag()
    {
        if (draggable)
        {
            //add a dragging sound?
            float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        }
     

    }

    public void DestroyEffect()
    {
        if (destroyEffect !=null )
        {
            GameObject clone = Instantiate(destroyEffect, transform.position, destroyEffect.transform.rotation);
        }
        else
        {
            Debug.Log("Missing destroyEffect on " + this.gameObject.name);
        }
    }

    #region
    //allows you to drag in 3 space
    /**  void OnMouseDrag()
      {
          Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

          Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
          transform.position = curPosition;

      }**/
    #endregion

    #region 
    //allows you to drag only left and right
    /**void OnMouseDrag()
    {
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
        transform.position = new Vector3(pos_move.x, transform.position.y, pos_move.z);

    }**/
    #endregion
}
