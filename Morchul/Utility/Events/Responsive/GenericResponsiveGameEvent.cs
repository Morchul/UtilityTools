using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Morchul.Utility.Events
{
	/// <summary>
	/// 
	/// </summary>
	public class GenericResponsiveGameEvent<T, F> : ScriptableObject
	{
		private event GenericResponsiveGameEventMethod _event;

		public delegate void GenericResponsiveGameEventMethod(F param, Respond respond);
		public delegate void Respond(T answer);

		public void AddListener(GenericResponsiveGameEventMethod listener)
		{
			_event += listener;
		}
		public void RemoveListener(GenericResponsiveGameEventMethod listener)
		{
			_event -= listener;
		}

		public void RaiseEvent(F param, Respond respond)
		{
			_event?.Invoke(param, respond);
		}
	}
}

