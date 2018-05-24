using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Game _game;

    // 1. Kabe 
    // 2. ..
    public BuildingManager BuildingManager;


    private void Start()
    {
        _game = UserManager.Instance.Game;

        BuildingManager.LockingAdjustments(_game.Achievements);
        
    }
}