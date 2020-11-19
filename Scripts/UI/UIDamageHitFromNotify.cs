using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiplayerARPG
{
    public class UIDamageHitFromNotify : MonoBehaviour
    {
        private class IndicatorUpdate
        {
            private RectTransform indicator;
            private CanvasGroup canvasGroup;
            private float duration = 0f;
            private float countDown = 0f;

            public IndicatorUpdate(RectTransform indicator)
            {
                this.indicator = indicator;
                canvasGroup = indicator.gameObject.GetOrAddComponent<CanvasGroup>();
            }

            public void Update(float deltaTime)
            {
                if (countDown <= 0f)
                    return;
                if (countDown > 0f)
                {
                    countDown -= deltaTime;
                    if (BasePlayerCharacterController.OwningCharacter != null)
                    {
                        canvasGroup.alpha = countDown / duration;
                        indicator.gameObject.SetActive(true);
                    }
                }
                if (countDown <= 0f)
                {
                    countDown = 0f;
                    Hide();
                }
            }

            public void Show(float duration)
            {
                this.duration = countDown = duration;
            }

            public void Hide()
            {
                indicator.gameObject.SetActive(false);
            }
        }

        public RectTransform[] indicators;
        public float showDuration = 2f;

        private readonly List<IndicatorUpdate> indicatorUpdates = new List<IndicatorUpdate>();

        private void Start()
        {
            BaseGameNetworkManager.Singleton.onHitFromSomeoneNotify += OnHitFromSomeoneNotify;
            if (indicators == null)
                return;
            foreach (RectTransform indicator in indicators)
            {
                indicator.gameObject.SetActive(false);
                indicatorUpdates.Add(new IndicatorUpdate(indicator));
            }
        }

        private void OnDestroy()
        {
            BaseGameNetworkManager.Singleton.onHitFromSomeoneNotify -= OnHitFromSomeoneNotify;
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach (IndicatorUpdate indicatorUpdate in indicatorUpdates)
            {
                indicatorUpdate.Update(deltaTime);
            }
        }

        public void OnHitFromSomeoneNotify(Vector3 position, uint attackerId)
        {
            if (indicatorUpdates.Count == 0)
                return;
            foreach (IndicatorUpdate indicatorUpdate in indicatorUpdates)
            {
                indicatorUpdate.Show(showDuration);
            }
        }
    }
}
