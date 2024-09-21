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
       

        if(scoreManager.getScore() < 100 && scoreManager.getScore() >= 0){
            Invoke("Spawn", Random.Range(7f, 9f));
            //levelTextManager.UpdateLevelText(1);
        }

        else if(scoreManager.getScore() < 200){
            Invoke("Spawn", Random.Range(4f, 6f));
            //levelTextManager.UpdateLevelText(2);
        }
        else if (scoreManager.getScore() < 300){
            Invoke("Spawn", Random.Range(3f, 5f));
            //levelTextManager.UpdateLevelText(3);
        } else if (scoreManager.getScore() <= -1 && scoreManager.getScore() >= 300) {
            CancelInvoke("Spawn");
            StopScene();
        }
    }
    public void StopSpawning()
    {
        CancelInvoke("Spawn");
    }

    public void StopScene()
    {
        // Load a different scene, e.g., GameOver scene
       // SceneManager.LoadScene("GameOver");
       Time.timeScale = 0;
    }


    void OnMouseDown()
    {
        Debug.Log("Hole was clicked");
    }

    void buildLevel()
    {

    }
}
