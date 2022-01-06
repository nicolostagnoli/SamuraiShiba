﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crane
{
	

[System.Serializable]
public class Dialogue {

	public string name;

	[TextArea(3, 10)]
	public string[] sentences;

}
}