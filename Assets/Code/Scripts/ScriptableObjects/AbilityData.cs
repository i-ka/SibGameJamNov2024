using UnityEngine;

namespace SibGameJam.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AbilityData", menuName = "AbilityData", order = 0)]
    public class AbilityData : ScriptableObject
    {
        [field:SerializeField]
        public Texture Icon { get; private set; }
        
        [field:SerializeField]
        public string Name { get; private set; }
    }
}