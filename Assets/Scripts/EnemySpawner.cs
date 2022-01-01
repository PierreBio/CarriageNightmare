using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnTransform1;
    [SerializeField] Transform spawnTransform2;
    [SerializeField] Transform spawnTransform3;
    [SerializeField] Transform spawnTransform4;
    [SerializeField] Transform spawnTransform5;

    public void SpawnEnemy(GameObject enemy, int transformId)
    {
        if(enemy.name != "Vampire")
        {
            SoundManager.GetInstance().Play("Wolf_cry", this.gameObject);
        }
        else
        {
            SoundManager.GetInstance().Play("Vampire_cry_1", this.gameObject);
        }

        switch (transformId)
        {
            case 1:
                Instantiate(enemy, spawnTransform1.position, spawnTransform1.rotation);
                break;
            case 2:
                Instantiate(enemy, spawnTransform2.position, spawnTransform2.rotation);
                break;
            case 3:
                Instantiate(enemy, spawnTransform3.position, spawnTransform3.rotation);
                break;
            case 4:
                Instantiate(enemy, spawnTransform4.position, spawnTransform4.rotation);
                break;
            default:
                Instantiate(enemy, spawnTransform5.position, spawnTransform5.rotation);
                break;
        }
    }
}
