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
    private bool isGameOver = false;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        speed = 3f;
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
            GameOver();
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
