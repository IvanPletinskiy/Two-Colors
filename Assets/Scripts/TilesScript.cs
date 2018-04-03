using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TilesScript : MonoBehaviour {

    public Text scoreText, levelText;
   

    public GameObject[] tiles = new GameObject[4];
    public Vector4[] colors = new Vector4[4];

    int level = 0;
    int score = 0;

    public bool next, lose;

    // Use this for initialization
    void Start () {
        next = false;
        lose = false;
        //tiles[0] = gameObject.transform.GetChild(0);
        updateLevel();
	}
	
	// Update is called once per frame
	void Update () {
        if (lose)
            endGame();
        if (next && !lose)
            updateLevel();
    }

    private void updateLevel()
    {
        level++;

        scoreText.text = score.ToString();
        levelText.text = "Level " + level.ToString();

        updateTiles();
    }

    private void updateTiles()
    {
        generateColors();

        for(int i = 0; i < 4; i ++)
        {
        //    tiles[i].GetComponent<Renderer>.material.color = colors[i];
        }
    }

    private void generateColors()
    {
        for(int i = 0; i < 4; i ++)
        {
            Color color = new Vector4(Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), Random.Range(0.1f, 1f), 1);

        //    tiles[i].GetComponent<Renderer>.material.color = aColor;

           // GetComponent<Renderer>().material.color = aColor;
        }
    }

    private void checkResult()
    {

    }

    private void endGame()
    {

    }


}
