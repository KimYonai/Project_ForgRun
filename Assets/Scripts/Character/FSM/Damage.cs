using UnityEngine;

/// <summary>
/// 플레이어의 Damage 상태 클래스
/// </summary>
public class DamageState : IPlayerState
{
    private PlayerController player;
    private float invincibleTime = 1f;   // 무적 시간
    private float timer = 0f;            // 타이머

    public void Enter(PlayerController player)
    {
        this.player = player;

        // 무적 상태이면 리턴
        if (player.IsInvincible)
        {
            Debug.Log("무적 상태입니다");
            return;
        }

        Debug.Log("<color=Blue>Enter Damage</color>");
        player.IsInvincible = true;
        player.Model.HP -= 1;
        player.Animator.Play(PlayerController.DamageHash);

        // 체력이 0 이하이면 Die 상태로 전환
        if (player.Model.HP <= 0)
        {
            player.ChangeState(new Die());
        }
    }

    public void Update()
    {
        // 무적 시간 경과 확인 후 Idle 상태로 전환
        if (player.Model.HP <= 0) return;

        timer += Time.deltaTime;
        if (timer >= invincibleTime)
        {
            player.IsInvincible = false;
            player.ChangeState(new Idle());
        }
    }

    public void Exit()
    {
        Debug.Log("<color=Blue>Exit Damage</color>");
    }
}
