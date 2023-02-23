using UnityEngine;
using Fishing;
public class TimeLineTest : MonoBehaviour
{
    [ContextMenu("play test")]
    public void StartTimeLine()
    {
        GameEntry.TimeLine.PlayTimeLine(EnumTimeLine.TestTimeLine);
    }
}
