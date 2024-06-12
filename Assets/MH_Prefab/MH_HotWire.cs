using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class MH_HotWire : MonoBehaviour
{
    public XRGrabInteractable interactableObject; // XRGrabInteractable für das HotWireStaff
    public Transform staticObject; // Referenz auf das statische Objekt in der Spielwelt (HotWire)
    public AudioClip collisionSound; // Audio-Clip für den Kollisionston

    private bool isColliding = false; // Flag, um zu überprüfen, ob die Objekte kollidieren
    private Rigidbody rb;
    private AudioSource audioSource;
    private CapsuleCollider[] interactableColliders;
    private Collider staticCollider;

    private void Start()
    {
        // Sicherstellen, dass das interagierbare Objekt nicht null ist
        if (interactableObject == null)
        {
            Debug.LogError("interactableObject ist null!");
            return;
        }

        // Name des interagierbaren Objekts ausgeben
        Debug.Log("Name des interagierbaren Objekts: " + interactableObject.name);

        // Sicherstellen, dass das interagierbare Objekt einen Rigidbody hat
        rb = interactableObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = interactableObject.gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = true; // Schwerkraft aktivieren
        rb.isKinematic = false; // Setze den Rigidbody auf nicht-kinematisch

        // Sicherstellen, dass das interagierbare Objekt CapsuleColliders hat
        /*interactableColliders = interactableObject.GetComponents<CapsuleCollider>();
        if (interactableColliders.Length == 0)
        {
            Debug.LogError("Interagierbares Objekt hat keine CapsuleCollider!");
            return;
        }

        Debug.Log("Anzahl der gefundenen CapsuleCollider: " + interactableColliders.Length);
        foreach (var collider in interactableColliders)
        {
            Debug.Log("Gefundener CapsuleCollider: " + collider.name + " auf Objekt: " + collider.gameObject.name);
        }*/

        // Sicherstellen, dass das statische Objekt einen Collider hat, der kein Trigger ist
        staticCollider = staticObject.GetComponent<Collider>();
        if (staticCollider == null)
        {
            Debug.LogError("Statisches Objekt hat keinen Collider!");
            return;
        }
        staticCollider.isTrigger = true; // Muss ein Trigger sein, damit die OnTrigger-Ereignisse ausgelöst werden

        // AudioSource hinzufügen, wenn nicht vorhanden
        audioSource = gameObject.AddComponent<AudioSource>();

        // Events abonnieren
        interactableObject.selectEntered.AddListener(OnSelectEnter);
        interactableObject.selectExited.AddListener(OnSelectExit);
    }

    private void OnDestroy()
    {
        // Events abmelden
        interactableObject.selectEntered.RemoveListener(OnSelectEnter);
        interactableObject.selectExited.RemoveListener(OnSelectExit);
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        // Wenn das Objekt aufgenommen wird, mache den Rigidbody kinematisch und deaktiviere die Schwerkraft
        rb.isKinematic = true;
        rb.useGravity = false;
        Debug.Log("HotWireStaff wurde aufgenommen.");
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        // Wenn das Objekt losgelassen wird, mache den Rigidbody nicht mehr kinematisch und aktiviere die Schwerkraft
        rb.isKinematic = false;
        rb.useGravity = true;
        Debug.Log("HotWireStaff wurde losgelassen.");
    }

    void Update()
    {
        // Überprüfe, ob die Objekte kollidieren
        if (isColliding)
        {
            // Spiele den Kollisionston ab, falls nicht bereits abgespielt
            if (collisionSound != null && !audioSource.isPlaying)
            {
                audioSource.PlayOneShot(collisionSound);
            }

            // Gib eine Meldung in der Konsole aus
            Debug.Log("Die beiden Objekte kollidieren!");
        }
    }

    // Wenn das interagierbare Objekt das statische Objekt berührt
    void OnTriggerEnter(Collider other)
    {
        if (other == staticCollider)
        {
            isColliding = true;
            Debug.Log("HotWireStaff hat HotWire berührt.");
        }
    }

    // Wenn das interagierbare Objekt das statische Objekt nicht mehr berührt
    void OnTriggerExit(Collider other)
    {
        if (other == staticCollider)
        {
            isColliding = false;
            Debug.Log("HotWireStaff hat HotWire nicht mehr berührt.");
        }
    }
}
