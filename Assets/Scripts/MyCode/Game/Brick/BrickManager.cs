// ######################################################################
// BrickManager - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Game
{
    public class BrickManager : MonoBehaviour
	{
		#region Event/Delegate(s):
		public event Action OnWinConditionEvent;
		#endregion

		#region Inspector Assigned Field(s):
		[SerializeField] private Brick m_brickPrefab;
		[SerializeField] private int m_lineCount = 6;
		#endregion

		#region Internal State Field(s):
		private List<Brick> m_currentBricks = new List<Brick>();
		#endregion

		#region MonoBehaviour Callback Method(s):
		private void Start()
		{
			const float step = 0.6f;
			int perLine = Mathf.FloorToInt(4.0f / step);
			
			int[] pointCountArray = new [] {1,1,2,2,5,5};
			for (int i = 0; i < m_lineCount; ++i)
			{
				for (int x = 0; x < perLine; ++x)
				{
					Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
					var brick = Instantiate(m_brickPrefab, position, Quaternion.identity, transform);
					brick.PointValue = pointCountArray[i];
					// brick.onDestroyed.AddListener(AwardPoints);
					brick.OnBrickDestroyedEvent += Brick_OnBrickDestroyedCallback;
					m_currentBricks.Add(brick);
				}
			}
		}

        private void Brick_OnBrickDestroyedCallback(Brick _brick)
        {
			GameManager.Instance.AwardPoints(_brick.PointValue);

			m_currentBricks.Remove(_brick);
			Destroy(_brick.gameObject);

			if (m_currentBricks.Count == 0)
			{
				OnWinConditionEvent?.Invoke();
			}
        }
        #endregion

        // #region Internally Used Method(s):
        // private void AwardPoints(int _point) => GameManager.Instance.AwardPoints(_point);
		// #endregion
	}
}