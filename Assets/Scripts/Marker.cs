using UnityEngine;

public class Marker : MonoBehaviour
{
    public int value = 1;

    [SerializeField]
    private Sprite[] sprites;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[value - 1];
    }
}
