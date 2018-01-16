using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{

    //单例模式
    private static MainUIController _instance;
    public static MainUIController Instance
    {
        get
        {
            return _instance;
        }
    }
    public int score;
    public int length;
    public Text msgText;
    public Text scoreText;
    public Text lengthText;
    public Image bgImage;
    public Image pauseImage;
    public Sprite[] pauseSprites;
    public bool isPause=false;
    public bool hasBorder = true;
    private Color tempColor;


    void Awake()
    {
        _instance = this;
    }
    /// <summary>
    /// 初始化游戏界面
    /// </summary>
    void Start()
    {
        if (PlayerPrefs.GetInt("border", 1) == 0)
        {
            hasBorder = false;
            foreach (Transform t in bgImage.gameObject.transform)
            {
                t.gameObject.GetComponent<Image>().enabled = false;
            }
        }
    }
    /// <summary>
    /// 更新界面
    /// </summary>
    void Update()
    {
        int i=score/100;
        if (i == Random.Range(3, 6)) 
        {
            ColorUtility.TryParseHtmlString("#80FFFF", out tempColor);
            bgImage.color = tempColor;
            //bgImage.color = Color.blue;
            msgText.text = "成长阶段";
        }
        else if (i == Random.Range(7, 10)) 
        {
                ColorUtility.TryParseHtmlString("#CCFF80",out tempColor);
                bgImage.color = tempColor;
                msgText.text = "进阶阶段";
        }
            else if (i == Random.Range(11, 14)) 
        {
                ColorUtility.TryParseHtmlString("#FFFF6F",out tempColor);
                bgImage.color = tempColor;
                msgText.text = "成熟阶段";
        }
            else if (i == Random.Range(15, 18)) 
        {
                ColorUtility.TryParseHtmlString("#CA8EC2",out tempColor);
                bgImage.color = tempColor;
                msgText.text = "巨蟒阶段";
        }
            else if (i>=19) 
        {
                ColorUtility.TryParseHtmlString("#FF7575",out tempColor);
                bgImage.color = tempColor;
                msgText.text = "无尽阶段";
        }
     
    }
    /// <summary>
    /// 更新控制面板
    /// </summary>
    /// <param name="s"></param>
    /// <param name="l"></param>
    public void UpdateUI(int s=5,int l=1)
    {
        score += s;
        length += l;
        scoreText.text = "分数：\n" + score;
        lengthText.text = "长度：\n" + length;
    }
    /// <summary>
    ///暂停事件
    /// </summary>
    public void Pause()
    {
        isPause=!isPause;
        if (isPause)
        {
            Time.timeScale = 0;
            pauseImage.sprite= pauseSprites[1];
        }
        else
        {
            Time.timeScale = 1;
            pauseImage.sprite = pauseSprites[0];
        }
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }


	
}
