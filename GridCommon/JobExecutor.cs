using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridCommon
{
    public class JobExecutor
    {

        static bool[] visited;
        List<int> currentPath = new List<int>();
        List<List<int>> paths = new List<List<int>>();
        /// <summary>
        /// Выполняет работу
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>

        public List<List<int>> Algorithm(int n, int[,] matrix)
        {
            visited = new bool[n];
            currentPath = new List<int>();
            paths.Clear();

            for (int i = 0; i < n; i++)
            {
                FindAllPaths(i, n, matrix);
            }
            return paths;

        }

        private void FindAllPaths(int node, int n, int[,] matrix)
        {

            visited[node] = true;
            currentPath.Add(node);

            if (currentPath.Count == n)
            {
                paths.Add(new List<int>(currentPath));
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    if (matrix[node, i] > 0 && !visited[i])
                    {
                        FindAllPaths(i, n, matrix);
                    }
                }
            }

            visited[node] = false;
            currentPath.RemoveAt(currentPath.Count - 1);
        }

        public JobResult Execute(Job job)
        {
            Dictionary<string, double> dic = new Dictionary<string, double>();

            string result = "";
            double minput = double.MaxValue;
            int n = job.lenPath;
            List<List<int>> allPaths = Algorithm(n, job.Matrix);

            foreach (var path in allPaths)
            {
                double miput = 0;
                for (int i = 1; i < path.Count; i++)
                {
                    miput += job.Matrix[path[i - 1], path[i]];
                }
                if (minput > miput)
                {
                    minput = miput;
                    result = string.Join("", path);
                }
            }

            dic.Add(result, minput);

            return new JobResult()
            {
                ResultTrack = result,
                Dict = dic,
                MinWay = minput
            };
        }
    }
}
