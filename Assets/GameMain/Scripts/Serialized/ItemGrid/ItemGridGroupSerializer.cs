using GameFramework;

namespace Fishing
{
    /// <summary>
    /// 物品格信息序列化器
    /// </summary>
    public class ItemGridGroupSerializer: GameFrameworkSerializer<ItemGridGroupBase>
    {
        private static readonly byte[] Header = new byte[] { (byte)'F', (byte)'I', (byte)'G' };
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