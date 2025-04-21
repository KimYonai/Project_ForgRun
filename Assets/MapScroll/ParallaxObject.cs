using UnityEngine;

public class ParallaxObject : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isLooping;
    [SerializeField] private float tileWidth;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnEnable()
    {
        if (ParallaxManager.Instance != null)
        {
            ParallaxManager.Instance.Register(this);
        }
    }

    private void OnDisable()
    {
        if (ParallaxManager.Instance != null)
        {
            ParallaxManager.Instance.Unregister(this);
        }
    }

    public void Move(float scrollSpeed)
    {
        // 오브젝트가 파괴되었거나 비활성 상태면 처리 생략
        if (!gameObject.activeInHierarchy) return;

        transform.Translate(Vector3.left * speed * scrollSpeed * Time.deltaTime);

        if (isLooping && transform.position.x <= startPos.x - tileWidth)
        {
            transform.position += Vector3.right * tileWidth * 2f;
        }
    }
}
