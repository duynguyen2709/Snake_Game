using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    private GameObject bf;

    private Transform border_Bottom;

    private Transform border_Left;

    private Transform border_Right;

    //Border
    private Transform border_Top;

    private int CountFood = 0;

    private float defaultDistance = 6f;

    private bool isAppear;

    private GameObject sf;

    private IEnumerator Appear(GameObject bigFood)
    {
        yield return new WaitForSeconds(3);
        Destroy(bigFood);
    }

    private bool IsRock(Vector3 pos, List<Vector3> listRock)
    {
        foreach (var posRock in listRock)
        {
            if (Vector3.Distance(pos, posRock) < defaultDistance)
                return true;
        }

        return false;
    }

    private bool IsSnake(Vector3 spawnPos, List<Transform> bodyPart)
    {
        foreach (var posSnake in bodyPart)
        {
            if (posSnake == null)
                return false;

            if (Vector3.Distance(spawnPos, posSnake.position) < defaultDistance / 4)
                return true;
        }

        return false;
    }

    private GameObject Spawn(GameObject food)
    {
        Vector3 spawnPos;
        do
        {
            float x = Mathf.Round(Random.Range(border_Left.position.x + 6f, border_Right.position.x - 6f)) + 0.5f;

            float z = Mathf.Round(Random.Range(border_Top.position.z - 6f, border_Bottom.position.z + 6f)) + 0.5f;

            spawnPos = new Vector3(x, 0.5f, z);
        } while (IsRock(spawnPos, CreatEnvironment.listRock) || (SnakeMove_Ver2.bodyPart.Count != 0 && IsSnake(spawnPos, SnakeMove_Ver2.bodyPart)));

        GameObject foodCreated = Instantiate(food, spawnPos, Quaternion.identity, this.transform) as GameObject;
        ate = false;

        return foodCreated;
    }

    // Use this for initialization
    private void Start()
    {
        ate = false;
        border_Top = GameObject.Find("Top_Border").transform;
        border_Bottom = GameObject.Find("Bottom_Border").transform;
        border_Left = GameObject.Find("Left_Border").transform;
        border_Right = GameObject.Find("Right_Border").transform;
        Spawn(smallFood);
        CountFood = 1;
    }

    private void Update()
    {
        sf = GameObject.Find("Apple(Clone)");
        bf = GameObject.Find("bigFood(Clone)");
        if (sf == null && bf == null)
        {
            if (CountFood == 5)
            {
                CountFood = 0;
                bf = Spawn(bigFood);
                StartCoroutine(Appear(bf));
            }
            else
            {
                sf = Spawn(smallFood);
                CountFood++;
            }
        }
    }

    public static bool ate = true;

    public GameObject bigFood;

    //Food Prefab
    public GameObject smallFood;
}