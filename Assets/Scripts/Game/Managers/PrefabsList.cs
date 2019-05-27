using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace PNN
{
    public class PrefabsList : MonoBehaviour
    {
        [BoxGroup("Battleground")] public Tile highlightTile;
        [BoxGroup("Battleground")] public GameObject entityPrefab;
        [BoxGroup("UI:Ability")] public GameObject abilityPrefabButton;
    }
}
