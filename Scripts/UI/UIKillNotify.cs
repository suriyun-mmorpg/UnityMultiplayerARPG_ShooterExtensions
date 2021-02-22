using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public class UIKillNotify : MonoBehaviour
    {
        public TextWrapper textKillNotify;
        public string formatKillNotify = "{0} kill {1} ({2})";
        public float showDuration = 3f;
        private float timeCount;

        private void Awake()
        {
            textKillNotify.gameObject.SetActive(false);
        }

        private void Start()
        {
            BaseGameNetworkManager.Singleton.onKillNotify += KillNotify;
        }

        private void OnDestroy()
        {
            BaseGameNetworkManager.Singleton.onKillNotify -= KillNotify;
        }

        private void Update()
        {
            timeCount += Time.deltaTime;
            if (timeCount >= showDuration)
                textKillNotify.gameObject.SetActive(false);
        }

        public void KillNotify(string killerName, string victimName, int weaponId, int skillId, short skillLevel)
        {
            if (!GameInstance.Items.ContainsKey(weaponId))
                return;
            timeCount = 0;
            textKillNotify.text = string.Format(formatKillNotify, killerName, victimName, GameInstance.Items[weaponId].Title);
            textKillNotify.gameObject.SetActive(true);
        }
    }
}
