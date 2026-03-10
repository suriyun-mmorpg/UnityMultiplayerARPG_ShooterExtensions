using Insthync.DevExtension;
using LiteNetLibManager;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class DamageableEntity
    {
        [DevExtMethods("Awake")]
        protected void Awake_DamageHitNotify()
        {
            onReceivedDamage += OnReceivedDamage_DamageHitNotify;
        }

        [DevExtMethods("OnDestroy")]
        protected void OnDestroy_DamageHitNotify()
        {
            onReceivedDamage -= OnReceivedDamage_DamageHitNotify;
        }

        private void OnReceivedDamage_DamageHitNotify(
            HitBoxPosition position,
            Vector3 fromPosition,
            EntityInfo instigator,
            CombatAmountType combatAmountType,
            int damage,
            CharacterItem weapon,
            BaseSkill skill,
            int skillLevel,
            CharacterBuff buff,
            bool isDamageOverTime)
        {
            uint attackerId = 0;
            if (instigator != null)
            {
                attackerId = instigator.ObjectId;
                if (attackerId != ObjectId && instigator.Type == EntityTypes.Player && Manager.Assets.TryGetSpawnedObject(attackerId, out LiteNetLibIdentity attackerIdentity))
                    CurrentGameManager.SendHitToSomeoneNotify(attackerIdentity.ConnectionId);
            }
            if (ConnectionId >= 0)
            {
                CurrentGameManager.SendHitFromSomeoneNotify(ConnectionId, fromPosition, attackerId, isDamageOverTime);
            }
        }
    }
}
