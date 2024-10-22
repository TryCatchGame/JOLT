﻿using UnityEngine;

namespace MyBox
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();
					if (_instance == null)
					{
						Debug.LogError("Singleton Instance caused: " + typeof(T).Name + " not found on scene");
					}
				}

				return _instance;
			}
		}
	}
}