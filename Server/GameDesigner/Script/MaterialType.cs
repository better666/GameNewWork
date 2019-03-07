using UnityEngine;
using System.Collections;

public class MaterialType : MonoBehaviour {

	public MaterialTypeEnum TypeOfMaterial = MaterialTypeEnum.Ground;

    [System.Serializable]
	public enum MaterialTypeEnum
	{
		Ground,//地面
		Woodenbox,//木箱
		humanbody,//人体
		Zombie,//僵尸体
	}
}
