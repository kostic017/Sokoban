using UnityEngine;
using UnityEngine.Tilemaps;

public class Crate : MonoBehaviour
{
public Sprite placedCrateSprite;
    
    private Tilemap walls;

    private SpriteRenderer spriteRenderer;

    private Sprite crateSprite;

    private Crate[] crates;

    internal bool isPlaced;

    void Start()
    {
        crates = FindObjectsOfType<Crate>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        walls = GameObject.Find("Walls").GetComponent<Tilemap>();
        crateSprite = spriteRenderer.sprite;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isPlaced = true;
        spriteRenderer.sprite = placedCrateSprite;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isPlaced = false;
        spriteRenderer.sprite = crateSprite;
    }

    public bool Move(Vector3 direction)
    {
        var position = transform.position + direction;
        var cell = walls.WorldToCell(position);

        if (walls.HasTile(cell))
            return false;

        foreach (var crate in crates)
            if (crate.transform.position == position)
                return false;

        transform.position = position;
        return true;
    }
}
