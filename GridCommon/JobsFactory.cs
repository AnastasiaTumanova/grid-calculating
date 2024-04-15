using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridCommon
{


    /// Класс, раздающий задания

    public class JobsFactory
    {
        // при 10 городах и длине пути равной 9 = время подсчета всех переборов значительно увеличилось = 15-20 секунд
        // при 10 городах  и длине пути равной 8 = 09.9669410 секунд
        // при 9 городах и длине пути равной 8 = примерно 1 секунда

        static int[,] jobs;
        static int[] currI;
        static int headSize;
        static int citiesCount;
        static bool isDone;
     
        static int[] Next()
        {
            var ret = (int[])currI.Clone();

            currI[headSize - 1]++;
            for (int i = headSize - 1; i >= 0; i--)
            {
                if (i > 0 && currI[i - 1] == currI[i])
                {
                    currI[i]++;
                    for (int j = i + 1; j < headSize; j++) currI[j] = 0;
                }
                if (currI[i] >= citiesCount)
                {
                    currI[i] = 0;
                    if (i == 0)
                    {
                        isDone = true;
                    }
                    else
                    {
                        currI[i - 1]++;
                    }
                }
            }
            return ret;
        }
        //Расчет размера подзадачи
        static int CompHeadSize(int citiesCount)
        {
            
            int size =(int) Math.Round(citiesCount * 0.25);
            if (size > 9)
            {
                return 9;
            }
            return size;
        }

        public static void SetJobsRaw(int[,] jobs, int PathLength)
        {
            if(jobs == null)
            {
                throw new ArgumentNullException();
            }
            citiesCount = PathLength;
            JobsFactory.jobs = jobs;
            headSize = CompHeadSize(citiesCount);
             
            currI = new int[headSize];
        }

        public static Job GetJob()
        {
            if (!isDone)
            {
                int[] head = Next();
                var j = new Job()
                {
                    Matrix = jobs,
                   Head = head,
                    lenPath = citiesCount
                };       
                return j;
            }
            return null;
        }
    }
}
