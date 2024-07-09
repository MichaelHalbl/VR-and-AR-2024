using UnityEngine;
public class Spawner : MonoBehaviour
{
    // Prefabs f�r die Dosen, die B�lle und die Tische
    public GameObject canPrefab;
    public GameObject ballPrefab;
    public GameObject tableBallsPrefab;
    public GameObject tableCanPrefab;

    // Positionen f�r die Tische
    public Transform tableBallsPosition;
    public Transform tableCanPosition;

    // Anzahl der Dosen und B�lle
    public int canCount = 10;
    public int ballCount = 5;
    public int distancetable = 5;

    void Start()
    {
        // Tische spawnen
        GameObject tableBalls = Instantiate(tableBallsPrefab, tableBallsPosition.position, Quaternion.identity);
        GameObject tableCan = Instantiate(tableCanPrefab, tableCanPosition.position, Quaternion.identity);

        // Dosen in Pyramidenform auf dem Tisch spawnen
        Vector3 startPosition = tableCan.transform.position + Vector3.up * 1.0f; // H�he des Tisches ber�cksichtigen
        float canSpacingX = 0.15f; // Horizontaler Abstand zwischen den Dosen
        float canSpacingY = 0.2f; // Vertikaler Abstand zwischen den Reihen der Dosen

        int currentCanCount = 0;
        int rows = 1;

        // Berechnung der maximalen Anzahl Dosen in der Basisreihe
        while (rows * (rows + 1) / 2 < canCount)
        {
            rows++;
        }
        for (int row = 0; row < rows; row++)
        {
            int cansPerRow = rows - row;
            for (int col = 0; col < cansPerRow; col++)
            {
                if (currentCanCount >= canCount) break;

                Vector3 position = startPosition + new Vector3(col * canSpacingX - (cansPerRow - 1) * 0.5f * canSpacingX, row * canSpacingY, 0);
                Instantiate(canPrefab, position, Quaternion.identity);
                currentCanCount++;
            }
            if (currentCanCount >= canCount) break;
        }
        // B�lle nebeneinander auf dem Tisch spawnen
        Vector3 ballStartPosition = tableBalls.transform.position + Vector3.up * 1.0f; // H�he des Tisches ber�cksichtigen
        float ballSpacing = 0.2f; // Abstand zwischen den B�llen
        for (int i = 0; i < ballCount; i++)
        {
            Vector3 position = ballStartPosition + new Vector3(-0.5f + (i * ballSpacing), -0.4f, 0);
            Instantiate(ballPrefab, position, Quaternion.identity);
        }
    }
}

