using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject heathBarPref;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private Animator _animator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject heathBarRef;

    [SerializeField] private GameObject _weaponSlot;


    public delegate void PlayerAction();

    public static event PlayerAction PlayerDieEvent;


    public int hp = 100;

    public void Init()
    {
        EnemyController.HitEvent += GetDamage;
        heathBarRef = Instantiate(heathBarPref, GameManager.Instance.canvas.transform);
        heathBarRef.GetComponent<Slider>().maxValue = hp;
        heathBarRef.GetComponent<Slider>().value = hp;
        heathBarRef.GetComponent<UIFollowObject>().objectToFollow = gameObject.transform;
        _weaponSlot.transform.GetChild(0).gameObject.GetComponent<Shooting>().enabled = true;
    }

    private void Start()
    {
        _joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FixedJoystick>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = _joystick.Horizontal;
        float moveVertical = _joystick.Vertical;

        _rigidbody2D.velocity = new Vector2(moveHorizontal * _moveSpeed, moveVertical * _moveSpeed);

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            _animator.SetBool("isWalking", true);

            // Если двигается влево и объект повернут вправо, или двигается вправо и объект повернут влево
            if ((moveHorizontal < 0 && transform.localScale.x > 0) ||
                (moveHorizontal > 0 && transform.localScale.x < 0))
            {
                // Перевернуть объект
                transform.localScale =
                    new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
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
            PlayerDieEvent?.Invoke();
        }
    }

    private void OnDisable()
    {
        EnemyController.HitEvent -= GetDamage;
    }
}