using UnityEngine;
using System.Collections;

//武器类型枚举
public enum WeaponType
{
    /// <summary>
    /// 空类 没有任何作用类
    /// </summary>
    None,
    /// <summary>
    /// 枪类 包含所有远程射击类型
    /// </summary>
    Gun,
    /// <summary>
    /// 刀类 包含所有近战类型 如匕首,甩枪,肉搏
    /// </summary>
    Knife,
    /// <summary>
    /// 投掷类 包含所有扔,抛类型 如手榴弹
    /// </summary>
    Toss,
}