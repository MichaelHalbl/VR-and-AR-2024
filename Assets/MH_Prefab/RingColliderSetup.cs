using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColliderSetup : MonoBehaviour
{
    public int segments = 8; // Anzahl der Segmente, die den Ring bilden
    private List<CapsuleCollider> colliders = new List<CapsuleCollider>();
    private List<Vector3> originalLocalPositions = new List<Vector3>();
    private List<Quaternion> originalLocalRotations = new List<Quaternion>();
    private float startAngleOffset = 45.0f; // Winkelversatz von 45 Grad

    void Start()
    {
        // Entferne den bestehenden Collider, falls vorhanden
        Collider existingCollider = GetComponent<Collider>();
        if (existingCollider != null)
        {
            Destroy(existingCollider);
        }

        // Berechne die Größe des Objekts
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("Kein Renderer gefunden! Das Skript benötigt einen Renderer, um die Größe zu berechnen.");
            return;
        }
        Bounds bounds = renderer.bounds;
        float radius = bounds.extents.x; // Halber Durchmesser entlang der x-Achse
        float thickness = bounds.size.z; // Dicke des Rings

        // Berechne den Winkelabstand zwischen den Segmenten
        float angleStep = 360.0f / segments;

        // Erstelle die CapsuleColliders
        for (int i = 0; i < segments; i++)
        {
            // Berechne die Position des Colliders
            float angle = (i * angleStep + startAngleOffset) * Mathf.Deg2Rad;
            Vector3 localPosition = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            originalLocalPositions.Add(localPosition);

            // Berechne die Rotation des Colliders
            Quaternion localRotation = Quaternion.Euler(0, -i * angleStep - startAngleOffset, 0);
            originalLocalRotations.Add(localRotation);

            // Füge den CapsuleCollider hinzu
            CapsuleCollider capsule = gameObject.AddComponent<CapsuleCollider>();
            capsule.radius = thickness / 2;
            capsule.height = thickness;
            capsule.direction = 1; // Y-Achse

            // Positioniere und rotiere den Collider relativ zum Objekt
            capsule.center = Vector3.zero; // Setze das Zentrum des Colliders auf den Ursprung
            capsule.transform.localPosition = localPosition;
            capsule.transform.localRotation = localRotation;

            // Füge den Collider zur Liste hinzu
            colliders.Add(capsule);
        }
    }

    void Update()
    {
        // Synchronisiere die Bewegung und Rotation aller Collider
        for (int i = 0; i < colliders.Count; i++)
        {
            CapsuleCollider capsule = colliders[i];
            Vector3 localPosition = originalLocalPositions[i];
            Quaternion localRotation = originalLocalRotations[i];
            capsule.transform.position = transform.TransformPoint(localPosition);
            capsule.transform.rotation = transform.rotation * localRotation;
        }
    }
}
