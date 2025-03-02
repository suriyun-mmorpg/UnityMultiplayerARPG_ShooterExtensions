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
            if (!this.IsDead())
                return;
            // Will notify only when character killed by player's character
            if (attacker != null && attacker.Entity is BasePlayerCharacterEntity)
            {
                // Notify
                BasePlayerCharacterEntity playerAttacker = attacker.Entity as BasePlayerCharacterEntity;
                var weaponId = weapon.dataId;
                var skillId = skill != null ? skill.DataId : 0;
                CurrentGameManager.SendKillNotify(playerAttacker.CharacterName, CharacterName, weaponId, skillId, skillLevel);
            }
        }
    }
}
