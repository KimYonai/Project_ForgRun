using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public int poolSize = 10;
    public float spawnInterval = 3f;         // 아이템 간 간격
    public float spawnY = 0f;                // Y 위치 고정
    public float scrollSpeed = 5f;           // 아이템 이동 속도
    public float spawnOffsetX = 10f;         // 카메라 오른쪽 여유 거리

    private List<GameObject> itemPool = new List<GameObject>();
    private float nextSpawnX;
    private float elapsedTime;

    void Start()
    {
        // 카메라 오른쪽 가장자리에서 spawnInterval만큼 떨어진 곳부터 생성 시작
        float cameraRightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        nextSpawnX = cameraRightEdge + spawnInterval;

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
        // 얼마나 이동했는지를 추적
        elapsedTime += Time.deltaTime;
        float scrollPositionX = elapsedTime * scrollSpeed;

        // 아이템 생성 기준 위치 (스크롤된 위치 + 여유 거리)
        float spawnThresholdX = scrollPositionX + spawnOffsetX;

        while (nextSpawnX < spawnThresholdX)
        {
            SpawnItem(nextSpawnX, spawnY);
            nextSpawnX += spawnInterval;  // 일정 간격 유지
        }
    }

    void SpawnItem(float x, float y)
    {
        GameObject item = GetPooledItem();
        if (item != null)
        {
            item.transform.position = new Vector2(x, y);
            item.SetActive(true);
            Debug.Log($"[ItemManager] Spawned item at X = {x}");
        }
        else
        {
            Debug.LogWarning("[ItemManager] No available item in pool!");
        }
    }

    GameObject GetPooledItem()
    {
        foreach (var item in itemPool)
        {
            if (!item.activeInHierarchy)
                return item;
        }
        return null;
    }
}
