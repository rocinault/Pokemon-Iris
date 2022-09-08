using System;

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Golem
{
    public sealed class Animation : TweenCoroutine
    {
        private sealed class AnimationFrame
        {
            internal Sprite sprite;

            internal float time;
            internal float length;
        }

        private readonly Image m_Image;

        private AnimationFrame[] m_Frames;

        private static string s_PropertyName = string.Concat($"m_Sprite");

        private int m_CurrentFrame;
        private int m_FrameDurationCounter;

        public override bool keepWaiting => Update();

        public Animation(Image image, AnimationClip clip)
        {
            m_Image = image;

            CreateFramesFromClip(clip, out m_Frames);
            CalculateAnimationDuration();
        }


        protected override bool Update()
        {
            m_TimeElapsed += Time.deltaTime;

            float timePerFrame = m_Frames[m_CurrentFrame].length;

            if (timePerFrame < m_TimeElapsed)
            {
                m_FrameDurationCounter++;
                m_TimeElapsed -= timePerFrame;

                m_Image.sprite = m_Frames[m_CurrentFrame].sprite;

                if (m_FrameDurationCounter >= m_Frames[m_CurrentFrame].length)
                {
                    m_CurrentFrame++;

                    if (m_CurrentFrame >= m_Frames.Length)
                    {
                        return false;
                    }

                    m_FrameDurationCounter = 0;
                }
            }

            return true;
        }

        private void CreateFramesFromClip(AnimationClip clip, out AnimationFrame[] frames)
        {
            EditorCurveBinding binding = Array.Find(AnimationUtility.GetObjectReferenceCurveBindings(clip), i => BindingMatchesSpritePropertyName(i));
            ObjectReferenceKeyframe[] keyframes = AnimationUtility.GetObjectReferenceCurve(clip, binding);

            frames = Array.ConvertAll(keyframes, i => ConvertKeyFrameToAnimationFrame(i));

            if (frames != null && frames.Length > 0)
            {
                int length = frames.Length;

                for (int i = 0; i < length - 1; ++i)
                {
                    frames[i].length = frames[i + 1].time - frames[i].time;
                }

                var lastFrame = frames[frames.Length - 1];

                if (lastFrame.length <= 0)
                {
                    lastFrame.length = 1f / clip.frameRate;
                }
            }
        }

        private void CalculateAnimationDuration()
        {
            float duration = 0;
            int length = m_Frames.Length;

            for (int i = 0; i < length; i++)
            {
                duration += m_Frames[i].time;
            }

            m_Duration = duration;
        }

        private static bool BindingMatchesSpritePropertyName(EditorCurveBinding binding)
        {
            return binding.propertyName == s_PropertyName;
        }

        private static AnimationFrame ConvertKeyFrameToAnimationFrame(ObjectReferenceKeyframe keyframe)
        {
            return new AnimationFrame()
            {
                sprite = keyframe.value as Sprite,
                time = keyframe.time
            };
        }
    }
}
