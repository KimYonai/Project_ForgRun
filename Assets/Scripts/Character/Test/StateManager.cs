using UnityEngine;

public enum PlayerState { Idle, Jump, Slide, Die }      // 플레이어의 상태 열거형

public class StateManager : MonoBehaviour
{
    private PlayerState state;

    private void Awake()
    {
        // 처음 상태를 Idle 상태로 설정
        ChangeState(PlayerState.Idle);
    }

    private void Update()
    {
        // 키 입력을 통해 플레이어의 상태 변경
        if (Input.GetKeyDown(KeyCode.Q)) { ChangeState(PlayerState.Idle); }
        else if (Input.GetKeyDown(KeyCode.W)) { ChangeState(PlayerState.Jump); }
        else if (Input.GetKeyDown(KeyCode.E)) { ChangeState(PlayerState.Slide); }
        else if (Input.GetKeyDown(KeyCode.R)) {  ChangeState(PlayerState.Die); }
    }

    /// <summary>
    /// 플레이어의 상태 업데이트용 함수
    /// PlayerState에 따라 현재 플레이어의 행동 실행
    /// </summary>
    private void UpdateState()
    {
        switch (state)
        {
            case PlayerState.Idle:
                Debug.Log("Idle State");
                break;
            case PlayerState.Jump:
                Debug.Log("Jump State");
                break;
            case PlayerState.Slide:
                Debug.Log("Slide State");
                break;
            case PlayerState.Die:
                Debug.Log("Die State");
                break;
        }
    }

    /// <summary>
    /// 플레이어의 상태를 newState로 변경하는 함수
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(PlayerState newState)
    {
        state = newState;
    }
}
