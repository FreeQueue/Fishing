using UnityEngine;
using System.Collections.Generic;

namespace Fishing
{
    [CreateAssetMenu(fileName = "New ItemImageList", menuName = "Scriptable/Create ItemImageList", order = 1)]
    public class ItemImage : ScriptableObject
    {
        [SerializeField]
        private List<Sprite> ImageList;
        public Sprite GetImage(int ImageID)
        {
            return ImageList[ImageID];
        }
    }
}