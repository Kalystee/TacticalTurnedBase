using PNN.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Damage", order = 0)]
public class AbilityDamage : AbilityBase
{
    [BoxGroup("Damage Settings"), OnValueChanged("OnDamageChanged")]
    public Vector2Int damageApplied;

    private void OnDamageChanged()
    {
        if (damageApplied.x > damageApplied.y)
        {
            int _oldY = damageApplied.y;
            damageApplied.y = damageApplied.x;
            damageApplied.x = damageApplied.y;
        }
    }
    
    public int GetDamageApplied()
    {
        return Random.Range(this.damageApplied.x, this.damageApplied.y);
    }

    protected override bool SubCanBeUsed(Entity caster, Vector2Int impactPoint)
    {
        return true;
    }

    protected override void SubEffect(Entity caster, Entity target)
    {
        target.Character.PV.CurrentValue -= GetDamageApplied();
    }
}