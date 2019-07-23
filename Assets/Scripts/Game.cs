using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game : MonoBehaviour
{
    private Vector3 spawnPosition1 = new Vector3(0.48f,1.44f,0f), spawnPosition2 = new Vector3(0.44f, 1.48f, 0f);
    public GameObject[] spawnPrefabs;
    public GameObject restartButton;
    public static int w = 11,h=19;

    public static Transform[,] grid = new Transform[w, h];
    // Start is called before the first frame update
    void Start()
    {
        grid = new Transform[w,h];
        SpawnRandomTetris();
    }
    public void SpawnRandomTetris()
    {
        int randomIndex = Random.Range(0, spawnPrefabs.Length);
        switch (randomIndex)
        {
            case 0:
                Instantiate(spawnPrefabs[randomIndex], spawnPosition1, Quaternion.identity);
                break;
            case 1:
                Instantiate(spawnPrefabs[randomIndex], spawnPosition1, Quaternion.identity);
                break;
            case 2:
                Instantiate(spawnPrefabs[randomIndex], spawnPosition1, Quaternion.identity);
                break;
            case 3:
                Instantiate(spawnPrefabs[randomIndex], spawnPosition1, Quaternion.identity);
                break;
            case 4:
                Instantiate(spawnPrefabs[randomIndex], spawnPosition1, Quaternion.identity);
                break;
            case 5:
                Instantiate(spawnPrefabs[randomIndex], spawnPosition2, Quaternion.identity);
                break;
            case 6:
                Instantiate(spawnPrefabs[randomIndex], spawnPosition2, Quaternion.identity);
                break;
        }
    }
    public void DeleteRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void DecreaseRow(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -0.08f, 0);
            }
        }
    }
    public void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < h; i++)
        {
            DecreaseRow(i);
        }
    }
    public bool IsRowFull(int y)
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] == null)
            {
                return false;
            }
        }
        return true;
    }
    public void DeleteFullRows()
    {
        for (int y = 0; y < h; y++)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                y--;
            }
        }
        SpawnRandomTetris();
    }
    public void ShowRestartButton()
    {
        restartButton.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartButtonPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene");
    }
}
