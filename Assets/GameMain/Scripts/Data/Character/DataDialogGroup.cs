using GameFramework.DataTable;
using System.Collections.Generic;
namespace Fishing.Data
{
    public class DataDialogGroup : DataBase{
        private IDataTable<DRDialogUnit> dtDialogUnit;
        private const int GroupSize=100;
        private Dictionary<int, List<DialogUnitData>> dicDialogGroupData;

        protected override void OnPreload()
        {
            LoadDataTable("DialogUnit");
        }

        protected override void OnLoad()
        {
            dtDialogUnit = GameEntry.DataTable.GetDataTable<DRDialogUnit>();
            if (dtDialogUnit == null)
                throw new System.Exception("Can not get data table Item");

            dicDialogGroupData = new Dictionary<int,List<DialogUnitData>>();

            DRDialogUnit[] drDialogUnits = dtDialogUnit.GetAllDataRows();
            int index;
            foreach (var drDialogUnit in drDialogUnits)
            {
                DialogUnitData dialogUnitData = new DialogUnitData(drDialogUnit);
                index = dialogUnitData.ID / GroupSize;
                if(!dicDialogGroupData.ContainsKey(index))
                {
                    dicDialogGroupData.Add(index, new List<DialogUnitData>());
                }
                dicDialogGroupData[index].Add(dialogUnitData);
            }
        }

        public List<DialogUnitData> GetDialogGroupData(int id)
        {
            if (dicDialogGroupData.ContainsKey(id))
            {
                return dicDialogGroupData[id];
            }
            return null;
        }


        protected override void OnUnload()
        {
            GameEntry.DataTable.DestroyDataTable<DRDialogUnit>();
            dtDialogUnit = null;
            dicDialogGroupData = null;
        }

        protected override void OnShutdown()
        {
        }
    }
}