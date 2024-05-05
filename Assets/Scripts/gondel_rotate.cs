using UnityEngine;

public class CabinController : MonoBehaviour
{
    public float tiltSpeed = 100f; // Ändern Sie den Wert, um die Neigunggeschwindigkeit anzupassen.
    public float rotationSpeed = 60f; // Legt fest, wie schnell das Rad sich drehen soll (in Umdrehungen pro Minute).
    public int currentRotationCount = 0;
    public float rotationPauseDuration = 10.0f; // Dauer des automatischen Pauses zwischen den Rotationen (in Sekunden)
    public float currentRotationTimer = 0.0f;
    
    void Update()
    {
        tiltSpeed=15;
        currentRotationTimer+=Time.deltaTime;
        
        if(currentRotationTimer > 5 ){
            tiltSpeed=0;
            currentRotationCount++;
          if(rotationPauseDuration<currentRotationTimer){
            currentRotationTimer=0;
          }
        }
        transform.Rotate(Vector3.forward * Time.deltaTime * tiltSpeed);
    }
}
