using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.left * 3.5f * Time.deltaTime);

        // Destroy the offscreen obstacles
        if(transform.position.x < -15.0f)
        {
            Destroy(this.gameObject);
        }
	}

}
