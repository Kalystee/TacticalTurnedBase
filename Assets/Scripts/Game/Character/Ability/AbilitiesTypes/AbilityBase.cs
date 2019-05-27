using PNN.Battlegrounds;
using PNN.Battlegrounds.Areas;
using PNN.Entities;
using PNN.Makers;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    [BoxGroup("Visual")]
    public Sprite sprite;

    [BoxGroup("General Settings"), MinValue(0)]
    public int range;
    [BoxGroup("General Settings")]
    public int cost;
    [BoxGroup("General Settings"), SerializeField] private AreaMaker areaMaker;

    public Area area;

    public void OnEnable()
    {
        area = areaMaker.CreateAbilityArea();
    }

    
    /// <summary>
    /// Method to use an ability
    /// </summary>
    /// <param name="battleground"></param>
    /// <param name="caster"></param>
    /// <param name="impactPoint"></param>
    /// <returns></returns>
    public bool Use(BattlegroundController battlegroundC,Entity caster, Vector2Int impactPoint)
    {
       caster.Character.PA.CurrentValue -= cost;
       foreach(Entity target in battlegroundC.GetEntitiesAffectedByAbility(impactPoint, this))
       {
        Effect(caster, target);
       }
       return true;
        
        return false;
    }

    protected abstract void SubEffect(Entity caster, Entity target);
    
    /// <summary>
    /// Methof to know if the caster has necessary PA to  cast the ability
    /// </summary>
    /// <param name="caster">Caster</param>
    /// <param name="impactPoint">Impact Point of the ability</param>
    /// <returns>True if the caster can use the ability</returns>
    public bool CanBeUsed(Entity caster, Vector2Int impactPoint)
    {
        if (caster.Character.PA.CurrentValue < this.cost)
        {
            return false;
        }

        return SubCanBeUsed(caster, impactPoint);
    }

    protected abstract bool SubCanBeUsed(Entity caster, Vector2Int impactPoint);

    /// <summary>
    /// Method to apply the effect and subEffect of abilities
    /// </summary>
    /// <param name="caster">Caster</param>
    /// <param name="target">Target Entity</param>
    public void Effect(Entity caster, Entity target)
    {
        SubEffect(caster, target);
    }

}