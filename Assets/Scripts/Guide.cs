using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Guide : MonoBehaviour
{
    // Start is called before the first frame update

    GUIStyle style = new GUIStyle();
    public Texture green_bottle;
    public Texture blue_bottle;
    public Texture red_bottle;
    public Texture pandora_box;

    void Start()
    {
        // Position the Text in the center of the Box
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 60;
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
        GUI.skin.box.fontSize = 48;
        GUI.skin.label.fontSize = 28;
        GUI.skin.button.fontSize = 36;
        int line_h = 45; int para_h = 80; int margin = 70;

        
        GUI.Box(new Rect(margin, margin, Screen.width- 2 * margin, Screen.height - 2 * margin), "游戏说明");//Task Guidance

        int x = 2*margin; int y = x;

        //para1 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("迷宫寻宝:"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("这是一个迷宫寻宝游戏：迷宫外的栅栏内有一块魔法石，魔法石需要得到魔法药水的滋养。"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("迷宫内部有装满药水的魔法瓶，你的任务是找到更多的魔法瓶带回到栅栏内部。"));
        y = y + line_h;//para1 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("每次进入迷宫，你只能带出一个魔法瓶。为了滋养魔法石，你需要尽可能多的进入迷宫，带回更多魔法瓶。 "));


        y = y + para_h; //para2 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("潘多拉盒:"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("在寻宝的路上，偶尔会出现潘多拉盒，打开盒子可能会得到奖励，也可能会得到惩罚。"));
        y = y + line_h;//para2 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("你可以选择打开潘多拉盒，也可以选择绕过它。"));

        y = y + para_h;//para3 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("感受报告:"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("游戏过程中，会时常要求你做感受报告。当评分画面出现的时候，请准确评估你当下的真实感受，并将滑动条滑到相应的位置。"));

        y = y + para_h;//para3 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("操作方法:"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("'W' 向前； shift+'W' 加速； 'A' 向左平移；'D' 向右平移； 鼠标控制转向；"));
        y = y + line_h;//para3 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("'E' 拾取魔法瓶，或者打开潘多拉盒；在栅栏内按 'Q' 将魔法瓶放下；"));

        y = y + para_h;//para4
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("如果你明白了游戏规则，那么现在开始游戏吧~ "));

        y = y + para_h;
        if (GUI.Button(new Rect(Screen.width / 2 - 80, y, 160, 80), "开始"))
        {
            SceneManager.LoadScene("Maze");
        }
        /*
        //para1 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("There are three kinds of magic drugs in the maze: ");
        GUI.Label(new Rect(Screen.width / 2 - (2*line_h + 5), y, line_h, line_h), new GUIContent(green_bottle));
        GUI.Label(new Rect(Screen.width / 2, y, line_h, line_h), new GUIContent(blue_bottle)); GUI.Label(new Rect(Screen.width / 2 + 2*line_h + 5, y, line_h, line_h), new GUIContent(red_bottle));
        y = y + line_h;//para1 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("Every time you enter the maze, you have a chance to find one, please take it back to the magic stone in the fence. "));
        y = y + line_h;//para1 line3
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("In order to nourish the magic stone, you should enter the maze as much as possible to retrieve enough magic drugs. "));

        y = y + para_h; //para2 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("Pandora's box")); GUI.Label(new Rect(x + 200, y, Screen.width - 2 * x, line_h), new GUIContent("will appear along the way, you can choose to open it or bypass it.",pandora_box));
        y = y + line_h;//para2 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("If you open the box, you have a chance to get rewards or punishment."));

        y = y + para_h;//para3 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("We need you to evaluate your feelings from time to time. Please report as accurately as possible."));

        y = y + para_h;//para3 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("'W' for moving forward; shift+'W' for running; 'A','D' for shifting left or right; Move the mouse to turn round;"));
        y = y + line_h;//para3 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("Press 'E' to pick up the drug bottle or open a pandora's box;"));
        y = y + line_h;//para3 line3
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("Press 'Q' in the fence to nourish the magic stone with drugs. "));

        y = y + para_h;//para4
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("If you understand the task, press the button to start."));

        y = y + para_h * 2;
        if (GUI.Button(new Rect(Screen.width/2 - 80, y, 160, 80), "START"))
        {
            SceneManager.LoadScene("Maze");
        }*/
    }
}
