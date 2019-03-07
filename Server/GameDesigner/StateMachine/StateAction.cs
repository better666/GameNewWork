using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDesigner
{
	public enum AnimMode
	{
		Random,
		Sequence,
	}

	public enum AudioModel
	{
		EnterPlayAudio,
		AnimEventPlayAudio,
		ExitPlayAudio
	}

	public enum ActiveModel
	{
		Instantiate,
		SetActive
	}

	public enum SpwanModel
	{
		localPosition,
		SetParent,
		SetInTargetPosition,
	}

	[System.Serializable]
	public class OnAnimExit
	{
		public bool isExitState = false;
		public int DstStateID = 0;
	}

    /// <summary>
    /// ARPG状态动作
    /// </summary>
	[System.Serializable]
	public class StateAction
	{
        /// <summary>
        /// 动画剪辑名称
        /// </summary>
		public string clipName = "";
        /// <summary>
        /// 动画剪辑索引
        /// </summary>
		public int clipIndex = 0;
        /// <summary>
        /// 当前动画时间
        /// </summary>
		public float animTime = 0;
        /// <summary>
        /// 动画事件时间
        /// </summary>
		public float animEventTime = 50f;
        /// <summary>
        /// 动画结束时间
        /// </summary>
		public float animTimeMax = 100;
        /// <summary>
        /// 是否已到达事件时间或超过事件时间，到为true，没到为flase
        /// </summary>
		public bool eventEnter = false;
        /// <summary>
        /// 技能粒子物体
        /// </summary>
		public Object effectSpwan = null;
        /// <summary>
        /// 粒子物体生成模式
        /// </summary>
		public ActiveModel activeModel = ActiveModel.Instantiate;
        /// <summary>
        /// 粒子物体销毁或关闭时间
        /// </summary>
		public float spwanTime = 1f;
        /// <summary>
        /// 粒子物体对象池
        /// </summary>
		public List<GameObject> activeObjs = new List<GameObject> ();
        /// <summary>
        /// 粒子位置设置
        /// </summary>
		public SpwanModel spwanmode = SpwanModel.localPosition;
        /// <summary>
        /// 作为粒子挂载的父对象 或 作为粒子生成在此parent对象的位置
        /// </summary>
		public Transform parent = null;
        /// <summary>
        /// 粒子出生位置
        /// </summary>
		public Vector3 effectPostion = new Vector3 ( 0 , 1.5f , 2f );
        /// <summary>
        /// 是否播放音效
        /// </summary>
		public bool isPlayAudio = false;
        /// <summary>
        /// 音效触发模式
        /// </summary>
		public AudioModel audioModel = AudioModel.AnimEventPlayAudio;
        /// <summary>
        /// 音效剪辑
        /// </summary>
		public List<AudioClip> audioClips = new List<AudioClip> ();
        /// <summary>
        /// ARPG动作行为
        /// </summary>
		public List<ActionBehaviour> behaviours = new List<ActionBehaviour> ();

		public bool foldout = true, audiofoldout = false;
		public int desAudioIndex = 0, behaviourMenuIndex = 0;
		public bool findBehaviours = false;
		public string CreateScriptName = "NewStateBehaviour";
		public string scriptPath = "/GameDesigner/Actions/Skill/SkillAction";

		public StateAction()
		{
            audioClips = new List<AudioClip> {
                null
            };
            behaviours = new List<ActionBehaviour> ();
		}
	}
}