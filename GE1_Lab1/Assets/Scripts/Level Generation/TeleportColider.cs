using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportColider : MonoBehaviour
{
    public GameObject TeleportButton;
    public GameObject TeleportGui;
    public List<GameObject> Spawners;
    public GameObject PortalPrefab;
    public GameObject SpawnLocation;
    public GameObject MapDropOff;

    public void GenerateAndOpen(int levle, float monsterDensity)
    {
        foreach(GameObject spawner in Spawners)
        {
            EnemySpawner enemySpawner = spawner.GetComponent<EnemySpawner>();

            enemySpawner.level = levle;
            enemySpawner.quantityMultiplier = monsterDensity;

            enemySpawner.SpawnEnemies();
        }

        GameObject portal = Instantiate(PortalPrefab, SpawnLocation.transform);
        portal.GetComponent<Teleport>().DropOff = MapDropOff;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other is CharacterController)
        {
            TeleportButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other is CharacterController)
        {
            TeleportButton.SetActive(false);

            if (TeleportGui.activeInHierarchy)
            {
                TeleportGui.SetActive(false);
            }
        }
    }
}
