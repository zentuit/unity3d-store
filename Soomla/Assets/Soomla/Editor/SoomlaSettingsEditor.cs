using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Soomla;

[CustomEditor(typeof(SoomlaEditorScript))]
public class SoomlaSettingsEditor : Editor
{
	public void OnEnable() 
	{
		SoomlaEditorScript.Instance.OnEnable();
	}

    public override void OnInspectorGUI()
    {
		SoomlaEditorScript.Instance.OnInspectorGUI();
    }


}
