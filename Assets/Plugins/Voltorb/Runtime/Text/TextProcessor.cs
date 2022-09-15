using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace Voltorb
{
    [RequireComponent(typeof(Text))]
    public sealed class TextProcessor : MonoBehaviour
    {
        private Text m_TextComponent;

        internal Text textComponent
        {
            get
            {
                if (m_TextComponent == null)
                {
                    m_TextComponent = GetComponent<Text>();
                }

                return m_TextComponent;
            }
        }

        private WaitForSeconds m_WaitForSeconds = new WaitForSeconds(kDelayBetweenPrintsInSeconds);

        //private readonly static char[] s_PunctutationCharacters = new char[] { '.', ',', '!', '?' };

        private const float kDelayBetweenPrintsInSeconds = 0.02f;

        public IEnumerator TypeTextCharByChar(string text)
        {
            int count = 0;
            int length = text.Length;

            while (count < length) 
            {
                count++;

                textComponent.text = text.Insert(count, "<color=#FFFFFF00>") + "</color>";

                yield return m_WaitForSeconds;
            }
        }

        public void PrintTextCharByChar(string text)
        {
            Cleanup();

            StartCoroutine(TypeTextCharByChar(text));
        }

        public void PrintCompletedText(string text)
        {
            Cleanup();

            textComponent.text = text;
        }

        public void CleanupAndClearAllText()
        {
            StopAllCoroutines();

            textComponent.text = string.Empty;
        }

        private void Cleanup()
        {
            StopCoroutine(TypeTextCharByChar(string.Empty));
        }
    }
}
