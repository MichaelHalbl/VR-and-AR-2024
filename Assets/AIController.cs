using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] destinations; // Array to store all destination points
    private int currentDestinationIndex = 0; // Index to track current destination
    public float idleTime = 2f; // Time to wait at each destination

    // Start is called before the first frame update
    void Start()
    {
        // Find all game objects with the tag "Destination"
        destinations = GameObject.FindGameObjectsWithTag("Destination");

        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        // Start the movement routine
        if (destinations.Length > 0)
        {
            StartCoroutine(MoveToNextDestination());
        }
    }

    // Coroutine to move the NPC to each destination
    IEnumerator MoveToNextDestination()
    {
        while (true)
        {
            // Set the destination of the NavMeshAgent
            agent.SetDestination(destinations[currentDestinationIndex].transform.position);

            // Wait until the NPC reaches the destination
            while (!agent.pathPending && agent.remainingDistance > agent.stoppingDistance)
            {
                yield return null;
            }

            // NPC has reached the destination
            yield return new WaitForSeconds(idleTime); // Pause and perform idle animation

            // Move to the next destination
            currentDestinationIndex = (currentDestinationIndex + 1) % destinations.Length;
        }
    }
}
