// ######################################################################
// ScoreManager - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using Project.Game;
using UnityEngine;

namespace Project
{
    [DefaultExecutionOrder(-1)]
    public class ScoreManager : SingletonMonoBehaviour<ScoreManager>
	{
        #region Inspector Assigned Field(s):
        [SerializeField] private bool m_resetScores = false;
        #endregion

        #region Properties:
		public ScoreHelper ScoreHelper { get; private set; }
        private readonly string m_scoreFormatString = "{0} {1}";
        #endregion

        #region MonoBehaviour Callback Method(s):
		protected override void Awake()
		{
            base.Awake();

            LoadScores();

            if (m_resetScores)
            {
                ResetTopScore();
            }
		}
        #endregion

        #region Internally Used Method(s):
        private void LoadScores()
        {
			ScoreHelper = new ScoreHelper();
            ScoreHelper.LoadData();
        }
        #endregion

        #region Debug:
        public string FormatScoreText(string _name, int _score)
        {
            string scoreFormat = $"{_score}";

            return string.Format(m_scoreFormatString, _name, scoreFormat);
        }

        // TODO: This currently only works with index 0.  Refactor to allow for reset of any score index:
        public void ResetTopScore(int _index = -1)
        {
			ScoreManager.Instance.ScoreHelper.SetScore("", 0);
        }
        #endregion
	}
}