using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnHelper : MonoBehaviour
{
    public GameObject[] ZombiePrefabs;

    public bool IsActive { get; set; }

    private List<SpawnModel> _spawnModels = new List<SpawnModel>()
    {
        new SpawnModel() {  EnemyType = 0 , SpawnTime = 1},
        new SpawnModel() {  EnemyType = 0 , SpawnTime = 2},
        new SpawnModel() {  EnemyType = 0 , SpawnTime = 3},
        new SpawnModel() {  EnemyType = 0 , SpawnTime = 4},
        new SpawnModel() {  EnemyType = 0 , SpawnTime = 7},
    };

    PlayerHelper _player;
    float _startTime;

    public void AddSpawnEnemy(SpawnModel addModel)
    {
        _spawnModels.Add(addModel);
    }

    public void StartSpawn()
    {
        _startTime = Time.time;
        IsActive = true;
        _player = GameObject.FindObjectOfType<PlayerHelper>();

        _spawnModels = new List<SpawnModel>();
    }

    void Update()
    {
        if (!IsActive)
            return;

        SpawnModel deleteModel = null;
        for (int i = 0; i < _spawnModels.Count; i++)
        {
            if (_spawnModels[i].SpawnTime < Time.time - _startTime)
            {
                GameObject zombie =
                    Instantiate(ZombiePrefabs[_spawnModels[i].EnemyType]);
                zombie.transform.position = transform.position;
                zombie.GetComponent<EnemyHelper>().Player = _player.gameObject;
                deleteModel = _spawnModels[i];
                break;
            }
        }

        if (deleteModel != null)
            _spawnModels.Remove(deleteModel);
    }
}
