using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class GameInstance
    {
        [Header("Shooter Extensions - Kill Drop")]
        public bool turnOnKillDrop;
        public bool killDropEquipItems = true;
        public bool killDropNonEquipItems = true;
        public ItemsContainerEntity corpsePrefab;
    }
}
