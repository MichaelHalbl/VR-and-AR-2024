using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamelogic : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {

        level = 1;
        cHoles = 2 + level;
        score = 0;
        spawnPoints = new SpawnPoint[cHoles];
        pointsToWin = 0;

        System.Random random = new System.Random();
        for(int i = 0; i < cHoles; i++) {
            float posx = (float)(random.NextDouble()*3.58);
            float posz = (float)(random.NextDouble()*3.58);
            bool overlap = false;
            for (int j = 0; j < i; j++)
            {
                if (top.transform.position.x + (posx - 1.79f) <= spawnPoints[j].hole.transform.position.x + 0.42f && top.transform.position.x + (posx - 1.79f) >= spawnPoints[j].hole.transform.position.x - 0.42f &&
                top.transform.position.z + (posz - 1.79f) >= spawnPoints[j].hole.transform.position.z - 0.42f && top.transform.position.z + (posz - 1.79f) <= spawnPoints[j].hole.transform.position.z + 0.42f)
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
                spawnPoints[i].hole = Instantiate(holes[color], new Vector3(top.transform.position.x+(posx-1.79f), top.transform.position.y-0.41f, top.transform.position.z+(posz-1.79f)), Quaternion.identity);
                spawnPoints[i].color = color;
                spawnPoints[i].spawnCount = (int)((level * 3 + 5) / (color+1));
                spawnPoints[i].time =  Time.time;
                spawnPoints[i].minWait = 2;
                spawnPoints[i].maxWait = 10+color;
                spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait-spawnPoints[i].minWait)+spawnPoints[i].minWait;
                spawnPoints[i].spawnFalse = random.Next(100) + 1 >= 80;
                Instantiate(holes[color], new Vector3(floor.transform.position.x+(posx-1.79f), floor.transform.position.y+0.41f, floor.transform.position.z+(posz-1.79f)), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
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

            //Level done

        }

    }
}
