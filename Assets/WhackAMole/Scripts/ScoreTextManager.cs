using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ScoreTextManager : MonoBehaviour
{
    public TextMeshPro scoreText;
    private ScoreScript scoreObject;
    private int level;
    private int maxPoints;
    void Start()
    {
        scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreScript>();
        level = scoreObject.MoleLevel;
        maxPoints = level * 150;
    }
   public void UpdateScoreText(int score)
    {
        if(score < -1){
            scoreText.text = "Score: " + score + "\nGameOver";
        }
        else if(score < maxPoints/3){
            scoreText.text = "Score: " + score;
        }
        else if(score < (maxPoints/3)*2){
           scoreText.text = "Score: " + score;
        }
        else if (score < maxPoints){
            scoreText.text = "Score: " + score;
        } else {
            scoreText.text = "Score: " + score + "\nYOU WON";
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
