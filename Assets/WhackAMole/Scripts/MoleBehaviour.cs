using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleBehaviour : MonoBehaviour
{

   Collider moleCollider;
    [HideInInspector]public Animator moleAnimator;
    public int hitPoints = 1;
    public int molePoints = 1;
     private ScoreManager scoreManager;
     private ScoreTextManager scoreTextManager;
     private AudioSource audioSource;

    void Start()
    { 
       
        moleCollider = GetComponent<Collider>();
        moleCollider.enabled = false;

        moleAnimator = GetComponent<Animator>();

         scoreManager = GameObject.FindObjectOfType<ScoreManager>();
         scoreTextManager = GameObject.FindObjectOfType<ScoreTextManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager instance not found in the scene.");
        }
        if (scoreTextManager == null)
        {
            Debug.LogError("ScoreTextManager instance not found in the scene.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource component found on this GameObject.");
        }
    }

    void Update()
    {
        
    }

    public void SwitchCollider(int state)
    {
        if (moleCollider == null)
    {
        moleCollider = GetComponent<Collider>();
        moleCollider.enabled = false;
    } else
     {
      if (state == 0 && hitPoints == 0)
        {
            moleCollider.enabled = false;
        }
        else
        {
            moleCollider.enabled = true;
        }
    }
        
    }

    public void GotHit()
    {
        hitPoints--;

        if (hitPoints <= 0)
        {
            
            audioSource.Play();
    
            moleCollider.enabled = false;
            StartCoroutine(DestroyAfterSeconds(gameObject, 0.2f));
            scoreManager.changeScore(molePoints);
            scoreTextManager.UpdateScoreText(scoreManager.getScore());


            //Debug.Log("Mole was hit, score: " + scoreManager.getScore());
        }
        else {
            moleCollider.enabled = true;
        }
    }

   public IEnumerator DestroyAfterSeconds(GameObject gameObjectToDestroy, float delay)
{
    yield return new WaitForSeconds(delay);
    Destroy(gameObjectToDestroy);
}

// Do not delete this method because it is used in the comeDown animation event
public void DestroyThisMole()
{
    Destroy(gameObject);
}

}
