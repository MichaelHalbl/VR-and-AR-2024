using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holebehaviour : MonoBehaviour
{
    public GameObject [] moles;
    private ScoreManager scoreManager;
    private ScoreTextManager scoreTextManager;
    private LevelTextManager levelTextManager;
    private ScoreScript scoreObject;
    private int level;
    private int maxPoints;
    
    void Start()
    {
        scoreObject = GameObject.Find("ScoreObject").GetComponent<ScoreScript>();
        level = scoreObject.MoleLevel;
        maxPoints = level * 150;
        Invoke("Spawn", 2f); 
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        scoreTextManager = GameObject.FindObjectOfType<ScoreTextManager>();
        levelTextManager = GameObject.FindObjectOfType<LevelTextManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void Spawn()
    {
        if (IsGameOver())
        {
            scoreObject.MoleOver = true;
            Debug.Log("Game Over! No more moles will spawn.");
            CancelInvoke("Spawn");  
            return;
        }

        int randomIndex = Random.Range(0, moles.Length);
        GameObject mole = Instantiate(moles[randomIndex], transform.position, Quaternion.identity) as GameObject;
       

        if(scoreManager.getScore() < maxPoints/3 && scoreManager.getScore() >= 0){
            Invoke("Spawn", Random.Range(7f, 9f));
            //levelTextManager.UpdateLevelText(1);
        }

        else if(scoreManager.getScore() < (maxPoints/3)*2){
            Invoke("Spawn", Random.Range(4f, 6f));
            //levelTextManager.UpdateLevelText(2);
        }
        else if (scoreManager.getScore() < maxPoints){
            Invoke("Spawn", Random.Range(3f, 5f));
            //levelTextManager.UpdateLevelText(3);
        }
    }

    public bool IsGameOver()
    {
        // Game over condition: score is negative or exceeds 300
        return scoreObject.redMoles >= 3 || scoreManager.getScore() >= maxPoints;
    }
   
}
