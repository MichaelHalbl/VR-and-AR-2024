using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermaSpawn : MonoBehaviour
{
    public GameObject[] holes;
    public GameObject[] balls;
    public GameObject floor;
    public GameObject top;
    private int cHoles;
    private SpawnPoint[] spawnPoints;

    struct SpawnPoint {
        public int color;
        public GameObject hole;
        public float time;
        public float wait;
        public int minWait;
        public int maxWait;
    }

    // Start is called before the first frame update
    void Start()
    {
        cHoles = 5;
        spawnPoints = new SpawnPoint[cHoles];

        System.Random random = new System.Random();

        spawnPoints[0].color = 2;
        spawnPoints[1].color = 0;
        spawnPoints[2].color = 2;
        spawnPoints[3].color = 0;
        spawnPoints[4].color = 1;

        for(int i = 0; i < 5; i++) {
            spawnPoints[i].hole = holes[i];
            spawnPoints[i].time =  Time.time; 
            spawnPoints[i].minWait = 2;
            spawnPoints[i].maxWait = 5;
            spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait-spawnPoints[i].minWait)+spawnPoints[i].minWait;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        bool done = true;
        
        for(int i = 0; i < cHoles; i++) {
            if(Time.time >= spawnPoints[i].time+spawnPoints[i].wait) {
                Instantiate(balls[spawnPoints[i].color], spawnPoints[i].hole.transform.position, Quaternion.identity);
                spawnPoints[i].time =  Time.time;
                System.Random random = new System.Random();
                spawnPoints[i].wait = random.Next(spawnPoints[i].maxWait-spawnPoints[i].minWait)+spawnPoints[i].minWait;
            }
        }

    }
}
