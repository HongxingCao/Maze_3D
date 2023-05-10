using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubjectMenu : MonoBehaviour
{
	public static int subjectNumber;

    GUIStyle style = new GUIStyle();

    void Awake()
    {
        subjectNumber = 0;
    }

    void Start()
    {
        // Position the Text in the center of the Box
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 40;
    }

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	void OnGUI()
	{

        if (subjectNumber == 0)
        {
            GUI.skin.box.fontSize = 48;
            GUI.skin.label.fontSize = 36;
            GUI.Box(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 400, 800, 900), "ª∂”≠≤Œº”√‘π¨”Œœ∑");//Welcome to Maze Game
            GUI.Label(new Rect(Screen.width / 2 - 345, Screen.height / 2 - 300, 800, 900), "«Î—°‘Òƒ„µƒ±ª ‘±‡∫≈");//Please choose your participant number

            for (int i=0; i<5; i++)
            {
                for (int j=0; j<6; j++)
                {
                    int num = i * 6 + j + 1;
                    if (GUI.Button(new Rect(Screen.width / 2 - 345 + j * 120, Screen.height / 2 - 230 + i * 120, 100, 100), num.ToString()))
                    {
                        subjectNumber = num;
                        SceneManager.LoadScene("Guide");
                    }
                }
            }
        }
    }
}
