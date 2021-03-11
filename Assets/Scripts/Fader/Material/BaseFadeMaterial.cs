using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rowlan
{
	public abstract class BaseFadeMaterial
	{
		protected string propertyNameID;

		protected Renderer renderer;
		protected List<MaterialBlockBase> materialBlocks = new List<MaterialBlockBase>();

		public BaseFadeMaterial(Renderer renderer, string propertyNameID)
		{
			this.renderer = renderer;
			this.propertyNameID = propertyNameID;

			RegisterMaterials();
		}

		/// <summary>
		/// Collect all supported materials of the render. The supported ones are the ones which have the specified property name id.
		/// </summary>
		private void RegisterMaterials()
		{
			Material[] materials = renderer.materials;

			for (int i = 0; i < materials.Length; i++)
			{
				Material material = materials[i];

				if (!material.HasProperty(propertyNameID))
					continue;

				MaterialBlockBase materialBlock = CreateMaterialBlock(i, material);

				materialBlocks.Add(materialBlock);
			}
		}

		/// <summary>
		/// Whether or not this material is supported for fading. It is valid if at least 1 material block has been registered.
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
		{
			return materialBlocks.Count > 0;
		}

		/// <summary>
		/// Create a material data container for float or color fading.
		/// </summary>
		/// <param name="index"></param>
		/// <param name="material"></param>
		/// <returns></returns>
		public abstract MaterialBlockBase CreateMaterialBlock(int index, Material material);

		/// <summary>
		/// Process the material data containers and set the fade value.
		/// </summary>
		/// <param name="value"></param>
		public abstract void UpdateMaterials(float value);

		/// <summary>
		/// Material data container class per supported material. Supported materials are the ones which contain the given property name id
		/// </summary>
		public class MaterialBlockBase
		{
			/// <summary>
			/// The index of the material in the renderer's material list
			/// </summary>
			public int index;
			public MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();

			public MaterialBlockBase(int index)
			{
				this.index = index;
			}
		}
	}
}