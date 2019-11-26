using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TetriminoType
{
    I, J, L, O, S, T, Z
};

public class Tetrimino : MonoBehaviour
{
    public TetriminoType Type;
    [Tooltip("Used for translation")]
    public GameObject Root;

    [Tooltip("Used for rotation")]
    public GameObject Pivot;
    public static int width = 10;
    public static int height = 20;

    [SerializeField]
    public GameObject[] cubes = new GameObject[4];



    // Start is called before the first frame update
   

    //public static GameObject[,] grid = new GameObject[width, height];

    //return the round value of the vector
    public static Vector2 RoundVector(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    //checks if the tetriminos is inside the grid

    public static bool IsInsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width && (int)pos.y >= 0);
    }



    public static bool IsToRight(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x > width - 1 && (int)pos.y >= 0);
    }

    public void UpdateGrid()
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                if (GameMaster.grid[x, y] != null)
                    if (GameMaster.grid[x, y].transform.parent.parent == GetComponent<GameMaster>().currentTetriminoFalling.GetComponent<Tetrimino>().Root.transform)
                        GameMaster.grid[x, y] = null;
            }
        }

        foreach (GameObject cube in FindObjectOfType<Tetrimino>().cubes)
        {

            Vector2 v = Tetrimino.RoundVector(cube.transform.position);

            GameMaster.grid[(int)v.x, (int)v.y] = cube;
        }
    }

    public static void Delete(int y)
    {
        for (int x = 0; x < width; x++)
        {
            Destroy(GameMaster.grid[x, y].gameObject);
            GameMaster.grid[x, y] = null;
        }
    }
    public static bool IsFull(int y)
    {
        for (int x = 0; x < width; x++)
            if (GameMaster.grid[x, y] == null)
                return false;
        return true;
    }

    public void DeleteRow()
    {
        int r = 0;
        for (int y = 0; y < height; y++)
        {
            if (IsFull(y))
            {
                Delete(y);
                RowDownAll(y + 1);
                --y;
                GameMaster.rows += 1;
                r = +1;
            }
        } 

        if(r == 1)
        {
            GameMaster.score += 40 * (GameMaster.level + 1);
        }
        if (r == 2)
        {
            GameMaster.score += 100 * (GameMaster.level + 1);
        }
        if (r == 3)
        {
            GameMaster.score += 300 * (GameMaster.level + 1);
        }
        if (r == 4)
        {
            GameMaster.score += 1200 * (GameMaster.level + 1);
        }
        //UpdateGrid();
    }

    public static void RowDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            if (GameMaster.grid[x, y] != null)
            {
                GameMaster.grid[x, y - 1] = GameMaster.grid[x, y];
                GameMaster.grid[x, y] = null;
                GameMaster.grid[x, y - 1].transform.position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void RowDownAll(int y)
    {
        for (int i = y; i < height; i++)
            RowDown(i);
    }
}


