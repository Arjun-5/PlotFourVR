using UnityEngine;

public class GameOverState : IGameState
{
    private GameManager gameManager;

    private int winnerIndex;

    public GameOverState(GameManager manager) => gameManager = manager;

    public void EnterState()
    {
        Debug.Log("Game Over State");

        gameManager.OnGameOver();
    }

    public void ExitState() { }
}