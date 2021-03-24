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
            if (!this.IsDead() || !CurrentGameInstance.turnOnKillDrop)
                return;
            // Prepare droping items and clear items from character
            List<CharacterItem> droppingItems = new List<CharacterItem>();
            if (CurrentGameInstance.killDropEquipItems)
            {
                droppingItems.AddRange(EquipItems);
                EquipItems.Clear();
                for (int i = 0; i < SelectableWeaponSets.Count; ++i)
                {
                    droppingItems.Add(SelectableWeaponSets[i].rightHand);
                    droppingItems.Add(SelectableWeaponSets[i].leftHand);
                    SelectableWeaponSets[i] = new EquipWeapons();
                }
            }
            if (CurrentGameInstance.killDropNonEquipItems)
            {
                droppingItems.AddRange(NonEquipItems);
                NonEquipItems.Clear();
            }
            // Instantiates corpse when there is an items only
            int dropCount = 0;
            for (int i = droppingItems.Count - 1; i >= 0; --i)
            {
                if (droppingItems[i].NotEmptySlot())
                    ++dropCount;
                else
                    droppingItems.RemoveAt(i);
            }
            if (dropCount == 0)
                return;
            // Put dropping items to corpse
            ItemsContainerEntity.DropItems(CurrentGameInstance.corpsePrefab, this, droppingItems);
        }
    }
}
