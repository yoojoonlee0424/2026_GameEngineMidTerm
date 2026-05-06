using UnityEngine;

[CreateAssetMenu(fileName = "Game/Item", menuName = "NewItem")]
public class ItemSO : ScriptableObject
{
    [Header("Score Value")]
    public int point = 10;
}
