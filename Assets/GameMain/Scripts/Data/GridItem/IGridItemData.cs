namespace Fishing.Data
{
    public interface IGridItemData
    {
        public int ID
        {
            get;
        }
        public string ItemName
        {
            get;
        }
        public string ItemType
        {
            get;
        }
        public int Level
        {
            get;
        }
        public int ImageID
        {
            get;
        }
        public string ItemDescription
        {
            get;
        }
        public int Price{
            get;
        }
    }
}
