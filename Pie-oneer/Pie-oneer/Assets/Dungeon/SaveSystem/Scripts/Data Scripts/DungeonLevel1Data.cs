using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DungeonLevel1Data
{
    //Enemies is hallways
    public List<EnemyData> enemiesData;

    //Rooms
    public DungeonL1PotionRiddleRoom potionRiddleRoom;
    public DungeonL1DarkRoom darkRoom;
    public DungeonL1GhostRoom ghostRoom;
    public DungeonL1TraderRoom traderRoom;
    public DungeonL1MiniBossRoom miniBossRoom;
}

[System.Serializable]
public class DungeonL1PotionRiddleRoom
{
    public bool wasPotionChosen;
}

[System.Serializable]
public class DungeonL1DarkRoom
{
    public List<bool> areBarrelsDestroyed;
}

[System.Serializable]
public class DungeonL1GhostRoom
{
    public int riddleStage;
}

[System.Serializable]
public class DungeonL1TraderRoom
{
    public string[] items;
}

[System.Serializable]
public class DungeonL1MiniBossRoom
{
    public bool miniBossIsDead;
}
