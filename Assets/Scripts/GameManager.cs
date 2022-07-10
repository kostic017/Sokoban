using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public string solution;

    public float nextMoveDelay = 0.5f;

    public float nextLevelDelay = 1f;

    private Player player;

    private Crate[] crates;

    void Start()
    {
        player = FindObjectOfType<Player>();
        crates = FindObjectsOfType<Crate>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.U))
            player.Undo();

        if (solution != "" && Input.GetKeyDown(KeyCode.H))
            StartCoroutine(Solve());

        if (IsWin())
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
                StartCoroutine(LoadSceneAfterDelay(nextSceneIndex));
        }
    }

    bool IsWin()
    {
        return !crates.Any(crate => !crate.isPlaced);
    }

    IEnumerator Solve()
    {
        while (player.Undo()) ;
        foreach (var ch in solution)
        {
            yield return new WaitForSeconds(nextMoveDelay);
            switch (ch)
            {
                case 'U':
                    player.Move(Vector3.up);
                    break;
                case 'D':
                    player.Move(Vector3.down);
                    break;
                case 'L':
                    player.Move(Vector3.left);
                    break;
                case 'R':
                    player.Move(Vector3.right);
                    break;
            }
        }
    }

    IEnumerator LoadSceneAfterDelay(int buildIndex)
    {
        yield return new WaitForSeconds(nextLevelDelay);
        SceneManager.LoadScene(buildIndex);
    }
}
