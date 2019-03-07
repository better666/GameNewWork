using UnityEngine;
using System.Collections;

namespace GameDesigner
{
	/// <summary>
	/// 状态过渡事件选项模式
	/// </summary>

	public enum TransitionModel
	{
		/// 这个过渡是用时间来转接过渡的
		ExitTime = 0,
		ScriptControl = 1,
	}
}