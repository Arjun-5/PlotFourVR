using TMPro;
using UnityEngine;

public class TurnDisplayUI : MonoBehaviour
{
    public TurnChangeEvent turnChangeEvent;

    public PlotFourGameConfig config;
    
    public TMP_Text turnText;

    private void OnEnable()
    {
        turnChangeEvent.OnEventTriggered += UpdateTurnText;
    }

    private void OnDisable()
    {
        turnChangeEvent.OnEventTriggered -= UpdateTurnText;
    }

    private void UpdateTurnText(int playerIndex)
    {
        turnText.text = $"{config.playerDefinitions[playerIndex].playerName}'s Turn";
    }
}
