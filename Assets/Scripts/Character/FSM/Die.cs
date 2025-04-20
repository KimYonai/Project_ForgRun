using UnityEngine;

/// <summary>
/// 플레이어의 Die 상태 클래스
/// </summary>
public class Die : IPlayerState
{
    private PlayerController player;

    public void Enter(PlayerController player)
    {
        this.player = player;
        Debug.Log("<color=Red>Enter Die</color>");
        player.gameObject.SetActive(false);
    }

    public void Update() { }

    public void Exit()
    {
        Debug.Log("<color=Red>Exit Die</color>");
    }
}