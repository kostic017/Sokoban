using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Tilemap walls;

    private Crate[] crates;

    void Start()
    {
        crates = FindObjectsOfType<Crate>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Move(Vector3.up);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            Move(Vector3.down);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Vector3.left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector3.right);
    }

    void Move(Vector3 direction)
    {
        var position = transform.position + direction;
        var cell = walls.WorldToCell(position);

        if (walls.HasTile(cell))
            return;
        
        foreach (var crate in crates)
            if (crate.transform.position == position && !crate.Move(direction))
                return;
        
        transform.position = position;
    }
}
