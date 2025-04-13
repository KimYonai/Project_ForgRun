using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;                    // 장애물 프리팹들
    [SerializeField] int poolSize = 5;                                  // 장애물 프리팹 전체 개수

    private Queue<GameObject> obstacleQueue = new Queue<GameObject>();  // 장애물 풀링 큐
    private List<GameObject> activeObstacles = new List<GameObject>();  // 활성화된 장애물 체크용 리스트

    [SerializeField] private float spawnOffset;                         // 카메라 바깥쪽에 장애물을 얼마나 더 생성할지 결정하는 값
    [SerializeField] private float distance;                            // 장애물 생성 간격
    [SerializeField] private float lastSpawnX;                          // 마지막으로 장애물을 생성한 위치          

    private void Start()
    {
        // obstacles에 있는 프리팹 중 하나를 랜덤하게 큐에 추가
        for (int i = 0; i < poolSize; i++)
        {
            // obstacles 배열 내 랜덤한 프리팹을 지정
            GameObject gameObject = obstacles[Random.Range(0, obstacles.Length)];
            // 랜덤으로 지정한 프리팹을 생성
            GameObject obj = Instantiate(gameObject);
            // 생성한 프리팹을 비활성화 처리
            obj.SetActive(false);
            // 생성한 프리팹을 오브젝트 풀링 큐에 추가
            obstacleQueue.Enqueue(obj);
        }
    }

    private void Update()
    {
        // 메인 카메라의 오른쪽 바깥 위치를 화면의 비율에 맞게 설정
        float cameraRight = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;

        int maxCount = 20;
        int count = 0;

        // 마지막 장애물 생성 스폰 위치 X 좌표가 카메라 오른쪽 바깥 + spawnOffset 위치보다 작을 때 반복
        while (lastSpawnX < cameraRight + spawnOffset && count < maxCount)
        {
            SpawnObstacle(lastSpawnX);  // lastSpawnX 위치에 장애물 생성
            lastSpawnX += distance;     // lastSpawnX 값을 distance만큼 추가
            count++;
        }

        // 장애물 제거
        RemoveObstacles();
    }

    private void SpawnObstacle(float x)
    {
        // 장애물 풀링 큐가 비었을 때 예외처리
        if (obstacleQueue.Count == 0) return;

        GameObject obj = obstacleQueue.Dequeue();                   // 생성할 오브젝트를 장애물 풀링 큐에서 내보내기

        // 테스트용 코드 (추후 삭제 후 값 재설정 필요)
        float randomX = Random.Range(-10f, 10f);
        float randomY = Random.Range(-3f, 3f);

        obj.transform.position = new Vector2(randomX, randomY);     // 생성할 오브젝트의 위치 설정 (필요 시 y 좌표값 수정)
        obj.SetActive(true);                                        // 생성할 오브젝트를 활성화 처리

        activeObstacles.Add(obj);                                   // 생성한 오브젝트를 활성화된 오브젝트 체크를 위해 리스트에 추가
    }

    private void RemoveObstacles()
    {
        // 메인 카메라의 왼쪽 바깥 위치를 화면의 비율에 맞게 설정
        float cameraLeft = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        // 카메라 왼쪽으로 나가 시야에 보이지 않은 장애물 처리에 대한 반복문
        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            // 활성화된 오브젝트 지정
            GameObject obj = activeObstacles[i];

            // 활성화된 오브젝트의 위치가 카메라 왼쪽 바깥 - 1의 좌표 값일 경우
            if (obj.transform.position.x < cameraLeft - 1f)
            {
                activeObstacles.RemoveAt(i);    // 활성화된 오브젝트 리스트에서 오브젝트 삭제
                Destroy(obj);                   // 활성화된 오브젝트를 파괴
            }
        }
    }
}
