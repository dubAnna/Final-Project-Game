/*Скрипт відповідає за керування контрольними точками та пунктом спавну гравця

Основні методи:
Start() знаходить всі контрольні точки на рівні, визначає початкову позицію спавну гравця та зберігає її у spawnPoint.

DeactivateCheckpoints() перебирає всі контрольні точки і викликає метод ResetCheckpoint() для кожної з них. 
*деактивує всі контрольні точки, коли гравець досяг нової контрольної точки, інформуючи систему що новий пункт спавну встановлений

SetSpawnPoint(Vector3 newSpawnPoint) приймає новий пункт спавну та встановлює його як spawnPoint. 
*викликається при активації контрольної точки коли гравець її активує
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance;

    private Checkpoint[] checkpoints;

    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>();
        
      spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints()
    {
        for(int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
