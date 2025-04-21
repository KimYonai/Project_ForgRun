/// <summary>
/// 플레이어의 상태 인터페이스. 각 상태 클래스는 Enter, Update, Exit 메서드를 구현해야 함
/// </summary>
public interface IPlayerState
{
    void Enter(PlayerController player);    // 상태 진입 시 호출
    void Update();                          // 매 프레임 호출
    void Exit();                            // 상태 종료 시 호출
}
