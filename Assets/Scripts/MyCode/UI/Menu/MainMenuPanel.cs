// ######################################################################
// MainMenuPanel - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

namespace Project.UI
{
    public class MainMenuPanel : MenuPanelItem
	{
        #region Inspector Assigned Field(s):
		[SerializeField] private GameObject m_highScorePanel;
        [SerializeField] private TextMeshProUGUI m_highScoreText; 
        #endregion

        #region MonoBehaviour Callback Method(s):
        private void OnEnable()
        {
            ScoreData highscoreData = ScoreManager.Instance.ScoreHelper.GetHighSCore();

            bool hasHighscore = highscoreData != null && highscoreData.Score > 0;

            if (hasHighscore)
            {
                m_highScoreText.SetText(ScoreManager.Instance.FormatScoreText(highscoreData.Name, highscoreData.Score));
            }

            m_highScorePanel.gameObject.SetActive(hasHighscore);

        }
        #endregion

		#region Public API:
		public void OnStartButtonClicked() => UnityEngine.SceneManagement.SceneManager.LoadScene(1);

		public void OnTopTenButtonClicked()
		{
            InvokeCurrentMenuStateChangeRequest(MenuState.TopTen);
		}

		public void OnExitButtonClicked()
		{
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
		}
		#endregion
	}
}