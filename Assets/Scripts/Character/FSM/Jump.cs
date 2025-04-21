using UnityEngine;

/// <summary>
/// 플레이어의 Jump 상태 클래스
/// </summary>
public class Jump : IPlayerState
{
    private PlayerController player;
    private Rigidbody2D rigid;
    private Animator animator;

    private float coyoteTime = 0.1f; // 안전 여유 시간
    private float elapsedTime;

    public void Enter(PlayerController player)
    {
        this.player = player;
        rigid = player.Rigid;
        animator = player.Animator;
        elapsedTime = 0f;
        Debug.Log($"Player Position Before Jump: {player.transform.position}"); // 점프 전 위치 출력

        Debug.Log("<color=Yellow>Enter Jump</color>");
        rigid.AddForce(Vector2.up * player.Model.JumpForce, ForceMode2D.Impulse);
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;

        if (rigid.velocity.y > 0.01f)
        {
            animator.Play(PlayerController.JumpHash);
        }
        else if (rigid.velocity.y < -0.01f)
        {
            animator.Play(PlayerController.FallHash);
        }

        // 일정 시간 이후에만 착지 체크
        if (elapsedTime > coyoteTime && player.IsGrounded())
        {
            player.ChangeState(new Idle());
        }
    }

    public void Exit()
    {
        Debug.Log("<color=Yellow>Exit Jump</color>");
    }
}