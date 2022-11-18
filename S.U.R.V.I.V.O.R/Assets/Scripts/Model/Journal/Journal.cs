using System.Linq;
using UnityEngine;

namespace Model.Journal
{
    public class Journal : MonoBehaviour
    {
        private IReporting[] reporting;

        private void Awake()
        {
            reporting = FindObjectsOfType<MonoBehaviour>().OfType<IReporting>().ToArray();
            foreach (var rep in reporting)
            {
                rep.Report += DrawReport;
            }
        }

        private void DrawReport(IReporting reporter)
        {
            var report = reporter.CreateReport();
        }
    }
}