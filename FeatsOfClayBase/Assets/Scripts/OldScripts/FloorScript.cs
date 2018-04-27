using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour {

	// Dumb way to keep track of jumping, dont use this for the actual game

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player") || collision.collider.gameObject.CompareTag("Player Body"))
            PlayerController.isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.gameObject.CompareTag("Player"))
            PlayerController.isGrounded = false;
    }
}
