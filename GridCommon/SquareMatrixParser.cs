using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridCommon
{
    public class SquareMatrixParser
    {
        public int[,] ParseStream(Stream stream)
        {
            using (StreamReader r = new StreamReader(stream))
            {
                List<int[]> data = new List<int[]>();

                string line = null;
                while ((line = r.ReadLine()) != null)
                {
                    List<int> curr = new List<int>();
                    var values = line.Split(' ');
                    foreach (var val in values)
                    {
                        curr.Add(int.Parse(val.Trim()));
                    }
                    data.Add(curr.ToArray());
                }

                int numRows = data.Count;
                int numCols = data.Max(x => x.Length);
                int[,] result = new int[numRows, numCols];

                for (int i = 0; i < numRows; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        result[i, j] = data[i][j];
                    }
                }

                return result;
            }
        }
    }
}
