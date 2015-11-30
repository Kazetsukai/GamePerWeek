using UnityEngine;
using System.Collections;

public class MazeGenerator : MonoBehaviour
{

    public const float BlockScale = 0.3f;

    public GameObject ProtoWall;

    public GameObject[,] _wallGrid;

    private int _height;
    private int _width;

    // Use this for initialization
    void Start()
    {
        InitialiseGrid();
    }

    private void InitialiseGrid()
    {
        _width = (int)(10 * transform.localScale.x / BlockScale);
        _height = (int)(10 * transform.localScale.z / BlockScale);

        _wallGrid = new GameObject[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                _wallGrid[x, y] = (GameObject)Instantiate(ProtoWall, new Vector3(x - _width / 2, 0, y - _height / 2) * BlockScale, Quaternion.identity);
                _wallGrid[x, y].transform.localScale = new Vector3(BlockScale, BlockScale, BlockScale);

                ///////////////////////
                if (Random.value < 0.4f)
                {
                    _wallGrid[x, y].SendMessage("Rise");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
