using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof(SkillLock))]
public class SkillLockEditor : Editor 
{
	SkillLock slock;

	string[] names = new string[ 0 ];

	Component[] coms = new Component[0];

	public override void OnInspectorGUI ()
	{
		slock = target as SkillLock;
		for( int n = 0 ; n < slock.set_Open.Count ; ++n )
		{
			if( slock.openIndex.Count < slock.set_Open.Count ){ slock.openIndex.Add(0); }
			slock.openIndex[n] = EditorGUILayout.Popup ( "set_Open " + n , slock.openIndex[n] , names );
			if( slock.openIndex[n] > coms.Length ) slock.openIndex[n] = 0;
			if( coms.Length > 0 ) slock.set_Open[n] = coms [ slock.openIndex[n] ];
			if( slock.set_Open[n] is Component ) continue;
			if( slock.set_Open[n] == null ) continue;
			GameObject go = slock.set_Open[n] as GameObject;
			coms = go.GetComponents<Component>();
			names = new string[ coms.Length ];
			for( int i = 1 ; i < names.Length ;++i )
			{
				names[i] = coms[i].GetType().Name;
			}
		}
		for( int n = 0 ; n < slock.set_Off.Count ; ++n )
		{
			if( slock.offIndex.Count < slock.set_Off.Count ){ slock.offIndex.Add(0); }
			slock.offIndex[n] = EditorGUILayout.Popup ( "set_Off " + n , slock.offIndex[n] , names );
			if( slock.offIndex[n] > coms.Length ) slock.offIndex[n] = 0;
			if( coms.Length > 0 ) slock.set_Off[n] = coms [ slock.offIndex[n] ];
			if( slock.set_Off[n] is Component ) continue;
			if( slock.set_Off[n] == null ) continue;
			GameObject go = slock.set_Off[n] as GameObject;
			coms = go.GetComponents<Component>();
			names = new string[ coms.Length ];
			for( int i = 1 ; i < names.Length ;++i )
			{
				names[i] = coms[i].GetType().Name;
			}
		}

		base.OnInspectorGUI ();
	}
}
