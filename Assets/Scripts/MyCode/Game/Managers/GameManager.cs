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
		[SerializeField] private BrickManager m_brickManager;
		[SerializeField] private DeathZone m_deathZone;
		[SerializeField] private HighscoreInputWindow m_highscoreInputWindow;
		[SerializeField] private TextMeshProUGUI m_currentScoreTMP;
		[SerializeField] private TextMeshProUGUI m_bestscoreTMP;
		#endregion

		#region Internal State Field(s):
		private int m_currentScore;
		private Coroutine m_endGameCoroutine = null;
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

		private void OnEnable()
		{
			m_deathZone.OnGameOverEvent += DeathZone_OnGameOverCallback;
			m_brickManager.OnWinConditionEvent += BrickManager_OnWinConditionCallback;
		}

        private void Start() => ResetBestScoreText();

		private void OnDisable()
		{
			m_deathZone.OnGameOverEvent -= DeathZone_OnGameOverCallback;
			m_brickManager.OnWinConditionEvent -= BrickManager_OnWinConditionCallback;
			StopEndGameCoroutine();
		}
		#endregion
		
		#region Public API:
		public void AwardPoints(int _pointsToAdd)
		{
			m_currentScore += _pointsToAdd;
			m_currentScoreTMP.text = $"Score : {m_currentScore}";
		}

        public void ResetBestScoreText()
        {
            ScoreData highScoreData = ScoreManager.Instance.ScoreHelper.GetHighSCore();
            bool hasHighScore = highScoreData != null && highScoreData.Score > 0;
            
			if (hasHighScore)
            {
				string bestScoreText = ScoreManager.Instance.FormatScoreText(highScoreData.Name, highScoreData.Score);
                m_bestscoreTMP.SetText($"Best Score : {bestScoreText}");
            }

            m_bestscoreTMP.gameObject.SetActive(hasHighScore);
        }
		#endregion

		#region Internally Used Method(s):

		private void InvokeOnGameOverEvent() => OnGameOverEvent?.Invoke();
		private void InvokeInformNewGameEvent() => OnInformNewGameEvent?.Invoke();

		private void GetHighscoreInput()
		{
			m_highscoreInputWindow.OnInputSubmittedEvent += HighscoreInputWindow_OnInputSubmittedCallback;
			m_highscoreInputWindow.SetVisibility(true);
		}
		private void WaitForNewGameInput() => m_endGameCoroutine = StartCoroutine(WaitForNewGameInputCoroutine());
		private void StopEndGameCoroutine()
		{
			if (m_endGameCoroutine != null)
			{
				StopCoroutine(m_endGameCoroutine);
				m_endGameCoroutine = null;
			}
		}
        #endregion

        #region Callback Method(s):
        private void DeathZone_OnGameOverCallback()
		{
			InvokeOnGameOverEvent();
			
			ScoreData highscoreData = ScoreManager.Instance.ScoreHelper.GetHighSCore();

			if (highscoreData == null || m_currentScore > highscoreData.Score)
				 { GetHighscoreInput(); }
			else { WaitForNewGameInput(); }
		}

        private void BrickManager_OnWinConditionCallback()
        {
            Debug.Log("You won!");
			DeathZone_OnGameOverCallback();

        }
        private void HighscoreInputWindow_OnInputSubmittedCallback(string _inputValue)
        {
            m_highscoreInputWindow.OnInputSubmittedEvent -= HighscoreInputWindow_OnInputSubmittedCallback;
			m_highscoreInputWindow.SetVisibility(false);
			ScoreManager.Instance.ScoreHelper.SetScore(_inputValue, m_currentScore);
			ResetBestScoreText();
			WaitForNewGameInput();
        }

		private void InputManager_OnSpaceBarInputCallback()
		{
			StopEndGameCoroutine();
			InputManager.Instance.OnSpaceBarInputEvent -= InputManager_OnSpaceBarInputCallback;
			InputManager.Instance.OnEscButtonInputEvent -= InputManager_OnEscButtonInputCallback;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}

		private void InputManager_OnEscButtonInputCallback()
		{
			StopEndGameCoroutine();
			InputManager.Instance.OnSpaceBarInputEvent -= InputManager_OnSpaceBarInputCallback;
			InputManager.Instance.OnEscButtonInputEvent -= InputManager_OnEscButtonInputCallback;
			SceneManager.LoadScene(0);
		}
		#endregion

		#region Coroutine(s):
		private bool m_hasNewGameInput = false;
		private IEnumerator WaitForNewGameInputCoroutine()
		{
			InvokeInformNewGameEvent();

			InputManager.Instance.OnSpaceBarInputEvent += InputManager_OnSpaceBarInputCallback;
			InputManager.Instance.OnEscButtonInputEvent += InputManager_OnEscButtonInputCallback;

			while(!m_hasNewGameInput)
			{
				yield return null;
			}
		}
		#endregion
	}
}