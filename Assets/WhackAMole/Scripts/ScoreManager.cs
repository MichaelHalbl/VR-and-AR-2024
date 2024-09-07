using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }

    public int getScore()
    {
        return score;
    }
}
