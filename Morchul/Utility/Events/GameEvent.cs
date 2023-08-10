using UnityEngine;

namespace Morchul.Utility.Events
{
	[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent")]
	public class GameEvent : ScriptableObject
	{
		private event GameEventMethod _event;

		public delegate void GameEventMethod();

		public void AddListener(GameEventMethod listener)
		{
			_event += listener;
		}
		public void RemoveListener(GameEventMethod listener)
		{
			_event -= listener;
		}

		public void RaiseEvent()
		{
			_event?.Invoke();
		}
	}
}

