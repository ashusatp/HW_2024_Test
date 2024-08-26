using UnityEngine;

public class PulpitController : MonoBehaviour
{
    public GameObject pulpitPrefab; 
    public float minPulpitDestroyTime = 4f; 
    public float maxPulpitDestroyTime = 5f;
    public float pulpitSpawnTime = 2.5f;

    public float pulpitSize = 36f; 

    private GameObject currentPulpit; 
    private GameObject previousPulpit; 

    private void Start()
    {
        
        SpawnInitialPulpit();
    }

    private void SpawnInitialPulpit()
    {
     
        currentPulpit = Instantiate(pulpitPrefab, Vector3.zero, Quaternion.identity);
        float lifetime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
        Invoke("DestroyPulpit", lifetime);
        Invoke("SpawnNewPulpit", pulpitSpawnTime);      
    }




    private void DestroyPulpit()
    {
        if (previousPulpit != null)
        {
            Destroy(previousPulpit);
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

          
            Vector3[] edgeAlignedPositions = new Vector3[]
            {
                lastPosition + new Vector3(pulpitSize, 0, 0),  
                lastPosition + new Vector3(-pulpitSize, 0, 0), 
                lastPosition + new Vector3(0, 0, pulpitSize),  
                lastPosition + new Vector3(0, 0, -pulpitSize)  
            };

           
            Vector3 newPosition;

            int randomIndex = Random.Range(0, edgeAlignedPositions.Length);
            newPosition = edgeAlignedPositions[randomIndex];


            currentPulpit = Instantiate(pulpitPrefab, newPosition, Quaternion.identity);

     
            float lifetime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
            Invoke("DestroyPulpit", lifetime);
        }
        else if (currentPulpit && previousPulpit == null)
        {
            Vector3 lastPosition = currentPulpit != null ? currentPulpit.transform.position : Vector3.zero;

            Vector3[] edgeAlignedPositions = new Vector3[]
           {
                lastPosition + new Vector3(pulpitSize, 0, 0),  // Right edge aligned
                lastPosition + new Vector3(-pulpitSize, 0, 0), // Left edge aligned
                lastPosition + new Vector3(0, 0, pulpitSize),  // Forward edge aligned
                lastPosition + new Vector3(0, 0, -pulpitSize)  // Backward edge aligned
           };

            int randomIndex = Random.Range(0, edgeAlignedPositions.Length);
            Vector3 newPosition = edgeAlignedPositions[randomIndex];

            previousPulpit = currentPulpit;

            currentPulpit = Instantiate(pulpitPrefab, newPosition, Quaternion.identity);


            float lifetime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
            Invoke("DestroyPulpit", lifetime);
        }
    }
}
