using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class StartCan : MonoBehaviour
{
    private InputDevice rightHandController; // Referenz auf den rechten Controller
    private bool isInRange = false; // Ob der Spieler in Reichweite des Ticket-Standes ist
    private bool isLoading = false; // Um zu verhindern, dass der Ladeprozess mehrfach gestartet wird

    void Start()
    {
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
        if (isInRange && !isLoading && rightHandController.IsPressed(InputHelpers.Button.PrimaryButton, out bool isPressedA, 0.1f) && isPressedA)
        {
            // Starte den Ladeprozess nur einmal
            isLoading = true;
            StartCoroutine(LoadSceneAsync("CanGame"));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true; // Spieler ist in Reichweite
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false; // Spieler ist nicht mehr in Reichweite
        }
    }

    // Coroutine für das asynchrone Laden der Szene
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Beginne das asynchrone Laden der Szene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;  // Verhindere die sofortige Aktivierung der neuen Szene

        // Warte, bis die Szene zu 90 % geladen ist
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;  // Warte, bis die nächste Frame berechnet wird
        }

        // Wenn die Szene zu 90 % geladen ist, aktiviere sie
        asyncLoad.allowSceneActivation = true;
    }
}
