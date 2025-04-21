using UnityEngine;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int count;
    public bool stackable;

    public enum ItemType { coin, health }
    public ItemType type;
}
