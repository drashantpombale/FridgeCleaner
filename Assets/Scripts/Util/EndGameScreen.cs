using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameScreen : MonoBehaviour
{
    private const string successText = "YOU DID IT!";
    private const string failureText = "TRY AGAIN!";
    private const string successTimeText = "Time remaining: ";
    private const string failureTimeText = "You did not finish in time!";



    [SerializeField]
    private GameObject screen;

    [SerializeField]
    private TextMeshProUGUI timeTextBox;

    [SerializeField]
    private TextMeshProUGUI endGameTime;

    [SerializeField]
    private TextMeshProUGUI successTextField;

    void Start()
    {
        GameController.Instance.endGame.AddListener(ShowEndGameScreen);    
    }

    private void ShowEndGameScreen(bool success)
    {
        if (success)
        {
            successTextField.text = successText;
            endGameTime.text = successTimeText + timeTextBox.text;
        }
        else
        {
            successTextField.text = failureText;
            endGameTime.text = failureTimeText;
        }
        screen.SetActive(true);
    }

    public void GameEnd() 
    {
        SceneManager.LoadScene("MainMenu");
    }
}
