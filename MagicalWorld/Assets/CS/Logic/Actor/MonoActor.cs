using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 游戏角色行为
/// </summary>
public class MonoActor : MonoBehaviour
{
    public Slider HP;
    public Transform launcher;
    public Text actorName;
    public ActorData actorData;
    private Image icon;
    private TweenPosition tweenPos;
    // 初始化调用
    public virtual void Awake()
    {
        
    }
    public void Start()
    {

    }
    void Update()
    {

    }
    public virtual void Init(ActorData data,Vector3 BP)
    {
        actorData = data;
        tweenPos = GetComponent<TweenPosition>();
        if (tweenPos == null)
        {
            tweenPos = gameObject.AddComponent<TweenPosition>();
        }
        tweenPos.ignoreTimeScale = false;
        icon = GetComponent<Image>();
        icon.sprite = GameUtil.LoadSprite(actorData.icon);
        icon.SetNativeSize();
        RefData();
        transform.localPosition = BP;

        //开场进入战场
        MoveTo(new Vector3(0, 0, 0), (float)actorData.moveSpeed);
    }
    /// <summary>
    /// 刷新数据
    /// </summary>
    public virtual void RefData()
    {
        HP.value = (float)actorData.HP / (float)actorData.maxHP;
        actorName.text = actorData.name;
    }
    public virtual void DestroySelf()
    {
        GameObject.Destroy(gameObject);
    }
    public virtual void MoveTo(Vector3 target,float duration)
    {
        TweenPosition.Begin(gameObject, duration, target);
    }
    /// <summary>
    /// 开火攻击
    /// </summary>
    /// <param name="target"></param>
    public virtual void Fire(Vector3 target)
    {

    }
}

