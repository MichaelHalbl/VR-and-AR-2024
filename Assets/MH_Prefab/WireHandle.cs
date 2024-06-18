using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WireHandle : MonoBehaviour
{
    private Transform playerHand;

    void Start()
    {
        var grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
        grabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;

        gameObject.tag = "WireHandle";
    }

    void Update()
    {
        // Die Position und Rotation des WireHandle-Objekts wird jetzt von XRGrabInteractable gehandhabt.
    }
}
