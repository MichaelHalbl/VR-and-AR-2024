using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabDisableCollider : MonoBehaviour
{
    private Collider objectCollider;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        // Hole den Collider und das XRGrabInteractable-Objekt
        objectCollider = GetComponent<Collider>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        // Events abonnieren (Verwendung von selectEntered und selectExited)
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    // Wird aufgerufen, wenn das Objekt gegriffen wird
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        if (objectCollider != null)
        {
            objectCollider.enabled = false;  // Collider deaktivieren
        }
    }

    // Wird aufgerufen, wenn das Objekt losgelassen wird
    private void OnReleased(SelectExitEventArgs args)
    {
        // Prüfen, ob das Loslassen nicht abgebrochen wurde
        if (!args.isCanceled && objectCollider != null)
        {
            objectCollider.enabled = true;  // Collider wieder aktivieren
        }
    }

    // Entferne die Listener, um Speicherlecks zu vermeiden
    void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }
}
