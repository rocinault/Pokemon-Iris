using System.Collections;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

using Slowbro;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu]
    internal sealed class Tackle : ScriptableAbility
    {
        [SerializeField]
        private Sprite m_Sprite;

        public override AbilitySpec CreateAbilitySpec()
        {
            return new TackleAbilitySpec(this, m_Sprite);
        }

        private sealed class TackleAbilitySpec : AbilitySpec
        {
            private readonly EffectSpec m_EffectSpec;
            private readonly Sprite m_Sprite;

            public TackleAbilitySpec(ScriptableAbility asset, Sprite sprite) : base(asset)
            {
                m_EffectSpec = asset.effect.CreateEffectSpec(asset);
                m_Sprite = sprite;
            }

            public override void PreAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PreAbilityActivate(instigator, target, out result);

                if (result.success)
                {
                    m_EffectSpec.PreApplyEffectSpec(instigator, target, ref result);
                }
            }

            public override IEnumerator ActivateAbility(Combatant instigator, Combatant target)
            {
                var position = instigator.rectTransform.anchoredPosition;
                var offset = target.rectTransform.anchoredPosition;

                float direction = Mathf.Clamp(Vector2.Dot(Vector2.up, offset - position), -1, 1);

                yield return instigator.rectTransform.Translate(position, Vector3.right * 15f * direction, 0.175f, Space.Self, EasingType.PingPong);

                var image = CreateHiddenGameObjectInstanceAndDontSave<Image>();

                image.transform.SetParent(target.transform.parent);

                image.rectTransform.anchorMin = target.rectTransform.anchorMin;
                image.rectTransform.anchorMax = target.rectTransform.anchorMax;

                image.rectTransform.pivot = target.rectTransform.pivot;

                image.rectTransform.position = target.rectTransform.position;
                image.rectTransform.localScale = target.rectTransform.localScale;
                image.rectTransform.sizeDelta = m_Sprite.rect.size;

                image.sprite = m_Sprite;
                image.color = new Color(1f, 1f, 1f, 0.75f);

                for (int i = 0; i < 2; i++)
                {
                    yield return target.rectTransform.Translate(offset, Vector3.left * 2f, 0.05f, Space.Self, EasingType.PingPong);
                    yield return target.rectTransform.Translate(offset, Vector3.right * 2f, 0.05f, Space.Self, EasingType.PingPong);
                }

                Destroy(image.gameObject);
            }

            public override void PostAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PostAbilityActivate(instigator, target, out result);

                m_EffectSpec.ApplyEffectSpec(instigator, target, ref result);
            }

            private static T CreateHiddenGameObjectInstanceAndDontSave<T>()
            {
                return EditorUtility.CreateGameObjectWithHideFlags(typeof(T).Name, HideFlags.HideAndDontSave, typeof(T)).GetComponent<T>();
            }
        }
    }
}
