using UnityEngine;

public class StarManager : MonoBehaviour
{
    public GameObject[] starPrefabs;
    public int initialStarCount = 50;
    public float spawnAreaWidth = 20f;
    public float spawnAreaHeight = 12f;

    void Start()
    {
        for (int i = 0; i < initialStarCount; i++)
        {
            SpawnStarRandomly();
        }
    }

    void SpawnStarRandomly()
    {
        float randomX = Random.Range(transform.position.x - spawnAreaWidth, transform.position.x + spawnAreaWidth);
        float randomY = Random.Range(transform.position.y - spawnAreaHeight, transform.position.y + spawnAreaHeight);

        Vector3 spawnPos = new Vector3(randomX, randomY, 0);

        GameObject newStar = Instantiate(starPrefabs[Random.Range(0, starPrefabs.Length)], spawnPos, Quaternion.identity);

        newStar.transform.SetParent(this.transform);
    }
}