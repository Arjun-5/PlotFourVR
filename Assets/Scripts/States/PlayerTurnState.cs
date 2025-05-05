using UnityEngine;

public class PlayerTurnState : IGameState
{
    private GameManager gameManager;

    public PlayerTurnState(GameManager manager) => gameManager = manager;

    public void EnterState()
    {
        Debug.Log("Player Turn State");

        gameManager.OnPlayerTurn();
    }

    public void ExitState() { }
}
