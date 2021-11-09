// ######################################################################
// BrickManager - Script description goes here
//
// Written by Tim McCune <tim.mccune1975@gmail.com>
// ######################################################################

using UnityEngine;

namespace Project.Game
{
    public class BallManager : MonoBehaviour
	{
        #region Inspector Assigned Field(s):
		[SerializeField] private Rigidbody Ball;
        #endregion

        #region MonoBehaviour Callback Method(s):
        private void OnEnable()
        {
            InputManager.Instance.OnSpaceBarInputEvent += InputManager_OnSpaceBarInputCallback;
        }

        private void OnDisable()
        {
            InputManager.Instance.OnSpaceBarInputEvent -= InputManager_OnSpaceBarInputCallback;
        }
        #endregion

		private void InputManager_OnSpaceBarInputCallback() 
        {
            float randomDirection = Random.Range(-1.0f, 1.0f);
            Vector3 forceDir = new Vector3(randomDirection, 1, 0);
            forceDir.Normalize();

            Ball.transform.SetParent(null);
            Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            Destroy(this);
		}
	}
}