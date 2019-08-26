﻿using System;
using UnityEngine;

namespace MyBox
{
	[Serializable]
	public struct MinMaxFloat
	{
		public float Min;
		public float Max;
	}

	[Serializable]
	public struct MinMaxInt
	{
		public int Min;
		public int Max;
	}

	public static class MinMaxExtensions
	{
		/// <summary>
		/// Clamp value between MinMax values
		/// </summary>
		public static int Clamp(this MinMaxInt minMax, int value)
		{
			return Mathf.Clamp(value, minMax.Min, minMax.Max);
		}
		
		/// <summary>
		/// Clamp value between MinMax values
		/// </summary>
		public static float Clamp(this MinMaxFloat minMax, float value)
		{
			return Mathf.Clamp(value, minMax.Min, minMax.Max);
		}
		
		
		/// <summary>
		/// Distance from Min to Max
		/// </summary>
		public static int Length(this MinMaxInt minMax)
		{
			return minMax.Max - minMax.Min;
		}
		
		/// <summary>
		/// Distance from Min to Max
		/// </summary>
		public static float Length(this MinMaxFloat minMax)
		{
			return minMax.Max - minMax.Min;
		}
		
		
		/// <summary>
		/// Point between Min and Max
		/// </summary>
		public static int MidPoint(this MinMaxInt minMax)
		{
			return minMax.Min + minMax.Length() / 2;
		}
		
		/// <summary>
		/// Point between Min and Max
		/// </summary>
		public static float MidPoint(this MinMaxFloat minMax)
		{
			return minMax.Min + minMax.Length() / 2f;
		}
		
		
		/// <summary>
		/// Lerp from min to max
		/// </summary>
		public static float Lerp(this MinMaxFloat minMax, float value)
		{
			return Mathf.Lerp(minMax.Min, minMax.Max, value);
		}
	}
}

#if UNITY_EDITOR
namespace MyBox.Internal
{
	using UnityEditor;

	[CustomPropertyDrawer(typeof(MinMaxInt), true)]
	public class MinMaxIntDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var minProp = property.FindPropertyRelative("Min");
			var maxProp = property.FindPropertyRelative("Max");

			var minMaxLabel = "Min : Max";
			var labelWidth = 58;
			var spaceWidth = 4;
			var valWidth = (position.width / 2) - (labelWidth / 2f) - (spaceWidth * 4);

			position.width = valWidth;
			EditorGUI.PropertyField(position, minProp, GUIContent.none);

			position.x += valWidth + spaceWidth;
			position.width = labelWidth;
			EditorGUI.LabelField(position, minMaxLabel);

			position.x += labelWidth + spaceWidth;
			position.width = valWidth;
			EditorGUI.PropertyField(position, maxProp, GUIContent.none);

			if (GUI.changed)
			{
				if (maxProp.intValue < minProp.intValue) maxProp.intValue = minProp.intValue;

				property.serializedObject.ApplyModifiedProperties();
			}

			EditorGUI.EndProperty();
		}
	}


	[CustomPropertyDrawer(typeof(MinMaxFloat), true)]
	public class MinMaxFloatDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

			var minProp = property.FindPropertyRelative("Min");
			var maxProp = property.FindPropertyRelative("Max");

			var minMaxLabel = "Min : Max";
			var labelWidth = 58;
			var spaceWidth = 4;
			var valWidth = (position.width / 2) - (labelWidth / 2f) - (spaceWidth * 4);

			position.width = valWidth;
			EditorGUI.PropertyField(position, minProp, GUIContent.none);

			position.x += valWidth + spaceWidth;
			position.width = labelWidth;
			EditorGUI.LabelField(position, minMaxLabel);

			position.x += labelWidth + spaceWidth;
			position.width = valWidth;
			EditorGUI.PropertyField(position, maxProp, GUIContent.none);

			if (GUI.changed)
			{
				if (maxProp.floatValue < minProp.floatValue) maxProp.floatValue = minProp.floatValue;

				property.serializedObject.ApplyModifiedProperties();
			}

			EditorGUI.EndProperty();
		}
	}
}
#endif