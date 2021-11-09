// ######################################################################
// GameManager - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

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
		[SerializeField] private HighscoreInputWindow m_highscoreInputWindow;
		[SerializeField] private TextMeshProUGUI m_currentScoreTMP;
		[SerializeField] private TextMeshProUGUI m_bestscoreTMP;
		#endregion

		#region Internal State Field(s):
		private int m_currentScore;
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
		private void Start()
        {
            SetBestScoreText();
        }

        private void OnDisable() => m_deathZone.OnGameOverEvent -= DeathZone_OnGameOverCallback;
		#endregion
		
		#region Public API:
		public void AwardPoints(int _pointsToAdd)
		{
			m_currentScore += _pointsToAdd;
			m_currentScoreTMP.text = $"Score : {m_currentScore}";
		}
		#endregion

		#region Internally Used Method(s):
        private void SetBestScoreText()
        {
            ScoreData highScoreData = ScoreManager.Instance.ScoreHelper.GetHighSCore();
            bool hasHighScore = highScoreData != null && highScoreData.Score > 0;
            if (hasHighScore)
            {
                m_bestscoreTMP.SetText($"Best Score : {highScoreData.Name} : {highScoreData.Score}");
            }

            m_bestscoreTMP.gameObject.SetActive(hasHighScore);
        }

		private void InvokeOnGameOverEvent() => OnGameOverEvent?.Invoke();
		private void InvokeInformNewGameEvent() => OnInformNewGameEvent?.Invoke();

		private void GetHighscoreInput()
		{
			m_highscoreInputWindow.OnInputSubmittedEvent += HighscoreInputWindow_OnInputSubmittedCallback;
			m_highscoreInputWindow.SetVisibility(true);
		}
        #endregion

        #region Callback Method(s):
        private void DeathZone_OnGameOverCallback()
		{
			InvokeOnGameOverEvent();
			
			ScoreData highscoreData = ScoreManager.Instance.ScoreHelper.GetHighSCore();

			if (highscoreData == null || m_currentScore > highscoreData.Score)
			{
				GetHighscoreInput();
			}
			else
			{
				InvokeInformNewGameEvent();
			}
		}

        private void HighscoreInputWindow_OnInputSubmittedCallback(string _inputValue)
        {
            m_highscoreInputWindow.OnInputSubmittedEvent -= HighscoreInputWindow_OnInputSubmittedCallback;
			m_highscoreInputWindow.SetVisibility(false);
			ScoreManager.Instance.ScoreHelper.SetScore(_inputValue, m_currentScore);
			SetBestScoreText();
			StartCoroutine(WaitForNewGameInput());
        }
		#endregion

		#region Coroutine(s):
		private bool m_hasNewGameInput = false;
		private IEnumerator WaitForNewGameInput()
		{
			InvokeInformNewGameEvent();
			InputManager.Instance.OnSpaceBarInputEvent += HasNewInput;

			while(!m_hasNewGameInput)
			{
				yield return null;
			}

			InputManager.Instance.OnSpaceBarInputEvent -= HasNewInput;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

			void HasNewInput() => m_hasNewGameInput = true;
		}
		#endregion
	}
}