using System.Collections;
using System.Collections.Generic;
using TWCore.Events;
using TWCore.Variables;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class UnitHealth : MonoBehaviour
{
    #region BLACKBOARD VARIABLES
    [SerializeField] public IntReference maxHealth;
    [SerializeField] public IntReference currHealth;
    [SerializeField] public GameEvent UnitDamaged;
    [SerializeField] public GameEvent UnitHealed;
    [SerializeField] public GameObjectEvent UnitDeath;
    #endregion

    #region PREFABS
    [SerializeField] private GameObject[] triggerableCollisions;
    #endregion

    [HideInInspector] public GameEvent OnUnitHit;

    private void Awake()
    {
        currHealth.SetValue(maxHealth);
        OnUnitHit = UnitDamaged;
    }

    public void Damage(int amount) 
    {
        currHealth.SetValue(Mathf.Max(0, currHealth - amount));
        UnitDamaged?.Raise();
        OnUnitHit?.Raise();

        if (currHealth == 0)
        {
            UnitDeath?.Raise(gameObject);
            Destroy(gameObject);
        }
    }

    public void Heal(int amount) 
    {
        currHealth.SetValue(Mathf.Min(currHealth + amount, maxHealth));
        UnitHealed?.Raise();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var triggerableCollision in triggerableCollisions)
        {
            if (collision.gameObject.name.Contains(triggerableCollision.name))
            {
                Damage(1);
                Destroy(collision.gameObject);
                return;
            }
        }
    }
}
