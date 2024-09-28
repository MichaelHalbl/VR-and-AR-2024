using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class ExitHubWorld : MonoBehaviour
{
    private InputDevice rightHandController; // Referenz auf den rechten Controller
    private bool isInRange = false; // Ob der Spieler in Reichweite des Ticket-Standes ist
    private bool isLoading = false; // Sicherstellen, dass die Szene nur einmal geladen wird

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        // Überprüfe, ob der Spieler in Reichweite ist und die A-Taste drückt, und ob die Szene nicht bereits geladen wird
        if (isInRange && !isLoading && rightHandController.IsPressed(InputHelpers.Button.SecondaryButton, out bool isPressedB, 0.1f) && isPressedB)
        {
            isLoading = true; // Verhindere, dass der Ladeprozess mehrfach gestartet wird
            StartCoroutine(LoadSceneAsync("StartWorld"));
        }
    }

    // Spieler betritt den Triggerbereich
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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

    // Coroutine für das asynchrone Laden der Szene
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Beginne das asynchrone Laden der Szene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;  // Verhindere die sofortige Aktivierung der neuen Szene

        // Warte, bis die Szene zu mindestens 90 % geladen ist
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;  // Warte bis zur nächsten Frame
        }

        // Wenn die Szene zu 90 % geladen ist, aktiviere sie
        asyncLoad.allowSceneActivation = true;
    }
}
