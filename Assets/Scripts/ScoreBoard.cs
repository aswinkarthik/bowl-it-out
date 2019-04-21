using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public static int Score = 0;
    private GUIStyle ScoreStyle;
    private static bool ShowScoreFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        ScoreStyle = new GUIStyle();
        ScoreStyle.fontSize = 48;
        ScoreStyle.fontStyle = FontStyle.Bold;
        ScoreStyle.normal.textColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnGUI()
    {
        if(ShowScoreFlag)
            GUI.Box(new Rect(100,100,200,200),"Score : " + Score, ScoreStyle);
    }

    public static void UpdateScore(int NewScore)
    {
        Score += NewScore;
    }

    public static void ShowScore()
    {
        ShowScoreFlag = true;
    }

    public static void HideScore()
    {
        ShowScoreFlag = true;
    }

}
