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

        
        GUI.Box(new Rect(margin, margin, Screen.width- 2 * margin, Screen.height - 2 * margin), "��Ϸ˵��");//Task Guidance

        int x = 2*margin; int y = x;

        //para1 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("�Թ�Ѱ��:"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("����һ���Թ�Ѱ����Ϸ���Թ����դ������һ��ħ��ʯ��ħ��ʯ��Ҫ�õ�ħ��ҩˮ��������"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("�Թ��ڲ���װ��ҩˮ��ħ��ƿ������������ҵ������ħ��ƿ���ص�դ���ڲ���"));
        y = y + line_h;//para1 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("ÿ�ν����Թ�����ֻ�ܴ���һ��ħ��ƿ��Ϊ������ħ��ʯ������Ҫ�����ܶ�Ľ����Թ������ظ���ħ��ƿ�� "));


        y = y + para_h; //para2 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("�˶�����:"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("��Ѱ����·�ϣ�ż��������˶����У��򿪺��ӿ��ܻ�õ�������Ҳ���ܻ�õ��ͷ���"));
        y = y + line_h;//para2 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("�����ѡ����˶����У�Ҳ����ѡ���ƹ�����"));

        y = y + para_h;//para3 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("���ܱ���:"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("��Ϸ�����У���ʱ��Ҫ���������ܱ��档�����ֻ�����ֵ�ʱ����׼ȷ�����㵱�µ���ʵ���ܣ�����������������Ӧ��λ�á�"));

        y = y + para_h;//para3 line1
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("��������:"));
        y = y + line_h;
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("'W' ��ǰ�� shift+'W' ���٣� 'A' ����ƽ�ƣ�'D' ����ƽ�ƣ� ������ת��"));
        y = y + line_h;//para3 line2
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("'E' ʰȡħ��ƿ�����ߴ��˶����У���դ���ڰ� 'Q' ��ħ��ƿ���£�"));

        y = y + para_h;//para4
        GUI.Label(new Rect(x, y, Screen.width - 2 * x, line_h), new GUIContent("�������������Ϸ������ô���ڿ�ʼ��Ϸ��~ "));

        y = y + para_h;
        if (GUI.Button(new Rect(Screen.width / 2 - 80, y, 160, 80), "��ʼ"))
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
