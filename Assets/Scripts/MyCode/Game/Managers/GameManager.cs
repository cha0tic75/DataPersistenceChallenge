// ######################################################################
// GameManager - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System;
using UnityEngine;
using TMPro;

namespace Project.Game
{
	[DefaultExecutionOrder(-2)]
	public class GameManager : MonoBehaviour
	{
		#region Event/Delegate(s):
		public event Action OnGameOverEvent;
		public event Action OnInformNewGameEvent;
		#endregion

		#region Inspector Assigned Field(s):
		[SerializeField] private DeathZone m_deathZone;
		[SerializeField] private GameObject m_gameovertext;
		[SerializeField] private GameObject m_pressSpace;
		[SerializeField] private TextMeshProUGUI m_scoreTMP;
		#endregion

		#region Internal State Field(s):
		private int m_points;
		#endregion
		
		#region Properties:
		public static GameManager Instance { get; private set; }
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this;
		}

		private void OnEnable() => m_deathZone.OnGameOverEvent += DeathZone_OnGameOverCallback;
		private void OnDisable() => m_deathZone.OnGameOverEvent -= DeathZone_OnGameOverCallback;
		#endregion
		
		#region Public API:
		public void AddPoint(int _pointsToAdd)
		{
			m_points += _pointsToAdd;
			m_scoreTMP.text = $"Score : {m_points}";
		}
		#endregion

		#region Internally Used Method(s):
		private void DeathZone_OnGameOverCallback()
		{
			OnGameOverEvent?.Invoke();


			OnInformNewGameEvent?.Invoke();
		}
		#endregion
	}
}