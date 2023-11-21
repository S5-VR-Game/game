using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // Die Geschwindigkeit, mit der sich das Objekt in der x-Richtung bewegt
    public float movement_speed = 10.0f;
    public float x_direction;
    public float y_direction;
    public float z_direction;

    void Update()
    {
        // Berechnung der Bewegungsrichtung
        Vector3 movement_direction = new Vector3(x_direction, y_direction, z_direction);

        // Bewege das Objekt basierend auf der Bewegungsrichtung und der Geschwindigkeit
        transform.Translate(movement_direction * movement_speed * Time.deltaTime);
    }
}