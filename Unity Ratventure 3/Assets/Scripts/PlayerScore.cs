using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public float distanceThreshold = 50f; // Distanz, nach der der Score erhöht wird
    private float distanceTraveled = 0f;  // Zurückgelegte Distanz des Spielers
    private float lastPositionX;          // Letzte X-Position des Spielers
    public MainMenu mainMenu;            // Referenz auf das MainMenu-Script, um den Score zu aktualisieren
    public int currentScore = 0;         // Aktueller Score

    void Start()
    {
        lastPositionX = transform.position.x; // Setze die Startposition
        mainMenu = FindObjectOfType<MainMenu>(); // Finde das MainMenu-Script
    }

    void Update()
    {
        // Berechne die zurückgelegte Strecke seit der letzten Position
        float currentPositionX = transform.position.x;
        distanceTraveled += Mathf.Abs(currentPositionX - lastPositionX); // Differenz zur letzten Position

        // Update die letzte Position des Spielers
        lastPositionX = currentPositionX;

        // Überprüfe, ob der Schwellenwert überschritten wurde
        if (distanceTraveled >= distanceThreshold)
        {
            // Score erhöhen
            currentScore += 10; // Erhöhe den Score um 10 Punkte
            if (mainMenu != null)
            {
                mainMenu.UpdateScore(currentScore);

            }

            // Zurücksetzen der zurückgelegten Strecke
            distanceTraveled = 0f;
        }
    }

   






}
