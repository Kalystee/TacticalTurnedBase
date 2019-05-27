using UnityEngine;

[CreateAssetMenu(menuName = "Other/Class", fileName = "New Class", order = 0)]
public class ClassBase : ScriptableObject
{
    private const int maxLevel = 20;
    
    public AnimationCurve HealthCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(maxLevel, 0));
    public AnimationCurve ActionPointCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(maxLevel, 0));
    public AnimationCurve StrengthCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(maxLevel, 0));
}
