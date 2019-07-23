using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tetromino : MonoBehaviour
{
    public float fallSpeed = 1;
    private float fall = 0;
    // Start is called before the first frame update
    void Start()
    {
        fall = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UpdatePostion(new Vector3(-0.08f, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UpdatePostion(new Vector3(0.08f, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdateRotation();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)|| Time.time - fall >= fallSpeed)
        {
            UpdatePostionDown(new Vector3(0, -0.08f, 0));
        }
    }
    void UpdatePostion(Vector3 newTransform)
    {
        ClearMatrixGrid();
        transform.position += newTransform;
        if (IsValidGridPosition())
        {
            UpdateMatrixGrid();
        }
        else
        {
            transform.position += newTransform * (-1);
        }
    }
    void UpdatePostionDown(Vector3 newTransform)
    {
        ClearMatrixGrid();
        transform.position += newTransform;
        if (IsValidGridPosition())
        {
            UpdateMatrixGrid();
        }
        else
        {
            transform.position += newTransform * (-1);
            UpdateMatrixGrid();
            if (IsLost())
            {
                FindObjectOfType<Game>().ShowRestartButton();
            }
            FindObjectOfType<Game>().DeleteFullRows();
            enabled = false;
        }
        fall = Time.time;
    }
    private void UpdateRotation()
    {
        ClearMatrixGrid();
        transform.Rotate(0, 0, 90);
        if (IsValidGridPosition())
        {
            UpdateMatrixGrid();
        }
        else
        {
            transform.Rotate(0, 0, -90);
            UpdateMatrixGrid();
        }
    }
    private bool IsValidGridPosition()
    {
        foreach (Transform child in transform)
        {
            if (!IsInsideBorder(child.position))
            {
                return false;
            }
            if (Game.grid[(int)(Mathf.Round(child.position.x * 100 / 8))-1, (int)(Mathf.Round(child.position.y * 100 / 8))-1] != null )
            {
                if (Game.grid[(int)(Mathf.Round(child.position.x * 100 / 8)) - 1, (int)(Mathf.Round(child.position.y * 100 / 8)) - 1].parent != transform)
                {
                    return false;
                }
            }
        }
        return true;
    }
    private void ClearMatrixGrid()
    {
        foreach (Transform child in transform)
        {
            Game.grid[(int)(Mathf.Round(child.position.x * 100 / 8)) - 1, (int)(Mathf.Round(child.position.y * 100 / 8)) - 1] = null;
        }
    }
    private void UpdateMatrixGrid()
    {
        foreach (Transform child in transform)
        {
            Game.grid[(int)(Mathf.Round(child.position.x * 100 / 8))-1, (int)(Mathf.Round(child.position.y * 100 / 8))-1] = child;
        }
    }
    private bool IsInsideBorder(Vector3 pos)
    {
        return (pos.x > 0.07f && pos.x < 0.89f && pos.y > 0.07f && pos.y<1.51f);
    }
    private bool IsLost()
    {
        for (int i = 0; i < Game.w; i++)
        {
            if (Game.grid[i,Game.h-1]!=null)
            {
                return true;
            }
        }
        return false;
    }
}
