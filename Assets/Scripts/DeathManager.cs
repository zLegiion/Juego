using UnityEngine;
using System;

public class DeathManager : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public static DeathManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerDied()
    {
        OnPlayerDeath?.Invoke();
    }
}
