using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightTrack : MonoBehaviour {

    private GameObject head;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = GameObject.Find("Head").transform.position;
        
	}
}
