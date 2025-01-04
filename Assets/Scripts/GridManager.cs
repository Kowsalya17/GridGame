using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public int gridSize = 7;
    public Color[] tileColors;
    private Tile[,] grid;
    public TextMeshProUGUI scoretext;
    int score = 0;
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tile[gridSize, gridSize];

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                GameObject tileObj = Instantiate(tilePrefab, new Vector3(x, y, -1), Quaternion.identity);

                tileObj.transform.parent = transform;

                Tile tile = tileObj.GetComponent<Tile>();
                tile.SetColor(tileColors[Random.Range(0, tileColors.Length)]);
                Debug.Log($"Assigned color {tile.tileColor} to tile at {x}, {y}");

                tile.gridPosition = new Vector2Int(x, y);

                grid[x, y] = tile;
            }
        }
    }
    public bool AreTilesAdjacent(Tile tile1, Tile tile2)
    {
        return Mathf.Abs(tile1.gridPosition.x - tile2.gridPosition.x) +
               Mathf.Abs(tile1.gridPosition.y - tile2.gridPosition.y) == 1;
    }
    public void UpdateGrid(Tile tile1, Tile tile2)
    {
        // Update the grid data structure to reflect the new positions of the swapped tiles
        grid[tile1.gridPosition.x, tile1.gridPosition.y] = tile1;
        grid[tile2.gridPosition.x, tile2.gridPosition.y] = tile2;
    }


    public bool CheckMatch(Tile tile1, Tile tile2)
    {
        if (tile1.tileColor == tile2.tileColor && AreTilesAdjacent(tile1, tile2))
        {
            tile1.RemoveTile();
            tile2.RemoveTile();
            score += 5;
            scoretext.text = score.ToString();
        }
        return false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
