using LiteNetLib;
using LiteNetLib.Utils;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class BaseGameNetworkManager
    {
        [Header("Damage hit to someone notify")]
        public ushort hitToSomeoneNotifyMessageId = 2001;
        public byte hitToSomeoneNotifyDataChannel = 0;
        public DeliveryMethod hitToSomeoneNotifyDeliveryMethod = DeliveryMethod.Unreliable;

        [Header("Damage hit from someone notify")]
        public ushort hitFromSomeoneNotifyMessageId = 2002;
        public byte hitFromSomeoneNotifyDataChannel = 0;
        public DeliveryMethod hitFromSomeoneNotifyDeliveryMethod = DeliveryMethod.Unreliable;

        public System.Action onHitToSomeoneNotify;
        /// <summary>
        /// Position, attacker's object ID, is damage over time ?
        /// </summary>
        public System.Action<Vector3, uint, bool> onHitFromSomeoneNotify;

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
                var isDamageOverTime = messageHandler.Reader.GetBool();
                if (onHitFromSomeoneNotify != null)
                    onHitFromSomeoneNotify.Invoke(position, attackerId, isDamageOverTime);
            });
        }

        public void SendHitToSomeoneNotify(long connectionId)
        {
            if (!IsServer)
                return;
            ServerSendPacket(connectionId, hitToSomeoneNotifyDataChannel, hitToSomeoneNotifyDeliveryMethod, hitToSomeoneNotifyMessageId);
        }

        public void SendHitFromSomeoneNotify(long connectionId, Vector3 position, uint attackerId, bool isDamageOverTime)
        {
            if (!IsServer)
                return;
            ServerSendPacket(connectionId, hitFromSomeoneNotifyDataChannel, hitFromSomeoneNotifyDeliveryMethod, hitFromSomeoneNotifyMessageId, (writer) =>
            {
                writer.PutVector3(position);
                writer.PutPackedUInt(attackerId);
                writer.Put(isDamageOverTime);
            });
        }
    }
}
