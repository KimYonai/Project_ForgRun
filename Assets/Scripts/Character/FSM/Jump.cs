using UnityEngine;

/// <summary>
/// 플레이어의 Jump 상태 클래스
/// </summary>
public class Jump : IPlayerState
{
    private PlayerController player;
    private Rigidbody2D rigid;
    private Animator animator;

    public void Enter(PlayerController player)
    {
        this.player = player;
        rigid = player.Rigid;
        animator = player.Animator;

        Debug.Log("<color=Yellow>Enter Jump</color>");
        rigid.AddForce(Vector2.up * player.Model.JumpForce, ForceMode2D.Impulse);
    }

    public void Update()
    {
        // 점프 시 상승 중/하강 중 애니메이션 분기
        if (rigid.velocity.y > 0.01f)
        {
            animator.Play(PlayerController.JumpHash);
        }
        else if (rigid.velocity.y < -0.01f)
        {
            animator.Play(PlayerController.FallHash);
        }

        // 땅에 닿았을 경우 Idle 상태로 전환
        if (player.IsGrounded())
        {
            player.ChangeState(new Idle());
        }
    }

    public void Exit()
    {
        Debug.Log("<color=Yellow>Exit Jump</color>");
    }
}