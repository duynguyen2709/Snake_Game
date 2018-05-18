using UnityEngine;

public class GetTextScore : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        scoreTxt.text = SnakeMove_Ver2.txtScore2.text;
    }

    public UnityEngine.UI.Text scoreTxt;
}