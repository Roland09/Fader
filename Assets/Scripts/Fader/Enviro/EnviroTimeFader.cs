using System;
using System.Collections;
using UnityEngine;
using static Rowlan.FadeConst;

namespace Rowlan
{
	/// <summary>
	/// Fade Enviro's time in and out
	/// </summary>
	public class EnviroTimeFader : MonoBehaviour
	{
		#region Public Variables

		[Header( "Fade")]

		[Tooltip("The minimum hour offset. Note that if you want from midday to 3 hours past midnight, you should use e. g. from 12 to 27")]
		public float minimumValue = 0f;

		[Tooltip("The maximum hour offset. Note that if you want from midday to 3 hours past midnight, you should use e. g. from 12 to 27")]
		public float maximumValue = 12f;

		[Tooltip("The fade duration in seconds")]
		public float duration = 5f;

		[Tooltip("The easing meachinsm")]
		public Ease ease = Ease.Linear;

		[Header("Input")]

		[Tooltip("The input key that triggers the fading")]
		public KeyCode toggleKeyCode = KeyCode.None;

		#endregion Public Variables

		#region Internal Variables
		private FadeDirection fadeDirection = FadeDirection.In;

		private Fader fader;
		#endregion Internal Variables

		#region Initialization
		void Start()
		{

			fader = new CustomFader();
		}
		#endregion Initialization

		#region Fade Trigger
		void Update()
		{
			if (Input.anyKeyDown)
			{
				if (Input.GetKeyDown(toggleKeyCode))
				{
					fader.Start(this, fadeDirection, duration, ease, minimumValue, maximumValue);

					// toggle fade direction
					fadeDirection = fadeDirection == FadeDirection.In ? FadeDirection.Out : FadeDirection.In;

				}
			}
		}
		#endregion Fade Trigger

		#region Fade Logic

		public class CustomFader : Fader
		{
			public override void ApplyFade(float value)
			{
				//EnviroSkyMgr.instance.SetTimeOfDay(EnviroSkyMgr.instance.GetUniversalTimeOfDay() + value);
				EnviroSkyMgr.instance.SetTimeOfDay(value);
			}
		}

		#endregion Fade Logic
	}
}