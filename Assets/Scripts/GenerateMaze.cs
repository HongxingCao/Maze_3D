using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GenerateMaze : MonoBehaviour
{
    public static int updateType;//0:no update; 1:update boxes; 2:updata boxes and goal

    GameObject maze;
    GameObject wallWell;
    GameObject junctionWell;
    GameObject cornerWell;
    GameObject goalWell;
    GameObject boxWell;
    GameObject droppingsWell;
    GameObject trees;


    int x_size = 31;
    int z_size = 23;
    int[,] predefMaze;
    int[,] mazeMatrix;
    int minbox = 5;
    int maxbox = 20;
    int mindroppings = 5;
    int maxdroppings = 20;


    Vector3 unit_scale = new Vector3(40, 30, 40);
    Vector3 init_point = new Vector3(400, 0, 700);

    const int roadType = 0;
    const int wallType = 1;
    const int entryType = 2;//record data
    const int junctionType = 3;//record data
    const int cornerType = 4;//record data, Update boxes

    const int goalType = 5;//show the goal, Event (Get the goal),  record data, Update boxes
    const int boxType = 6;//show the box, Event (Get the box),  record data
    const int droppingsType = 7;//show the effect, Event (good or bad),  record data


    void Reset()
    {
        //Awake();

    }

    void Awake()
    {
        Application.targetFrameRate = 30;

        GameObject world = Instantiate(Resources.Load("World", typeof(GameObject))) as GameObject;
        GameObject home = Instantiate(Resources.Load("Home", typeof(GameObject))) as GameObject;

        maze = new GameObject("maze");
        wallWell = new GameObject("wallWell"); wallWell.transform.parent = maze.transform;
        junctionWell = new GameObject("junctionWell"); junctionWell.transform.parent = maze.transform;
        cornerWell = new GameObject("cornerWell"); cornerWell.transform.parent = maze.transform;
        goalWell = new GameObject("goalWell"); goalWell.transform.parent = maze.transform;
        boxWell = new GameObject("boxWell"); boxWell.transform.parent = maze.transform;
        droppingsWell = new GameObject("droppingsWell"); droppingsWell.transform.parent = maze.transform;

        plantTrees();

        updateType = 0;

        StartCoroutine(InitializeMaze());
    }

    void Start()
    {
        PlayerDynamic.Instance.LogInfo = "Generate Maze"; Debug.Log(PlayerDynamic.Instance.LogInfo);
    }
    
    void plantTrees()
    {
        trees = new GameObject("trees");

        Vector4 world_space = new Vector4(50, 50, 1950, 1950);
        Vector4 maze_space = new Vector4(init_point.x - 100, init_point.z - 100, init_point.x + x_size * unit_scale.x + 100, init_point.z + z_size * unit_scale.z + 100);
        Vector4 home_space = new Vector4(1000 - 70, 480 - 170, 1000 + 70, 480 + 170);
        Debug.Log("world_space: " + world_space[0] + ","+ world_space[1] + "," + world_space[2] + "," + world_space[3]);
        Debug.Log("maze_space: " + maze_space[0] + "," + maze_space[1] + "," + maze_space[2] + "," + maze_space[3]);
        int num_trees = 0;
        do
        {
            float tree_x = UnityEngine.Random.Range(world_space[0], world_space[2]);
            float tree_z = UnityEngine.Random.Range(world_space[1], world_space[3]);
            if (!(tree_x > maze_space[0] && tree_x < maze_space[2] && tree_z > maze_space[1] && tree_z < maze_space[3]) && !(tree_x > home_space[0] && tree_x < home_space[2] && tree_z > home_space[1] && tree_z < home_space[3]))
            {
                num_trees++;
                int ind = UnityEngine.Random.Range(1, 9);
                string tree_name = "trees/Coconut_Palm_Tree0" + ind.ToString();
                GameObject tree = Instantiate(Resources.Load(tree_name, typeof(GameObject))) as GameObject;
                tree.transform.position = new Vector3(tree_x, 0, tree_z);
                tree.transform.parent = trees.transform;

            }
        } while (num_trees < 100);
    }

    void Update()
    {
        if(updateType != 0)
        {
            if(updateType == 1)
            {
                PlayerDynamic.Instance.LogInfo = "Update Boxes & droppings"; Debug.Log(PlayerDynamic.Instance.LogInfo);
            }
            else if(updateType == 2)
            {
                PlayerDynamic.Instance.LogInfo = "Update Goal and Boxes & droppings"; Debug.Log(PlayerDynamic.Instance.LogInfo);
            }

            UpdateMaze(updateType);
            updateType = 0;
        }
    }

    void UpdateMaze(int updateType)
    {
        Destroy(boxWell);
        Destroy(droppingsWell);
        boxWell = new GameObject("boxWell"); boxWell.transform.parent = maze.transform;
        droppingsWell = new GameObject("droppingsWell"); droppingsWell.transform.parent = maze.transform;
        RandomEvents();

        if(updateType == 2)
        {
            Destroy(goalWell);
            goalWell = new GameObject("goalWell"); goalWell.transform.parent = maze.transform;
        }

        for (int x = 0; x < x_size; x++)
        {
            for (int z = 0; z < z_size; z++)
            {
                switch (mazeMatrix[x, z])
                {
                    case boxType:
                        GameObject box_trigger = Instantiate(Resources.Load("box_trigger", typeof(GameObject))) as GameObject;
                        box_trigger.transform.localScale = unit_scale;
                        box_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        box_trigger.transform.parent = boxWell.transform;

                        GameObject box = Instantiate(Resources.Load("BoxOfPandora", typeof(GameObject))) as GameObject;
                        box.transform.position = new Vector3(init_point.x + x * unit_scale.x, 2, init_point.z + z * unit_scale.z);
                        box.transform.parent = box_trigger.transform;
                        box.SetActive(false);

                        GameObject boxMarker = Instantiate(Resources.Load("MiniMapPrefab\\BoxMarker", typeof(GameObject))) as GameObject;
                        boxMarker.transform.position = box.transform.position;
                        boxMarker.transform.parent = box_trigger.transform;
                        break;

                    case droppingsType:
                        GameObject droppings_trigger = Instantiate(Resources.Load("droppings_trigger", typeof(GameObject))) as GameObject;
                        droppings_trigger.transform.localScale = unit_scale;
                        droppings_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        droppings_trigger.transform.parent = droppingsWell.transform;

                        GameObject droppings = Instantiate(Resources.Load("Droppings", typeof(GameObject))) as GameObject;
                        droppings.transform.position = new Vector3(init_point.x + x * unit_scale.x, 2, init_point.z + z * unit_scale.z);
                        droppings.transform.parent = droppings_trigger.transform;
                        droppings.SetActive(false);

                        GameObject droppingsMarker = Instantiate(Resources.Load("MiniMapPrefab\\DroppingsMarker", typeof(GameObject))) as GameObject;
                        droppingsMarker.transform.position = droppings.transform.position;
                        droppingsMarker.transform.parent = droppings_trigger.transform;
                        break;

                    case goalType:
                        if(updateType == 2)
                        {
                            GameObject goal_trigger = Instantiate(Resources.Load("goal_trigger", typeof(GameObject))) as GameObject;
                            goal_trigger.transform.localScale = unit_scale;
                            goal_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                            goal_trigger.transform.parent = goalWell.transform;

                            int goal_ani = UnityEngine.Random.Range(1, 4);
                            string goal_name = "Bottle_Endurance";
                            switch (goal_ani)
                            {
                                case 1:
                                    goal_name = "Bottle_Endurance";
                                    break;
                                case 2:
                                    goal_name = "Bottle_Health";
                                    break;
                                case 3:
                                    goal_name = "Bottle_Mana";
                                    break;
                            }
                            GameObject goal = Instantiate(Resources.Load(goal_name, typeof(GameObject))) as GameObject;
                            goal.transform.position = new Vector3(init_point.x + x * unit_scale.x, 2, init_point.z + z * unit_scale.z);
                            goal.transform.parent = goal_trigger.transform;
                            goal.SetActive(false);

                            GameObject goalMarker = Instantiate(Resources.Load("MiniMapPrefab\\GoalMarker", typeof(GameObject))) as GameObject;
                            goalMarker.transform.position = goal.transform.position;
                            goalMarker.transform.parent = goal_trigger.transform;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
    }

    IEnumerator InitializeMaze()
    {
        yield return new WaitForSeconds(0f);

        loadMazeConfig();
        RandomEvents();

        for (int x = 0; x < x_size; x++)
        {
            for (int z = 0; z < z_size; z++)
            {
                switch (mazeMatrix[x, z])
                {
                    case roadType:
                        break;
                    case wallType:
                        GameObject wall = Instantiate(Resources.Load("wall", typeof(GameObject))) as GameObject;
                        wall.transform.localScale = unit_scale;
                        wall.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        wall.transform.parent = wallWell.transform;
                        break;

                    case entryType:
                        GameObject entry_trigger = Instantiate(Resources.Load("entry_trigger", typeof(GameObject))) as GameObject;
                        entry_trigger.transform.localScale = unit_scale;
                        entry_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        entry_trigger.transform.parent = junctionWell.transform;
                        break;
                    case junctionType:
                        GameObject junction_trigger = Instantiate(Resources.Load("junction_trigger", typeof(GameObject))) as GameObject;
                        junction_trigger.transform.localScale = unit_scale;
                        junction_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        junction_trigger.transform.parent = junctionWell.transform;
                        break;

                    case cornerType:
                        GameObject corner_trigger = Instantiate(Resources.Load("corner_trigger", typeof(GameObject))) as GameObject;
                        corner_trigger.transform.localScale = unit_scale;
                        corner_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        corner_trigger.transform.parent = cornerWell.transform;
                        break;

                    case boxType:
                        GameObject box_trigger = Instantiate(Resources.Load("box_trigger", typeof(GameObject))) as GameObject;
                        box_trigger.transform.localScale = unit_scale;
                        box_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        box_trigger.transform.parent = boxWell.transform;

                        GameObject box = Instantiate(Resources.Load("BoxOfPandora", typeof(GameObject))) as GameObject;
                        box.transform.position = new Vector3(init_point.x + x * unit_scale.x, 2, init_point.z + z * unit_scale.z);
                        box.transform.parent = box_trigger.transform;
                        box.SetActive(false);

                        GameObject boxMarker = Instantiate(Resources.Load("MiniMapPrefab\\BoxMarker", typeof(GameObject))) as GameObject;
                        boxMarker.transform.position = box.transform.position;
                        boxMarker.transform.parent = box_trigger.transform;
                        break;

                    case droppingsType:
                        GameObject droppings_trigger = Instantiate(Resources.Load("droppings_trigger", typeof(GameObject))) as GameObject;
                        droppings_trigger.transform.localScale = unit_scale;
                        droppings_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        droppings_trigger.transform.parent = droppingsWell.transform;

                        GameObject droppings = Instantiate(Resources.Load("Droppings", typeof(GameObject))) as GameObject;
                        droppings.transform.position = new Vector3(init_point.x + x * unit_scale.x, 2, init_point.z + z * unit_scale.z);
                        droppings.transform.parent = droppings_trigger.transform;
                        droppings.SetActive(false);

                        GameObject droppingsMarker = Instantiate(Resources.Load("MiniMapPrefab\\DroppingsMarker", typeof(GameObject))) as GameObject;
                        droppingsMarker.transform.position = droppings.transform.position;
                        droppingsMarker.transform.parent = droppings_trigger.transform;
                        break;

                    case goalType:
                        GameObject goal_trigger = Instantiate(Resources.Load("goal_trigger", typeof(GameObject))) as GameObject;
                        goal_trigger.transform.localScale = unit_scale;
                        goal_trigger.transform.position = new Vector3(init_point.x + x * unit_scale.x, 0, init_point.z + z * unit_scale.z);
                        goal_trigger.transform.parent = goalWell.transform;

                        int goal_ani = UnityEngine.Random.Range(1, 4);
                        string goal_name = "Bottle_Endurance";
                        switch (goal_ani)
                        {
                            case 1: 
                                goal_name = "Bottle_Endurance";
                                break;
                            case 2:
                                goal_name = "Bottle_Health";
                                break;
                            case 3:
                                goal_name = "Bottle_Mana";
                                break;
                        }
                        GameObject goal = Instantiate(Resources.Load(goal_name, typeof(GameObject))) as GameObject;
                        goal.transform.position = new Vector3(init_point.x + x * unit_scale.x, 2, init_point.z + z * unit_scale.z);
                        goal.transform.parent = goal_trigger.transform;
                        goal.SetActive(false);

                        GameObject goalMarker = Instantiate(Resources.Load("MiniMapPrefab\\GoalMarker", typeof(GameObject))) as GameObject;
                        goalMarker.transform.position = goal.transform.position;
                        goalMarker.transform.parent = goal_trigger.transform;
                        break;

                    default:
                        //Debug.Log("Unknown maze component: " + mazeMatrix[x, z]);
                        break;
                }
            }
        }
    }



    #region Utils
    void Shuffle(int[] Posarray)
    {
        for (int i = 0; i < Posarray.Length; i++)
        {
            int temp = Posarray[i];
            int randomIndex = UnityEngine.Random.Range(i, Posarray.Length);
            Posarray[i] = Posarray[randomIndex];
            Posarray[randomIndex] = temp;
        }
    }

    void loadMazeConfig()
    {
        predefMaze = new int[x_size, z_size];
        var mazeFile = Resources.Load<TextAsset>("Maze");
        string mazeString = mazeFile.ToString();
        string[] mazeLines = mazeString.Split('\n');
        for (int x = 0; x < x_size; x++)
        {
            string[] objs = mazeLines[x].Split(' ');
            for (int z = 0; z < z_size; z++)
            {
                predefMaze[x, z] = int.Parse(objs[z]);
            }
        }

        string configFile = "Config.json";
        using (System.IO.StreamReader file = System.IO.File.OpenText(configFile))
        {
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject config = (JObject)JToken.ReadFrom(reader);
                minbox = int.Parse(config["min_box_num"].ToString()); Debug.Log("minbox: " + minbox.ToString());
                maxbox = int.Parse(config["max_box_num"].ToString()); Debug.Log("maxbox: " + maxbox.ToString());
                mindroppings = int.Parse(config["min_droppings_num"].ToString()); Debug.Log("mindroppings: " + mindroppings.ToString());
                maxdroppings = int.Parse(config["max_droppings_num"].ToString()); Debug.Log("maxdroppings: " + maxdroppings.ToString());
            }
        }
    }

    void RandomEvents()
    {
        mazeMatrix = new int[x_size, z_size];
        Array.Copy(predefMaze, mazeMatrix, x_size * z_size);

        PlayerDynamic.Instance.boxNum = UnityEngine.Random.Range(minbox, maxbox);
        PlayerDynamic.Instance.droppingsNum = UnityEngine.Random.Range(mindroppings, maxdroppings);

        List<int> Poslist = new List<int>();
        for (int x = 0; x < x_size; x++)
        {
            for (int z = 0; z < z_size; z++)
            {
                if (mazeMatrix[x, z] == 0)
                {
                    Poslist.Add(x * z_size + z);
                }
            }
        }
        int[] Posarray = Poslist.ToArray();
        Shuffle(Posarray);
        
        for (int i = 0; i < PlayerDynamic.Instance.boxNum; i++)
        {
            mazeMatrix[Posarray[i] / z_size, Posarray[i] % z_size] = boxType;
        }

        for (int i = PlayerDynamic.Instance.boxNum; i < (PlayerDynamic.Instance.boxNum + PlayerDynamic.Instance.droppingsNum); i++)
        {
            mazeMatrix[Posarray[i] / z_size, Posarray[i] % z_size] = droppingsType;
        }

        StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
        for (int x = 0; x < x_size; x++)
        {
            sw.Write("\t\t\t\t\t");
            for (int z = 0; z < z_size; z++)
            {
                sw.Write(mazeMatrix[x, z]);
                sw.Write('\t');
            }
            sw.Write('\n');
        }
        sw.Close();
    }
    #endregion

}
