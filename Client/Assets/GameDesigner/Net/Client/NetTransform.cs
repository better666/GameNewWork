namespace Net.Client
{
    using System;
    using UnityEngine;

    [Serializable]
    public struct NetTransform
    {
        public Vector3 position;
        public Quaternion rotation;

        /// <summary>
        /// 设置网络位置和转向
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void SetLocation(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        /// <summary>
        /// 判断网络位置是否同步，如果出现差异则插值更新transform对象的位置为当前网络的位置
        /// </summary>
        /// <param name="transform">与网络位置判断的对象</param>
        /// <param name="distance">约束本地位置和网络位置的距离误差在distance范围内，超出则更新同步网络位置</param>
        /// <param name="lerpTime">当更新同步网络位置时的移动插值时间</param>
        public void SyncLocation(Transform transform, float distance = 2f, float lerpTime = 0.5f)
        {
            if (Vector3.Distance(transform.position, position) > distance)
            {
                transform.position = Vector3.Lerp(transform.position, position, lerpTime);
                transform.rotation = rotation;
            }
        }

        /// <summary>
        /// 同步位置
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="position"></param>
        /// <param name="distance"></param>
        /// <param name="lerpTime"></param>
        public static void SyncPosition(Transform transform, Vector3 position, float distance = 2f, float lerpTime = 0.5f)
        {
            if (Vector3.Distance(transform.position, position) > distance)
            {
                transform.position = Vector3.Lerp(transform.position, position, lerpTime);
            }
        }
    }
}
