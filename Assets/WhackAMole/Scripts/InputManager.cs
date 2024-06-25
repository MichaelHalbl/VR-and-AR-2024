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
    if (mouse != null && mouse.leftButton.wasPressedThisFrame)
    {
        Vector2 mousePosition = mouse.position.ReadValue();
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject != null && 
                    (hit.collider.gameObject.CompareTag("NormalMole") || hit.collider.gameObject.CompareTag("HatMole")))
                {
                    MoleBehaviour mole = hit.collider.gameObject.GetComponent<MoleBehaviour>();
                    if (mole != null)
                    {
                        mole.SwitchCollider(0);
                        mole.moleAnimator.SetTrigger("hit");
                        mole.GotHit();
                    }
                }
            }
        }
    }
}

   
}
