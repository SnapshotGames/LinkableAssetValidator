// Copyright (c) Snapshot Games 2014, All Rights Reserved, http://www.snapshotgames.com

using System;
using System.Reflection;
using UnityEngine;

public static class AssetValidator
{
	/// <summary>
	/// Validate marked asset fields. Returns true if all fields have references.
	/// </summary>
	/// <param name="objRef">Validated component</param>
	public static bool ValidateAssets(MonoBehaviour objRef)
	{
		Type currentValidatedType = objRef.GetType();
		FieldInfo[] fields = currentValidatedType.GetFields(
			BindingFlags.Public | BindingFlags.Instance);

		bool allFieldsAreValid = true;
		foreach (FieldInfo field in fields) {
			Attribute linkableUIAsset = field.GetCustomAttribute(typeof(LinkableAsset));
			if (linkableUIAsset == null)
				continue;

			UnityEngine.Object obj = field.GetValue(objRef) as UnityEngine.Object;
			if (obj == null) {
				allFieldsAreValid = false;
				Debug.LogError($"Unlinked asset detected!  Component: {currentValidatedType.Name} in {objRef} has unlinked asset field: {field.Name}!", objRef.gameObject);
			}
		}

		return allFieldsAreValid;
	}

	/// <summary>
	/// Validate marked assets in component in specified game object.
	/// </summary>
	/// <param name="gameObject">Game object where the component would be searched for</param>
	/// <param name="validateComponentType">Type of the validated component</param>
	/// <exception cref="ArgumentException">Would be thrown if <paramref name="validateComponentType"/> is not <see cref ="MonoBehaviour"/>
	/// or couldn't be found in <paramref name="gameObject"/></exception>
	public static bool ValidateAssets(GameObject gameObject, Type validateComponentType)
	{
		Component comp = gameObject.GetComponent(validateComponentType);
		if (comp != null) {
			if (comp is MonoBehaviour) {
				return ValidateAssets(comp as MonoBehaviour);
			}

			throw new ArgumentException($"Component of type '{validateComponentType} is not valid '{typeof(MonoBehaviour)}'.");
		}

		throw new ArgumentException($"Couldn't find attached component of type '{validateComponentType}' in '{gameObject.name}' game object");
	}
}
