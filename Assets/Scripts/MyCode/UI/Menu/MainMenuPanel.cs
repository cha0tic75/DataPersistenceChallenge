// ######################################################################
// MainMenuPanel - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.UI
{
    public class MainMenuPanel : MonoBehaviour
	{
        #region Inspector Assigned Field(s):
		[SerializeField] private HighScorePanel m_highScorePanel;
        #endregion

        #region MonoBehaviour Callback Method(s):
        private void OnEnable()
        {
            ScoreData highscoreData = ScoreManager.Instance.ScoreHelper.GetHighSCore();

            bool hasHighscore = highscoreData != null && highscoreData.Score > 0;

            if (hasHighscore)
            {
                m_highScorePanel.SetData(highscoreData);
            }

            m_highScorePanel.gameObject.SetActive(hasHighscore);

        }
        #endregion

		#region Public API:
		public void OnStartButtonClicked() => UnityEngine.SceneManagement.SceneManager.LoadScene(1);

		public void OnTopTenButtonClicked()
		{
            // TODO: Coming soon to a theater near you!
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