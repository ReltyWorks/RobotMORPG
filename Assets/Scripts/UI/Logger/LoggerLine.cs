using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UI.Logger
{
    /// <summary> LoggerLinePrepab에 부착해서 복사기능을 부여합니다. </summary>
    public class LoggerLine : MonoBehaviour, IPointerClickHandler
    {
        public TMP_Text TMPText;
        [HideInInspector] public string DebugText;

        private Coroutine _showToastMessage;
        private string _textBuffer;

        private void Awake()
        {
            TMPText.text = "";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_showToastMessage != null ||
                TMPText.text == "")
                return;

            CopyText(DebugText);

            _showToastMessage = StartCoroutine(ShowToastMessage());
        }

        private void CopyText(string log)
        {
            GUIUtility.systemCopyBuffer = log;
        }

        private IEnumerator ShowToastMessage()
        {
            _textBuffer = TMPText.text;
            TMPText.text = "Copy to clipboard!!";

            yield return new WaitForSeconds(0.5f);

            TMPText.text = _textBuffer;
            _textBuffer = "";
            _showToastMessage = null;
        }
    }
}
