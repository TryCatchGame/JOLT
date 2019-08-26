﻿// ---------------------------------------------------------------------------- 
// Author: Richard Fine
// Source: https://bitbucket.org/richardfine/scriptableobjectdemo
// ----------------------------------------------------------------------------

using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

namespace MyBox
{
	public class MinMaxRangeAttribute : Attribute
	{
		public MinMaxRangeAttribute(float min, float max)
		{
			Min = min;
			Max = max;
		}

		public readonly float Min;
		public readonly float Max;
	}

	[Serializable]
	public struct RangedFloat
	{
		public float Min;
		public float Max;
	}

	[Serializable]
	public struct RangedInt
	{
		public int Min;
		public int Max;
	}
	
	public static class RangedExtensions 
	{
		public static float LerpFromRange(this RangedFloat ranged, float t)
		{
			return Mathf.Lerp(ranged.Min, ranged.Max, t);
		}
		
		public static float LerpFromRangeUnclamped(this RangedFloat ranged, float t)
		{
			return Mathf.LerpUnclamped(ranged.Min, ranged.Max, t);
		}		
		
		public static float LerpFromRange(this RangedInt ranged, float t)
		{
			return Mathf.Lerp(ranged.Min, ranged.Max, t);
		}
		
		public static float LerpFromRangeUnclamped(this RangedInt ranged, float t)
		{
			return Mathf.LerpUnclamped(ranged.Min, ranged.Max, t);
		}
	}
}

#if UNITY_EDITOR
namespace MyBox.Internal
{
	[CustomPropertyDrawer(typeof(RangedInt), true)]
	public class MinMaxRangeIntAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			label = EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, label);

			SerializedProperty minProp = property.FindPropertyRelative("Min");
			SerializedProperty maxProp = property.FindPropertyRelative("Max");

			float minValue = minProp.intValue;
			float maxValue = maxProp.intValue;

			float rangeMin = 0;
			float rangeMax = 1;

			var ranges = (MinMaxRangeAttribute[]) fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
			if (ranges.Length > 0)
			{
				rangeMin = ranges[0].Min;
				rangeMax = ranges[0].Max;
			}

			const float rangeBoundsLabelWidth = 40f;

			var rangeBoundsLabel1Rect = new Rect(position);
			rangeBoundsLabel1Rect.width = rangeBoundsLabelWidth;
			GUI.Label(rangeBoundsLabel1Rect, new GUIContent(minValue.ToString("F2")));
			position.xMin += rangeBoundsLabelWidth;

			var rangeBoundsLabel2Rect = new Rect(position);
			rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
			GUI.Label(rangeBoundsLabel2Rect, new GUIContent(maxValue.ToString("F2")));
			position.xMax -= rangeBoundsLabelWidth;

			EditorGUI.BeginChangeCheck();
			EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
			if (EditorGUI.EndChangeCheck())
			{
				minProp.intValue = Mathf.RoundToInt(minValue);
				maxProp.intValue = Mathf.RoundToInt(maxValue);
			}

			EditorGUI.EndProperty();
		}
	}

	[CustomPropertyDrawer(typeof(RangedFloat), true)]
	public class MinMaxRangeFloatAttributeDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			label = EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, label);

			SerializedProperty minProp = property.FindPropertyRelative("Min");
			SerializedProperty maxProp = property.FindPropertyRelative("Max");

			float minValue = minProp.floatValue;
			float maxValue = maxProp.floatValue;

			float rangeMin = 0;
			float rangeMax = 1;

			var ranges = (MinMaxRangeAttribute[]) fieldInfo.GetCustomAttributes(typeof(MinMaxRangeAttribute), true);
			if (ranges.Length > 0)
			{
				rangeMin = ranges[0].Min;
				rangeMax = ranges[0].Max;
			}

			const float rangeBoundsLabelWidth = 40f;

			var rangeBoundsLabel1Rect = new Rect(position);
			rangeBoundsLabel1Rect.width = rangeBoundsLabelWidth;
			GUI.Label(rangeBoundsLabel1Rect, new GUIContent(minValue.ToString("F2")));
			position.xMin += rangeBoundsLabelWidth;

			var rangeBoundsLabel2Rect = new Rect(position);
			rangeBoundsLabel2Rect.xMin = rangeBoundsLabel2Rect.xMax - rangeBoundsLabelWidth;
			GUI.Label(rangeBoundsLabel2Rect, new GUIContent(maxValue.ToString("F2")));
			position.xMax -= rangeBoundsLabelWidth;

			EditorGUI.BeginChangeCheck();
			EditorGUI.MinMaxSlider(position, ref minValue, ref maxValue, rangeMin, rangeMax);
			if (EditorGUI.EndChangeCheck())
			{
				minProp.floatValue = minValue;
				maxProp.floatValue = maxValue;
			}

			EditorGUI.EndProperty();
		}
	}
}
#endif