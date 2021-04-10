using UnityEngine;
using System.Collections;

public class EnemyHelper : MonoBehaviour
{
    public GameObject Player;
    public int Damage = 10;

    NavMeshHelper _navMeshHelper;
    GameHelper _gameHelper;

    float _lastAttackTime;
    float _lastAttackSpeed = 2;

    void Start()
    {
        _navMeshHelper = GetComponent<NavMeshHelper>();
        _gameHelper = GameObject.FindObjectOfType<GameHelper>();
        _gameHelper.AllEnemyes.Add(gameObject);
        Move();
    }

    public void Move()
    {
        _navMeshHelper.Move(Player);
    }

    void Update()
    {
        if (Vector3.Distance(transform.position,
            Player.transform.position) < 4 &&
            Time.time > _lastAttackTime + _lastAttackSpeed)
        {
            if (GetComponent<Animator>())
                GetComponent<Animator>().SetTrigger("Attack");
            _lastAttackTime = Time.time;
            _gameHelper.PlayerTakeDamage(Damage);
        }
    }
}
