using UnityEngine;
using System.Collections;
using System;

public class NavMeshHelper : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent _navMeshAgent;
    GameObject _target;
    HealthHelper _healthHelper;

    void Start()
    {
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _healthHelper = GetComponent<HealthHelper>();
    }

    void Update()
    {
        if (_target == null ||
            _healthHelper && _healthHelper.IsDead)
            return;

        _navMeshAgent.SetDestination(_target.transform.position);
    }

    internal void Move(GameObject target)
    {
        _target = target;
    }
}
