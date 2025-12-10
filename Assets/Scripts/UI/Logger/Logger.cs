using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Logger
{
    public class Logger : MonoBehaviour
    {
        [SerializeField] private GameObject _logLinePrefab;

        private Queue<GameObject> _logLines = new Queue<GameObject>(64);
        private Color[] CustomColor = new Color[5]
        {
            new Color(1f, 0f, 0f, 1f),       // LogType.Error
            new Color(0f, 1f, 0f, 1f),       // LogType.Assert
            new Color(1f, 0.9f, 0f, 1f),     // LogType.Warning
            new Color(0.2f, 0.2f, 0.2f, 1f), // LogType.Log
            new Color(1f, 0f, 0.9f, 1f)      // LogType.Exception
        };

        private void Awake()
        {
            Application.logMessageReceived += HandleLog;
        }

        private void HandleLog(string logString, string stackTrace, LogType type)
        {
            while (_logLines.Count >= 64)
            {
                GameObject deleteLine = _logLines.Dequeue();
                GameObject.Destroy(deleteLine);
            }

            GameObject newObject = Instantiate(_logLinePrefab, transform);
            _logLines.Enqueue(newObject);

            newObject.GetComponent<LoggerLine>().TMPText.color
                = CustomColor[(int)type];

            newObject.GetComponent<LoggerLine>().TMPText.text
                = $"<color=black>[{type}]</color> {logString}";

            if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
            {
                string wrappingLog = $"[{DateTime.Now:HH:mm:ss}/{type}] {logString} \n        ";

                string normalizedStackTrace = stackTrace.Replace("\r\n", "\n");
                string indentedStackTrace = normalizedStackTrace.Replace("\n", "\n        ");

                newObject.GetComponent<LoggerLine>().DebugText
                    = wrappingLog + indentedStackTrace.TrimEnd();
            }
            else
            {
                newObject.GetComponent<LoggerLine>().DebugText
                    = $"[{DateTime.Now:HH:mm:ss}/{type}] {logString}";
            }
        }
    }
}
