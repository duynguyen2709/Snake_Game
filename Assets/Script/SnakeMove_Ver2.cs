using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SnakeMove_Ver2 : MonoBehaviour
{

    public float distance = 1f;

    public float time = 0.45f;

    public GameObject cube;

    public GameObject endGameScreen;

    private bool paused;

    private Vector3 newBodyPos;

    private int bodyCount = 0;

    public static List<Transform> bodyPart;

    private float smooth;

    public int score = 0;
    private bool isAppear;
    public Text txtScore;

    private int count = 0;

    // Use this for initialization
    void Start()
    {
        SnakeDirection snakeDirection = this.GetComponent<SnakeDirection>();
        smooth = snakeDirection.smooth;
        bodyPart = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        //IF PAUSING THEN DO NOTHING
        if (paused)
            return;
        Vector3 headPos = this.transform.position;
       
        if (count == 10)
        {
            time = time - time * 0.2f;
            count = 0;
            
        }
        //ALWAYS MOVE TOWARD
        transform.position += transform.forward * distance;

        if (bodyPart.Count > 0)
        {
            Vector3 firstPos = bodyPart[0].position;
            Vector3 secondPos = bodyPart[0].position;

            bodyPart[0].rotation = Quaternion.Lerp(transform.rotation, SnakeDirection.targetRotation, 10 * smooth * Time.deltaTime);
            bodyPart[0].position = headPos;

            int i = 1;
            while (i < bodyPart.Count)
            {
                secondPos = bodyPart[i].position;
                
                bodyPart[i].rotation = Quaternion.Lerp(transform.rotation, SnakeDirection.targetRotation, 10 * smooth * Time.deltaTime);

                bodyPart[i].position = firstPos;

                firstPos = secondPos;
                
                i++;
            }
        }
        StartCoroutine(Pause());
    }

    //PAUSE THE SNAKE FOR X SECONDS
    private IEnumerator Pause()
    {
        paused = true;
        yield return new WaitForSeconds(time);
        paused = false;
    }

    
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name.StartsWith("Rock"))
        {
            //SpawnFood.ate = true;
            LoadScene.EndGame();
        }

        Vector3 newPos = new Vector3();
        switch (col.gameObject.name)
        {
            case "Right_Border":
                this.gameObject.transform.DetachChildren();

                newPos = GameObject.Find("Left_Border").transform.position;
                newPos.x += 1.5f;
                newPos.y = 0.5f;
                newPos.z = this.transform.position.z;
                transform.position = newPos;

                break;
            case "Left_Border":
                this.gameObject.transform.DetachChildren();

                newPos = GameObject.Find("Right_Border").transform.position;
                newPos.x -= 1.5f;
                newPos.y = 0.5f;
                newPos.z = this.transform.position.z;
                transform.position = newPos;

                break;
            case "Top_Border":
                this.gameObject.transform.DetachChildren();

                newPos = GameObject.Find("Bottom_Border").transform.position;
                newPos.z += 1.5f;
                newPos.y = 0.5f;
                newPos.x = this.transform.position.x;
                transform.position = newPos;

                break;
            case "Bottom_Border":
                this.gameObject.transform.DetachChildren();

                newPos = GameObject.Find("Top_Border").transform.position;
                newPos.z -= 1.5f;
                newPos.y = 0.5f;
                newPos.x = this.transform.position.x;
                transform.position = newPos;

                break;

            case "Apple(Clone)":
            
                if (bodyCount == 0)
                    newBodyPos = this.transform.position - transform.forward;
                else newBodyPos = bodyPart[bodyCount - 1].position - bodyPart[bodyCount - 1].forward;
                setPoint(1);
                bodyCount++;
                Destroy(col.gameObject);
                SpawnFood.ate = true;
                bodyPart.Add((Instantiate(cube, newBodyPos, this.transform.rotation) as GameObject).transform);

                count++;
                break;
            case "bigFood(Clone)":
                for (int i = 0; i < 2; i++)
                {
                    if (bodyCount == 0)
                        newBodyPos = this.transform.position - transform.forward;
                    else newBodyPos = bodyPart[bodyCount - 1].position - bodyPart[bodyCount - 1].forward;

                    bodyCount++;

                    bodyPart.Add((Instantiate(cube, newBodyPos, this.transform.rotation) as GameObject).transform);
                }
                setPoint(5);
                Destroy(col.gameObject);
                SpawnFood.ate = true;

                count++;
                break;
            case "Body(Clone)":

                if (col.gameObject.transform != bodyPart[0] && col.gameObject.transform != bodyPart[1])
                {
                    
				LoadScene.EndGame();
                }
                break;
           
        }

    }

    void setPoint(int point)
    {
        score += point;
        txtScore.text = "Score : " + score.ToString();
    }
}
