using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Plot Four/Event/Turn Change")]
public class TurnChangeEvent : ScriptableObject
{
    public Action<int> OnEventTriggered;

    public void Trigger(int playerIndex) => OnEventTriggered?.Invoke(playerIndex);
}
