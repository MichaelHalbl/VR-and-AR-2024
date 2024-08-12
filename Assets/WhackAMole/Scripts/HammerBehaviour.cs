using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : MonoBehaviour
{


    public AudioSource audioSource; // The AudioSource component attached to the hammer
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnCollisionEnter(Collision collision)
    {

       
        if (collision.gameObject.CompareTag("NormalMole") || collision.gameObject.CompareTag("HatMole"))
        {
            MoleBehaviour mole = collision.gameObject.GetComponent<MoleBehaviour>();
            if (mole != null)
            {

                Debug.Log("Mole is hit");
                mole.SwitchCollider(0);
                mole.moleAnimator.SetTrigger("hit");
                mole.GotHit();
                // Play the hit sound
                audioSource.Play();
                // Log the status of the audio
                Debug.Log("Audio is playing: " + audioSource.isPlaying);
            }
        }
    }
}
