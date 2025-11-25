// GameEvents.cs
using System;

public static class GameEvents
{
    
    public static event Action OnGameReset;
    public static void TriggerGameReset()
    {
        OnGameReset?.Invoke();
    }
    public static event Action<int> OnScoreUpdated;
    public static void TriggerScoreUpdated(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }

    public static event Action OnPlayerDied;

    public static void TriggerPlayerDied()
    {
        OnPlayerDied?.Invoke();
    }
}