using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public partial class BasePlayerCharacterEntity
    {
        [DevExtMethods("Awake")]
        protected void Awake_KillDrop()
        {
            onReceivedDamage += OnReceivedDamage_KillDrop;
        }

        [DevExtMethods("OnDestroy")]
        protected void OnDestroy_KillDrop()
        {
            onReceivedDamage -= OnReceivedDamage_KillDrop;
        }

        private void OnReceivedDamage_KillDrop(
            Vector3 fromPosition,
            IGameEntity attacker,
            CombatAmountType combatAmountType,
            int damage,
            CharacterItem weapon,
            BaseSkill skill,
            short skillLevel)
        {
            if (!this.IsDead())
                return;
            // Prepare droping items and clear items from character
            List<CharacterItem> droppingItems = new List<CharacterItem>();
            droppingItems.AddRange(EquipItems);
            EquipItems.Clear();
            droppingItems.AddRange(NonEquipItems);
            NonEquipItems.Clear();
            for (int i = 0; i < SelectableWeaponSets.Count; ++i)
            {
                droppingItems.Add(SelectableWeaponSets[i].rightHand);
                droppingItems.Add(SelectableWeaponSets[i].leftHand);
                SelectableWeaponSets[i] = new EquipWeapons();
            }
            // Put dropping items to corpse
        }
    }
}
