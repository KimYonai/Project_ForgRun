using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;                    // 장애물 프리팹들
    [SerializeField] int poolSize = 5;                                  // 장애물 풀링 수

    private Queue<GameObject> obstacleQueue = new Queue<GameObject>();  // 장애물 풀링 큐
    private List<GameObject> activeObstacles = new List<GameObject>();  // 활성화된 장애물 리스트

    [SerializeField] private float spawnOffset;                         // 카메라 오른쪽 외부에서 얼마나 떨어진 위치에 생성할지 결정
    [SerializeField] private float distance;                            // 장애물 생성 간격
    [SerializeField] private float minSpacing;                          // 장애물 간 최소 거리

    [SerializeField] private float minSpawnX;                           // 장애물 생성 가능한 최소 X 위치
    [SerializeField] private float maxSpawnX;                           // 장애물 생성 가능한 최대 X 위치

    private float nextSpawnX;                                           // 다음 장애물을 생성할 X 위치

    private void Start()
    {
        // 장애물 풀 초기화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab = obstacles[Random.Range(0, obstacles.Length)]; // 랜덤 프리팹 선택
            GameObject obj = Instantiate(prefab);                             // 인스턴스 생성
            obj.SetActive(false);                                             // 비활성화
            obstacleQueue.Enqueue(obj);                                       // 큐에 저장
        }

        //// 초기 생성 위치 설정
        //float cameraRight = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        nextSpawnX = Mathf.Max(minSpawnX, Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect + spawnOffset);  // 시작 시점 기준으로 설정
    }

    private void Update()
    {
        // 메인 카메라의 오른쪽 경계 계산
        float cameraRight = Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        //Debug.Log("cameraRight: " + cameraRight + " nextSpawnX: " + nextSpawnX);

        float spawnX = cameraRight + spawnOffset;

        // nextSpawnX가 현재 카메라 오른쪽 + offset보다 왼쪽에 있다면 반복해서 생성
        for (int i = 0; i < 1; i++)
        {
            // nextSpawnX가 특정 X 범위 내에 있을 때만 생성
            if (nextSpawnX >= minSpawnX && nextSpawnX <= maxSpawnX)
            {
                //Debug.Log("SpawnObstacle 호출됨: x = " + nextSpawnX);
                SpawnObstacle(nextSpawnX);
            }

            nextSpawnX += distance; // 다음 생성 위치 설정

            // nextSpawnX 값이 maxSpawnX를 넘으면 리셋
            if (nextSpawnX > maxSpawnX)
            {
                nextSpawnX = minSpawnX; // 리셋
            }
        }

        // 장애물 제거 처리
        RemoveObstacles();
    }

    /// <summary>
    /// 장애물을 카메라 오른쪽 외부에 생성
    /// </summary>
    /// <param name="x">생성할 X 위치</param>
    private void SpawnObstacle(float x)
    {
        // 50% 확률로 생성하지 않음
        if (Random.value > 0.5f)
        {
            //Debug.Log("확률로 인해 생성되지 않음");
            return;
        }

        // 풀에 사용 가능한 오브젝트가 없으면 생성하지 않음
        if (obstacleQueue.Count == 0)
        {
            //Debug.Log("큐가 비어있음");
            return;
        }

        // 기존 활성화된 장애물들과의 간격 확인
        foreach (GameObject obj in activeObstacles)
        {
            if (Mathf.Abs(obj.transform.position.x - x) < minSpacing)
            {
                // 너무 가까우면 생성하지 않음
                return;
            }
        }

        GameObject objToSpawn = obstacleQueue.Dequeue(); // 풀에서 꺼냄

        // 장애물의 Y 위치 랜덤 결정 (예: -3 또는 2)
        float y = (Random.value < 0.5f) ? -3f : 2f;

        objToSpawn.transform.position = new Vector2(x, y); // 위치 설정
        objToSpawn.SetActive(true);                        // 활성화
        activeObstacles.Add(objToSpawn);                   // 리스트에 추가
        //Debug.Log($"장애물 생성 : {objToSpawn.name} 오브젝트가 {objToSpawn.transform.position} 위치에 생성");

        // ParallaxObject 등록
        ParallaxObject po = objToSpawn.GetComponent<ParallaxObject>();
        if (po != null)
        {
            GameObject.FindObjectOfType<ParallaxManager>().Register(po);
        }
    }

    /// <summary>
    /// 카메라 왼쪽으로 벗어난 장애물을 비활성화하고 풀로 반환
    /// </summary>
    private void RemoveObstacles()
    {
        float cameraLeft = Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obj = activeObstacles[i];
            //Debug.Log($"활성화된 오브젝트 : {obj.name} / {obj.transform.position.x}");

            // 왼쪽 화면 바깥으로 벗어난 경우 처리
            if (obj.transform.position.x < cameraLeft)
            {
                activeObstacles.RemoveAt(i);  // 리스트에서 제거
                obj.SetActive(false);         // 비활성화
                obstacleQueue.Enqueue(obj);   // 다시 풀에 넣기
            }
        }
    }
}
