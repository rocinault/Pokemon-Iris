using System.Collections;

namespace Golem
{
    public static class CoroutineUtility
    {
        public static void StartCoroutine(IEnumerator routine)
        {
            EmptyBehaviour.instance.StartCoroutine(routine);
        }

        public static void StopCoroutine(IEnumerator routine)
        {
            EmptyBehaviour.instance.StopCoroutine(routine);
        }
    }
}
