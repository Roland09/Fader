using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Fade one or more material property values and colors in and out with specified easing functions.
	/// 
	/// Supported property types:
	/// + Float
	/// + Color
	/// 
	/// </summary>
	public class MaterialPropertyFader : MonoBehaviour
	{
		#region Public Variables
		
		// [Header("GameObjects")]

		public List<GameObject> sceneGameObjects = new List<GameObject>(new GameObject[1]);

		// [Header("Material Settings")]

		public List<FadeSettings> propertyFadeSettings = new List<FadeSettings>( new FadeSettings[1]);

		// [Space]

		[Header("Interpolation")]

		[Tooltip("The fade duration in seconds")]
		public float duration = 1f;

		[Header("Input")]

		[Tooltip("The input key that triggers the fading")]
		public KeyCode toggleKeyCode = KeyCode.None;

		#endregion Public Variables

		#region Internal Variables
		private FadeDirection fadeDirection = FadeDirection.In;

		private InputToggleFader fader;
		#endregion Internal Variables

		#region Initialization
		void Start()
		{
			fader = new CustomFader(sceneGameObjects, propertyFadeSettings);
		}
		
		#endregion Initialization

		#region Fade Trigger
		void Update()
		{
			// check if the toggle key got pressed and if so start the fading and toggle the fade direction afterwards
			fader.StartToggleOnInput(toggleKeyCode, this, ref fadeDirection, duration);
		}
		#endregion Fade Trigger

		#region Fade Logic

		public class CustomFader : InputToggleFader
		{
			private readonly List<FadeMaterial> fadeMaterials;

			public CustomFader(List<GameObject> sceneGameObjects, List<FadeSettings> propertyFadeSettings) {
				this.fadeMaterials = GetFadeMaterials(sceneGameObjects, propertyFadeSettings);
			}

			/// <summary>
			/// Collect all fade materials depending on the property type and name id
			/// </summary>
			/// <param name="sceneGameObject"></param>
			/// <param name="propertyType"></param>
			/// <param name="propertyNameID"></param>
			/// <returns></returns>
			private List<FadeMaterial> GetFadeMaterials(List<GameObject> sceneGameObjects, List<FadeSettings> propertyFadeSettings)
			{
				List<FadeMaterial> fadeMaterials = new List<FadeMaterial>();

				foreach (GameObject sceneGameObject in sceneGameObjects)
				{
					Renderer[] rendererChildren = sceneGameObject.GetComponentsInChildren<Renderer>();

					foreach (Renderer renderer in rendererChildren)
					{
						FadeMaterial fadeMaterial = new FadeMaterial(renderer, propertyFadeSettings);

						if (fadeMaterial.IsValid())
						{
							fadeMaterials.Add(fadeMaterial);
						}
					}
				}

				return fadeMaterials;
			}

			public CustomFader(List<FadeMaterial> fadeMaterials) {
				this.fadeMaterials = fadeMaterials;
			}

			public override void ApplyFade(float percentage)
			{
				foreach (FadeMaterial fadeMaterial in fadeMaterials)
				{
					fadeMaterial.UpdateMaterials(percentage);
				}
			}
		}

		#endregion Fade Logic
	}
}