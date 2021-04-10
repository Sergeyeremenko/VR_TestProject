using UnityEngine;
using System.Collections;
using System;

public class HealthHelper : MonoBehaviour
{
    public GameObject BoodPrefab;

    public float MaxHealth = 100;
    public float Health = 100;

    private bool _isDead;
    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }
    GameHelper _gameHelper;

    void Start()
    {
        _gameHelper = GameObject.FindObjectOfType<GameHelper>();
    }

    internal void TakeDamage(int damagePerShot, Vector3 point)
    {
        float health = Health - damagePerShot;

        #region Hit
        GameObject hitEffect = Instantiate(BoodPrefab);
        hitEffect.transform.position = point;
        Destroy(hitEffect, 1);
        #endregion

        if (IsDead)
            return;

        if (health <= 0)
        {
            Health = 0;
            IsDead = true;

            if (GetComponent<Animator>())
                GetComponent<Animator>().SetTrigger("Dead");
            _gameHelper.DeadEnemyes++;

            GetComponent<Collider>().enabled = false;
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            Destroy(gameObject, 3);
        }
        else
        {
            Health = health;
        }
    }
}
