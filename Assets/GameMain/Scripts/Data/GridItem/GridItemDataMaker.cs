using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Data;
using GameFramework.DataTable;
using UnityGameFramework.Runtime;
namespace Fishing.Data
{
    public class GridItemDataMaker
    {
        public static IGridItemData GetGridItemData(int itemID)
        {
            switch (itemID / 1000)
            {
                case 1:
                    return GameEntry.Data.GetData<DataSpoil>().GetSpoilData(itemID);
                case 2:
                    return GameEntry.Data.GetData<DataProp>().GetPropData(itemID);
                default:
                    Log.Warning("The itemID do not in any table");
                    break;
            }

            return null;
        }
    }
}