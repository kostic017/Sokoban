using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private Player player;
    private Crate[] crates;

    void Start()
    {
        player = FindObjectOfType<Player>();
        crates = FindObjectsOfType<Crate>();
    }

    void Update()
    {
        if (IsWin())
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
                StartCoroutine(LoadSceneAfterDelay(nextSceneIndex, 2f));
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (Input.GetKeyDown(KeyCode.U))
            player.Undo();
    }

    bool IsWin()
    {
        foreach (var crate in crates)
            if (!crate.isPlaced)
                return false;
        return true;
    }

    IEnumerator LoadSceneAfterDelay(int buildIndex, float delay)
    {
        if (delay > 0)
            yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(buildIndex);
        yield break;
    }
}
