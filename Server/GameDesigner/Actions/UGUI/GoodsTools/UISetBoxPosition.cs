#if UNITY_EDITOR
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UISetBoxPosition : MonoBehaviour 
{
    public GameObject           boxObject;
    public List<RectTransform>  boxrt;
    public List<Vector3>        boxPoint;

    public int                  设置横向格子最大值 = 9;
    public int                  当前横向格子值 = 0;

    public int                  设置竖向格子最大值 = 7;
    public int                  当前竖向格子值 = 0;

    public float                设置初始化横向位置 = -230f;
    public float                设置初始化竖向位置 = 160f;

    public float                横向位置 = -230f;
    public float                竖向位置 = 160f;

    public float                设置横向移动距离 = 55f;
    public float                设置竖向移动距离 = -55f;

    public bool                 setboxpoint = false;
    public bool                 createBox = false;
    public bool                 desBox = false;

    public int                  boxInt = 0;

    protected bool              initList = true;

    public void SetBoxPosition ( ) 
    {
        if( setboxpoint == false ) return ;

        for( int i = 0 ; i < boxrt.Count ; i++ )
        {
            if ( boxrt [ i ] == null ) 
            {
                boxrt.Remove ( boxrt[i] );
                boxPoint.Remove ( boxPoint [ i ] );
                return;
            }
            boxrt [ i ].anchoredPosition3D = boxPoint[i];
        }
        //setboxpoint = false;
    }

    public void CreateBoxImage ( ) 
    {
        if( createBox == false ) return;

        if( null == boxObject )
        {
            Debug.Log ( "没有物品格子预制物体！" );
            return;
        }

        if( initList )
        {
            boxrt = new List<RectTransform> ( );
            boxPoint = new List<Vector3> ( );
            initList = false;
        }

        if ( 当前横向格子值 == 设置横向格子最大值 )
        {
            竖向位置 += 设置竖向移动距离;
            横向位置 = 设置初始化横向位置;
            当前横向格子值 = 0;
            ++当前竖向格子值;
        }

        if ( 当前竖向格子值 >= 设置竖向格子最大值 )
        {
            竖向位置 = 设置初始化竖向位置;
            横向位置 = 设置初始化横向位置;
            当前竖向格子值 = 0;
            createBox = false;
            return;
        }

        ++当前横向格子值;

        GameObject box = Instantiate ( boxObject ) as GameObject;
        box.name = boxInt.ToString();
        box.transform.SetParent ( transform );
        RectTransform rt = box.GetComponent<RectTransform>();

        ++boxInt;

        rt.anchoredPosition3D = new Vector3 ( 横向位置 , 竖向位置 , 1f );
        rt.localScale = new Vector3 ( 1,1,1 );

        横向位置 += 设置横向移动距离;

        boxrt.Add ( rt );
        boxPoint.Add ( rt.anchoredPosition3D );
    }

    public void DesBoxs ( ) 
    {
        if ( desBox == false ) return;

        for ( int i = 0 ; i < boxrt.Count ; )
        {
            DestroyImmediate ( boxrt [ i ].gameObject );
            boxrt.Remove ( boxrt [ i ] );
            boxPoint.Remove ( boxPoint [ i ] );
            i++;
            return;
        }

        desBox = false;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate() 
	{
		SetBoxPosition();
		CreateBoxImage();
		DesBoxs();
	}
}
#endif