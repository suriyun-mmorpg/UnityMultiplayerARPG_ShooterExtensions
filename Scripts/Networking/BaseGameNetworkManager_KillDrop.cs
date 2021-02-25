using LiteNetLibManager;
using System.Collections;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    public partial class BaseGameNetworkManager
    {
        [DevExtMethods("InitPrefabs")]
        protected void RegisterCorpseEntityPrefab()
        {
            List<LiteNetLibIdentity> assets = new List<LiteNetLibIdentity>(Assets.spawnablePrefabs);
            if (CurrentGameInstance.corpsePrefab != null)
                assets.Add(CurrentGameInstance.corpsePrefab.Identity);
            Assets.spawnablePrefabs = assets.ToArray();
        }
    }
}
