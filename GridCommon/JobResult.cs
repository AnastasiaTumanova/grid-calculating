using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridCommon
{
    [System.Serializable]
    /// <summary>
    /// Модель описания результата работы
    /// </summary>
    public class JobResult
    {
        public List<string> AllTracks { get; set; }
        public string ResultTrack { get; set; }
        public double MinWay { get; set; }
        public int PathLength { get; set; }

        public Dictionary<string, double> Dict = new Dictionary<string, double>(); 

    }

}
