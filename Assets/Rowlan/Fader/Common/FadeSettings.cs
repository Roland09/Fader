using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	[Serializable]
    public class FadeSettings
    {
		[Header("Material")]

		public MaterialPropertyType propertyType = MaterialPropertyType.Float;

		[Tooltip("The property name in the shader, e. g. _EmissionColor")]
		public string propertyNameID;

		[Header("Fade")]

		public float minimumValue = 0f;
		public float maximumValue = 1f;

		[Tooltip("The easing meachinsm")]
		public Ease ease = Ease.Linear;

		public FadeSettings() : this ( 0f, 1f)
		{
		}

		public FadeSettings(float minimumValue, float maximumValue)
		{
			this.minimumValue = minimumValue;
			this.maximumValue = maximumValue;
		}

	}
}