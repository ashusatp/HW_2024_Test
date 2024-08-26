using UnityEngine;

public class PulpitController : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public float minPulpitDestroyTime = 4f; 
    public float maxPulpitDestroyTime = 5f;
    public float pulpitSpawnTime = 2.5f; 

    private GameObject currentPulpit; 
    private GameObject previousPulpit; 

    private void Start()
    {
        SpawnInitialPulpit();
    }

    private void SpawnInitialPulpit()
    {
     
        currentPulpit = Instantiate(pulpitPrefab, new Vector3(0, 0, 0), Quaternion.identity);

      
        float lifetime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
        Invoke("DestroyPulpit", lifetime);
    }

    private void DestroyPulpit()
    {
        if (previousPulpit != null)
        {
            Destroy(previousPulpit);
            previousPulpit = null;
        }

        previousPulpit = currentPulpit;
        currentPulpit = null;

    
        Invoke("SpawnNewPulpit", pulpitSpawnTime);
    }

    private void SpawnNewPulpit()
    {
        if (currentPulpit == null)
        {
           
            Vector3 lastPosition = previousPulpit != null ? previousPulpit.transform.position : Vector3.zero;

          
            Vector3[] adjacentPositions = new Vector3[]
            {
                lastPosition + new Vector3(9, 0, 0),  // Right
                lastPosition + new Vector3(-9, 0, 0), // Left
                lastPosition + new Vector3(0, 0, 9),  // Forward
                lastPosition + new Vector3(0, 0, -9)  // Backward
            };

          
            Vector3 newPosition;
            do
            {
                int randomIndex = Random.Range(0, adjacentPositions.Length);
                newPosition = adjacentPositions[randomIndex];
            } while (newPosition == lastPosition);

            currentPulpit = Instantiate(pulpitPrefab, newPosition, Quaternion.identity);

      
            float lifetime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
            Invoke("DestroyPulpit", lifetime);
        }
    }
}
