using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AIController1 : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] destinations; // Array to store all destination points
    private int currentDestinationIndex = 0; // Index to track current destination
    public float idleTime = 2f; // Time to wait at each destination
    public float stopDistance = 1.0f; // Distance to stop before reaching the destination
    public float pauseTimeNearDestination = 1.0f; // Time to pause near the destination

    private Animator animator;

    void Start()
    {
        // Find all game objects with the tag "Destination"
        destinations = GameObject.FindGameObjectsWithTag("Destination");

        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Start the movement routine
        if (destinations.Length > 0)
        {
            StartCoroutine(MoveToNextDestination());
        }
    }

    IEnumerator MoveToNextDestination()
    {
        while (true)
        {
            // Überprüfen, ob der Agent auf einem NavMesh steht
            if (!agent.isOnNavMesh)
            {
                
                yield break; // Beende die Coroutine, wenn der Agent nicht auf dem NavMesh ist
            }

            // Set the destination of the NavMeshAgent
            agent.SetDestination(destinations[currentDestinationIndex].transform.position);

            // Start walking animation
            SetWalkingAnimation(true);

            // Pause near the destination
            yield return StartCoroutine(PauseNearDestination());

            // NPC has reached the destination, wait for idle time
            yield return new WaitForSeconds(idleTime);

            // Move to the next destination
            currentDestinationIndex = (currentDestinationIndex + 2) % destinations.Length;
        }
    }

    IEnumerator PauseNearDestination()
    {
        bool paused = false;
        while (true)
        {
            if (!agent.isOnNavMesh)
            {
                
                yield break; // Beende die Coroutine, wenn der Agent nicht auf dem NavMesh ist
            }

            // Calculate the distance to the destination
            float distance = Vector3.Distance(transform.position, destinations[currentDestinationIndex].transform.position);

            // Check if the NPC is near the destination and should pause
            if (distance <= stopDistance && !paused)
            {
                agent.isStopped = true;
                SetWalkingAnimation(false);
                yield return new WaitForSeconds(pauseTimeNearDestination);
                agent.isStopped = false;
                paused = true;

                // Restart walking animation
                SetWalkingAnimation(true);
            }

            // Check if the NPC has reached the destination
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                SetWalkingAnimation(false);
                yield break; // Exit the coroutine to wait at the destination
            }

            yield return null; // Wait for the next frame
        }
    }

    private void SetWalkingAnimation(bool isWalking)
    {
        animator.SetBool("isWalking", isWalking);
    }
}
