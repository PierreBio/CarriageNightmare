using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] EnemySpawner enemySpawner;
    [SerializeField] GameObject lycan;
    [SerializeField] GameObject vampire;

    static SpawnManager _instance;
    public static SpawnManager Instance { get { return _instance; } }
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Restart()
    {
        Start();
    }

    private void Start()
    {
        GameManager.OnGameRestart += Restart;
        // For Marie
        StartCoroutine(SpawnCoroutine(5, vampire, 1));

        StartCoroutine(SpawnCoroutine(13, lycan, 5));
        StartCoroutine(SpawnCoroutine(13, vampire, 5));

        StartCoroutine(SpawnCoroutine(26, lycan, 2));
        StartCoroutine(SpawnCoroutine(26, vampire, 4));

        StartCoroutine(SpawnCoroutine(39, vampire, 3));
        StartCoroutine(SpawnCoroutine(39, vampire, 3));

        StartCoroutine(SpawnCoroutine(52, lycan, 1));
        StartCoroutine(SpawnCoroutine(52, vampire, 3));

        StartCoroutine(SpawnCoroutine(65, lycan, 1));
        StartCoroutine(SpawnCoroutine(65, lycan, 3));
        StartCoroutine(SpawnCoroutine(65, vampire, 4));

        StartCoroutine(SpawnCoroutine(78, vampire, 2));
        StartCoroutine(SpawnCoroutine(78, vampire, 2));

        StartCoroutine(SpawnCoroutine(91, vampire, 1));
        StartCoroutine(SpawnCoroutine(91, lycan, 2));
        StartCoroutine(SpawnCoroutine(91, vampire, 5));

        StartCoroutine(SpawnCoroutine(104, vampire, 3));
        StartCoroutine(SpawnCoroutine(104, vampire, 3));

        StartCoroutine(SpawnCoroutine(117, vampire, 2));
        StartCoroutine(SpawnCoroutine(117, lycan, 3));
        StartCoroutine(SpawnCoroutine(117, vampire, 4));

        StartCoroutine(SpawnCoroutine(130, vampire, 1));
        StartCoroutine(SpawnCoroutine(130, lycan, 2));
        StartCoroutine(SpawnCoroutine(130, lycan, 4));
        StartCoroutine(SpawnCoroutine(130, vampire, 5));

        StartCoroutine(SpawnCoroutine(130, vampire, 2));
        StartCoroutine(SpawnCoroutine(130, lycan, 4));
        StartCoroutine(SpawnCoroutine(130, vampire, 4));

        StartCoroutine(SpawnCoroutine(143, vampire, 2));
        StartCoroutine(SpawnCoroutine(143, lycan, 4));
        StartCoroutine(SpawnCoroutine(143, vampire, 4));

        StartCoroutine(SpawnCoroutine(156, vampire, 2));
        StartCoroutine(SpawnCoroutine(156, lycan, 3));
        StartCoroutine(SpawnCoroutine(156, lycan, 3));
        StartCoroutine(SpawnCoroutine(156, vampire, 4));
        StartCoroutine(SpawnCoroutine(156, vampire, 4));

        StartCoroutine(SpawnCoroutine(169, vampire, 2));
        StartCoroutine(SpawnCoroutine(169, lycan, 2));
        StartCoroutine(SpawnCoroutine(169, vampire, 4));
        StartCoroutine(SpawnCoroutine(169, lycan, 4));
    }

    IEnumerator SpawnCoroutine(float time, GameObject enemy, int spawnSpotId)
    {
        yield return new WaitForSeconds(time);
        enemySpawner.SpawnEnemy(enemy, spawnSpotId);
    }
}
