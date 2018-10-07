using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

using WL_OA.NET;

namespace WpfApp1.Data.NDAL
{
    public class SimpleUdpClientC
    {
        public Socket Client
        {
            get
            {
                return m_socketClient;
            }
        }

        public ICipher Crypter { get; set; }

        public int UHash { get; set; }

        byte[] m_buffer;

        Socket m_socketClient;

        EndPoint m_remoteEP = new IPEndPoint(IPAddress.Any, 0);

        string m_ip;

        int m_port;

        uint in_sequence = 0;

        uint out_sequence = 0;

        private NetHandler m_handler = null;

        public void Connect()
        {
            in_sequence = 0;
            out_sequence = 1;

            if (m_socketClient != null)
                m_socketClient.Close();

            m_socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //m_socketClient.NoDelay = true;
            m_socketClient.Blocking = false;
            m_socketClient.ReceiveBufferSize = NetBase.MAX_UDP_BUF_SIZE;
            m_socketClient.SendBufferSize = NetBase.MAX_UDP_BUF_SIZE;
            m_socketClient.Connect(m_ip, m_port);

            StartReceive();
        }

        void StartReceive()
        {
            m_socketClient.BeginReceiveFrom(m_buffer, 0, NetBase.MAX_UDP_BUF_SIZE,
                SocketFlags.None, ref m_remoteEP, new AsyncCallback(ReceiveCallback), null);
        }


        void ReceiveCallback(IAsyncResult ar)
        {
            int bytesRead = 0;
            try
            {
                bytesRead = m_socketClient.EndReceiveFrom(ar, ref m_remoteEP);
            }
            catch (ObjectDisposedException ex)
            {
                return;
            }
            catch (SocketException ex)
            {
                StartReceive();
                return;
            }

            var packet = SimpleProtocol.Decode(m_buffer, bytesRead);

            m_handler?.Invoke(packet, m_remoteEP as IPEndPoint);

            /*
            if (p.Sequence <= in_sequence)
            {
                in_sequence = p.Sequence;
                if (OnIncomingPacket != null)
                    OnIncomingPacket(p);
            }
            */

            StartReceive();
        }

        public virtual void SendPacket(SimpleProtocolStruct packet)
        {
            byte[] data = packet.ToBytes();
            m_socketClient.Send(data, 0, data.Length, SocketFlags.None);
        }


        public void Close()
        {
            if (m_socketClient != null)
                m_socketClient.Close();
        }

        public void Dispose()
        {
            Close();
        }

        public SimpleUdpClientC(int port, NetHandler handler, string host = "")
        {
            m_ip = host;
            m_port = port;
            m_buffer = new byte[NetBase.MAX_UDP_BUF_SIZE];

            m_handler = handler;

            if (string.IsNullOrWhiteSpace(m_ip)) m_ip = NetBase.DEFAULT_IP;

            Connect();
        }
    }
}
