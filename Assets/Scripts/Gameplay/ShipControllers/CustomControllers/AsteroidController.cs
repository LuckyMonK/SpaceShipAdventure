using Gameplay.ShipSystems;
using Gameplay.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IDamagable
{
    private MovementSystem _moveController;
    [SerializeField]
    private UnitBattleIdentity _battleIdentity;


    public UnitBattleIdentity BattleIdentity => _battleIdentity;


    private void Start()
    {
        _moveController = GetComponent<MovementSystem>();
    }
    private void Update()
    {
        ProcessHandling();
    }
    protected void ProcessHandling()
    {
        _moveController.LongitudinalMovement(Time.deltaTime);
    }

    public void ApplyDamage(IDamageDealer damageDealer)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var damagableObject = other.gameObject.GetComponent<IDamagable>();

        if (damagableObject != null
            && damagableObject.BattleIdentity != BattleIdentity)
        {
            damagableObject.ApplyDamage(null);
            Destroy(gameObject);
        }
    }
}
