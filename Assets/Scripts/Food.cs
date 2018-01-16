using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    //单例模式
    private static Food _instance;
    public static Food Instance 
    {
        get
        {
            return _instance;
        }
    }


    public int xlimit = 21;
    public int ylimit = 9;
    public int xoffset = 7;
    public GameObject rewardPrefab;//奖励
    public GameObject foodPrefab;//食物预制体
    public Sprite[] foodSprites;//食物的集合
    private Transform foodHolder;//食物的容器

    void Awake() 
    {
        _instance = this;
    }

	void Start ()
    {
        foodHolder = GameObject.Find("foodHolder").transform;
        MakeFood(false);
	}
	
    /// <summary>
    /// 生成食物
    /// </summary>
    public void MakeFood(bool isReward) 
    {
        int index = Random.Range(0,foodSprites.Length);
        GameObject food = Instantiate(foodPrefab);
        food.GetComponent<Image>().sprite=foodSprites[index];
        food.transform.SetParent(foodHolder,false);
        int x = Random.Range(-xlimit+xoffset,xlimit);
        int y = Random.Range(-ylimit,ylimit);
        food.transform.localPosition = new Vector3(x*30,y*30,0);
        if (isReward)
        {
            GameObject reward = Instantiate(rewardPrefab);
            reward.transform.SetParent(foodHolder, false);
            x = Random.Range(-xlimit + xoffset, xlimit);
            y = Random.Range(-ylimit, ylimit);
            reward.transform.localPosition = new Vector3(x * 30, y * 30, 0);
        }
    }
}
