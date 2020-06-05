using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace R2
{
	public class AnimatorHook : MonoBehaviour
	{
		CharacterStateManager states;

		public virtual void Init(CharacterStateManager stateManager)
		{
			states = (CharacterStateManager)stateManager;
		}

		public void OnAnimatorMove()
		{
			OnAnimatorMoveOverrride();
		}

		protected virtual void OnAnimatorMoveOverrride()
		{
			if (states.useRootMotion == false)
				return;

			if (states.isGrounded && states.delta > 0)
			{
				Vector3 v = (states.anim.deltaPosition) / states.delta;
				v.y = states.rigidbody.velocity.y;
				states.rigidbody.velocity = v;
			}
		}

	}
}
