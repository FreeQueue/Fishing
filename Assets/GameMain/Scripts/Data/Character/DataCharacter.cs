using GameFramework.DataTable;
using System.Collections.Generic;
namespace Fishing.Data
{
    public class DataCharacter : DataBase{
        private IDataTable<DRCharacter> dtCharacter;

        private Dictionary<int, CharacterData> dicCharacterData;

        protected override void OnInit()
        {

        }

        protected override void OnPreload()
        {
            LoadDataTable("Character");
        }

        protected override void OnLoad()
        {
            dtCharacter = GameEntry.DataTable.GetDataTable<DRCharacter>();
            if (dtCharacter == null)
                throw new System.Exception("Can not get data table Item");

            dicCharacterData = new Dictionary<int,CharacterData>();

            DRCharacter[] drCharacters = dtCharacter.GetAllDataRows();
            foreach (var drCharacter in drCharacters)
            {
                CharacterData characterData = new CharacterData(drCharacter);
                dicCharacterData.Add(drCharacter.Id, characterData);
            }
        }

        public CharacterData GetCharacterData(int id)
        {
            if (dicCharacterData.ContainsKey(id))
            {
                return dicCharacterData[id];
            }
            return null;
        }
        public CharacterData GetCharacterData(EnumCharacter enumCharacter)
        {
            int id = ((int)enumCharacter);
            if (dicCharacterData.ContainsKey(id))
            {
                return dicCharacterData[id];
            }
            return null;
        }
        public CharacterData[] GetAllCharacterData()
        {
            int index = 0;
            CharacterData[] results = new CharacterData[dicCharacterData.Count];
            foreach (var characterData in dicCharacterData.Values)
            {
                results[index++] = characterData;
            }
            return results;
        }

        protected override void OnUnload()
        {
            GameEntry.DataTable.DestroyDataTable<DRCharacter>();
            dtCharacter = null;
            dicCharacterData = null;
        }

        protected override void OnShutdown()
        {
        }
    }

    public class CharacterData
    {
        private DRCharacter dRCharacter;

        public int ID
        {
            get
            {
                return dRCharacter.Id;
            }
        }
        public string CharacterName
        {
            get
            {
                return dRCharacter.CharacterName;
            }
        }
        public string NameTag
        {
            get
            {
                return dRCharacter.NameTag;
            }
        }
        public int SpeakVoiceIndex
        {
            get{
                return dRCharacter.SpeakVoiceIndex;
            }
        }
        
        public CharacterData(DRCharacter dRCharacter)
        {
            this.dRCharacter = dRCharacter;
        }
    }
}