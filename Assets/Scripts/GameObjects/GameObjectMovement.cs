using System.Collections.Generic;
using UnityEngine;

public class GameObjectMovement : MonoBehaviour
{
    public List<Vector3> routePoints; // Die Routenpunkte als Vector3
    public float speed = 5f; // Die Geschwindigkeit des GameObjects
    public bool doCycle = true; // Steuert, ob ein Zyklus durchgeführt werden soll

    private int _currentPoint = 0;
    private Vector3 _startPosition; // Der Startpunkt des GameObjects

    private void Start()
    {
        if (routePoints.Count > 0)
        {
            _startPosition = routePoints[0];
        }
        else
        {
            Debug.LogWarning("No route points specified. Please add route points.");
        }
    }

    void Update()
    {
        // Bewege das GameObject zum nächsten Routenpunkt
        transform.position = Vector3.MoveTowards(transform.position, routePoints[_currentPoint], speed * Time.deltaTime);

        // Wenn das GameObject den aktuellen Routenpunkt erreicht hat, gehe zum nächsten
        if (Vector3.Distance(transform.position, routePoints[_currentPoint]) < 0.1f)
        {
            // Überprüfe, ob das letzte Routenpunkt erreicht wurde und doCycle true ist,
            // wenn ja, setze den aktuellen Punkt auf den Anfangspunkt zurück
            if (_currentPoint == routePoints.Count - 1 && doCycle)
            {
                _currentPoint = 0;
            }
            else
            {
                // Ansonsten erhöhe den aktuellen Punkt
                _currentPoint++;

                // Wenn der Zyklus deaktiviert ist, stelle sicher, dass currentPoint nicht über die Länge der Routenpunkte geht
                if (!doCycle && _currentPoint >= routePoints.Count)
                {
                    _currentPoint = routePoints.Count - 1;
                }
            }
        }
    }
}