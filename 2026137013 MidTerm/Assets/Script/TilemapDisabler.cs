using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDisabler : MonoBehaviour
{
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        GetComponent<TilemapRenderer>().enabled = false;
    }
}
