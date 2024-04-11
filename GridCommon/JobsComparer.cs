using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridCommon
{
    public class JobsComparer
    {
        static Action<string> callback;

        public static void AddJobResult(JobResult result)
        {

            if (result != null)
            {
                double minWay = result.MinWay;
                List<string> tracksWithMinWay = new List<string>();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Клиент посчитал: Пути с минимальной стоимостью {minWay}:");
                foreach (var track in result.AllTracks)
                {
                    if (result.MinWay == minWay)
                    {
                        tracksWithMinWay.Add(track);
                    }
                }

                foreach (var track in tracksWithMinWay)
                {
                    sb.AppendLine($"Путь - {track} Расстояние - {minWay}");
                }

                callback(sb.ToString());
            }

        }

        public static void SetJobDoneCallback(Action<string> callback)
        {
            JobsComparer.callback = callback;
        }
    }
}
