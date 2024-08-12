using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WireHandle : MonoBehaviour
{
    private CapsuleCollider handleCollider;

    void Start()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
            grabInteractable.movementType = XRBaseInteractable.MovementType.Instantaneous;
        }

        handleCollider = GetComponent<CapsuleCollider>();
        if (handleCollider == null)
        {
            handleCollider = gameObject.AddComponent<CapsuleCollider>();
            handleCollider.isTrigger = true;
        }

        var rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = true;
        }

        gameObject.tag = "WireHandle";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HotWire"))
        {
            var hotWire = other.GetComponentInParent<HotWire>();
            if (hotWire != null)
            {
                hotWire.OnHandleCollisionEnter();
            }
        }
        else if (other.CompareTag("StartPoint"))
        {
            var hotWire = other.GetComponentInParent<HotWire>();
            if (hotWire != null)
            {
                hotWire.OnStartTriggerEnter();
            }
        }
        else if (other.CompareTag("EndPoint"))
        {
            var hotWire = other.GetComponentInParent<HotWire>();
            if (hotWire != null)
            {
                hotWire.OnEndTriggerEnter();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HotWire"))
        {
            var hotWire = other.GetComponentInParent<HotWire>();
            if (hotWire != null)
            {
                hotWire.OnHandleCollisionExit();
            }
        }
    }
}