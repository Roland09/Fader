using System;
using System.Collections;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Example about how to fade Enviro's time in and out
	/// 
	/// Important: Using Enviro requires this scripting symbol to be defined: ENVIRO. Or alternatively adjust the #if codeblock in <see cref="CustomFader"/>.
	/// 
	/// </summary>
	public class EnviroTimeFader : MonoBehaviour
	{
#if ENVIRO

		#region Public Variables

		[Header( "Fade")]

		[Tooltip("The minimum hour offset. Note that if you want from midday to 3 hours past midnight, you should use e. g. from 12 to 27. The maximum hour offset. Note that if you want from midday to 3 hours past midnight, you should use e. g. from 12 to 27")]
		public FadeSettings propertyFadeSettings = new FadeSettings( 0f, 12f);

		[Tooltip("The fade duration in seconds")]
		public float duration = 5f;

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
			fader = new CustomFader( propertyFadeSettings);
		}
		#endregion Initialization

		#region Fade Trigger
		void Update()
		{
			if (Input.anyKeyDown)
			{
				if (Input.GetKeyDown(toggleKeyCode))
				{
					fader.Start(this, fadeDirection, duration);

					// toggle fade direction
					fadeDirection = fadeDirection == FadeDirection.In ? FadeDirection.Out : FadeDirection.In;
				}
			}
		}
		#endregion Fade Trigger

		#region Fade Logic

		public class CustomFader : Fader
		{
			private FadeSettings propertyFadeSettings;

			public CustomFader( FadeSettings propertyFadeSettings) 
			{
				this.propertyFadeSettings = propertyFadeSettings;
			}

			public override void ApplyFade(float percentage)
			{
			    float interpolatedValue = propertyFadeSettings.ease.Lerp(propertyFadeSettings.minimumValue, propertyFadeSettings.maximumValue, percentage);

				//EnviroSkyMgr.instance.SetTimeOfDay(EnviroSkyMgr.instance.GetUniversalTimeOfDay() + interpolatedValue);
				EnviroSkyMgr.instance.SetTimeOfDay(interpolatedValue);
			}
		}

		#endregion Fade Logic

#endif

	}
}