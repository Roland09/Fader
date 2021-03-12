using System;
using System.Collections;
using UnityEngine;
using static Rowlan.FadeConst;

namespace Rowlan
{
	/// <summary>
	/// Fade between two Materials using Unity's internal function:
	/// 
	/// https://docs.unity3d.com/ScriptReference/Material.Lerp.html
	/// 
	/// </summary>
	public class MaterialFader : MonoBehaviour
	{
		#region Public Variables

		[Header("Renderer")]

		public Renderer gameObjectRenderer;

		[Header("Materials")]

		[Tooltip("The source material for fade in, target material for fade out")]
		public Material material1;

		[Tooltip("The target material for fade in, source material for fade out")]
		public Material material2;

		[Header("Fade")]

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
			fader = new CustomFader(gameObjectRenderer, material1, material2);
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
			Renderer renderer;
			Material material1;
			Material material2;

			public CustomFader(Renderer renderer, Material material1, Material material2)
			{
				this.renderer = renderer;
				this.material1 = material1;
				this.material2 = material2;
			}

			public override void ApplyFade(float value)
			{
				renderer.material.Lerp(material1, material2, value);
			}
		}

		#endregion Fade Logic
	}
}