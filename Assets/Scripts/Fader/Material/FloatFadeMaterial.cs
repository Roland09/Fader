using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rowlan
{
	/// <summary>
	/// Material float value fading handler.
	/// </summary>
	public class FloatFadeMaterial : BaseFadeMaterial
	{
		public FloatFadeMaterial(Renderer renderer, string propertyNameID) : base(renderer, propertyNameID)
		{
		}

		public override MaterialBlockBase CreateMaterialBlock(int index, Material material)
		{
			FloatMaterialBlock data = new FloatMaterialBlock(index, material.GetFloat(propertyNameID));

			return data;
		}

		public override void UpdateMaterials(float value)
		{
			foreach (FloatMaterialBlock materialBlock in materialBlocks)
			{
				materialBlock.propertyBlock.SetFloat(propertyNameID, value);

				renderer.SetPropertyBlock(materialBlock.propertyBlock, materialBlock.index);
			}

			//renderer.UpdateGIMaterials();
		}

		public class FloatMaterialBlock : MaterialBlockBase
		{
			/// <summary>
			/// The initial float value of the material property
			/// </summary>
			public float value;

			public FloatMaterialBlock(int index, float value) : base(index)
			{
				this.value = value;
			}

		}
	}
}