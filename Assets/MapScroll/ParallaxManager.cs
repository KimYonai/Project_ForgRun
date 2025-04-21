using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ParallaxManager : MonoBehaviour
{
    public static ParallaxManager Instance { get; private set; }

    [SerializeField] private float scrollSpeed;
    private List<ParallaxObject> objects = new List<ParallaxObject>();

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        // 현재 씬이 게임 씬이 아닐 경우 패럴랙스 중지
        if (SceneManager.GetActiveScene().name != "GameScene")
            return;

        // 파괴된 오브젝트를 제거하면서 Move 호출
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            var obj = objects[i];
            if (obj == null)
            {
                objects.RemoveAt(i); // 파괴된 참조 제거
                continue;
            }

            if (obj.gameObject.activeInHierarchy)
            {
                obj.Move(scrollSpeed);
            }
        }
    }

    public void Register(ParallaxObject obj)
    {
        if (!objects.Contains(obj))
        {
            objects.Add(obj);
        }
    }

    public void Unregister(ParallaxObject obj)
    {
        objects.Remove(obj);
    }
}
