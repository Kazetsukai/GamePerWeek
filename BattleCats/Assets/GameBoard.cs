using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour {

    public int BoardHeight;
    public int BoardWidth;

    public GameObject GrassTileProto;

    GameTile[,] Tiles;
    private GameTile _highlightedTile;
    private GameTile _selectedTile;

    // Use this for initialization
    void Start () {
        Tiles = new GameTile[BoardHeight, BoardWidth];

        // Make a board of grass tiles.
        for (int y = 0; y < BoardHeight; y++)
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                var tile = Instantiate(GrassTileProto);
                tile.transform.position = new Vector3(x - BoardWidth / 2.0f, y - BoardHeight / 2.0f, 0);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        
        // Raycast from mouse to highlight and select tiles
        if (Physics.Raycast(ray, out rayHit))
        {
            var hitObj = rayHit.collider.gameObject;

            var gameTile = hitObj.GetComponent<GameTile>();
            if (gameTile != null)
            {
                if (_highlightedTile != gameTile)
                {
                    if (_highlightedTile != null)
                    {
                        _highlightedTile.SetHighlighted(false);
                    }
                    _highlightedTile = gameTile;
                    _highlightedTile.SetHighlighted(true);
                }
            }

            // Select when we click on something
            if (gameTile != _selectedTile)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (_selectedTile != null)
                        _selectedTile.SetSelected(false);

                    _selectedTile = gameTile;

                    if (_selectedTile != null)
                        _selectedTile.SetSelected(true);
                }
            }
        }
    }

    public GameTile GetSelectedTile()
    {
        return _selectedTile;
    }
}
