using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour
{
    public Button startButton;
    public Text titleText;
    public Text subTitleText;
    private static bool ShowGameStart = false;
    public GoogleARCore.Examples.HelloAR.Interactions interactions;

    // Start is called before the first frame update
    void Start()
    {
        titleText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2+140);
        startButton.transform.position = new Vector3(Screen.width/2, Screen.height/2-120);
        subTitleText.transform.position = new Vector3(Screen.width / 2, Screen.height / 2 - 220);
        startButton.onClick.AddListener(StartGame);
        StopGame();
    }

    private void OnGUI()
    {
        if (ShowGameStart)
        {

        }
        else
        {

        }

    }

    public void StartGame()
    {
        Debug.LogError("Game start Click----");
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("startbutton");
        foreach (GameObject button in buttons)
        {
            button.SetActive(false);
        }
        interactions.StartDetectingPlane();
    }

    public void HideTitle()
    {
        Debug.LogError("Game stop title----");
        ShowGameStart = false;
    }

    private void ShowTitle()
    {
        Debug.LogError("Game start title----");
        ShowGameStart = true;
    }

    public void StopGame()
    {
        HideTitle();
        interactions.StopARSession();
    }

    public IEnumerator ShowStartUpTitle()
    {
        Debug.LogError("Game start Click--enter--");
        ShowTitle();
        yield return new WaitForSeconds(3);
        HideTitle();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
