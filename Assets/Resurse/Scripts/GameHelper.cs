using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameHelper : MonoBehaviour
{
    public GameObject GameMenu;
    public GameObject Weapon;

    public SpawnHelper[] SpawnPoints;

    public TextMesh StartGameText;
    public TextMesh ScoreText;
    public TextMesh BestScoreText;

    public bool IsStartedGame { get; set; }

    private int _playerMaxHealth = 100;
    public int PlayerMaxHealth
    {
        get { return _playerMaxHealth; }
        set { _playerMaxHealth = value; }
    }

    private List<GameObject> _allEnemyes = new List<GameObject>();
    public List<GameObject> AllEnemyes
    {
        get { return _allEnemyes; }
        set { _allEnemyes = value; }
    }

    public int PlayerHealth { get; set; }

    public int[] Levels;
    public int[] LevelsHealth;

    int _level;
    int _score;
    int _bestScore;

    int _currentScore;
    public int DeadEnemyes { get; set; }

    void Start()
    {
        InvokeRepeating("Timer", 0, 1);
    }

    public void StartGame()
    {
        PlayerHealth = PlayerMaxHealth;
        AllEnemyes = new List<GameObject>();
        IsStartedGame = true;
        GameMenu.SetActive(false);
        Weapon.SetActive(true);
        _currentScore = 0;
        StartNewLevel();
    }

    void Timer()
    {
        if (!IsStartedGame)
            return;
        if (Levels[_level] == DeadEnemyes)
        {
            _level++;

            if (Levels.Length == _level)
            {
                EndGame();
                DeadEnemyes = 0;
            }
            else
            {
                Debug.Log("New Level! " + _level);
                DeadEnemyes = 0;
                StartNewLevel();
            }
        }
    }

    public void EndGame()
    {
        for (int i = 0; i < AllEnemyes.Count; i++)
            Destroy(AllEnemyes[i], 1);

        _level = 0;
        StartCoroutine(EndWait());
        Weapon.SetActive(false);

        _score = DeadEnemyes * 10;
        if (_score > _bestScore)
            _bestScore = _score;

        ScoreText.text = _score.ToString();
        BestScoreText.text = _bestScore.ToString();
    }

    private IEnumerator EndWait()
    {
        yield return new WaitForSeconds(3);
        IsStartedGame = false;
        GameMenu.SetActive(true);
    }

    internal void PlayerTakeDamage(int damage)
    {
        if (!IsStartedGame)
            return;

        int health = PlayerHealth - damage;

        if (health <= 0)
        {
            EndGame();
        }
        PlayerHealth = health;
    }

    private void StartNewLevel()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
            SpawnPoints[i].StartSpawn();

        for (int i = 0; i < Levels[_level]; i++)
        {
            int index = UnityEngine.Random.Range(0, SpawnPoints.Length);
            SpawnPoints[index].AddSpawnEnemy(new SpawnModel()
            {
                EnemyType = 0,
                SpawnTime = i * 2,
                AddHealth = LevelsHealth[_level]
            });
        }
    }
}
