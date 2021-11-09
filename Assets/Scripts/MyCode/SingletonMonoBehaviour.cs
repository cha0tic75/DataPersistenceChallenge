// ######################################################################
// SingletonMonoBehaviour - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;

namespace Project
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		#region Inspector Assigned Field(s):
		[SerializeField] private bool m_dontDestroyOnLoad = false;
		#endregion

		#region Properties:
		public static T Instance { get; private set; }
		#endregion

		#region MonoBehaviour Callback Method(s):
		protected virtual void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}	

			Instance = this as T;

			if (m_dontDestroyOnLoad)
			{
				DontDestroyOnLoad(gameObject);
			}
		}
		#endregion
	}
}