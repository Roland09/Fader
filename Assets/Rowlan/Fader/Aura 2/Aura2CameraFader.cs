using System;
using System.Collections;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Example about how to fade Aura 2's extinction
	/// </summary>
	public class Aura2CameraFader : MonoBehaviour
	{
#if AURA_IN_PROJECT

		#region Public Variables

		[Header("Aura")]

		public Aura2API.AuraCamera auraCamera;

		[Header( "Fade")]

		[Tooltip("The minimum fog density and he maximum fog density")]
		public FadeSettings propertyFadeSettings = new FadeSettings( -2f, 5f);

		[Tooltip("The fade duration in seconds")]
		public float duration = 2f;

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
			fader = new CustomFader(auraCamera, propertyFadeSettings);
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
			Aura2API.AuraCamera auraCamera;
			FadeSettings propertyFadeSettings;

			public CustomFader(Aura2API.AuraCamera auraCamera, FadeSettings propertyFadeSettings) {
				this.auraCamera = auraCamera;
				this.propertyFadeSettings = propertyFadeSettings;
			}

			public override void ApplyFade(float percentage)
			{
				float interpolatedValue = propertyFadeSettings.ease.Lerp(propertyFadeSettings.minimumValue, propertyFadeSettings.maximumValue, percentage);

				auraCamera.frustumSettings.baseSettings.extinction = interpolatedValue;
			}
		}

		#endregion Fade Logic

#endif
	}
}