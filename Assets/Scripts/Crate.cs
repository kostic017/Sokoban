using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crate : MonoBehaviour
{
    [SerializeField]
    private int value = 1;

    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private Sprite[] placedSprites;

    private Tilemap walls;

    private SpriteRenderer spriteRenderer;

    private Crate[] crates;

    internal bool isPlaced;

    void Start()
    {
        crates = FindObjectsOfType<Crate>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        walls = GameObject.Find("Walls").GetComponent<Tilemap>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Marker>().value == value)
        {
            isPlaced = true;
            spriteRenderer.sprite = placedSprites[value - 1];
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isPlaced = false;
        spriteRenderer.sprite = sprites[value - 1];
    }

    public bool Move(Vector3 direction)
    {
        var position = transform.position + direction;
        var cell = walls.WorldToCell(position);

        if (walls.HasTile(cell))
            return false;

        if (crates.Any(crate => crate.transform.position == position))
            return false;

        SetValue(value + 1);
        transform.position = position;
        return true;
    }

    public void Undo(Vector3 position)
    {
        SetValue(value - 1);
        transform.position = position;
    }

    private void SetValue(int v)
    {
        value = v;
        if (value > 6) value = 1;
        if (value < 1) value = 6;
        spriteRenderer.sprite = isPlaced ? placedSprites[value - 1] : sprites[value - 1];
    }
}
