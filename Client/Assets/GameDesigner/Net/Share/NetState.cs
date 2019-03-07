using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net.Share
{
    /// <summary>
    /// 网络玩家状态
    /// </summary>
    public enum NetState
    {
        /// <summary>
        /// 待机状态
        /// </summary>
        Idle,
        /// <summary>
        /// 等待其他玩家加入状态
        /// </summary>
        WaitTeam,
        /// <summary>
        /// 组队状态
        /// </summary>
        Team,
        /// <summary>
        /// 作战中
        /// </summary>
        InCombat,
        /// <summary>
        /// 玩家掉线
        /// </summary>
        Disconnected,
        /// <summary>
        /// 离开状态
        /// </summary>
        AwayStatus,

    }
}
