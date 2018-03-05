using UnityEngine;

public class LabirentSpawner : MonoBehaviour
{
    public MazeManager manager;

//    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom;
    public int RandomSeed = 12345;
    public GameObject Floor;

    public GameObject[] WallFabs;

//    public GameObject Wall = null;
    public GameObject Pillar;
    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 5;
    public float CellHeight = 5;
    public GameObject GoalPrefab;

    public Transform front;
    public Transform back;
    public Transform left;
    public Transform right;


    private void Start()
    {
        var goals = 0;
        if (!FullRandom)
        {
            Random.seed = RandomSeed;
        }


        var mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);

        mMazeGenerator.GenerateMaze();
        for (var row = 0; row < Rows; row++)
        {
            for (var column = 0; column < Columns; column++)
            {
                var x = column * CellWidth;
                var z = row * CellHeight;
                var cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;
                tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
                tmp.transform.parent = transform;
                var wall = WallFabs[Random.Range(0, WallFabs.Length)];
                if (cell.WallRight)
                {
                    tmp = Instantiate(wall, new Vector3(x + CellWidth / 2, 0, z) + wall.transform.position,
                        Quaternion.Euler(0, 90, 0)); // right
                    tmp.transform.parent = right;
                }

                if (cell.WallFront)
                {
                    tmp = Instantiate(wall, new Vector3(x, 0, z + CellHeight / 2) + wall.transform.position,
                        Quaternion.Euler(0, 0, 0)); // front
                    tmp.transform.parent = front;
                }

                if (cell.WallLeft)
                {
                    tmp = Instantiate(wall, new Vector3(x - CellWidth / 2, 0, z) + wall.transform.position,
                        Quaternion.Euler(0, 270, 0)); // left
                    tmp.transform.parent = left;
                }

                if (cell.WallBack)
                {
                    tmp = Instantiate(wall, new Vector3(x, 0, z - CellHeight / 2) + wall.transform.position,
                        Quaternion.Euler(0, 180, 0)); // back
                    tmp.transform.parent = back;
                }

                if (cell.IsGoal && GoalPrefab != null)
                {
                    tmp = Instantiate(GoalPrefab, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0));
                    tmp.transform.parent = transform;
                    goals++;
                }
            }
        }

        if (Pillar != null)
        {
            for (var row = 0; row < Rows + 1; row++)
            {
                for (var column = 0; column < Columns + 1; column++)
                {
                    var x = column * (CellWidth);
                    var z = row * (CellHeight);
                    var tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2),
                        Quaternion.identity);
                    tmp.transform.parent = transform;
                }
            }
        }

        manager.SetRemaining(goals);
    }
}