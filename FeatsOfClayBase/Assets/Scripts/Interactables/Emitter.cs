using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Emitter : MonoBehaviour
{
	
	LineRenderer lr;//line renderer
    public GameObject emitter;//itself

	public GameObject clayton;
    public LayerMask playerBodyLayer;//the stuff it can hit
    public GameObject claytonBeam;//the thing that rotates the player character, we can get rid of this later on
    public Vector3 raycastShootPosition;//used to set the location where the raycast is cast from
	public List<GameObject> hitObjects;
    private int vertexCount = 2;//its the size used for the Line Renderer Stuff

    Vector3 hit;//its the ourpout for raycast hitting stuff

    Vector3 fwd;//used for direction


    // Use this for initialization
    void Start()
    {
        lr = GetComponent<LineRenderer>();//gets the line renderer
		clayton = Camera.main.GetComponent<GameController>().clayton;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        vertexCount = 2;
        raycastShootPosition = emitter.transform.position;//sets the raycast position to itself
        fwd = transform.TransformDirection(Vector3.forward);//sets the foward vector to its forward vector
        lr.positionCount = vertexCount;//sets the number of line segment to vertex count
        ShootRay();     //shoots the ray  
    }

    void ShootRay()
    {
        RaycastHit Hit;//sets up the raycast 
        lr.SetPosition(0, raycastShootPosition);//sets the first postion in the line renderer to the emitter
        for (int i = 10; i >= 0; i--)//loops 10 times
        {
			hitObjects.Clear();
			if (Physics.Raycast(raycastShootPosition, fwd, out Hit, 100, playerBodyLayer))//does it hit
            {

                if (Hit.collider.gameObject.tag == "Player" && clayton.GetComponent<PlayerControllerV2>().gCon.eState.OnLaserHit() )  // HitsClayton
                {

					AddPoint(Hit);
					if (!hitObjects.Contains(clayton))
					{
						hitObjects.Add(clayton);
					}
					else
					{
						break;
					}

                }
                else if (Hit.collider.gameObject.tag == "Interactable" && Hit.rigidbody.gameObject.GetComponent<Interactable>().OnLaserHit())// Hits Other
                {
					AddPoint(Hit);
					if (!hitObjects.Contains(Hit.rigidbody.gameObject))
					{
						hitObjects.Add(Hit.rigidbody.gameObject);
					}
					else
					{
						break;
					}
				}

                else // Stop bouncing on hitting non crystal
                {
                    SetPos(Hit.transform.position);//sets positon of last line segmnet
					break;//ends the loop
                }
            }

			else
            {
                Vector3 missPos = fwd * 100;//creats a Vector3 that will travel 100 units in the forward vector
                SetPos(missPos);//sets positon of last line segment
				break;//ends the loop
            }
        }
		// Jank solution to stopping the laser form defaulting to the origin. It works, but may need to be dejanked later.
		//if (lr.GetPosition(lr.positionCount-1) == Vector3.zero)
		//{
		//	lr.positionCount--;
		//}

    }
	void AddPoint(RaycastHit Hit)
	{
		Debug.Log("Bit a Box");
		SetPos(Hit.transform.position);//sets postions of the last line segment
		fwd = Hit.transform.TransformDirection(Vector3.forward);//sets foward for next raycast
		if (lr.positionCount <= 11)
		{
			vertexCount++;//make the vertex Count go up by 1
			lr.positionCount = vertexCount;//sets the number of line segments to vertex count
		}
	}
    void SetPos(Vector3 vertPos)//used to set posiont, takes in a vector3
    {
        lr.SetPosition(vertexCount - 1, vertPos);//sets the position in the line renderer that is equal to vertexCount minus 1, and sets it to vertpos which is the vector3 needed to use the function
        raycastShootPosition = vertPos;        //sets the position to shoot the next raycast
    }
}
