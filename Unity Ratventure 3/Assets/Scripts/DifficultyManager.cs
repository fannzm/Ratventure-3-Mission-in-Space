using System;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public enum Difficulty { Easy, Normal, Hard }
    public Difficulty currentDifficulty = Difficulty.Normal;

    public static event Action<Difficulty> OnDifficultyChanged;


    [Header("Enemy Settings")]
    public float projectileSpeedMultiplierEasy = 0.8f;
    public float projectileSpeedMultiplierNormal = 1f;
    public float projectileSpeedMultiplierHard = 1.2f;

    public int enemyDamageMultiplierEasy = 1;
    public int enemyDamageMultiplierNormal = 2;
    public int enemyDamageMultiplierHard = 3;

    // Methode, um den Schwierigkeitsgrad auf Easy zu setzen
    public void SetEasyDifficulty()
    {
        currentDifficulty = Difficulty.Easy;
        Debug.Log("Schwierigkeitsgrad auf Easy gesetzt.");
        OnDifficultyChanged?.Invoke(currentDifficulty);
    }

    // Methode, um den Schwierigkeitsgrad auf Normal zu setzen
    public void SetNormalDifficulty()
    {
        currentDifficulty = Difficulty.Normal;
        Debug.Log("Schwierigkeitsgrad auf Normal gesetzt.");
        OnDifficultyChanged?.Invoke(currentDifficulty);
    }

    // Methode, um den Schwierigkeitsgrad auf Hard zu setzen
    public void SetHardDifficulty()
    {
        currentDifficulty = Difficulty.Hard;
        Debug.Log("Schwierigkeitsgrad auf Hard gesetzt.");
        OnDifficultyChanged?.Invoke(currentDifficulty);
    }

    public Difficulty GetCurrentDifficulty()
    {
        return currentDifficulty;
    }
}
