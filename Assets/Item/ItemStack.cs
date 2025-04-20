using UnityEngine;

public class ItemStack : MonoBehaviour
{
    public Item item;
    private float offset = 0.1f;

    [SerializeField] float speed;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * 3 * Time.deltaTime);

        if (IsOutOfView())
        {
            gameObject.SetActive(false);
        }
    }

    private bool IsOutOfView()
    {
        float leftEnd = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        return transform.position.x < leftEnd - 1f;
    }
}
