using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{
   

    // Update is called once per frame
   void Update()
{
    Mouse mouse = Mouse.current;
    if (mouse.leftButton.wasPressedThisFrame)
    {
        Vector2 mousePosition = mouse.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.CompareTag("NormalMole") 
            || hit.collider.gameObject.CompareTag("HatMole"))
            {
                MoleBehaviour mole = hit.collider.gameObject.GetComponent<MoleBehaviour>();
                mole.SwitchCollider(0);
                mole.moleAnimator.SetTrigger("hit");
               // StartCoroutine(mole.DestroyAfterSeconds(hit.collider.gameObject, 0.5f));
                mole.GotHit();
            }
        }
    }
}

   
}
