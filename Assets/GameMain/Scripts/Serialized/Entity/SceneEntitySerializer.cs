using UnityEngine;
using GameFramework;
namespace Fishing
{
    public class SceneEntitySerializer : GameFrameworkSerializer<SceneEntityBase>
    {
        private static readonly byte[] Header = new byte[] { (byte)'F', (byte)'S', (byte)'E' };
        /// <summary>
        /// 获取物品格信息头标识。
        /// </summary>
        /// <returns>物品格信息头标识。</returns>
        protected override byte[] GetHeader()
        {
            return Header;
        }
    }
}