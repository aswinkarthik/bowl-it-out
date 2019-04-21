using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBox : MonoBehaviour
{
    private static bool ShowCompletionMessage = false;
    private GUIStyle MessageStyle;
    static  int RandomNumber;
    string[] UserCodes = new string[10] {"8363", "3901", "6528", "3124", "9078", "3243", "8767", "7417", "8734", "5454"};

    // Start is called before the first frame update
    void Start()
    {
        MessageStyle = new GUIStyle();
        MessageStyle.fontSize = 64;
        MessageStyle.fontStyle = FontStyle.Bold;
        MessageStyle.normal.textColor = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (ShowCompletionMessage)
        {
            GUI.Box(new Rect(100, Screen.height / 2, 400, 400), "Congrats! you Scored " + ScoreBoard.Score + "\n" + "Your Code " + UserCodes[RandomNumber], MessageStyle);
        }

    }

    public static void ShowGameCompletionMessage()
    {
        RandomNumber = Random.Range(0, 10);
        ShowCompletionMessage = true;
    }
}
