using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holebehaviour : MonoBehaviour
{
    public GameObject [] moles;
     private ScoreManager scoreManager;
     private ScoreTextManager scoreTextManager;
     private LevelTextManager levelTextManager;
    void Start()
    {
        Invoke("Spawn", 2f); 
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        scoreTextManager = GameObject.FindObjectOfType<ScoreTextManager>();
        levelTextManager = GameObject.FindObjectOfType<LevelTextManager>();
    }

    // Update is called once per frame
    void Update()
    {
       /* if(scoreManager.getScore() < 100){
            levelTextManager.UpdateLevelText(1);
        }

        else if(scoreManager.getScore() < 200){
            levelTextManager.UpdateLevelText(2);
        }
        else {
            levelTextManager.UpdateLevelText(3);
        }
        */
       
    }

    void Spawn()
    {
        int randomIndex = Random.Range(0, moles.Length);
        GameObject mole = Instantiate(moles[randomIndex], transform.position, Quaternion.identity) as GameObject;
       

        if(scoreManager.getScore() < 100){
            Invoke("Spawn", Random.Range(8f, 10f));
            //levelTextManager.UpdateLevelText(1);
        }

        else if(scoreManager.getScore() < 200){
            Invoke("Spawn", Random.Range(5f, 7f));
            //levelTextManager.UpdateLevelText(2);
        }
        else {
            Invoke("Spawn", Random.Range(3f, 5f));
            //levelTextManager.UpdateLevelText(3);
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Hole was clicked");
    }

    void buildLevel()
    {

    }
}
