using UnityEngine;
using UnityEngine.UI;

public class HighScoreCounter : MonoBehaviour
{
    private Text highScore;
    void Start()
    {
        highScore = GetComponent<Text>();
    }

    public void checkForHighScore(Rigidbody2D rb)
    {
        if ((rb.position.x - 43) > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", (rb.position.x - 43));
            highScore.text = (rb.position.x - 43).ToString("0");
        }
    }
}
