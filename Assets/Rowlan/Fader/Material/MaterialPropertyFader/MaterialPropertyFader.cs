using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Fade a material property value in and out with specified easing functions.
	/// 
	/// Supported property types:
	/// + Float
	/// + Color
	/// 
	/// </summary>
	public class MaterialPropertyFader : MonoBehaviour
	{
		#region Public Variables
		[Header("GameObjects")]

		public List<GameObject> sceneGameObjects = new List<GameObject>(new GameObject[1]);

		[Header("Material")]

		public MaterialPropertyType propertyType = MaterialPropertyType.Float;

		[Tooltip("The property name in the shader, e. g. _EmissionColor")]
		public string propertyNameID;

		[Header( "Fade")]

		public float minimumValue = 0f;
		public float maximumValue = 1f;

		[Tooltip("The fade duration in seconds")]
		public float duration = 1f;

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
			fader = new CustomFader(sceneGameObjects, propertyType, propertyNameID);
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
			private readonly List<BaseFadeMaterial> fadeMaterials;

			public CustomFader(List<GameObject> sceneGameObjects, MaterialPropertyType propertyType, string propertyNameID) {
				this.fadeMaterials = GetFadeMaterials(sceneGameObjects, propertyType, propertyNameID);
			}

			/// <summary>
			/// Collect all fade materials depending on the property type and name id
			/// </summary>
			/// <param name="sceneGameObject"></param>
			/// <param name="propertyType"></param>
			/// <param name="propertyNameID"></param>
			/// <returns></returns>
			private List<BaseFadeMaterial> GetFadeMaterials(List<GameObject> sceneGameObjects, MaterialPropertyType propertyType, string propertyNameID)
			{
				List<BaseFadeMaterial> fadeMaterials = new List<BaseFadeMaterial>();

				foreach (GameObject sceneGameObject in sceneGameObjects)
				{
					Renderer[] rendererChildren = sceneGameObject.GetComponentsInChildren<Renderer>();

					foreach (Renderer renderer in rendererChildren)
					{
						BaseFadeMaterial fadeMaterial = null;

						switch (propertyType)
						{
							case MaterialPropertyType.Float:
								fadeMaterial = new FloatFadeMaterial(renderer, propertyNameID);
								break;
							case MaterialPropertyType.Color:
								fadeMaterial = new ColorFadeMaterial(renderer, propertyNameID);
								break;
						}

						if (fadeMaterial.IsValid())
						{
							fadeMaterials.Add(fadeMaterial);
						}
					}
				}

				return fadeMaterials;
			}

			public CustomFader(List<BaseFadeMaterial> fadeMaterials) {
				this.fadeMaterials = fadeMaterials;
			}

			public override void ApplyFade(float value)
			{
				foreach (BaseFadeMaterial fadeMaterial in fadeMaterials)
				{
					fadeMaterial.UpdateMaterials(value);
				}
			}
		}

		#endregion Fade Logic
	}
}