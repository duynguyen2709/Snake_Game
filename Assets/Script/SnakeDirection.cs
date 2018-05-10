using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeDirection : MonoBehaviour {

    public float smooth = 10f;

    public static Vector3 beforeTurningPos;

    public static Quaternion targetRotation;

    private string Direction;

    private bool paused;

    public float time = 0.2f;

	void Start () {
        targetRotation = transform.rotation;
        Direction = "Up";
 
	}
	
	
	void Update () {

        //GET INPUT KEY TO ROTATE
        if (paused)
            return;
        if (Input.GetKey(KeyCode.RightArrow) && Direction != "Right" && Direction != "Left")
        {
            if (Direction == "Up")
            {
                setDirection("Right", 1);                
            }
            else if (Direction == "Down")
            {
                setDirection("Right", -1);               
            }
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && Direction != "Right" && Direction != "Left")
        {
            if (Direction == "Up")
            {
                setDirection("Left", -1);               
            }
            else if (Direction == "Down")
            {
                setDirection("Left", 1);              
            }
        }

        else if (Input.GetKey(KeyCode.UpArrow) && Direction != "Up" && Direction != "Down")
        {
            if (Direction == "Right")
            {
                setDirection("Up", -1);               
            }
            else if (Direction == "Left")
            {
                setDirection("Up",1);             
            }
        }

        else if (Input.GetKey(KeyCode.DownArrow) && Direction != "Up" && Direction != "Down")
        {
            if (Direction == "Right")
            {
                setDirection("Down", 1);       
            }
            else if (Direction == "Left")
            {
                setDirection("Down", -1);     
            }
        }
        
        //ROTATE THE SNAKE AFTER INPUT
        
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);

        //FIX THE POSITION AFTER ROTATE

        Vector3 currentPos = this.transform.position;
        if (currentPos.x * 10 % 10 != 5 || currentPos.z * 10 % 10 != 5)
        {
            Vector3 fixPos = new Vector3();
            fixPos.x = Mathf.Floor(currentPos.x) + 0.5f;
            fixPos.y = 0.5f;
            fixPos.z = Mathf.Floor(currentPos.z) + 0.5f;
            this.transform.position = fixPos;
        }
      
	}
   
    void setDirection(string Dir,int C)
    {
        Direction = Dir;
        targetRotation *= Quaternion.AngleAxis(C*90, Vector3.up);
        StartCoroutine(Pause());
    }
    private IEnumerator Pause()
    {
        paused = true;
        yield return new WaitForSeconds(time);
        paused = false;
    }
}
 