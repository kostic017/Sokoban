using System.Collections.Generic;
using System.Linq;
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

    private AudioSource audioSource;

    private readonly Stack<MoveInfo> moves = new();

    void Start()
    {
        crates = FindObjectsOfType<Crate>();
        audioSource = GetComponent<AudioSource>();
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

    public void Move(Vector3 direction)
    {
        var position = transform.position + direction;
        var cell = walls.WorldToCell(position);

        if (walls.HasTile(cell))
            return;

        var moveInfo = new MoveInfo();
        var crate = crates.FirstOrDefault(crate => crate.transform.position == position);

        if (crate != null && !crate.Move(direction))
            return;

        moveInfo.from = transform.position;
        moveInfo.to = position;
        moveInfo.crate = crate;
        moves.Push(moveInfo);

        transform.position = position;
        audioSource.Play();
    }

    public bool Undo()
    {
        if (moves.Count == 0)
            return false;
     
        var moveInfo = moves.Pop();
        transform.position = moveInfo.from;
        if (moveInfo.crate != null)
            moveInfo.crate.Undo(moveInfo.to);

        return true;
    }

}
