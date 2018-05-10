using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeOut : MonoBehaviour {

    public float opacity = 0.5f;
	// Use this for initialization

	void Start () {
        var color = this.GetComponent<Renderer>().material.color; 
        var newColor = new Color(color.r, color.g, color.b, opacity); 
        this.GetComponent<Renderer>().material.color = newColor;
        this.gameObject.SetActive(false);
        Vector3 pos = new Vector3(25, 5, 22);
        this.transform.position = pos;
	}
	
}
