using UnityEngine;
using System.Collections.Generic;

public class UIDepth : MonoBehaviour 
{
	public List<RectTransform> depth = new List<RectTransform>();
	public List<Vector3> penV3 = new List<Vector3>();
	public List<Vector3> outV3 = new List<Vector3>();

	public void Open( int list )
	{
		depth[ list ].anchoredPosition3D = penV3[ list ];
	}

	public void Out()
	{
		for( int i = 0 ; i < depth.Count ; i ++ )
		{
			depth[i].anchoredPosition3D = outV3[i];
		}
	}

	public void Off( int list )
	{
		depth[ list ].gameObject.SetActive( false );
	}
}
