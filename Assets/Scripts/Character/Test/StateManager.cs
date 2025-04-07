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
        else if (Input.GetKeyDown(KeyCode.R)) { ChangeState(PlayerState.Die); }

        // TODO : 각 행동 이후 진행되는 행동 및 조건에 따라 상태가 변경되도록 구현
    }

    /// <summary>
    /// 플레이어의 상태를 newState로 변경하는 함수
    /// </summary>
    /// <param name="newState"></param>
    private void ChangeState(PlayerState newState)
    {
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

        while (true)
        {
            // TODO : Jump 상태일 때 진행할 행동 기능 추가
            Debug.Log("Jump State");
            yield return null;
        }
    }

    /// <summary>
    /// Slide 상태 행동 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator Slide()
    {
        Debug.Log("<color=Orange>Change State : Slide</color>");

        while (true)
        {
            // TODO : Slide 상태일 때 진행할 행동 기능 추가
            Debug.Log("Slide State");
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
}
