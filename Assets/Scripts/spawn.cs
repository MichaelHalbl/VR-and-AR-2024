using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    // Prefabs für die Dosen, die Bälle und die Tische
    public GameObject canPrefab;
    public GameObject ballPrefab;
    public GameObject tableBallsPrefab;
    public GameObject tableCanPrefab;

    // Positionen für die Tische
    public Transform tableBallsPosition;
    public Transform tableCanPosition;

    // Anzahl der Dosen und Bälle
    public int canCount = 10;
    public int ballCount = 5;
    public int distancetable = 5;

    // Sound für das Dosen-Zerstören
    public AudioClip canDespawnSound;

    // Sound für das Ball-Despawn
    public AudioClip ballDespawnSound;

    private int cansKnockedDown = 0;
    private int ballsRemaining;

    void Start()
    {
        ballsRemaining = ballCount;

        // Berechne die neue Position des Tischs für die Bälle basierend auf dem Abstand
        Vector3 tableBallsNewPosition = tableBallsPosition.position + new Vector3(0, 0, distancetable);

        // Tische spawnen
        GameObject tableBalls = Instantiate(tableBallsPrefab, tableBallsNewPosition, Quaternion.identity);
        GameObject tableCan = Instantiate(tableCanPrefab, tableCanPosition.position, Quaternion.identity);

        // Dosen in Pyramidenform auf dem Tisch spawnen
        Vector3 startPosition = tableCan.transform.position + Vector3.up * 1.0f; // Höhe des Tisches berücksichtigen
        float canSpacingX = 0.15f; // Horizontaler Abstand zwischen den Dosen
        float canSpacingY = 0.2f; // Vertikaler Abstand zwischen den Reihen der Dosen

        int currentCanCount = 0;
        int rows = 1;

        // Berechnung der maximalen Anzahl Dosen in der Basisreihe
        while (rows * (rows + 1) / 2 < canCount)
        {
            rows++;
        }
        for (int row = 0; row < rows; row++)
        {
            int cansPerRow = rows - row;
            for (int col = 0; col < cansPerRow; col++)
            {
                if (currentCanCount >= canCount) break;

                Vector3 position = startPosition + new Vector3(col * canSpacingX - (cansPerRow - 1) * 0.5f * canSpacingX, row * canSpacingY, 0);
                GameObject can = Instantiate(canPrefab, position, Quaternion.identity);
                Can canComponent = can.AddComponent<Can>(); // Add the Can script to each can
                canComponent.despawnSound = canDespawnSound; // Assign the sound to the can component
                currentCanCount++;
            }
            if (currentCanCount >= canCount) break;
        }

        // Bälle nebeneinander auf dem Tisch spawnen
        Vector3 ballStartPosition = tableBalls.transform.position + Vector3.up * 1.0f; // Höhe des Tisches berücksichtigen
        float ballSpacing = 0.2f; // Abstand zwischen den Bällen
        for (int i = 0; i < ballCount; i++)
        {
            Vector3 position = ballStartPosition + new Vector3(-0.5f + (i * ballSpacing), -0.4f, 0);
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            Ball ballComponent = ball.AddComponent<Ball>();
            ballComponent.despawnSound = ballDespawnSound;
            ballComponent.spawner = this; // Set the reference to this spawner
        }
    }

    public void CanKnockedDown()
    {
        cansKnockedDown++;
        if (cansKnockedDown >= canCount)
        {
            Debug.Log("Level gemeistert!");
        }
    }

    public void BallDespawned()
    {
        ballsRemaining--;
        if (ballsRemaining <= 0)
        {
            StartCoroutine(LevelFailedWithDelay(3.0f));
        }
    }

    private IEnumerator LevelFailedWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Level gescheitert!");
    }
}

public class Can : MonoBehaviour
{
    private bool hasFallen = false;
    public AudioClip despawnSound;

    void Update()
    {
        if (transform.position.y < 0.1f && !hasFallen)
        {
            hasFallen = true;
            PlayDespawnSound();
            Spawner spawner = FindObjectOfType<Spawner>();
            spawner.CanKnockedDown();
            Destroy(gameObject);
        }
    }

    void PlayDespawnSound()
    {
        if (despawnSound != null)
        {
            // Wenn ein AudioSource-Komponente vorhanden ist, benutze sie, sonst erstelle eine temporäre
            AudioSource.PlayClipAtPoint(despawnSound, transform.position);
        }
    }
}

public class Ball : MonoBehaviour
{
    private bool hasFallen = false;
    public AudioClip despawnSound;
    public Spawner spawner;

    void Update()
    {
        if (transform.position.y < 0.1f && !hasFallen)
        {
            hasFallen = true;
            PlayDespawnSound();
            spawner.BallDespawned();
            Destroy(gameObject);
        }
    }

    void PlayDespawnSound()
    {
        if (despawnSound != null)
        {
            // Wenn ein AudioSource-Komponente vorhanden ist, benutze sie, sonst erstelle eine temporäre
            AudioSource.PlayClipAtPoint(despawnSound, transform.position);
        }
    }
}
