using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tilia.Trackers.PseudoBody;
using Zinnia.Data.Collection.List;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPoint; // El punto de spawn (la esfera)
    public float spawnRadius = 1.0f; // Radio de la esfera de spawn
    public List<GameObject> prefabsToSpawn = new List<GameObject>(); // Lista de prefabs a spawnear, inicializada como nueva lista
    public int initialPoolSize = 10; // Tamaño inicial de la piscina de objetos
    public float spawnInterval = 0.5f; // Intervalo entre cada spawn
    public float burstDuration = 2.0f; // Duración del estado de ametralladora
    public float fireRate = 0.5f; // Tiempo mínimo entre cada spawn
    public bool isBursting = false; // Indica si está en el estado de ametralladora
    public float maxInstances = 30;
    private bool isBurstingEnabled = false; // Controla si el estado de ametralladora está activado o desactivado
    private float lastSpawnTime; // Tiempo del último spawn
    private float lastBurstStartTime; // Tiempo del inicio del último estado de ametralladora
    private List<GameObject> objectPool = new List<GameObject>(); // Piscina de objetos
    public static GameObject poolParent; // Objeto padre que contendrá los clones
    public GameObjectObservableList ignoreCollisionList;

    private void Start()
    {
        // Crear el objeto padre para los clones
        poolParent = new GameObject("ObjectPool");
        poolParent.transform.parent = transform;

        // Llenar la piscina de objetos con clones apagados
        for (int i = 0; i <= initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)], poolParent.transform);
            obj.SetActive(false);
            objectPool.Add(obj);
            ignoreCollisionList.AddUnique(obj);
        }
    }

    // Función pública para activar/desactivar el estado de ametralladora
    public void SetBurstingEnabled(bool enabled)
    {
        isBurstingEnabled = enabled;
    }

    // Función pública para iniciar el estado de ametralladora
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

        // Spawnear repetidamente durante la duración de la ametralladora
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

    // Función pública para spawnear un objeto
    private void SpawnObject()
    {
        // Obtener un objeto de la piscina
        GameObject obj = GetObjectFromPool();

        if (obj == null)
        {
            // La piscina está vacía, agreguemos más objetos a la piscina
            AddObjectsToPool(initialPoolSize);
            obj = GetObjectFromPool(); // Intentemos obtener un objeto nuevamente
        }

        if (obj != null)
        {
            // Genera una posición aleatoria dentro de la esfera de spawn
            Vector3 randomPointInUnitSphere = Random.insideUnitSphere;
            Vector3 randomSpawnPoint = spawnPoint.transform.position + randomPointInUnitSphere * spawnRadius;
            // Teleporta el objeto a la posición aleatoria
            obj.transform.position = randomSpawnPoint;
            // Activa el objeto
            obj.SetActive(true);
        }
    }

    // Función para obtener un objeto de la piscina
    private GameObject GetObjectFromPool()
    {
        // Busca un objeto inactivo en la piscina
        foreach (GameObject obj in objectPool)
            if (!obj.activeSelf)
                return obj;
        
        return null; // Devuelve null si no hay objetos disponibles en la piscina
    }

    // Función para devolver un objeto a la piscina
    public static void ReturnObjectToPool(GameObject obj)
    {
        obj.transform.position = poolParent.transform.position; // Restablece la posición al objeto padre de la piscina
        obj.SetActive(false); // Desactiva el objeto
    }

    // Función para agregar objetos a la piscina
    private void AddObjectsToPool(int count)
    {
        // Verificar si agregar estos objetos excederá el límite
        if (objectPool.Count + count > maxInstances)
        {
            // Calcular cuántos objetos adicionales se necesitan eliminar para mantener el límite
            int excessCount = objectPool.Count + count - 60;

            // Eliminar los objetos adicionales de la piscina
            for (int i = 0; i < excessCount; i++)
            {
                GameObject objToRemove = objectPool[0]; // Tomar el primer objeto de la lista
                objectPool.RemoveAt(0); // Eliminar el objeto de la lista
                Destroy(objToRemove); // Destruir el objeto
            }
        }

        // Agregar los objetos restantes a la piscina
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)], poolParent.transform);
            obj.SetActive(false);
            objectPool.Add(obj);
            ignoreCollisionList.AddUnique(obj);
        }
    }
}