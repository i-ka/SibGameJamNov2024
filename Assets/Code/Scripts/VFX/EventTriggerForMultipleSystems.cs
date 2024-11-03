using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(Collider))]
public class EventTriggerForMultipleSystems : MonoBehaviour
{
	public List<VisualEffect> vfxSystems;
	public string eventName = "OnHit";

	public void SendEventToVFX()
	{
		foreach (var vfx in vfxSystems.Where(vfx => vfx != null))
		{
			vfx.SendEvent(eventName);
		}
	}
}