using System.Collections;
using System.Collections.Generic;
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
            Vector3 fromPosition,
            IGameEntity attacker,
            CombatAmountType combatAmountType,
            int damage,
            CharacterItem weapon,
            BaseSkill skill,
            short skillLevel)
        {
            uint attackerId = 0;
            if (attacker != null)
            {
                attackerId = attacker.GetObjectId();
                if (attacker.Entity is BasePlayerCharacterEntity)
                    CurrentGameManager.SendHitToSomeoneNotify(attacker.GetConnectionId());
            }
            CurrentGameManager.SendHitFromSomeoneNotify(ConnectionId, fromPosition, attackerId);
        }
    }
}
