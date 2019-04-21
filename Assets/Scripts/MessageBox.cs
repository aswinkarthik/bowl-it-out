using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox : MonoBehaviour
{
    private static bool ShowCompletionMessage = false;
    public Text MessageText;
    int RandomNumber;
    string[] UserCodes = new string[10] {"8363", "3901", "6528", "3124", "9078", "3243", "8767", "7417", "8734", "5454"};

    // Start is called before the first frame update
    void Start()
    {
        MessageText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2 + 140);
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void ShowGameCompletionMessage()
    {
        RandomNumber = Random.Range(0, 10);

        string msgTxt = "Congrats! you Scored " + ScoreBoard.Score + "\n";
        if (ScoreBoard.Score > 100)
        {
            msgTxt += " Your Code " + UserCodes[RandomNumber];
        }
        MessageText.text = msgTxt;
        ShowCompletionMessage = true;
    }
}
