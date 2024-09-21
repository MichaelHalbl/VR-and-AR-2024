using System.Collections;
using System.Collections.Generic;
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

    struct SpawnPoint {
        public int color;
        public GameObject hole;
        public int spawnCount;
        public float time;
        public float wait;
        public int minWait;
        public int maxWait;
        public bool spawnFalse;
    }

    void Awake() {
        player.transform.position = spawnPoint.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreScript>();
        level = scoreObject.fallingBallsLevel;
        cHoles = 2 + level;
        score = 0;
        spawnPoints = new SpawnPoint[cHoles];
        pointsToWin = 0;

        System.Random random = new System.Random();
        for(int i = 0; i < cHoles; i++) {
            float posx = (float)(random.NextDouble()*1.79);
            float posz = (float)(random.NextDouble()*1.79);
            bool overlap = false;
            for (int j = 0; j < i; j++)
            {
                if (top.transform.position.x + (posx - 0.895f) <= spawnPoints[j].hole.transform.position.x + 0.205f && top.transform.position.x + (posx - 0.895f) >= spawnPoints[j].hole.transform.position.x - 0.205f &&
                top.transform.position.z + (posz - 0.895f) >= spawnPoints[j].hole.transform.position.z - 0.205f && top.transform.position.z + (posz - 0.895f) <= spawnPoints[j].hole.transform.position.z + 0.205f)
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

            if(!overlap){
                int color = random.Next(3);
                spawnPoints[i].hole = Instantiate(holes[color], new Vector3(top.transform.position.x+(posx-0.895f), top.transform.position.y-0.16f, top.transform.position.z+(posz-0.895f)), Quaternion.identity);
                spawnPoints[i].color = color;
                spawnPoints[i].spawnCount = (int)((level * 3 + 5) / (color+1));
                spawnPoints[i].time =  Time.time;
                spawnPoints[i].minWait = 2;
                spawnPoints[i].maxWait = 10+color;
                spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait-spawnPoints[i].minWait)+spawnPoints[i].minWait;
                spawnPoints[i].spawnFalse = random.Next(100) + 1 >= 80;
                Instantiate(holes[color], new Vector3(floor.transform.position.x+(posx-0.895f), floor.transform.position.y+0.16f, floor.transform.position.z+(posz-0.895f)), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = "Score: " + score;

        bool done = true;
        
        for(int i = 0; i < cHoles; i++) {
            if(Time.time >= spawnPoints[i].time+spawnPoints[i].wait) {
                if(spawnPoints[i].spawnCount > 0) {
                    System.Random random = new System.Random();
                    if (spawnPoints[i].spawnFalse) 
                    {
                        Instantiate(balls[spawnPoints[i].color+3], spawnPoints[i].hole.transform.position, Quaternion.identity);
                        spawnPoints[i].time = Time.time;
                        spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait - spawnPoints[i].minWait) + spawnPoints[i].minWait;
                        spawnPoints[i].spawnFalse = random.Next(100) + 1 >= 80;
                        continue;
                    }
                    Instantiate(balls[spawnPoints[i].color], spawnPoints[i].hole.transform.position, Quaternion.identity);
                    spawnPoints[i].time =  Time.time;
                    spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait-spawnPoints[i].minWait)+spawnPoints[i].minWait;
                    spawnPoints[i].spawnFalse = random.Next(100) + 1 >= 80;
                    spawnPoints[i].spawnCount--;
                }
            }
            done = done && (spawnPoints[i].spawnCount == 0);
        }

        if (score < 0)
            done = true;

        if(done) {
            if(score > 0) {
                scoreObject.fallingBallsLevel++;
            }

            if(score > scoreObject.fallingBallsHighscore)
                scoreObject.fallingBallsHighscore = score;
            var op =  SceneManager.LoadSceneAsync("HubWorld");
            op.allowSceneActivation = false;
            while(op.progress < 0.9f) {
                
            }
            op.allowSceneActivation = true;
        }

    }
}
