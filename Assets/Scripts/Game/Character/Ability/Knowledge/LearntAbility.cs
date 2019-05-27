public class LearntAbility
{
    public readonly object source;
    public readonly AbilityBase ability;

    public LearntAbility(object source, AbilityBase ability)
    {
        this.source = source;
        this.ability = ability;
    }
}
