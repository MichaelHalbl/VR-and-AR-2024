using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gamelogic : MonoBehaviour
{
    private ScoreScript scoreObject;
    public GameObject[] holes;
    public GameObject[] balls;
    public GameObject floor;
    public GameObject top;
    public int level;
    private int cHoles;
    public int score;
    public int highscore;
    public int pointsToWin;
    private SpawnPoint[] spawnPoints;
    public TextMeshProUGUI scoreText;
    public GameObject spawnPoint;
    public GameObject player;
    public GameObject leftController;
    public GameObject rightController;
    private bool isLoading = false; // Sicherstellen, dass die Szene nur einmal geladen wird

    struct SpawnPoint
    {
        public int color;
        public GameObject hole;
        public int spawnCount;
        public float time;
        public float wait;
        public int minWait;
        public int maxWait;
        public bool spawnFalse;
    }

    private float waitforEnd;
    private float end = 10;
    private bool waitingForEnd = false;

    void Awake()
    {
        player.transform.position = spawnPoint.transform.position;
    }

    void Start()
    {
        //add collider to controllers for contact with balls  and  bombs
        SphereCollider leftCollider = leftController.AddComponent<SphereCollider>();
        leftCollider.center = Vector3.zero;
        leftCollider.radius = 0.1f;
        SphereCollider rightCollider = rightController.AddComponent<SphereCollider>();
        rightCollider.center = Vector3.zero;
        rightCollider.radius = 0.1f;


        scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreScript>();
        level = scoreObject.fallingBallsLevel;
        cHoles = 2 + level;
        score = 0;
        spawnPoints = new SpawnPoint[cHoles];
        pointsToWin = 0;

        //spawn holes at random positions
        System.Random random = new System.Random();
        for (int i = 0; i < cHoles; i++)
        {
            float posx = (float)(random.NextDouble() * 1.79);
            float posz = (float)(random.NextDouble() * 1.79);
            bool overlap = false;
            for (int j = 0; j < i; j++)
            {
                if (top.transform.position.x + (posx - 0.895f) <= spawnPoints[j].hole.transform.position.x + 0.205f &&
                    top.transform.position.x + (posx - 0.895f) >= spawnPoints[j].hole.transform.position.x - 0.205f &&
                    top.transform.position.z + (posz - 0.895f) >= spawnPoints[j].hole.transform.position.z - 0.205f &&
                    top.transform.position.z + (posz - 0.895f) <= spawnPoints[j].hole.transform.position.z + 0.205f)
                {
                    spawnPoints[j].spawnCount += (int)((level * 3 + 5) / (spawnPoints[j].color + 1));
                    if (spawnPoints[j].maxWait > spawnPoints[j].minWait + 1)
                        spawnPoints[j].maxWait -= 1;
                    spawnPoints[j].wait = random.Next(spawnPoints[i].maxWait - spawnPoints[i].minWait) + spawnPoints[i].minWait;
                    overlap = true;
                    cHoles -= 1;
                    break;
                }
            }

            //if two holes would overlab squash them together  as one hole
            if (!overlap)
            {
                int color = random.Next(3);
                spawnPoints[i].hole = Instantiate(holes[color], new Vector3(top.transform.position.x + (posx - 0.895f), top.transform.position.y - 0.16f, top.transform.position.z + (posz - 0.895f)), Quaternion.identity);
                spawnPoints[i].color = color;
                spawnPoints[i].spawnCount = (int)((level * 3 + 5) / (color + 1));
                spawnPoints[i].time = Time.time;
                spawnPoints[i].minWait = 2;
                spawnPoints[i].maxWait = 10 + color;
                spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait - spawnPoints[i].minWait) + spawnPoints[i].minWait;
                spawnPoints[i].spawnFalse = random.Next(100) + 1 >= 80;
                Instantiate(holes[color], new Vector3(floor.transform.position.x + (posx - 0.895f), floor.transform.position.y + 0.16f, floor.transform.position.z + (posz - 0.895f)), Quaternion.identity);
            }
        }
    }

    void Update()
    {
        //display score while playing
        if(!waitingForEnd)
            scoreText.text = "Score: " + score;

        bool done = true;

        //check for spawn timers and spawn  balls if  time is up, if all balls spawned game  is done
        if(!waitingForEnd)
            for (int i = 0; i < cHoles; i++)
            {
                if (Time.time >= spawnPoints[i].time + spawnPoints[i].wait)
                {
                    if (spawnPoints[i].spawnCount > 0)
                    {
                        System.Random random = new System.Random();
                        if (spawnPoints[i].spawnFalse)
                        {
                            Instantiate(balls[spawnPoints[i].color + 3], spawnPoints[i].hole.transform.position, Quaternion.identity);
                            spawnPoints[i].time = Time.time;
                            spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait - spawnPoints[i].minWait) + spawnPoints[i].minWait;
                            spawnPoints[i].spawnFalse = random.Next(100) + 1 >= 80;
                            continue;
                        }
                        Instantiate(balls[spawnPoints[i].color], spawnPoints[i].hole.transform.position, Quaternion.identity);
                        spawnPoints[i].time = Time.time;
                        spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait - spawnPoints[i].minWait) + spawnPoints[i].minWait;
                        spawnPoints[i].spawnFalse = random.Next(100) + 1 >= 80;
                        spawnPoints[i].spawnCount--;
                    }
                }
                done = done && (spawnPoints[i].spawnCount == 0);
            }

        if (score < -50)
            done = true;

        //end  game  but  wait for a bit to show the game over/level complete screen and clean up scene
        if(done && !waitingForEnd) {
            waitingForEnd = true;
            waitforEnd = Time.time;
            if(score >= 100 * level) 
                scoreText.text = "Level Complete";
            if(score < 100 * level)
                scoreText.text = "Game Over";
            Destroy(leftController.GetComponent<SphereCollider>());
            Destroy(rightController.GetComponent<SphereCollider>());
        }

        if (done && !isLoading && Time.time > waitforEnd + end)
        {
            isLoading = true; // Verhindere mehrfaches Laden der Szene

            if (score >= 100*level)
            {
                scoreObject.fallingBallsLevel++;
            }

            if (score > scoreObject.fallingBallsHighscore)
            {
                scoreObject.fallingBallsHighscore = score;
            }

            // Starte den asynchronen Szenenwechsel
            StartCoroutine(LoadHubWorldSceneAsync());
        }
    }

    // Coroutine für das asynchrone Laden der HubWorld-Szene
    IEnumerator LoadHubWorldSceneAsync()
    {
        // Lade die Szene asynchron, aber blockiere die Aktivierung, bis sie bereit ist
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HubWorld");
        asyncLoad.allowSceneActivation = false;

        // Warte, bis die Szene zu mindestens 90 % geladen ist
        while (asyncLoad.progress < 0.9f)
        {
            yield return null; // Warte bis zur nächsten Frame
        }

        // Aktiviere die neue Szene
        asyncLoad.allowSceneActivation = true;
    }
}
