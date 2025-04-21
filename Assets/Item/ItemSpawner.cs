using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static ItemSpawner Instance { get; private set; }

    [Header("Item Settings")]
    public GameObject itemPrefab;       // 생성할 아이템 프리팹
    public int poolSize = 10;           // 오브젝트 풀 크기
    public float spawnInterval = 3f;    // 아이템 생성 간격 (x 축)
    public float spawnY = 0f;           // 아이템 Y 위치
    public float scrollSpeed = 5f;      // 아이템 이동 속도
    public float spawnOffsetX = 30f;    // 카메라 오른쪽 끝으로부터 여유 거리

    [Header("Limit")]
    public int maxActiveItems = 5;      // 동시에 활성화 가능한 아이템 최대 개수

    private List<GameObject> itemPool = new List<GameObject>(); // 오브젝트 풀 리스트
    private float nextSpawnX;           // 다음 생성할 X 위치
    private float elapsedTime;          // 시간 누적용
    private int currentActiveCount = 0; // 현재 활성화된 아이템 수

    public static float ScrollSpeed { get; private set; } // 외부에서 접근 가능한 속도

    private void Awake()
    {
        // 싱글톤 초기화
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 이동 속도 static 변수에 저장 (ItemStack 등에서 접근 가능)
        ScrollSpeed = scrollSpeed;
    }

    void Start()
    {
        // 카메라 오른쪽 경계를 기준으로 첫 아이템 생성 위치 계산
        float cameraRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        nextSpawnX = cameraRightEdge + spawnOffsetX;

        // 오브젝트 풀 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(itemPrefab, transform);
            obj.SetActive(false);
            itemPool.Add(obj);
        }
    }

    void Update()
    {
        // 이동된 거리 계산 (시간 × 속도)
        elapsedTime += Time.deltaTime;
        float scrollPositionX = elapsedTime * scrollSpeed;

        // 스폰 조건 위치 (현재 위치 + 여유 거리)
        float spawnThresholdX = scrollPositionX + spawnOffsetX;

        // 조건을 만족할 때까지 아이템 생성 시도
        while (nextSpawnX < spawnThresholdX)
        {
            // 최대 활성 개수를 초과하면 생성 중단
            if (currentActiveCount >= maxActiveItems)
                break;

            // 풀에서 비활성화된 아이템 가져오기
            GameObject item = GetPooledItem();
            if (item != null)
            {
                // 아이템 위치 지정 및 활성화
                item.transform.position = new Vector2(nextSpawnX, spawnY);
                item.SetActive(true);

                // 상태 갱신
                currentActiveCount++;
                Debug.Log($"[ItemSpawner] Spawned item at X = {nextSpawnX}");

                // 오직 생성 성공 시에만 다음 위치로 이동
                nextSpawnX += spawnInterval;
            }
            else
            {
                // 오브젝트가 부족하면 반복 종료 → 다음 프레임에 재시도
                Debug.LogWarning("[ItemSpawner] No available item in pool!");
                break;
            }
        }
    }

    // 풀에서 비활성화된 아이템을 하나 반환
    GameObject GetPooledItem()
    {
        foreach (var item in itemPool)
        {
            if (!item.activeInHierarchy)
                return item;
        }
        return null;
    }

    // 아이템이 비활성화될 때 호출하여 카운트를 줄임
    public void OnItemDisabled()
    {
        currentActiveCount = Mathf.Max(0, currentActiveCount - 1);
    }
}
