using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I : Tetrimino, ITetrimino
{
    public float speed = 1.0f;
    public float coolOffTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    TetriminoType ITetrimino.GetType()
    {
        return Type;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForUserInput();
        //UpdateGrid();
        //FindObjectOfType<GameMaster>().UpdateGrid();

    }

    void CheckForUserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Root.transform.Translate(Vector3.left);

            if (!FindObjectOfType<GameMaster>().CheckIsValidPosition())
            {
                Root.transform.Translate(Vector3.right);
            }
           

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Root.transform.Translate(Vector3.right);
            if (!FindObjectOfType<GameMaster>().CheckIsValidPosition())
            {
                Root.transform.Translate(Vector3.left);
            }

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pivot.transform.Rotate(Vector3.forward, 90);

        
            if (!FindObjectOfType<GameMaster>().CheckIsValidPosition())
            {
                    Pivot.transform.Rotate(Vector3.forward, -90);
            }

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            Pivot.transform.Rotate(Vector3.forward, -90);

            if (!FindObjectOfType<GameMaster>().CheckIsValidPosition())
            {
                Pivot.transform.Rotate(Vector3.forward, 90);

            }

        }

        if (Input.GetKey(KeyCode.Space))
        {
            Root.transform.Translate(Vector3.down);

            if (!FindObjectOfType<GameMaster>().CheckIsValidPosition())
            {
                Root.transform.Translate(Vector3.up);

            }
            //UpdateGrid();
        }
    }



}

