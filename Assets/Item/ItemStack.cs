using UnityEngine;

public class ItemStack : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Item itemData; // 스크립터블 오브젝트 참조

    private void Awake()
    {
        if (itemData != null)
        {
            gameObject.name = itemData.name; // 게임 오브젝트 이름 설정
        }
        else
        {
            Debug.LogWarning($"Item data not assigned on {gameObject.name}");
        }
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * 3f * Time.deltaTime);

        if (IsOutOfView())
        {
            gameObject.SetActive(false);

            // 아이템 스포너에 알림
            if (ItemSpawner.Instance != null)
                ItemSpawner.Instance.OnItemDisabled();
        }
    }

    private bool IsOutOfView()
    {
        float leftEnd = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        return transform.position.x < leftEnd - 1f;
    }
}
