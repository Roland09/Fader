using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Rowlan.Fader.FadeConst;

namespace Rowlan.Fader
{
	/// <summary>
	/// Various enums and types used for the fading parameters
	/// </summary>
	public class FadeConst
	{
		public enum FadeDirection
		{
			In,
			Out
		}

		public enum PropertyType
		{
			Float,
			Color
		}

		public enum Ease
		{
			Linear,
			EaseInQuad,
			EaseOutQuad
		}
	}

	/// <summary>
	/// Perform the Lerp algorithm depending on the selected ease type
	/// </summary>
	public static class EaseExtensions
	{
		public static float Lerp(this Ease ease, float start, float end, float value)
		{
			switch (ease)
			{
				case Ease.Linear: return Mathf.Lerp(start, end, value);
				case Ease.EaseInQuad: end -= start; return end * value * value + start;
				case Ease.EaseOutQuad: end -= start; return -end * value * (value - 2) + start;
				default: throw new ArgumentOutOfRangeException("Unsupported parameter " + ease);
			}
		}

	}
}
