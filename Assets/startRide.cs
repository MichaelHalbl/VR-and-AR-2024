using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RideController : MonoBehaviour
{
    public Transform gondolaPosition; // Position der Gondel, wo die Kamera platziert wird
    public Transform playerStartPosition; // Ausgangsposition des Spielers

    public Transform xrOrigin;
    private bool isRiding = false; // Ob der Spieler in der Gondel sitzt
    private bool isInRange = false; // Ob der Spieler in Reichweite des Ticket-Standes ist
    private GameObject playerCamera; // Die Kamera des Spielers
    private InputDevice rightHandController; // Referenz auf den rechten Controller

    void Start()
    {
        // Find the player's VR camera
        playerCamera = Camera.main.gameObject;

        // Suche den rechten Hand-Controller
        var rightHandDevices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);

        if (rightHandDevices.Count > 0)
        {
            rightHandController = rightHandDevices[0]; // Nimm das erste gefundene Gerät
        }
    }

    void Update()
    {
        // Nur wenn der Spieler in Reichweite ist, darf er die A-Taste drücken
        if (isInRange && rightHandController.IsPressed(InputHelpers.Button.PrimaryButton, out bool isPressedA, 0.1f) && isPressedA)
        {
            if (!isRiding)
            {
                StartRide();
            }
        }

        // Überprüfen, ob der Spieler die B-Taste drückt, um die Fahrt abzubrechen
        if (isRiding && rightHandController.IsPressed(InputHelpers.Button.SecondaryButton, out bool isPressedB, 0.1f) && isPressedB)
        {
            StopRide();
        }
    }

    // Spieler betritt den Triggerbereich (in der Nähe des Ticket-Standes)
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
              Debug.Log("Player ist in Reichweite des Ticket-Standes!");
            isInRange = true; // Spieler ist in Reichweite
        }
    }

    // Spieler verlässt den Triggerbereich
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false; // Spieler ist nicht mehr in Reichweite
        }
    }

    void StartRide()
    {
        // Kamera an die Gondel-Position setzen und fixieren
        playerCamera.transform.position = gondolaPosition.position;
        playerCamera.transform.rotation = gondolaPosition.rotation;
        playerCamera.transform.parent = gondolaPosition; // Kamera an Gondel "anheften"

        isRiding = true;
    }

    void StopRide()
    {
        // Kamera wieder an die Ausgangsposition setzen und freigeben
        playerCamera.transform.parent = xrOrigin;
        playerCamera.transform.position = playerStartPosition.position;
        playerCamera.transform.rotation = playerStartPosition.rotation;

        isRiding = false;
    }
}
