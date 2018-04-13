using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClaytonParent : MonoBehaviour {

    public GameObject player;
    public Vector3 offset;

    public float rayLength;
    public LayerMask layerMask;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        Plane plane = new Plane(Vector3.up, transform.position);

        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        if(plane.Raycast(myRay, out rayLength))
        {
            Vector3 intersection = myRay.GetPoint(rayLength);
            Vector3 diff = intersection - transform.position;

            float angle = Mathf.Atan2(diff.z, diff.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 90 - angle, 0);
        }


	}

    void LateUpdate()
    {

        transform.position = player.transform.position + offset;
    }
}
