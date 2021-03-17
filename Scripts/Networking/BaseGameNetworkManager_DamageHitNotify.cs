using LiteNetLib.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class BaseGameNetworkManager
    {
        [Header("Damage hit notify")]
        public ushort hitToSomeoneNotifyMessageId = 2001;
        public ushort hitFromSomeoneNotifyMessageId = 2002;
        public System.Action onHitToSomeoneNotify;
        /// <summary>
        /// Position, attacker's object ID
        /// </summary>
        public System.Action<Vector3, uint> onHitFromSomeoneNotify;
        [DevExtMethods("RegisterClientMessages")]
        public void RegisterClientMessages_DamageHitNotify()
        {
            RegisterClientMessage(hitToSomeoneNotifyMessageId, (messageHandler) =>
            {
                if (onHitToSomeoneNotify != null)
                    onHitToSomeoneNotify.Invoke();
            });
            RegisterClientMessage(hitFromSomeoneNotifyMessageId, (messageHandler) =>
            {
                var position = messageHandler.Reader.GetVector3();
                var attackerId = messageHandler.Reader.GetPackedUInt();
                if (onHitFromSomeoneNotify != null)
                    onHitFromSomeoneNotify.Invoke(position, attackerId);
            });
        }

        public void SendHitToSomeoneNotify(long connectionId)
        {
            if (!IsServer)
                return;
            ServerSendPacket(connectionId, 0, LiteNetLib.DeliveryMethod.Unreliable, hitToSomeoneNotifyMessageId);
        }

        public void SendHitFromSomeoneNotify(long connectionId, Vector3 position, uint attackerId)
        {
            if (!IsServer)
                return;
            ServerSendPacket(connectionId, 0, LiteNetLib.DeliveryMethod.Unreliable, hitFromSomeoneNotifyMessageId, (writer) =>
            {
                writer.PutVector3(position);
                writer.PutPackedUInt(attackerId);
            });
        }
    }
}
