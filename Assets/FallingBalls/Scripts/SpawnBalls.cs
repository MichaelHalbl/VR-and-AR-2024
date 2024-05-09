using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnBalls : MonoBehaviour
{

    public Transform[] spawns;
    public GameObject[] balls;
    private bool spawnOnce = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnOnce){
            for(int i = 0; i < 3; i++) {
                Instantiate(balls[i], spawns[i].position, Quaternion.identity);
            }
            spawnOnce = false;
        }
        
    }
}
