using UnityEngine;

[CreateAssetMenu(menuName ="Plot Four/Game Config")]
public class PlotFourGameConfig : ScriptableObject
{
    public int girdColumns;

    public int girdRows;

    public int winCondition;

    public PlayerDefinition[] playerDefinitions;

    public GameObject gridPrefab;

    public GameObject selectorGridPrefab;
}
