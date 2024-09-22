using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    // Prefabs für die Dosen, die Bälle und die Tische
    public GameObject canPrefab;
    public GameObject ballPrefab;
    public GameObject tableBallsPrefab;
    public GameObject tableCanPrefab;
    private ScoreScript scoreObject;
    public GameObject spawnPoint;
    public GameObject player;

    // Positionen für die Tische
    public Transform tableBallsPosition;
    public Transform tableCanPosition;

    // Anzahl der Dosen und Bälle
    private int canCount;
    private int ballCount = 6;
    private int distancetable = 0;

    private int cansKnockedDown = 0;
    private int ballsFallen = 0;  // Zählt, wie viele Bälle gefallen sind
    private bool won = false;
    private bool over =  false;

    void Awake() {
        player.transform.position = spawnPoint.transform.position;
    }

    void Start()
    {
        scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreScript>();
        canCount = 4 + (2*scoreObject.DosenLevel);
        ballCount = 6 - (scoreObject.DosenLevel/3);
        distancetable = scoreObject.DosenLevel - 1;


        // Tische spawnen
        Vector3 tableballOffsetPosition = tableBallsPosition.position + new Vector3(0, 0, distancetable);
        GameObject tableBalls = Instantiate(tableBallsPrefab, tableballOffsetPosition, Quaternion.identity);
        
        
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
                can.AddComponent<Can>(); // Add the Can script to each can
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
            ball.AddComponent<Ball>(); // Add the Ball script to each ball
        }
    }

    public void CanKnockedDown()
    {
        cansKnockedDown++;
        if (cansKnockedDown >= canCount)
        {
            over = true;
            won = true;
            Debug.Log("Level gemeistert!");
        }
    }

    public void BallFallen()
    {
        ballsFallen++;
        if (ballsFallen >= ballCount)
        {
            over = true;
            Debug.Log("Spiel verloren!");
        }
    }

    void Update() {
        if(over) {
            if(won) {
                scoreObject.DosenLevel++;
            }
            var op =  SceneManager.LoadSceneAsync("HubWorld");
            op.allowSceneActivation = false;
            while(op.progress < 0.9f) {
                
            }
            op.allowSceneActivation = true;
        }
    }
}


public class Can : MonoBehaviour
{
    private bool hasFallen = false;

    void Update()
    {
        if (transform.position.y < 0.1f && !hasFallen)
        {
            hasFallen = true;
            Spawner spawner = FindObjectOfType<Spawner>();
            spawner.CanKnockedDown();
            Destroy(gameObject);
        }
    }
}


public class Ball : MonoBehaviour
{
    private bool hasFallen = false;

    void Update()
    {
        // Überprüfen, ob der Ball unter eine bestimmte Y-Position gefallen ist
        if (transform.position.y < 0.1f && !hasFallen)
        {
            hasFallen = true;
            Spawner spawner = FindObjectOfType<Spawner>();
            spawner.BallFallen(); // Informiere den Spawner, dass ein Ball gefallen ist
            Destroy(gameObject); // Zerstöre (despawne) den Ball
        }
    }
}

