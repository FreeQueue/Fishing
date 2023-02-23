using System;
namespace Fishing
{
    public static class EnumExtension
    {
        public static EnumIntData ToIntData(this EnumBuff enumBuff)
        {
            return (EnumIntData)Enum.Parse(typeof(EnumIntData), enumBuff.ToString());
        }
    }
}