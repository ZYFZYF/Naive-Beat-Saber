using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Generate : MonoBehaviour
{

    public GameObject leftNormalCubePrefab; //左手正常切割方块（需按照一定方向）
    public GameObject leftSpecialCubePrefab;//左手任意切割方块（无需按照一定方向）
    public GameObject rightNormalCubePrefab;
    public GameObject rightSpecialCubePrefab;
    private string[][] cubeData;
    private int destroyCubeIndex = 0;
    private int generateCubeIndex = 0;
    private int cubeCount;
    static public float Z_MOVE_TARGET = 1;//被切割的Z位置
    static public float Z_MOVE_TIME = 2;//从生成的地方飘到被切割的地方所需花费的时间
    static public float Z_MOVE_SPEED = 20;//飘的速度
    private float X_START_POS = -1.5f;
    private float X_STEP_LENGTH = 1;
    private float Y_CENTER_POS = 2;
    private float Y_STEP_LENGTH = 1;
    private float Z_DISAPPEAR_POS = 0.2f;
    // Use this for initialization

    void Start()
    {
        TextAsset textAsset = Resources.Load("dejavu", typeof(TextAsset)) as TextAsset;
        string[] lineArray = textAsset.text.Split('\n');
        cubeCount = lineArray.Length - 1;
        cubeData = new string[cubeCount][];
        for (int i = 0; i < cubeCount; i++)
        {
            cubeData[i] = lineArray[i + 1].Split(',');
            for (int j = 0; j < cubeData[i].Length; j++)
            {
                //Debug.Log(string.Format("{0} {1} {2}", i, j, cubeData[i][j]));
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        float nowTime = Time.timeSinceLevelLoad;
        //Debug.Log(strisng.Format("{0} {1}", generateCubeIndex, cubeCount));
        while (generateCubeIndex < cubeCount && float.Parse(cubeData[generateCubeIndex][0]) < nowTime + Z_MOVE_TIME)
        {
            //先从每一行解析出数据
            int color = int.Parse(cubeData[generateCubeIndex][1]);
            int direction = int.Parse(cubeData[generateCubeIndex][2]);
            int xPos = int.Parse(cubeData[generateCubeIndex][3]);
            int yPos = int.Parse(cubeData[generateCubeIndex][4]);
            //Debug.Log(string.Format("{0} {1} {2} {3}", color, direction, xPos, yPos));
            //得到初始位置和旋转角度
            Vector3 position = new Vector3(X_START_POS + X_STEP_LENGTH * xPos, Y_CENTER_POS + Y_STEP_LENGTH * yPos, Z_MOVE_TARGET
            + Z_MOVE_TIME * Z_MOVE_SPEED);
            Vector3 rotation = new Vector3(0, 0, direction * 45);
            //不同设定的模块贴图不一样，实现上是prefab不一样
            GameObject template;
            if (color == 0 && direction >= 0) template = leftNormalCubePrefab;
            else if (color == 0 && direction < 0) template = leftSpecialCubePrefab;
            else if (color == 1 && direction >= 0) template = rightNormalCubePrefab;
            else template = rightSpecialCubePrefab;
            //实例化，并且存一些meta信息进去
            GameObject cube = Instantiate(template, position, Quaternion.identity);
            if (direction >= 0) cube.transform.Rotate(rotation);
            cube.name = "Cube " + generateCubeIndex;
            //cube.transform.localScale *= 0.8f;
            Meta meta = cube.GetComponent<Meta>();
            meta.color = color;
            meta.direction = direction;
            // Debug.Log("generate " + cube.name);
            generateCubeIndex++;
        }

        while (destroyCubeIndex < generateCubeIndex)
        {
            GameObject destroyCube = GameObject.Find("Cube " + destroyCubeIndex);
            if (destroyCube != null)
            {
                if (destroyCube.transform.position.z < Z_DISAPPEAR_POS)
                {
                    //Debug.Log(destroyCube);
                    Destroy(destroyCube);
                    //Debug.Log("destroy cube " + destroyCubeIndex);
                    destroyCubeIndex++;
                }
                else break;
            }
            else
            {
                destroyCubeIndex++;
                //Debug.Log("add destroy cube " + destroyCubeIndex);
            }
            // if (destroyCube != null && destroyCube.transform.position.z < Z_DISAPPEAR_POS)
            // {
            //     Destroy(destroyCube);
            //     destroyCubeIndex++;
            //     Debug.Log("teste!!!!");
            //     Debug.Log("destroy cube " + destroyCubeIndex);
            // }
            // else {
            //     destroyCubeIndex++;
            //     Debug.Log("teste!!!!");
            //     Debug.Log("destroy cube " + destroyCubeIndex);
            // }
        }
    }
}
