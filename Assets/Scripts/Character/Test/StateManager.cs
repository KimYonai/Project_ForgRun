/*using System.Collections;
using UnityEngine;

//public enum PlayerState { Idle, Jump, Slide, Damage, Die }      // 플레이어의 상태 열거형

public class StateManager : MonoBehaviour
{
    private PlayerState state;                      // 플레이어의 상태 열거형을 받아오기 위한 변수
    private Coroutine currentState;                 // 현재 상태 코루틴 변수
    [SerializeField] private Rigidbody2D rigid;     // 플레이어의 rigidbody
    [SerializeField] private float jumpForce;       // 플레이어의 점프력

    [SerializeField] private Transform groundCheck;
    private float rayLength = 0.6f;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private BoxCollider2D playerCollider;
    private float slideHeight = 0.4f;
    private float colliderHeight;
    private Vector2 colliderOffset;

    private void Awake()
    {
        // 콜라이더의 높이와 오프셋을 플레이어와 동일하게 동기화
        // TODO : 추후 에셋 적용 시 플레이어의 기본 오프셋을 발 밑으로 설정 후 오프셋 관련 코드는 삭제
        colliderHeight = playerCollider.size.y;
        colliderOffset = playerCollider.offset;

        // 처음 상태를 Idle 상태로 설정
        ChangeState(PlayerState.Idle);
    }

    private void Update()
    {
        // 키 입력을 통해 플레이어의 상태 변경 (테스트용 코드)
        // TODO : 각 상태에 맞는 키 입력 및 조건으로 조건문 수정
        if (Input.GetKeyDown(KeyCode.Q)) { ChangeState(PlayerState.Idle); }
        else if (Input.GetKeyDown(KeyCode.W)) 
        { 
            if (state != PlayerState.Jump && IsGrounded()) { ChangeState(PlayerState.Jump); }
        }
        else if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (state != PlayerState.Slide && IsGrounded()) { ChangeState(PlayerState.Slide); }
        }
        else if (Input.GetKeyDown(KeyCode.R)) { ChangeState(PlayerState.Die); }
    }

    /// <summary>
    /// 플레이어의 상태를 newState로 변경하는 함수
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(PlayerState newState)
    {
        if (state == newState) return;

        // 현재 행동 중인 상태가 있을 경우 해당 행동의 코루틴 정지
        if (currentState != null) { StopCoroutine(currentState); }
        // 플레이어의 상태를 newState로 변경
        state = newState;

        // 현재 상태에 맞는 코루틴 실행
        currentState = StartCoroutine(state.ToString());
    }

    /// <summary>
    /// Idle 상태 행동 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Idle()
    {
        Debug.Log("<color=Green>Change State : Idle</color>");

        while (true)
        {
            // TODO : Idle 상태일 때 진행할 행동 기능 추가
            Debug.Log("Idle State");
            yield return null;
        }
    }

    /// <summary>
    /// Jump 상태 행동 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Jump()
    {
        Debug.Log("<color=Yellow>Change State : Jump</color>");
        // 위쪽 방향으로 jumpForce만큼 점프
        rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // IsGrounded의 반환값이 false일 때
        while (!IsGrounded())
        {
            // null 값을 반환하여 점프 동작 실행 X
            yield return null;
        }

        // Idle 상태로 전환
        ChangeState(PlayerState.Idle);
    }

    /// <summary>
    /// Slide 상태 행동 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Slide()
    {
        Debug.Log("<color=Orange>Change State : Slide</color>");

        // 플레이어의 콜라이더 높이를 slideHeight만큼 낮추기
        // TODO : 추후 에셋 적용 시 플레이어의 기본 오프셋을 발 밑으로 설정 후 오프셋 관련 코드는 삭제
        playerCollider.size = new Vector2(playerCollider.size.x, slideHeight);
        playerCollider.offset = new Vector2(playerCollider.offset.x, slideHeight / 2);

        while (Input.GetKey(KeyCode.E))
        {
            // TODO : Slide 상태일 때 진행할 행동 기능 추가
            Debug.Log("Slide State");
            yield return null;
        }

        // 플레이어의 콜라이더 높이를 원상복구
        // TODO : 추후 에셋 적용 시 플레이어의 기본 오프셋을 발 밑으로 설정 후 오프셋 관련 코드는 삭제
        playerCollider.size = new Vector2(playerCollider.size.x, colliderHeight);
        playerCollider.offset = colliderOffset;

        // Idle 상태로 전환
        ChangeState(PlayerState.Idle);
    }

    /// <summary>
    /// Damage 상태 행동 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Damage()
    {
        Debug.Log("<color=Blue>Change State : Damage</color>");

        while (true)
        {
            // TODO : Damage 상태일 때 진행할 행동 기능 추가
            Debug.Log("Damage State");
            yield return null;
        }
    }

    /// <summary>
    /// Die 상태 행동 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Die()
    {
        Debug.Log("<color=Red>Change State : Die</color>");

        while (true)
        {
            // TODO : Die 상태일 때 진행할 행동 기능 추가
            Debug.Log("Die State");
            yield return null;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }
}
*/