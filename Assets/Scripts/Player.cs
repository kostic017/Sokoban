using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{   

    struct MoveInfo
    {
        public Vector3 from;
        public Vector3 to;
        public Crate crate;
    }

    private Tilemap walls;

    private Crate[] crates;

    private readonly Stack<MoveInfo> moves = new();

    void Start()
    {
        crates = FindObjectsOfType<Crate>();
        walls = GameObject.Find("Walls").GetComponent<Tilemap>();
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

        var moveInfo = new MoveInfo();

        foreach (var crate in crates)
        {
            if (crate.transform.position == position)
            {
                if (!crate.Move(direction))
                    return;
                moveInfo.crate = crate;
            }
        }

        moveInfo.from = transform.position;
        moveInfo.to = position;
        moves.Push(moveInfo);

        transform.position = position;
    }

    public void Undo()
    {
        if (moves.Count > 0)
        {
            var moveInfo = moves.Pop();
            transform.position = moveInfo.from;
            if (moveInfo.crate != null)
                moveInfo.crate.transform.position = moveInfo.to;
        }
    }

}
