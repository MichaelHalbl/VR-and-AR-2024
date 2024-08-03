using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{

    public int fallingBallsLevel = 1;
    public int fallingBallsHighscore = 0;

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Score");
        if(objs.Length > 1) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
