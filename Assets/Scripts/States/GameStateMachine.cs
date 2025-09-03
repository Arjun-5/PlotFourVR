using UnityEngine;

public class GameStateMachine
{
    private IGameState currentState;

    public void ChangeState(IGameState newState)
    {
        currentState?.ExitState();

        currentState = newState;

        currentState?.EnterState();
    }
}
