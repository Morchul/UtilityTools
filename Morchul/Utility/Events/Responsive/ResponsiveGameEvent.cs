using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Morchul.Utility.Events
{
	/// <summary>
	/// 
	/// </summary>
	[CreateAssetMenu(fileName = "ResponsiveGameEvent", menuName = "Events/ResponsiveGameEvent")]
	public class ResponsiveGameEvent<T> : ScriptableObject
	{
		private event ResponsiveGameEventMethod _event;

		public delegate void ResponsiveGameEventMethod(Respond respond);
		public delegate void Respond(T answer);

		public void AddListener(ResponsiveGameEventMethod listener)
		{
			_event += listener;
		}
		public void RemoveListener(ResponsiveGameEventMethod listener)
		{
			_event -= listener;
		}

		public void RaiseEvent(Respond respond)
		{
			_event?.Invoke(respond);
		}
	}
}

