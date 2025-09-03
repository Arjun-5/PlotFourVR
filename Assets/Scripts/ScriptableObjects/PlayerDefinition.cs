using UnityEngine;

[CreateAssetMenu(menuName = "Plot Four/Player Definition")]
public class PlayerDefinition : ScriptableObject
{
    public string playerName;

    public Color discColor;

    public GameObject discPrefab;
}
