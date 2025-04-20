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
        Debug.Log($"Player Position: {player.transform.position}"); // 플레이어 위치 출력
        Debug.Log("<color=Red>Enter Die</color>");

        // SpriteRenderer가 있으면 상태도 출력
        var renderer = player.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            Debug.Log($"Renderer enabled: {renderer.enabled}"); // Renderer 상태 출력
        }

        player.gameObject.SetActive(false);
    }

    public void Update() { }

    public void Exit()
    {
        Debug.Log("<color=Red>Exit Die</color>");
    }
}