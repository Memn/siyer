using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour {
    public GameObject hexPrefab;
    
    int width = 5, height=5;
    public static string word = "Kainatboşlukkabuletmezabii";
    public static string imgDir = "Assets/Resources/Letters/";
    public static string Empty = imgDir + "Empty.png";
    void Start () {

        Puzzle puzzle = PuzzleMaker.MakePuzzle(word);
        int index = 0;
        Vector2 pos=new Vector2(0,0);
        Vector2 start = new Vector2(-1*puzzle.width/2, -1*puzzle.height/2);
        for (int x = 0; x < puzzle.width; x++)
        {
            for (int y = 0; y < puzzle.height; y++)
            {
             
                if (y % 2 == 0)
                {
                    pos = new Vector2(start.x+ x + 0.5f, start.y+ y);
                }
                else
                {
                    pos = new Vector2(start.x + x , start.y + y);
                }

                string img2 = imgDir + puzzle.puzzleData[x,y]+".png";
                GameObject go = Instantiate(hexPrefab, pos, Quaternion.identity) ;
                go.GetComponent<SpriteRenderer>().sprite = LoadNewSprite(img2);
                go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                index++;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Sprite NewSprite = new Sprite();
        Texture2D SpriteTexture = LoadTexture(FilePath);
        NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit);

        return NewSprite;
    }

    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D=null;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            
             // If data = readable -> return texture
        }
        else
        {
            FileData = File.ReadAllBytes(Empty);
        }
        Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
        if (Tex2D.LoadImage(FileData))
        {
            //Tex2D.Resize(Tex2D.width / 2, Tex2D.height / 2);
            return Tex2D;
        }// Load the imagedata into the texture (size is set automatically)
        return null;                     // Return null if load failed
    }
}
