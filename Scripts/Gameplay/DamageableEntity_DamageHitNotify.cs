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
            IGameEntity attacker,
            CombatAmountType combatAmountType,
            int damage,
            CharacterItem weapon,
            BaseSkill skill,
            int skillLevel,
            CharacterBuff buff,
            bool isDamageOverTime)
        {
            uint attackerId = 0;
            if (attacker != null)
            {
                attackerId = attacker.GetObjectId();
                if (attackerId != ObjectId && (attacker.Entity is BasePlayerCharacterEntity))
                    CurrentGameManager.SendHitToSomeoneNotify(attacker.GetConnectionId());
            }
            if (ConnectionId >= 0)
                CurrentGameManager.SendHitFromSomeoneNotify(ConnectionId, fromPosition, attackerId);
        }
    }
}
