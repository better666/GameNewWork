using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDesigner
{
    /// <summary>
    /// 计时器
    /// </summary>
	[System.Serializable]
	public class Timer
	{
		public float time;

		[SerializeField]
		private float _EndTime;
		public float EndTime{
			get{
				if(_EndTime<=0){
					_EndTime=1.0F;
				}
				return _EndTime;
			}
			set{ _EndTime = value; }
		}

		public Timer( float timeMax )
		{
			time = 0;
			_EndTime = timeMax;
		}

		public Timer( float _time , float timeMax )
		{
			time = _time;
			_EndTime = timeMax;
		}

        private static List<Timer> Timers = new List<Timer>();

        /// <summary>
        /// 是否到达指定时间 offset必须是独有的,否则将会造成共用计时器
        /// </summary>
        public static bool SpecifiedTime(int offset,float timeMax)
        {
            while (Timers.Count <= offset) {
                Timers.Add(new Timer(timeMax));
            }
            return Timers[offset].UpdateTime(timeMax,true);
        }

        /// <summary>
        /// 获得时间是否到达时间的最大值 , 到达后m_Time归零
        /// </summary>
        public bool IsTimeOut
		{
			get{ return UpdateTime(); }
		}

        /// <summary>
        /// 当时间到达时间的最大值后返回真 , 到达后m_Time不归零
        /// </summary>
        public bool OnTimeOut
		{
			get{ return UpdateTime( false ); }
		}

        /// <summary>
        /// 计时器是否到达最大值,到达则不再计时
        /// </summary>
        public bool IsOutTime{
			get{
				if (time > _EndTime)
					return true;
				time += Time.deltaTime;
				return false;
			}
		}

        /// <summary>
        /// 如果时间大于定义的时间则返回真,zero参数为真则时间大于定义的时间后归零
        /// </summary>
        public bool UpdateTime( bool zero = true )
		{
			if(time > _EndTime){
				time = zero ? 0 : time;
				return true;
			}
            time += Time.deltaTime;
            return false;
		}

        /// <summary>
        /// 如果时间大于定义的时间则返回真,zero参数为真则时间大于定义的时间后归零
        /// </summary>
        public bool UpdateTime( float timeMax , bool zero = true )
		{
			if(time > timeMax){
				time = zero ? 0 : time;
				return true;
			}
            time += Time.deltaTime;
            return false;
		}
	}
}