using LiteNetLibManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class BaseGameNetworkManager
    {
        [DevExtMethods("InitPrefabs")]
        protected void RegisterCorpseEntityPrefab()
        {
            List<LiteNetLibIdentity> assets = new List<LiteNetLibIdentity>(Assets.spawnablePrefabs);
            if (CurrentGameInstance.corpseEntityPrefab != null)
                assets.Add(CurrentGameInstance.corpseEntityPrefab.Identity);
            Assets.spawnablePrefabs = assets.ToArray();
        }
    }
}
