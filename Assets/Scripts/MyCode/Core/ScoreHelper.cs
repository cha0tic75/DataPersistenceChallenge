// ######################################################################
// ScoreHelper - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System.IO;
using UnityEngine;

// TODO: Finish Implementing the TopTen system

namespace Project
{
    public class ScoreHelper
	{
		#region Internal State Field(s):
		private string m_savePath;
		#endregion

		#region Properties:
		public TopTenScores TopTen { get; private set; }
		#endregion

		#region Constructor(s):
		public ScoreHelper()
		{
			m_savePath = Application.persistentDataPath + "/savefile.json";
			
			TopTen = new TopTenScores();

			for (int i = 0; i < 10; i++)
			{
				TopTen.Data[i] = new ScoreData();
			}
		}
		#endregion

		#region Public API:
		public ScoreData GetHighSCore() => GetScoreByRank(0);

        public ScoreData GetScoreByRank(int _rank) => (_rank >= 0 && _rank < TopTen.Data.Length) ? TopTen.Data[_rank] : null;

        public void SetScore(string _name, int _score)
        {
			string nameString = (!string.IsNullOrEmpty(_name) ? _name : $"<mark><i>anonymous</i></mark>");

            TopTen.Data[0] = new ScoreData() {Name = nameString, Score = _score};
			SaveData();
        }

        public void SaveData()
		{
			string json = JsonUtility.ToJson(TopTen);

			File.WriteAllText(m_savePath, json);
		}

		public void LoadData()
		{
			if (File.Exists(m_savePath))
			{
				string json = File.ReadAllText(m_savePath);
				TopTenScores data = JsonUtility.FromJson<TopTenScores>(json);

				TopTen = data;
			}
		}
		#endregion

		#region Support Class(es):
		[System.Serializable]
		public class TopTenScores
		{
			public ScoreData[] Data = new ScoreData[10];
		}
        #endregion
    }
}