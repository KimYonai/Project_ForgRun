using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerModel model;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator animator;

    public Animator Animator => animator;
    public Rigidbody2D Rigid => rigid;
    public BoxCollider2D Collider => playerCollider;
    public Transform GroundCheck => groundCheck;
    public LayerMask GroundLayer => groundLayer;
    public PlayerModel Model => model;

    public static readonly int IdleHash = Animator.StringToHash("Idle");
    public static readonly int JumpHash = Animator.StringToHash("Jump");
    public static readonly int FallHash = Animator.StringToHash("Fall");
    public static readonly int SlideHash = Animator.StringToHash("Slide");
    public static readonly int DamageHash = Animator.StringToHash("Damage");

    public bool IsInvincible { get; set; }

    private IPlayerState currentState;

    private void Start()
    {
        ChangeState(new Idle());
    }

    private void Update()
    {
        currentState?.Update();

        // 상태 변경 테스트용 키 입력
        //if (Input.GetKeyDown(KeyCode.Q)) ChangeState(new Idle());
        //else if (Input.GetKeyDown(KeyCode.W) && IsGrounded()) ChangeState(new Jump());
        //else if (Input.GetKeyDown(KeyCode.E) && IsGrounded()) ChangeState(new Slide());
        //else if (Input.GetKeyDown(KeyCode.D)) ChangeState(new Damage());
        //else if (Input.GetKeyDown(KeyCode.R)) ChangeState(new Die());
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.6f, groundLayer);
        return hit.collider != null;
    }
}
