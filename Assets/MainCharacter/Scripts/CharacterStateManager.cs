﻿using UnityEngine;
using System.Collections;

namespace R2
{
	public abstract class CharacterStateManager : StateManager
	{
		[Header("References")]
		public Animator anim;
		public new Rigidbody rigidbody;
		public AnimatorHook animHook;

		[Header("States")]
		public bool isGrounded;
		public bool useRootMotion;

		[Header("Controller Values")]
		public float vertical;
		public float horizontal;
		public bool lockOn;
		public float delta;
		public Vector3 rootMovement;

		public override void Init()
		{
			anim = GetComponentInChildren<Animator>();
			animHook = GetComponentInChildren<AnimatorHook>();
			rigidbody = GetComponentInChildren<Rigidbody>();
			anim.applyRootMotion = false;

			animHook.Init(this);

		}

		public void PlayTargetAnimation(string targetAnim, bool isInteracting)
		{
			anim.SetBool("isInteracting", isInteracting);
			anim.CrossFade(targetAnim, 0.2f);
		}
	}
}