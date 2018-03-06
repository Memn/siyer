using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public struct Position
{
    public int x;
    public int y;
    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
public struct Puzzle
{
    public int width;
    public int height;
    public char[,] puzzleData;
    public string word;
    public Puzzle(string word)
    {
        this.word = word;
        int size = (int)(Math.Round(Math.Sqrt(word.Length)));
        if (size * size < word.Length)
        {
            this.height = size + 1;
        }
        else
        {
            this.height = size;
        }
        
        this.width = size;
        puzzleData = new char[ width, height];
        Array.Clear(puzzleData, 0, height * width);
    }
}
public class PuzzleMaker : MonoBehaviour {

    static Position UP = new Position(-1, 0);
    static Position UPL = new Position(-1, -1);
    static Position RIGHT = new Position(0, 1);
    static Position DOWNL = new Position(1, -1);
    static Position DOWN = new Position(1, 0);
    static Position LEFT = new Position(0, -1);
    //static Position[] SIDES = { UP, UPL,  RIGHT, DOWNL, DOWN, LEFT };
    static Position[] SIDES = { UP, RIGHT, DOWN, LEFT };

    //public static string word = "Siyer, İslam dini literatüründe peygamberlerin, din büyüklerinin ve halifelerin hayat hikâyesidir.";

    public static Puzzle MakePuzzle(string word)
    {
        
        Puzzle puzzle = new Puzzle(word);

        //Position position = new Position(randomNumber.Next(size-1), randomNumber.Next(size-1) );
        Position position = new Position(0, 0);
        
        addNewCharToPuzzle(0, position, puzzle);
        printPuzzle(puzzle);
        return puzzle;
    }

    public static bool addNewCharToPuzzle(int charIndex, Position position, Puzzle puzzle)
    {

        short sideNumber;
        int tryNumber = 0;
        bool added = false;
        if (puzzle.word.Length <= charIndex)
        {
            added = true;
        }
        else if (position.x >= 0 && position.x < puzzle.width && position.y >= 0 && position.y < puzzle.height && puzzle.puzzleData[position.x, position.y] == 0)
        {
            puzzle.puzzleData[position.x, position.y] = puzzle.word.ElementAt(charIndex);

            sideNumber = (short)UnityEngine.Random.Range(1,SIDES.Length);
            // sideNumber = 0;
            while (!added && tryNumber < SIDES.Length)
            {
                tryNumber++;
                Position nextPosition = new Position((SIDES[sideNumber].x + position.x), (SIDES[sideNumber].y + position.y));
                sideNumber = (short)((sideNumber + 1) % SIDES.Length);
                added = addNewCharToPuzzle(charIndex + 1, nextPosition, puzzle);
            }
            if (!added)
            {
                puzzle.puzzleData[position.x, position.y] = (char)0;
            }


        }


        return added;
    }

    public static void printPuzzle(Puzzle puzzle)
    {
        for (int i = 0; i < puzzle.width; i++)
        {
            for (int j = 0; j < puzzle.height; j++)
            {
                Console.Write(puzzle.puzzleData[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}
