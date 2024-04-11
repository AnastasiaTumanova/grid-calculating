using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

using GridCommon;

namespace GridServer
{
    public class Server
    {
        public void Start(int[,] matrix, int PathLength, Action<string> resultCallback)
        {
            JobsComparer.SetJobDoneCallback(resultCallback);
            JobsFactory.SetJobsRaw(matrix, PathLength);

            TcpServerChannel channel = new TcpServerChannel(3000);
            ChannelServices.RegisterChannel(channel, false);

            RemotingConfiguration.RegisterWellKnownServiceType(
             typeof(GridJobController), "Grid", WellKnownObjectMode.Singleton);
        }
    }
}
