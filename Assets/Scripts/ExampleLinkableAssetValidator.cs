// Copyright (c) Snapshot Games 2014, All Rights Reserved, http://www.snapshotgames.com

using UnityEngine;
using UnityEngine.UI;

public class ExampleLinkableAssetValidator : MonoBehaviour
{
	[LinkableAsset] public GameObject DemoGameObject;
	[LinkableAsset] public Transform DemoTransform;
	[LinkableAsset] public Text DemoUITextField;
	[LinkableAsset] public Image DemoUIImage;
	[LinkableAsset] public RectTransform DemoUIRectTransform;

	private void OnValidate()
	{
		AssetValidator.ValidateAssets(this);

		//Indirect validation on specified gameobject
		AssetValidator.ValidateAssets(DemoUIRectTransform.gameObject, typeof(ExampleUIValidator));
	}
}
