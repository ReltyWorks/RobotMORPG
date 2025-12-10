using System.Collections;
using UnityEngine;

public class LoggerTester : MonoBehaviour
{
    private void Start()
    {
        // 게임 시작하자마자 테스트 코루틴 실행!
        StartCoroutine(TestLogsRoutine());
    }

    private IEnumerator TestLogsRoutine()
    {
        // 'a' 부터 'z' 까지 반복 (아스키 코드 이용)
        for (char c = 'a'; c <= 'z'; c++)
        {
            // 5가지 타입을 순서대로 돌리기 위해 나머지 연산(%) 사용
            // 0: Log, 1: Warning, 2: Error, 3: Assert, 4: Exception
            int logTypeIndex = (c - 'a') % 5;

            string msg = $"테스트 메시지 : {c}";

            switch (logTypeIndex)
            {
                case 0:
                    Debug.Log(msg); // 일반 로그
                    break;
                case 1:
                    Debug.LogWarning(msg); // 워닝
                    break;
                case 2:
                    Debug.LogError(msg); // 에러
                    break;
                case 3:
                    // 어설션 (Assert) - 논리적으로 절대 일어나면 안 되는 상황
                    Debug.LogAssertion(msg);
                    break;
                case 4:
                    // 예외 (Exception) - try-catch 등에서 발생하는 시스템 예외
                    Debug.LogException(new System.Exception(msg));
                    break;
            }

            // 1초 대기
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("테스트 끝! 수고했어~");
    }
}