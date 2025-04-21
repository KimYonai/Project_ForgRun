using UnityEngine;

/// <summary>
/// FSM 구조 기반의 플레이어 컨트롤러 클래스
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerModel model;                       // 플레이어 데이터 모델
    [SerializeField] private Rigidbody2D rigid;                        // 플레이어의 Rigidbody2D 컴포넌트
    [SerializeField] private BoxCollider2D playerCollider;            // 플레이어의 BoxCollider2D 컴포넌트
    [SerializeField] private Animator animator;                        // 플레이어의 Animator 컴포넌트

    private IPlayerState currentState;                                 // 현재 상태 클래스

    public static int IdleHash = Animator.StringToHash("Idle");       // 애니메이션 해시값들
    public static int JumpHash = Animator.StringToHash("Jump");
    public static int FallHash = Animator.StringToHash("Fall");
    public static int SlideHash = Animator.StringToHash("Slide");
    public static int DamageHash = Animator.StringToHash("Damage");

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;                   // 지면 확인용 위치
    [SerializeField] private LayerMask groundLayer;                   // 지면 레이어
    private float rayLength = 0.6f;                                   // 레이 길이

    public bool IsInvincible { get; set; }                            // 무적 상태 여부

    public PlayerModel Model => model;
    public Rigidbody2D Rigid => rigid;
    public Animator Animator => animator;
    public BoxCollider2D Collider => playerCollider;

    private void Awake()
    {
        ChangeState(new Idle());
    }

    private void Update()
    {
        HandleInput();
        currentState?.Update();
    }

    /// <summary>
    /// 상태를 변경하는 함수
    /// </summary>
    /// <param name="newState">새로운 상태 클래스</param>
    public void ChangeState(IPlayerState newState)
    {
        // 동일 상태로의 중복 전환 방지
        if (currentState != null && currentState.GetType() == newState.GetType()) return;

        Debug.Log($"<color=white>State Change: {currentState?.GetType().Name} → {newState.GetType().Name}</color>");
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    /// <summary>
    /// 키 입력에 따라 상태 전환을 처리하는 함수 (테스트용)
    /// </summary>
    private void HandleInput()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetKeyDown(KeyCode.Q))
        {
            if (IsGrounded()) { ChangeState(new Jump()); }
        }
        else if (model.HP <= 0 || Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Player HP <= 0. Transitioning to Die state.");
            ChangeState(new Die());
        }

        // 에디터 or PC 테스트용 키 입력
        #if Unity_Editor || Unity_StandLone
        if (Input.GetKeyDown(KeyCode.Q)) { ChangeState(new Idle()); }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (IsGrounded()) { ChangeState(new Jump()); }
        }
        else if (Input.GetKeyDown(KeyCode.D)) { ChangeState(new Damage()); }
        else if (Input.GetKeyDown(KeyCode.R)) { ChangeState(new Die()); }
        #endif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            ChangeState(new Damage());
        }
        else if (collision.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            model.CoinCount++;
        }
    }

    /// <summary>
    /// 플레이어가 지면에 닿아 있는지 판별하는 함수
    /// </summary>
    /// <returns>지면에 닿아 있으면 true 반환</returns>
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }
}