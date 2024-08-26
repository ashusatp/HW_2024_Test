using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI scoreText;
    AudioController audioController;
    public MenuScript menuScript;

    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void IncrementScore()
    {
        audioController.PlaySFX(audioController.checkPoint);
        score++;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {

        scoreText.text = score.ToString();
        if (score == 50)
        {
            menuScript.GameComplete();
        }

    }
}
