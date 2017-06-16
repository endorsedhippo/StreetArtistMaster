using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour 
{

	public GameObject teleportOutLocation;
    public GameObject teleportOutLocation2;
    public GameObject teleportOutLocation3;
    public GameObject teleportOutLocation4;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    // Use this for initialization
    void Start () 
    {
	
		//Check if Teleport out location has been set.
		if (teleportOutLocation == null) {
			Debug.LogWarning("No Teleporter Out Location has been set for " + transform.name + ", Please set it and restart");
		}

		//Checks to see if you have set the collider on this object to a trigger 
		if (!GetComponent<Collider2D> ().isTrigger)
        {
			Debug.LogWarning("Collider2D on " + transform.name + " is not set to \"Is Trigger\", please update and restart");
		}
	}
	
	// This function is called then any collider touches the collider on this object.
	void OnTriggerEnter2D(Collider2D collidedObject) 
    {
		Debug.Log ("Player Teleported");
		//Teleport GameObject that was collided with to the teleportOutLocation
		if (collidedObject.gameObject == player1) 
        {

			collidedObject.transform.position = teleportOutLocation.transform.position;

		}
        if (collidedObject.gameObject == player2)
        {

            collidedObject.transform.position = teleportOutLocation2.transform.position;

        }
        if (collidedObject.gameObject == player3)
        {

            collidedObject.transform.position = teleportOutLocation3.transform.position;

        }
        if (collidedObject.gameObject == player4)
        {

            collidedObject.transform.position = teleportOutLocation4.transform.position;

        }
    }

	//This section Draws a line from the teleporter in to the teleporter out within the editor
	void OnDrawGizmos(){
		Gizmos.DrawLine(transform.position,teleportOutLocation.transform.position);
	}
}
