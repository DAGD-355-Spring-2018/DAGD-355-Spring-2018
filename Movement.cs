using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;
    private Transform rb;
    private float timer = 0;
    //public GameObject WaxPrefab;
    float t = 0;
    //private Rigidbody rb;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        float mp = Input.mousePosition.x/10;
        rb.rotation = Quaternion.Euler(0, mp, 0);

        

        if (Input.GetKey("a"))
        {
            rb.localPosition -= rb.right * speed;
        }

        if (Input.GetKey("d"))
        {
            rb.localPosition += rb.right * speed;
        }

        if (Input.GetKey("w"))
        {
            rb.localPosition += rb.forward * speed;
        }

        if (Input.GetKey("s"))
        {
            rb.localPosition -= rb.forward * speed;
        }

        

        t += Time.deltaTime;
        // float f = Mathf.Lerp(1, 10, t);
        // print(f);


        timer += Time.deltaTime;

       // if (timer > 1)
    //    {
        //    Instantiate(WaxPrefab, new Vector3 (rb.position.x, rb.position.y, rb.position.z), Quaternion.identity);
        //    timer = 0;
    //    }
    }
}
