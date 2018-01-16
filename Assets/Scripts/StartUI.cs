using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartUI : MonoBehaviour {

    public Text lastText;
    public Text bestText;

    public Toggle blue;
    public Toggle yellow;
    public Toggle border;
    public Toggle freedom;



    void Awake()
    {
        lastText.text = "上次:长度" + PlayerPrefs.GetInt("lastl", 0) + ",分数" + PlayerPrefs.GetInt("lasts", 0);
        bestText.text = "最高:长度" + PlayerPrefs.GetInt("bestl", 0) + ",分数" + PlayerPrefs.GetInt("bests", 0);
    }

    void Start()
    {
        if (PlayerPrefs.GetString("sh", "sh01") == "sh01")
        {
            blue.isOn = true;
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102");
        }
        else
        {
            yellow.isOn = true;
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");
        }
        if (PlayerPrefs.GetInt("border", 1) == 1)
        {
            border.isOn = true;
            PlayerPrefs.SetInt("border", 1);
        }
        else
        {
            freedom.isOn = true;
            PlayerPrefs.SetInt("border", 0);
        }
    }

    public void BlueSnake(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("sh", "sh01");
            PlayerPrefs.SetString("sb01", "sb0101");
            PlayerPrefs.SetString("sb02", "sb0102");
        }
    }
    public void YellowSnake(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetString("sh", "sh02");
            PlayerPrefs.SetString("sb01", "sb0201");
            PlayerPrefs.SetString("sb02", "sb0202");
        }
    }
    public void BorderLimit(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 1);
        }
    }
    public void Freedom(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("border", 0);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Instruction()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
