using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public GameObject[] TetriminoPrefabs;
    public static int height = 20;
    public static int width = 10;
    public float speed = 1.0f;
    public float coolOffTime;
    public GameObject currentTetriminoFalling = null;
    public GameObject nextTetriminoFalling = null;
    public static GameObject[,] grid = new GameObject[width, height];
    public static int rows = 0;
    public static int score = 0;
    public static int level = 1;
    public Text hud_score;
    public Text hud_level;
    public Text hud_rows;

    public GameObject NextTetrimino;
    public GameObject PreviewTetrimino;
    public bool gameStarted = false;


    // Start is called before the first frame update
    void Start()
    {

        coolOffTime = 0.0f;

        //currentTetriminoFalling = SpawnNextTetrimino();

    }

    // Update is called once per frame
    void Update()
    {


        if (coolOffTime < Time.time)
        {
            // instantiate a new tetrimino and move it down ...
            if (currentTetriminoFalling == null || currentTetriminoFalling.GetComponent<Tetrimino>().enabled == false)
            {


                currentTetriminoFalling = SpawnNextTetrimino();


            }
        }

        if (coolOffTime < Time.time)
        {
            if (currentTetriminoFalling.GetComponent<Tetrimino>().enabled)
            {

                currentTetriminoFalling.transform.Translate(Vector3.down);


                if (!CheckIsValidPosition())
                {
                    currentTetriminoFalling.transform.Translate(Vector3.up);
                    currentTetriminoFalling.GetComponent<Tetrimino>().enabled = false;
                    if (CheckIsAboveGrid(currentTetriminoFalling))
                    {
                        GameOver();
                    }

                    GetComponent<Tetrimino>().UpdateGrid();
                    GetComponent<Tetrimino>().DeleteRow();
                    GameInfoUpdate();


                }
                else
                {

                }

            }
            coolOffTime = Time.time + speed;
            Speed();

        }


    }

    GameObject SpawnNextTetrimino()
    {
        GameObject NextTetrimino = GameObject.Instantiate(
                        TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                        new Vector3(5, 20, 0),
                        Quaternion.identity) as GameObject;

        Debug.Log(NextTetrimino.GetComponent<ITetrimino>().GetType());

        return NextTetrimino;
    }
    /*
    public GameObject SpawnNextTetrimino()
    {

        if (!gameStarted)
        {
            gameStarted = true;

            NextTetrimino = GameObject.Instantiate(
                            TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                            new Vector3(5, 20, 0),
                            Quaternion.identity) as GameObject;

             PreviewTetrimino = GameObject.Instantiate(
                            TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                            new Vector3(15, 5, 0),
                            Quaternion.identity) as GameObject;
            PreviewTetrimino.GetComponent<Tetrimino>().enabled = false;
            Debug.Log(NextTetrimino.GetComponent<ITetrimino>().GetType());
            return NextTetrimino;

        }

        else
        {
            PreviewTetrimino.GetComponent<Tetrimino>().enabled = true;
            NextTetrimino = PreviewTetrimino;
            NextTetrimino.transform.localPosition = new Vector3(5, 20, 0);
            //PreviewTetrimino = null;
            PreviewTetrimino = GameObject.Instantiate(
                            TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                            new Vector3(15, 5, 0),
                            Quaternion.identity) as GameObject;
            PreviewTetrimino.GetComponent<Tetrimino>().enabled = false;
            Debug.Log(NextTetrimino.GetComponent<ITetrimino>().GetType());

            return NextTetrimino;


        }
    }

    /*
    public void SpawnNextTetrimino()
    {
        if (!gameStarted)
        {
            gameStarted = true;
            currentTetriminoFalling = GameObject.Instantiate(
                        TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                        new Vector3(5, 20, 0),
                        Quaternion.identity) as GameObject;

            previewTetrimino =  GameObject.Instantiate(
                        TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                        previewTetriminoPosition,
                        Quaternion.identity) as GameObject;
            previewTetrimino.GetComponent<Tetrimino>().enabled = false;
            //return nextTetrimino;
        }

        else
        {
            previewTetrimino.transform.localPosition = new Vector3(5, 20, 0);
            currentTetriminoFalling = previewTetrimino;
            currentTetriminoFalling.GetComponent<Tetrimino>().enabled = true;
            previewTetrimino = GameObject.Instantiate(
                        TetriminoPrefabs[Random.Range(0, TetriminoPrefabs.Length)],
                        previewTetriminoPosition,
                        Quaternion.identity) as GameObject;
            previewTetrimino.GetComponent<Tetrimino>().enabled = false;
            //return nextTetrimino;

        }
       
        Debug.Log(nextTetrimino.GetComponent<ITetrimino>().GetType());


    }
     */

    public bool CheckIsValidPosition()
    {
        foreach (GameObject cube in FindObjectOfType<Tetrimino>().cubes)
        {
            Vector2 v = Tetrimino.RoundVector(cube.transform.position);

            if (!Tetrimino.IsInsideBorder(v))
                return false;
            if (grid[(int)v.x, (int)v.y] != null && grid[(int)v.x, (int)v.y].transform.parent.parent != currentTetriminoFalling.transform)
                return false;
        }

        return true;
    }

    public bool CheckIsAboveGrid(GameObject tetrimino)
    {
        for(int x = 0; x < width; x++)
        {
            foreach(GameObject cube in FindObjectOfType<Tetrimino>().cubes)
            {
                Vector2 v = Tetrimino.RoundVector(cube.transform.position);
                if ((int)v.y > height - 1)
                    return true;
            }
        }
        return false;
    }

    public void GameInfoUpdate()
    {
        Debug.Log("Rows = " + rows);
        hud_rows.text = rows.ToString();
        Debug.Log("Score = " + score);
        hud_score.text = score.ToString();
        level = (rows / 10)+ 1;
        Debug.Log("Level = " + level);
        hud_level.text = level.ToString();
    }

    public void OutOfBounds(GameObject tetrimino)
    {
        for (int x = 0; x < width; ++x)
        {
            foreach (GameObject cube in FindObjectOfType<Tetrimino>().cubes)
            {
                Vector2 v = Tetrimino.RoundVector(cube.transform.position);
                if (v.y > height - 1)
                    GameOver();
            }
        }

    }

    public void Speed()
    {
        if (level == 1)
            speed = 1.0f;
        if (level == 2)
            speed = 0.8f;
        if (level == 3)
            speed = 0.6f;
        if (level == 4)
            speed = 0.5f;
        if (level == 5)
            speed = 0.4f;
        if (level == 6)
            speed = 0.3f;
        if (level == 7)
            speed = 0.2f;
        if (level == 8)
            speed = 0.1f;
        if (level > 8)
            speed = 0.1f;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");

    }
}