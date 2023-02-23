using GameFramework.DataTable;
using System.Collections.Generic;
namespace Fishing.Data
{
    public class DialogUnitData
    {
        private DRDialogUnit dRDialogUnit;

        public int ID
        {
            get
            {
                return dRDialogUnit.Id;
            }
        }
        public int CharacterID
        {
            get
            {
                return dRDialogUnit.CharacterID;
            }
        }
        public bool IsLeft
        {
            get
            {
                return dRDialogUnit.IsLeft;
            }
        }
        public string Dialog
        {
            get{
                return dRDialogUnit.Dialog;
            }
        }
        public int VoiceID{
            get{
                return dRDialogUnit.VoiceID;
            }
        }
        public DialogUnitData(DRDialogUnit dRDialogUnit)
        {
            this.dRDialogUnit = dRDialogUnit;
        }
    }

}