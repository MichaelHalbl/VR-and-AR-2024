using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ScoreTextManager : MonoBehaviour
{
    public TextMeshPro scoreText;
    void Start()
    {
       // scoreText.text = "Score: 0";
    }
   public void UpdateScoreText(int score)
    {
        if(score < -1){
            scoreText.text = "Score: " + score + "\nGameOver";
        }
        else if(score < 100){
            scoreText.text = "Score: " + score + "\nLevel: 1";
        }

        else if(score < 200){
           scoreText.text = "Score: " + score + "\nLevel: 2";
        }
        else if (score < 300){
            scoreText.text = "Score: " + score + "\nLevel: 3";
        } else {
            scoreText.text = "Score: " + score + "\nYOU WON";
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
