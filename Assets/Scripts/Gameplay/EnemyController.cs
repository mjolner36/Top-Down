using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject heathBarPref;
    private GameObject heathBarRef;
    private Rigidbody2D rigidbody2D;
    
    public NavMeshAgent agent;
    
    private BaseState currentState;
    public IdleState idleState = new IdleState();
    public AttackState attackState = new AttackState();
    public RunState runState = new RunState();
        
    public Animator animator;
    private float moveSpeed;
    
    public delegate void Action(int damage);
    public static event Action HitEvent;
    
    [SerializeField] private EnemySO enemyData;
    private int hp;

    public void Init()
    {
        animator = gameObject.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
        currentState = idleState;
        currentState.EnterState(this);
        
        heathBarRef = Instantiate(heathBarPref,GameManager.Instance.canvas.transform);
        heathBarRef.GetComponent<Slider>().maxValue = enemyData.hp;
        heathBarRef.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = enemyData.healthBarColor;
        heathBarRef.GetComponent<Slider>().value = enemyData.hp; 
        heathBarRef.GetComponent<UIFollowObject>().objectToFollow = gameObject.transform;
        hp = enemyData.hp;
        
        moveSpeed = enemyData.moveSpeed;
    }
    
    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        state.EnterState(this);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GetDamage(other.gameObject.GetComponent<Bullet>().damage);
            Destroy(other.gameObject);
        }
    }
    
    public void TriggerAnimationAttackEvent()
    {
        HitEvent?.Invoke(enemyData.damage);
    }

    private void GetDamage(int damage)
    {
        if (damage < hp)
        {
            hp -= damage;
            heathBarRef.GetComponent<Slider>().value = hp;
        }
        else
        {
            Destroy(heathBarRef);
            DropItem();
            Destroy(gameObject);
        }
    }

    private void DropItem()
    {
        var rnd = Random.Range(0, GameManager.Instance.Inventory.AllArtifactSOs.Count);
        var tempGameObject = GameManager.Instance.Inventory.AllArtifactSOs[rnd].prefab;
        var item = Instantiate(tempGameObject, gameObject.transform.position, Quaternion.identity);
        item.GetComponent<Artifact>().amount = Random.Range(1, 10);
    }

    public int rangeToSee()
    {
        return enemyData.rangeToSee;
    }

    public int attackRange()
    {
        return enemyData.attackRange;
    }
    
}
