using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : MonoBehaviour
{


    

    // Start is called before the first frame update
    void Start()
    {
       

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
               
            }
        }
    }
}
