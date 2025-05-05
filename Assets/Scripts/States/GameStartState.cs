using UnityEngine;

public class GameStartState : IGameState
{
    private GameManager gameManager;

    public GameStartState(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void EnterState()
    {
        gameManager.StartTurn();
    }

    public void ExitState() 
    {

    }
}
