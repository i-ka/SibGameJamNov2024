using Code.Scripts.AI.Data;
using UnityEngine;

namespace Code.Scripts
{
	public class Tower : MonoBehaviour
	{
		[field: SerializeField] public Team Team { get; set; }
	}
}