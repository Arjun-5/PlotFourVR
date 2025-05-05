using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] PlotFourGameConfig gameConfig;

    [SerializeField] float spacing = 1.1f;

    [Header("References")]
    [SerializeField] Transform gridParent;

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (gameConfig.gridPrefab == null || gridParent == null)
        {
            Debug.LogError("Assign cellPrefab and gridParent.");
            return;
        }

        for (int x = 0; x < gameConfig.girdColumns; x++)
        {
            for (int y = 0; y < gameConfig.girdRows; y++)
            {
                Vector3 position = new Vector3(x * spacing, y * spacing, 0f);

                GameObject cell = Instantiate(gameConfig.gridPrefab, position, Quaternion.identity, gridParent);
 
                cell.name = $"Cell_{x}_{y}";
            }
        }
    }
}
