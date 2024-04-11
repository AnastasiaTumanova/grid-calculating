using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

using GridCommon;

namespace GridClient
{
    class Client
    {
        static void Main(string[] args)
        {
                ChannelServices.RegisterChannel(new TcpChannel(), false);
                GridJobController controller = (GridJobController)Activator.GetObject(typeof(GridJobController), "tcp://localhost:3000/Grid");
                if (controller == null)
                {
                    Console.WriteLine("could not locate server");
                    return;
                }

                JobExecutor exe = new JobExecutor();
                for (int i = 0; i < 500; i++)
                {
                    var job = controller.GetJob();
                    if (job == null) break;
                    Console.WriteLine($"Берем работу: " + i);
                    controller.SetResult(job, exe.Execute(job));
                }

                Console.WriteLine("END");
                Console.ReadKey();
            }
        }
    }
