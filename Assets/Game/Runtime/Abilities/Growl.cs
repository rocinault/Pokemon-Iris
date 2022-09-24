using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

using Slowbro;
using Umbreon;

namespace Iris
{
    [CreateAssetMenu]
    internal sealed class Growl : ScriptableAbility
    {
        [SerializeField]
        private GameObject m_Asset;

        public override AbilitySpec CreateAbilitySpec()
        {
            return new GrowlAbilitySpec(this);
        }

        private sealed class GrowlAbilitySpec : AbilitySpec
        {
            private readonly EffectSpec m_EffectSpec;

            public GrowlAbilitySpec(ScriptableAbility asset) : base(asset)
            {
                m_EffectSpec = asset.effect.CreateEffectSpec(asset);
            }

            public override void PreAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PreAbilityActivate(instigator, target, out result);

                if (result.success)
                {
                    m_EffectSpec.PreApplyEffectSpec(instigator.pokemon, target.pokemon, ref result);
                }
            }

            public override System.Collections.IEnumerator ActivateAbility(Combatant instigator, Combatant target)
            {
                var position = instigator.rectTransform.anchoredPosition;
                var offset = target.rectTransform.anchoredPosition;

                float direction = Mathf.Clamp(Vector2.Dot(Vector2.up, offset - position), -1, 1);

                var instance = Instantiate(((Growl)asset).m_Asset).GetComponent<RectTransform>();

                instance.SetParent(instigator.transform.parent);
                instance.gameObject.hideFlags = HideFlags.HideAndDontSave;

                instance.anchorMin = instigator.rectTransform.anchorMin;
                instance.anchorMax = instigator.rectTransform.anchorMax;

                instance.pivot = instigator.rectTransform.pivot;

                instance.position = instigator.rectTransform.position;
                instance.localScale = instigator.rectTransform.localScale;
                instance.sizeDelta = instigator.rectTransform.sizeDelta;

                instance.anchoredPosition += new Vector2(instance.sizeDelta.x / 4, instance.sizeDelta.y / 16) * direction;

                var subEmitters = instance.GetComponentsInChildren<ParticleSystem>();

                foreach (var ps in subEmitters)
                {
                    var velocityOverLifetime = ps.velocityOverLifetime;
                    velocityOverLifetime.xMultiplier *= direction;

                    var renderer = ps.GetComponent<ParticleSystemRenderer>();

                    if (direction < 0)
                    {
                        renderer.flip = Vector3.up * Mathf.Abs(direction);
                    }
                    else
                    {
                        renderer.flip = Vector3.zero;
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    yield return target.rectTransform.Translate(offset, Vector3.left * 2f, 0.2f, Space.Self, EasingType.PingPong);
                    yield return target.rectTransform.Translate(offset, Vector3.right * 2f, 0.2f, Space.Self, EasingType.PingPong);
                }

                Destroy(instance.gameObject);
            }

            public override void PostAbilityActivate(Combatant instigator, Combatant target, out SpecResult result)
            {
                base.PostAbilityActivate(instigator, target, out result);

                if (result.success)
                {
                    m_EffectSpec.PostApplyEffectSpec(instigator.pokemon, target.pokemon, ref result);
                }
            }

        }

    }
}


/*
 
                //yield return image.Animate(((Growl)asset).m_Sprites, 24f, 4);
                //for (int i = 0; i < 4; i++)
                //{
                //    yield return image.Animate(((Growl)asset).m_Sprites, 12f);
                //}

                //yield return image.Animate(((Growl)asset).m_Sprites, 12f);

                //yield return new Parallel(instigator, image.Animate(((Growl)asset).m_Sprites, 12f, 4),
                //    image.rectTransform.Translate(position + instigator.rectTransform.sizeDelta / 2 * direction, Vector3.one * 32f * direction, 1f, Space.Self, EasingType.EaseOutSine));


                var image = CreateHiddenGameObjectInstanceAndDontSave<Image>();

                image.transform.SetParent(instigator.transform.parent);

                image.rectTransform.anchorMin = instigator.rectTransform.anchorMin;
                image.rectTransform.anchorMax = instigator.rectTransform.anchorMax;

                image.rectTransform.pivot = instigator.rectTransform.pivot;

                image.rectTransform.position = instigator.rectTransform.position;
                image.rectTransform.localScale = instigator.rectTransform.localScale;
                image.rectTransform.sizeDelta = ((Growl)asset).m_Sprites[0].rect.size;

                var start = position + instigator.rectTransform.sizeDelta / 2.5f * direction;

                yield return new Parallel(instigator,
                    AnimateGrowlSpriteOnImage(instigator, image, start, Vector3.up * 24f * direction, 0, 1),
                    AnimateGrowlSpriteOnImage(instigator, image, start, Vector3.right * 24f * direction, 2, 3),
                    AnimateGrowlSpriteOnImage(instigator, image, start, Vector3.down * 24f * direction, 4, 5));



 */ 