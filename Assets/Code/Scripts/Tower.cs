using Code.Scripts.AI.Data;
using Code.Scripts.HealthSystem;
using UnityEngine;

namespace Code.Scripts
{
	public class Tower : MonoBehaviour
	{
		[field: SerializeField] public Team Team { get; set; }
		[field: SerializeField] public HealthController HealthController { get; private set; }
	}
}