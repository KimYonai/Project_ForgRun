using UnityEngine;

public class ItemStack : MonoBehaviour
{
    [SerializeField] private float speed;

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
