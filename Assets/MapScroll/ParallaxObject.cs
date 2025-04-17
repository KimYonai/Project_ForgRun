using System.Collections;
using System.Collections.Generic;
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

    public void Move(float scrollSpeed)
    {
        transform.Translate(Vector3.left * speed * scrollSpeed *  Time.deltaTime);

        if (isLooping)
        {
            if (transform.position.x <= startPos.x - tileWidth)
            {
                transform.position += Vector3.right * tileWidth * 2f;
            }
        }  
    }
}
