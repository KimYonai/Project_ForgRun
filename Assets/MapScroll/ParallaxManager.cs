using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    private List<ParallaxObject> objects = new List<ParallaxObject>();

    private void Start()
    {
        ParallaxObject[] objs = FindObjectsOfType<ParallaxObject>();
        objects.AddRange(objs);
    }

    private void Update()
    {
        foreach (ParallaxObject obj in objects)
        {
            obj.Move(scrollSpeed);
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
        if (objects.Contains(obj))
        {
            objects.Remove(obj);
        }
    }
}
