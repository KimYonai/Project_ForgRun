using UnityEngine;

/// <summary>
/// 플레이어의 Idle 상태 클래스
/// </summary>
public class Idle : IPlayerState
{
    private PlayerController player;
    private Animator animator;

    public void Enter(PlayerController player)
    {
        this.player = player;
        animator = player.Animator;
        animator.Play(PlayerController.IdleHash);
        Debug.Log("<color=Green>Enter Idle</color>");
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log("<color=Green>Exit Idle</color>");
    }
}
