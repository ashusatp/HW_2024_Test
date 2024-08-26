using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private HashSet<Collider> visitedPulpits = new HashSet<Collider>();


    private ScoreManager scoreManager;
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        speed = 3.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object Doofus collided with has the "Pulpit" tag
        if (other.CompareTag("Pulpit"))
        {
            // Check if the Pulpit has been visited before
            if (!visitedPulpits.Contains(other))
            {
                // Update the score and mark this Pulpit as visited
                scoreManager.IncrementScore();
                visitedPulpits.Add(other);
            }
        }
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}
