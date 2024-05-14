using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MH_HotWire : MonoBehaviour
{
    public Transform vrHand; // Referenz auf die Hand des Spielers in VR
    public Transform staticObject; // Referenz auf das statische Objekt in der Spielwelt (HotWire)
    public Transform interactableObject; // Referenz auf das interagierbare Objekt in der Spielwelt (HotWireStaff)
    public AudioClip collisionSound; // Audio-Clip für den Kollisionston

    private bool isColliding = false; // Flag, um zu überprüfen, ob die Objekte kollidieren

    void Update()
    {
        // Überprüfe, ob die Objekte kollidieren
        if (isColliding)
        {
            // Spiele den Kollisionston ab
            if (collisionSound != null)
            {
                AudioSource.PlayClipAtPoint(collisionSound, transform.position);
            }

            // Gib eine Meldung in der Konsole aus
            Debug.Log("Die beiden Objekte kollidieren!");
        }
    }

    // Wenn das interagierbare Objekt das statische Objekt berührt
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == staticObject.gameObject && gameObject == interactableObject.gameObject)
        {
            isColliding = true;
        }
    }

    // Wenn das interagierbare Objekt das statische Objekt nicht mehr berührt
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == staticObject.gameObject && gameObject == interactableObject.gameObject)
        {
            isColliding = false;
        }
    }

    // Bewegt das interagierbare Objekt entsprechend der Handbewegung des Spielers in VR
    void FixedUpdate()
    {
        if (gameObject == interactableObject.gameObject)
        {
            transform.position = vrHand.position;
        }
    }
}
