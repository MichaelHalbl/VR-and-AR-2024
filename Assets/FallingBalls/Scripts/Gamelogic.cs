using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamelogic : MonoBehaviour
{
    public GameObject[] holes;
    public GameObject[] balls;
    public GameObject floor;
    public GameObject top;
    private bool spawnOnce = true;
    public int level;
    private int cHoles;
    public int score;
    public int highscore;
    private SpawnPoint[] spawnPoints;

    struct SpawnPoint {
        public int color;
        public GameObject hole;
        public int spawnCount;
        public float time;
        public float wait;
    }

    // Start is called before the first frame update
    void Start()
    {

        level = 1;
        cHoles = 2 + level;
        score = 0;
        spawnPoints = new SpawnPoint[cHoles];

        System.Random random = new System.Random();
        for(int i = 0; i < cHoles; i++) {
            double posx = random.NextDouble()*3.6;
            double posz = random.NextDouble()*3.6;
            int color = random.Next(3);
            spawnPoints[i].hole = Instantiate(holes[color], new Vector3(top.transform.position.x+((float)posx-1.8f), top.transform.position.y-0.41f, top.transform.position.z+((float)posz-1.8f)), Quaternion.identity);
            spawnPoints[i].color = color;
            spawnPoints[i].spawnCount = level * 10;
            spawnPoints[i].time =  Time.time;
            spawnPoints[i].wait = random.Next(8)+2;
            Instantiate(holes[color], new Vector3(floor.transform.position.x+((float)posx-1.8f), floor.transform.position.y+0.41f, floor.transform.position.z+((float)posz-1.8f)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool done = true;
        
        for(int i = 0; i < cHoles; i++) {
            if(Time.time >= spawnPoints[i].time+spawnPoints[i].wait) {
                if(spawnPoints[i].spawnCount > 0) {
                    Instantiate(balls[spawnPoints[i].color], spawnPoints[i].hole.transform.position, Quaternion.identity);
                    spawnPoints[i].time =  Time.time;
                    System.Random random = new System.Random();
                    spawnPoints[i].wait = random.Next(8)+2;
                    spawnPoints[i].spawnCount--;
                }
            }
            done = done && (spawnPoints[i].spawnCount == 0);
        }

        if(done) {

            //Level done

        }

    }
}
