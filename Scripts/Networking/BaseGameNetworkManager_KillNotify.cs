using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class BaseGameNetworkManager
    {
        [Header("Kill notify")]
        public ushort killNotifyMessageId = 2000;
        /// <summary>
        /// Killer Name, Victim Name, Weapon ID, Skill ID, Skill Level
        /// </summary>
        public System.Action<string, string, int, int, short> onKillNotify;
        [DevExtMethods("RegisterClientMessages")]
        public void RegisterClientMessages_KillNotify()
        {
            RegisterClientMessage(killNotifyMessageId, (messageHandler) =>
            {
                var killerName = messageHandler.Reader.GetString();
                var victimName = messageHandler.Reader.GetString();
                var weaponId = messageHandler.Reader.GetInt();
                var skillId = messageHandler.Reader.GetInt();
                var skillLevel = messageHandler.Reader.GetShort();
                if (onKillNotify != null)
                    onKillNotify.Invoke(killerName, victimName, weaponId, skillId, skillLevel);
            });
        }

        public void SendKillNotify(string killerName, string victimName, int weaponId, int skillId, short skillLevel)
        {
            if (!IsServer)
                return;
            ServerSendPacketToAllConnections(0, LiteNetLib.DeliveryMethod.Sequenced, killNotifyMessageId, (writer) =>
            {
                writer.Put(killerName);
                writer.Put(victimName);
                writer.Put(weaponId);
                writer.Put(skillId);
                writer.Put(skillLevel);
            });
        }
    }
}
