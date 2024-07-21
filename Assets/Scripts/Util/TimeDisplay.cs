using System;
using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI textField;

    //in seconds
    [SerializeField]
    private float TimeToFinish = 180;

    private bool isGameOver = false;
    
    public string TimeInMinutes()
    {
        string seconds = "", minutes = "";

        seconds = ((int)TimeToFinish % 60).ToString("00");

        minutes = ((int)TimeToFinish / 60).ToString("00");

        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    private void Start()
    {
        GameController.Instance.endGame.AddListener(GameEnded);
    }

    private void GameEnded(bool arg0)
    {
        isGameOver = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            TimeToFinish -= Time.deltaTime;
            textField.text = TimeInMinutes();
        }

        if (TimeToFinish <= 0) 
        {
            GameController.Instance.endGame.Invoke(false);
        }
    }
}
