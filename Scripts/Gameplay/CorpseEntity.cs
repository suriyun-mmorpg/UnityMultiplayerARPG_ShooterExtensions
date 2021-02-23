using LiteNetLibManager;
using System.Collections;
using System.Collections.Generic;

namespace MultiplayerARPG
{
    public class CorpseEntity : LiteNetLibBehaviour
    {
        private SyncListCharacterItem items = new SyncListCharacterItem();
        public SyncListCharacterItem Items
        {
            get { return items; }
        }

        public void Setup(BaseCharacterEntity character, List<CharacterItem> items)
        {
            items.Clear();
            items.AddRange(items);
        }
    }
}
