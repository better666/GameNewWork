﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable : MonoBehaviour 
{
	void OnDisable()
	{
		gameObject.SetActive (false);
	}
}
