using System;
using System.Collections;
using UnityEngine;
using static Rowlan.FadeConst;

namespace Rowlan
{
	/// <summary>
	/// Example about how to fade Aura 2's fog density
	/// </summary>
	public class Aura2Fader : MonoBehaviour
	{
#if AURA_IN_PROJECT

		#region Public Variables

		[Header("Aura")]

		public Aura2API.AuraVolume auraVolume;

		[Header( "Fade")]

		[Tooltip("The minimum fog density")]
		public float minimumValue = -2f;

		[Tooltip("The maximum fog density")]
		public float maximumValue = 5f;

		[Tooltip("The fade duration in seconds")]
		public float duration = 2f;

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
			fader = new CustomFader(auraVolume);
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
			Aura2API.AuraVolume auraVolume;

			public CustomFader(Aura2API.AuraVolume auraVolume) {
				this.auraVolume = auraVolume;
			}

			public override void ApplyFade(float value)
			{
				auraVolume.densityInjection.strength = value;
			}
		}

		#endregion Fade Logic

#endif
	}
}