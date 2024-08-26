using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private HashSet<Collider> visitedPulpits = new HashSet<Collider>();
    public float fallThreshold = 1f;

    private ScoreManager scoreManager;
    public GameObject gameOverPanel;
    private bool isGameOver = false;

    AudioController audioController;


    private void Awake()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
    }


    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        speed = 3f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pulpit"))
        {
            BoxCollider platformCollider = other.GetComponent<BoxCollider>();
            platformCollider.isTrigger = false;

            if (!visitedPulpits.Contains(other))
            {
                scoreManager.IncrementScore();
                visitedPulpits.Add(other);
            }
        }
    }

    void Update()
    {
        if (transform.position.y < fallThreshold && !isGameOver)
        {
            isGameOver = true;
            audioController.PlaySFX(audioController.death);
            audioController.stopBackgroundMusic();
            GameOver();
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void GameOver()
    {

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        Debug.Log("Game Over");
    }
}
