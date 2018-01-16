using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class SnakeHead : MonoBehaviour 
{
    public static SnakeHead _instance;
    public static SnakeHead instance
    {
        get
        {
            return _instance;
        }
    }
    public List<Transform> bodylist = new List<Transform>();//创建list存储
    public int step;//蛇头每次移动的长度
    private int x;
    private int y;
    private float velocity=0.5f;
    private Vector3 headPos;//头部位置

    private Transform canvas;
    public GameObject bodyPerfab;//蛇身预制体
    public Sprite[] bodySprites=new Sprite[2];//蛇身图集长度
    public bool isDie = false;
    public GameObject dieEffect;
    public AudioClip eatAudio;
    public AudioClip dieAudio;

    
    void Awake()
    {
        canvas = GameObject.Find("Canvas").transform;
        //Resources.Load(string path)动态加载资源
        gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>(PlayerPrefs.GetString("sh","sh02"));
        bodySprites[0] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb01","sb0201"));
        bodySprites[1] = Resources.Load<Sprite>(PlayerPrefs.GetString("sb02", "sb0202"));
    }
	/// <summary>
	/// 初始化调用Move方法，并让蛇头开始往右移动
	/// </summary>
	void Start () 
    {

        InvokeRepeating("Move",0,velocity);
        x = 0;
        y = step;
	}
	
    
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&MainUIController.Instance.isPause==false&&isDie==false) 
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity-0.35f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && MainUIController.Instance.isPause == false && isDie == false)
        {
            CancelInvoke();
            InvokeRepeating("Move", 0, velocity);
        }
        if (Input.GetKey(KeyCode.W) && y != -step && MainUIController.Instance.isPause == false && isDie == false) 
        {
            gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
            x = 0;
            y = step;
        }
        if (Input.GetKey(KeyCode.S) && y != step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
            x = 0;
            y = -step;
        }
        if (Input.GetKey(KeyCode.A) && x != step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            x = -step;
            y = 0;
        }
        if (Input.GetKey(KeyCode.D) && x != -step && MainUIController.Instance.isPause == false && isDie == false)
        {
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, -90);
            x = step;
            y = 0;
        }
	}
    /// <summary>
    /// 移动方法，双色蛇身
    /// </summary>
    void Move() 
    {
        headPos = gameObject.transform.localPosition;//保存蛇头还未移动前的位置
        gameObject.transform.localPosition = new Vector3(headPos.x+x,headPos.y+y,headPos.z);//将蛇头移动到预期位置
        if (bodylist.Count > 0) 
        {
            for (int i = bodylist.Count - 2; i >= 0;i--)
            {
                bodylist[i + 1].localPosition = bodylist[i].localPosition; //每个蛇身移动到其前面蛇身节点处
            }
            bodylist[0].localPosition = headPos;
        }
    }
    /// <summary>
    /// 生成蛇身体
    /// </summary>
    void Grow() 
    {
        AudioSource.PlayClipAtPoint(eatAudio,Vector3.zero);
        int index = (bodylist.Count % 2 == 0) ? 0 : 1;//判断身体的奇偶
        GameObject body = Instantiate(bodyPerfab,new Vector3(2000,2000,0),Quaternion.identity)as GameObject;
        body.GetComponent<Image>().sprite=bodySprites[index];
        body.transform.SetParent(canvas,false);
        bodylist.Add(body.transform);
    }
    /// <summary>
    /// 死亡
    /// </summary>
    void Die()
    {
            AudioSource.PlayClipAtPoint(dieAudio, Vector3.zero);
            CancelInvoke();
            isDie = true;
            Instantiate(dieEffect);
            //记录最后得分，长度，最佳得分、长度
            PlayerPrefs.SetInt("lastl",MainUIController.Instance.length);
            PlayerPrefs.SetInt("lasts", MainUIController.Instance.score);
            if(PlayerPrefs.GetInt("bests",0)<MainUIController.Instance.score)
            {
                PlayerPrefs.SetInt("bestl", MainUIController.Instance.length);
                PlayerPrefs.SetInt("bests", MainUIController.Instance.score);
            }
            StartCoroutine(GameOver());

    }
    /// <summary>
    /// 游戏结束
    /// </summary>
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }
    /// <summary>
    /// 检测碰撞方法
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag=="Food")
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI();
            Grow();
            Food.Instance.MakeFood((Random.Range(1,100)<30)?true:false);//百分之三十的几率生成奖励
        }
        else if (collision.gameObject.tag == "Reward")
        {
            Destroy(collision.gameObject);
            MainUIController.Instance.UpdateUI(Random.Range(10,20)*10);//吃到奖励加100~200不等
            Grow();
        }

        else if (collision.gameObject.tag == "Body")
        {
            Die();
        }
        else
        {
            if (MainUIController.Instance.hasBorder)
            {
                Die();
            }
            else
            {
                switch (collision.gameObject.name)
                {
                    case "up":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y + 30, transform.localPosition.z);
                        break;
                    case "down":
                        transform.localPosition = new Vector3(transform.localPosition.x, -transform.localPosition.y - 30, transform.localPosition.z);
                        break;
                    case "left":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 180, transform.localPosition.y, transform.localPosition.z);
                        break;
                    case "right":
                        transform.localPosition = new Vector3(-transform.localPosition.x + 240, transform.localPosition.y, transform.localPosition.z);
                        break;
                }
            }
           
                
        }
    }
}
