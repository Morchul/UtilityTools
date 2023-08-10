using System.Collections.Generic;
using UnityEngine;

namespace Morchul.Utility.Events
{
	/// <summary>
	/// 
	/// </summary>
	public class GenericGameEvent<T> : ScriptableObject
	{
		private event GenericGameEventMethod _event;

		public delegate void GenericGameEventMethod(T param);

		public void AddListener(GenericGameEventMethod listener)
		{
			_event += listener;
		}
		public void RemoveListener(GenericGameEventMethod listener)
		{
			_event -= listener;
		}

		public void RaiseEvent(T param)
		{
			_event?.Invoke(param);
		}
	}
}

