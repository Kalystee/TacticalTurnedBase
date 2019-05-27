using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Other/Team", order = 100)]
public class Team : ScriptableObject
{
    [BoxGroup("Visual")]
    public Color teamColor;
}