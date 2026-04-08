using Insthync.DevExtension;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class BasePlayerCharacterEntity
    {
        [DevExtMethods("Awake")]
        protected void Awake_KillNotify()
        {
            onReceivedDamage += OnReceivedDamage_KillNotify;
        }

        [DevExtMethods("OnDestroy")]
        protected void OnDestroy_KillNotify()
        {
            onReceivedDamage -= OnReceivedDamage_KillNotify;
        }

        private void OnReceivedDamage_KillNotify(
            DamageableEntity target,
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
            if (!this.IsDead())
                return;
            // Will notify only when character killed by player's character
            if (instigator == null || instigator.Type != EntityTypes.Player || !instigator.TryGetEntity(out BasePlayerCharacterEntity playerAttacker))
                return;
            // Notify
            var weaponId = weapon.dataId;
            var skillId = skill != null ? skill.DataId : 0;
            CurrentGameManager.SendKillNotify(playerAttacker.CharacterName, CharacterName, weaponId, skillId, skillLevel);
        }
    }
}
