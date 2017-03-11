using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[CustomEditor(typeof(Inventory))]
public class InventoryEditor : Editor {

	private SerializedProperty itemImagesProperty;
	private SerializedProperty itemsProperty;
	private SerializedProperty buttonsProperty;

	private bool[] showItemSlots =  new bool[Inventory.numItemSlots];

	private const string inventoryPropItemImagesName = "itemImages";
	private const string inventoryPropItemsName = "items";
	private const string inventoryPropButtonsName = "buttons";


	private void OnEnable()
	{
		itemImagesProperty = serializedObject.FindProperty (inventoryPropItemImagesName);
		itemsProperty = serializedObject.FindProperty (inventoryPropItemsName);
		buttonsProperty = serializedObject.FindProperty (inventoryPropButtonsName);

	}

	public override void OnInspectorGUI ()
	{
		serializedObject.Update ();

		for (int i = 0; i < Inventory.numItemSlots; i++) {
			ItemSlotGUI (i);
		}

		serializedObject.ApplyModifiedProperties ();


	}

	public void ItemSlotGUI(int index)
	{
		EditorGUILayout.BeginVertical (GUI.skin.box);
		EditorGUI.indentLevel++;

		showItemSlots [index] = EditorGUILayout.Foldout (showItemSlots [index], "Item slot" + index);

		if (showItemSlots [index]) 
		{
			EditorGUILayout.PropertyField (itemImagesProperty.GetArrayElementAtIndex (index));
			EditorGUILayout.PropertyField (itemsProperty.GetArrayElementAtIndex (index));
			EditorGUILayout.PropertyField (buttonsProperty.GetArrayElementAtIndex (index));

		}

		EditorGUI.indentLevel--;
		EditorGUILayout.EndVertical ();

	}



}