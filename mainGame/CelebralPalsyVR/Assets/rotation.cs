using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float rotY = this.transform.rotation.eulerAngles.y;

        //print(rotY);
        this.transform.root.transform.eulerAngles = new Vector3(0, rotY, 0);
        this.transform.root.transform.eulerAngles = new Vector3(0, this.transform.rotation.eulerAngles.y - rotY, 0);
        return;
    }
}
