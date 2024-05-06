using UnityEngine;
using System.Collections; // Importiere die Namespace für IEnumerator

public class FloatingObject : MonoBehaviour
{
    // Variablen zur Einstellung des Float-Verhaltens
    public float floatingHeight = 25f; // Die Höhe, um die das Objekt schweben soll
    public float upSpeed = 3f; // Die Geschwindigkeit, mit der das Objekt nach oben schwebt
    public float downSpeed = -35f; // Die Geschwindigkeit, mit der das Objekt nach unten fällt
    public float waitTime = 2f; // Die Zeit, die das Objekt oben oder unten wartet
    public float driveTime=9f;
    public float repeatTime = 2f; // Die Zeit, nach der das Schweben wiederholt wird

    private Vector3 originalPosition; // Die ursprüngliche Position des Objekts
    private bool isFloating = false; // Eine Flagge, die anzeigt, ob das Objekt gerade schwebt

    void Start()
    {
        originalPosition = transform.position; // Speichere die ursprüngliche Position des Objekts
        InvokeRepeating("ToggleFloating", 0f, repeatTime); // Starte die Wiederholung des Schwebens
    }

    void ToggleFloating()
    {
        if (!isFloating) // Überprüfe, ob das Objekt bereits schwebt
        {
            StartCoroutine(FloatingUpAndDown()); // Starte das Coroutine-Skript zum Schweben
        }
    }

    IEnumerator FloatingUpAndDown()
    {
        isFloating = true; // Setze die Flagge, dass das Objekt jetzt schwebt
        float elapsedTime = 0f; // Die vergangene Zeit seit Beginn des Schwebens
        Vector3 targetPosition = originalPosition + Vector3.up * floatingHeight; // Die Zielposition für das Schweben nach oben

        while (elapsedTime < driveTime) // Schleife, um das Objekt nach oben zu bewegen und zu warten
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, upSpeed * Time.deltaTime); // Bewege das Objekt nach oben
            elapsedTime += Time.deltaTime; // Aktualisiere die vergangene Zeit
            yield return null; // Warte bis zur nächsten Frame
        }

        yield return new WaitForSeconds(waitTime); // Warte für die eingestellte Zeit oben

        targetPosition = originalPosition; // Setze die Zielposition auf die ursprüngliche Position zurück
        elapsedTime = 0f; // Setze die vergangene Zeit zurück

        while (elapsedTime < driveTime) // Schleife, um das Objekt nach unten zu bewegen und zu warten
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Mathf.Abs(downSpeed) * Time.deltaTime); // Bewege das Objekt nach unten
            elapsedTime += Time.deltaTime; // Aktualisiere die vergangene Zeit
            yield return null; // Warte bis zur nächsten Frame
        }

        transform.position = originalPosition; // Setze die Position des Objekts zurück auf die ursprüngliche Position
        isFloating = false; // Setze die Flagge, dass das Objekt nicht mehr schwebt
    }
}