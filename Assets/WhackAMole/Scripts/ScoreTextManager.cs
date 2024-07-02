using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class ScoreTextManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    void Start()
    {
        
    }
   public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
