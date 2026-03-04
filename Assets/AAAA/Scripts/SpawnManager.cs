using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    public GameObject[] objectsPrefab;
    public Vector3 spawnPos = new (-0.200000003f, 3.00999999f, -0.75999999f);
    public float startDelay = 2.0f;
    public float spawnInterval = 2.0f;
    public float lifeTime = 2.0f;
 


    public void StartSpawning(int diffuculty)
    {
        lifeTime = 2.0f / diffuculty;
        // Zamanlayıcıyı başlat
        spawnInterval = 2.0f / diffuculty;
        InvokeRepeating(nameof(SpawnRandomObject), startDelay, spawnInterval);
    }

    private void SpawnRandomObject()
    {

        // 1. Rastgele bir obje seç
        int index = Random.Range(0, objectsPrefab.Length);

        // 2. Objeyi doğur ve doğan bu klonu "yeniObje" adında bir kutuya (değişkene) koy
        GameObject newObj = Instantiate(objectsPrefab[index], spawnPos, objectsPrefab[index].transform.rotation);
        // 3. Sadece bu yeni doğan objenin fişini "lifeTime" (2sn) sonra çek!
        Destroy(newObj, lifeTime);
    }
}