using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPoint; // El punto de spawn (la esfera)
    public float spawnRadius = 1.0f; // Radio de la esfera de spawn
    public List<GameObject> prefabsToSpawn = new List<GameObject>(); // Lista de prefabs a spawnear, inicializada como nueva lista
    public int initialPoolSize = 10; // Tama�o inicial de la piscina de objetos
    public float spawnInterval = 0.5f; // Intervalo entre cada spawn
    public float burstDuration = 2.0f; // Duraci�n del estado de ametralladora
    public float fireRate = 0.5f; // Tiempo m�nimo entre cada spawn
    private bool isBursting = false; // Indica si est� en el estado de ametralladora

    private bool isBurstingEnabled = false; // Controla si el estado de ametralladora est� activado o desactivado
    private float lastSpawnTime; // Tiempo del �ltimo spawn
    private float lastBurstStartTime; // Tiempo del inicio del �ltimo estado de ametralladora
    private List<GameObject> objectPool = new List<GameObject>(); // Piscina de objetos
    public static GameObject poolParent; // Objeto padre que contendr� los clones

    private void Start()
    {
        // Crear el objeto padre para los clones
        poolParent = new GameObject("ObjectPool");
        poolParent.transform.parent = transform;

        // Llenar la piscina de objetos con clones apagados
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefabsToSpawn[0], poolParent.transform);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }

    // Funci�n p�blica para activar/desactivar el estado de ametralladora
    public void SetBurstingEnabled(bool enabled)
    {
        isBurstingEnabled = enabled;
    }

    // Funci�n p�blica para iniciar el estado de ametralladora
    public void StartBurst()
    {
        lastBurstStartTime = Time.time;
        StartCoroutine(BurstCoroutine());
    }

    // Corrutina para el estado de ametralladora
    private IEnumerator BurstCoroutine()
    {
        isBursting = true;

        // Espera un tiempo antes de comenzar el primer spawn
        yield return new WaitForSeconds(spawnInterval);

        // Spawnear repetidamente durante la duraci�n de la ametralladora
        float timer = 0.0f;
        while (timer < burstDuration)
        {
            if (isBurstingEnabled && Time.time > lastSpawnTime + fireRate)
            {
                SpawnObject();
                lastSpawnTime = Time.time;
            }
            yield return new WaitForSeconds(spawnInterval);
            timer += spawnInterval;
        }

        isBursting = false;
    }

    // Funci�n p�blica para spawnear un objeto
    private void SpawnObject()
    {
        // Obtener un objeto de la piscina
        GameObject obj = GetObjectFromPool();

        if (obj == null)
        {
            // La piscina est� vac�a, agreguemos m�s objetos a la piscina
            AddObjectsToPool(initialPoolSize);
            obj = GetObjectFromPool(); // Intentemos obtener un objeto nuevamente
        }

        if (obj != null)
        {
            // Genera una posici�n aleatoria dentro de la esfera de spawn
            Vector3 randomPointInUnitSphere = Random.insideUnitSphere;
            Vector3 randomSpawnPoint = spawnPoint.transform.position + randomPointInUnitSphere * spawnRadius;
            // Teleporta el objeto a la posici�n aleatoria
            obj.transform.position = randomSpawnPoint;
            // Activa el objeto
            obj.SetActive(true);
        }
    }

    // Funci�n para obtener un objeto de la piscina
    private GameObject GetObjectFromPool()
    {
        // Busca un objeto inactivo en la piscina
        foreach (GameObject obj in objectPool)
            if (!obj.activeSelf)
                return obj;
        
        return null; // Devuelve null si no hay objetos disponibles en la piscina
    }

    // Funci�n para devolver un objeto a la piscina
    public static void ReturnObjectToPool(GameObject obj)
    {
        obj.transform.position = poolParent.transform.position; // Restablece la posici�n al objeto padre de la piscina
        obj.SetActive(false); // Desactiva el objeto
    }

    // Funci�n para agregar objetos a la piscina
    private void AddObjectsToPool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefabsToSpawn[0], poolParent.transform);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }
}