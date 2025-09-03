using System.Collections;
using UnityEngine;

public class PlotFourGameBoard : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] float spacing;

    [Header("References")]
    [SerializeField] Transform gridParent;

    [SerializeField] Transform selectorRowParent;

    [SerializeField] TurnChangeEvent turnChangeEvent;

    [Header("Disc References")]
    [SerializeField] Transform discParent;

    private PlotFourGameConfig gameConfig;

    private bool isGameOver = false;

    private int[,] grid;

    private int currentPlayerIndex = 0;

    private GameManager gameManager;

    private bool[] columnFull;
    public int GetCurrentPlayerIndex() => currentPlayerIndex;

    public void Initialize(GameManager manager)
    {
        gameManager = manager;

        gameConfig = manager.gameConfig;

        grid = new int[gameConfig.girdColumns, gameConfig.girdRows];
        
        columnFull = new bool[gameConfig.girdColumns];

        GenerateGrid();
        
        GenerateSelectorRow();
    }

    private void GenerateGrid()
    {
        Vector3 offset = new Vector3((gameConfig.girdColumns - 1) / 2f * spacing, (gameConfig.girdRows - 1) / 2f * spacing, 0f);

        for (int x = 0; x < gameConfig.girdColumns; x++)
        {
            for (int y = 0; y < gameConfig.girdRows; y++)
            {
                Vector3 position = new Vector3(x * spacing, y * spacing, 0f) - offset;

                Instantiate(gameConfig.gridPrefab, position, Quaternion.identity, gridParent).name = $"Cell_{x}_{y}";
            }
        }
    }

    private void GenerateSelectorRow()
    {
        float topY = (gameConfig.girdRows) * spacing;

        float selectorY = topY + spacing;

        Vector3 offset = new Vector3((gameConfig.girdColumns - 1) / 2f * spacing, selectorY / 2f, 0f);

        for (int x = 0; x < gameConfig.girdColumns; x++)
        {
            Vector3 position = new Vector3(x * spacing, selectorY, 0f) - offset;

            GameObject selector = Instantiate(gameConfig.selectorGridPrefab, position, Quaternion.identity, selectorRowParent);

            selector.name = $"Selector_{x}";

            var selectorComponent = selector.AddComponent<SelectorGridCell>();

            selectorComponent.Initialize(this, x);
        }
    }


    public void TryDropDisc(int column)
    {
        if (isGameOver)
        {
            Debug.Log("Game is over. No more moves allowed.");
          
            return;
        }

        if (columnFull[column])
        {
            return;
        }

        for (int y = 0; y < gameConfig.girdRows; y++)
        {
            if (grid[column, y] == 0)
            {
                grid[column, y] = currentPlayerIndex + 1;

                if (y == gameConfig.girdRows - 1)
                {
                    columnFull[column] = true;
                }

                Vector3 start = selectorRowParent.GetChild(column).position;

                Vector3 end = gridParent.GetChild(column * gameConfig.girdRows + y).position;

                GameObject disc = Instantiate(gameConfig.playerDefinitions[currentPlayerIndex].discPrefab, start, Quaternion.identity, discParent);

                StartCoroutine(MoveDiscToPosition(disc.transform, end, 0.5f));

                if (CheckWin(column, y))
                {
                    isGameOver = true;

                    gameManager.SetGameOverState(currentPlayerIndex);

                    return;
                }
                else if (IsBoardFull())
                {
                    isGameOver = true;
                    
                    gameManager.SetGameOverState(-1);

                    return;
                }
                currentPlayerIndex = (currentPlayerIndex + 1) % gameConfig.playerDefinitions.Length;

                turnChangeEvent.Trigger(currentPlayerIndex);

                return;
            }
        }
    }
    private bool IsBoardFull()
    {
        foreach (var full in columnFull)
        {
            if (!full)
            {
                return false;
            }
        }
        return true;
    }
    private IEnumerator MoveDiscToPosition(Transform disc, Vector3 target, float duration)
    {
        Vector3 start = disc.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            disc.position = Vector3.Lerp(start, target, elapsed / duration);
         
            elapsed += Time.deltaTime;
            
            yield return null;
        }

        disc.position = target;
    }

    private bool CheckWin(int x, int y)
    {
        int playerId = grid[x, y];


        if (CountDiscs(x, y, 1, 0, playerId) + CountDiscs(x, y, -1, 0, playerId) >= gameConfig.winCondition - 1)
        {
            return true;
        }

        if (CountDiscs(x, y, 0, 1, playerId) + CountDiscs(x, y, 0, -1, playerId) >= gameConfig.winCondition - 1)
        {
            return true;
        }

        if (CountDiscs(x, y, 1, 1, playerId) + CountDiscs(x, y, -1, -1, playerId) >= gameConfig.winCondition - 1)
        {
            return true;
        }

        if (CountDiscs(x, y, 1, -1, playerId) + CountDiscs(x, y, -1, 1, playerId) >= gameConfig.winCondition - 1)
        {
            return true;
        }

        return false;
    }

    private int CountDiscs(int x, int y, int dx, int dy, int playerId)
    {
        int count = 0;

        int nx = x + dx;
        
        int ny = y + dy;

        while (nx >= 0 && nx < gameConfig.girdColumns && ny >= 0 && ny < gameConfig.girdRows && grid[nx, ny] == playerId)
        {
            count++;
            
            nx += dx;
            
            ny += dy;
        }

        return count;
    }
}
