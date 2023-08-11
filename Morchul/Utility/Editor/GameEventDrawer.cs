using UnityEngine;
using System.Collections;
using UnityEditor;
using Morchul.Utility.Events;

namespace Morchul.Utility
{
	/// <summary>
	/// 
	/// </summary>
	[CustomEditor(typeof(GameEvent))]
	public class GameEventDrawer : Editor
	{
		private GameEvent gameEvent;
		private void OnEnable()
		{
			gameEvent = (GameEvent)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if(GUILayout.Button("Raise Event"))
			{
				gameEvent.RaiseEvent();
			}
		}
	}
}

