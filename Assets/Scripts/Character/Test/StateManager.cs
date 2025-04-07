using System.Collections;
using UnityEngine;

public enum PlayerState { Idle, Jump, Slide, Die }      // 플레이어의 상태 열거형

public class StateManager : MonoBehaviour
{
    private PlayerState state;          // 플레이어의 상태 열거형을 받아오기 위한 변수
    private Coroutine currentState;     // 현재 상태 코루틴 변수

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
    /// 플레이어의 상태를 newState로 변경하는 함수
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(PlayerState newState)
    {
        if (currentState != null) { StopCoroutine(currentState); }
        state = newState;
        currentState = StartCoroutine(state.ToString());
    }

    private IEnumerator Idle()
    {
        Debug.Log("<color=Green>Change State : Idle</color>");

        while (true)
        {
            Debug.Log("Idle State");
            yield return null;
        }
    }

    private IEnumerator Jump()
    {
        Debug.Log("<color=Yellow>Change State : Jump</color>");

        while (true)
        {
            Debug.Log("Jump State");
            yield return null;
        }
    }

    private IEnumerator Slide()
    {
        Debug.Log("<color=Orange>Change State : Slide</color>");

        while (true)
        {
            Debug.Log("Slide State");
            yield return null;
        }
    }

    private IEnumerator Die()
    {
        Debug.Log("<color=Red>Change State : Die</color>");

        while (true)
        {
            Debug.Log("Die State");
            yield return null;
        }
    }
}
