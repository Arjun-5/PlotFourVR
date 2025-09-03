using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public PlotFourGameConfig gameConfig;

    [SerializeField] TurnChangeEvent turnChangeEvent;

    [SerializeField] GameStateMachine stateMachine;

    [SerializeField] PlotFourGameBoard plotFourGameBoard;

    [Header("Player Information")]
    [SerializeField] TMP_Text playerNameText;

    [SerializeField] Image playerColor;

    [Header("Game Status")]
    [SerializeField] TMP_Text gameStatusText;

    private int winnerIndex = -1;

    private void Start()
    {
        plotFourGameBoard.Initialize(this);

        stateMachine = new GameStateMachine();

        stateMachine.ChangeState(new GameStartState(this));
    }

    public void StartTurn()
    {
        gameStatusText.text = "The Game has Begun!!!";

        turnChangeEvent.OnEventTriggered += OnTurnChanged;

        SetPlayerTurnState();
    }
    public void OnTurnChanged(int playerIndex)
    {
        SetPlayerTurnState();
    }
    public void OnPlayerTurn()
    {
        var player = gameConfig.playerDefinitions[plotFourGameBoard != null ? plotFourGameBoard.GetCurrentPlayerIndex() : 0];

        Debug.Log($"Player {player.playerName} Turn");

        playerNameText.text = player.playerName;

        playerColor.color = player.discColor;
    }

    public void SetPlayerTurnState()
    {
        stateMachine.ChangeState(new PlayerTurnState(this));
    }

    public void SetGameOverState(int winnerPlayerIndex)
    {
        winnerIndex = winnerPlayerIndex;

        stateMachine.ChangeState(new GameOverState(this));
    }

    public void OnGameOver()
    {
        Debug.Log("Game Over!");

        if (winnerIndex >= 0)
        {
            var player = gameConfig.playerDefinitions[winnerIndex];
            
            gameStatusText.text = $"{player.playerName} wins!";

            Debug.Log($"{player.playerName} is the winner!");
        }
        else
        {
            gameStatusText.text = "It's a draw!";

            Debug.Log("No winner. It's a draw.");
        }
    }
}
