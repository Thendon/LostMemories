using System;
using System.Collections.Generic;
using System.Net.Sockets;
using TCPNetPackage;
using LostMemoriesNetMessages;
using UnityEngine;

namespace TCPSocketNetwork
{
    using System;
    using System.IO;
    using System.Text;

    using UnityEngine;

    /// <summary>
    /// Redirects writes to System.Console to Unity3D's Debug.Log.
    /// </summary>
    /// <author>
    /// Jackson Dunstan, http://jacksondunstan.com/articles/2986
    /// </author>
    public static class UnitySystemConsoleRedirector
    {
        private class UnityTextWriter : TextWriter
        {
            private StringBuilder buffer = new StringBuilder();

            public override void Flush()
            {
                Debug.Log(buffer.ToString());
                buffer.Length = 0;
            }

            public override void Write(string value)
            {
                buffer.Append(value);
                if (value != null)
                {
                    var len = value.Length;
                    if (len > 0)
                    {
                        var lastChar = value[len - 1];
                        if (lastChar == '\n')
                        {
                            Flush();
                        }
                    }
                }
            }

            public override void Write(char value)
            {
                buffer.Append(value);
                if (value == '\n')
                {
                    Flush();
                }
            }

            public override void Write(char[] value, int index, int count)
            {
                Write(new string(value, index, count));
            }

            public override Encoding Encoding
            {
                get { return Encoding.Default; }
            }
        }

        public static void Redirect()
        {
            Console.SetOut(new UnityTextWriter());
        }
    }

    public delegate void Task();

    public class TCPSocketConnection : MonoBehaviour
    {
        public enum ConnectionStatus
        {
            None,
            Connecting,
            Connected,
            Initializing,
            Initialized
        }

        [Header("Network Settings")]
        public string serverIP = "127.0.0.1";
        public int serverPort = 5505;

        [Header("Debug")]
        public ConnectionStatus connectionStatus;
        public ulong localPlayerID = 0;
        public List<ulong> connectedPlayers = new List<ulong>();

        private Queue<Task> TaskQueue = new Queue<Task>();
        private object _queueLock = new object();

        public static Action OnConnected;
        public static Action OnInitialized;
        public static Action OnDisconnected;
        public static Action<ulong> OnPlayerConnected;
        public static Action<ulong> OnPlayerDisconnected;

        void Awake()
        {
            //UnitySystemConsoleRedirector.Redirect();
            PackageManager.RegisterNetPackage((uint)NetBase.Package.Initialized, NetBase.channel, OnClientInitialized);
            PackageManager.RegisterNetPackage((uint)NetBase.Package.PlayerInitialized, NetBase.channel, OnOtherClientInitialized);
            PackageManager.RegisterNetPackage((uint)NetBase.Package.PlayerConnected, NetBase.channel, OnOtherClientConnected);
            PackageManager.RegisterNetPackage((uint)NetBase.Package.PlayerDisconnected, NetBase.channel, OnOtherClientDisconnected); 
            
            string[] arguments = Environment.GetCommandLineArgs();
            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] == "-ip" && arguments.Length > i + 1)
                    serverIP = arguments[i + 1];
            }
        }

        void OnEnable()
        {
            ConnectWorldServer();
        }

        void OnDisable()
        {
            if (connectionStatus != ConnectionStatus.None)
                DisconnectServer();
        }

        public void ScheduleTask(Task newTask)
        {
            lock(_queueLock)
            {
                TaskQueue.Enqueue(newTask);
            }
        }

        public void RunQueuedTasks()
        {
            lock (_queueLock)
            {
                while (TaskQueue.Count > 0)
                    TaskQueue.Dequeue()();
            }
        }

        void Update()
        {
            if (!Ready())
                return;

            RunQueuedTasks();
        }

        Server server = null;

        void ConnectWorldServer()
        {
            if (server != null)
            {
                if (connectionStatus == ConnectionStatus.Connecting || connectionStatus == ConnectionStatus.Connected)
                    return;

                server.CloseConnection();
            }

            server = new Server(serverIP, serverPort);
            server.OnConnectionResponse += ConnectionResponse;
            server.OnDisconnected += ServerDisconnected;
            server.Connect(new TcpClient());
            connectionStatus = ConnectionStatus.Connecting;

            return;
        }

        public bool Ready()
        {
            return connectionStatus == ConnectionStatus.Initialized;
        }

        public void ConnectionResponse(bool success)
        {
            if (!success)
            {
                connectionStatus = ConnectionStatus.None;
                return;
            }
            connectionStatus = ConnectionStatus.Connected;
            print("Connected to world server!");
            OnConnected?.Invoke();
            print("Send init package!");
            SendInitializationRequest();
        }

        public void SendInitializationRequest()
        {
            using (PackageOut initPackage = new PackageOut())
            {
                print("send init package");
                connectionStatus = ConnectionStatus.Initializing;
                PackageManager.SendData(initPackage, (uint)NetBase.Package.Initialize, NetBase.channel);
            }
        }

        void DisconnectServer()
        {
            if (server == null)
                return;

            server.CloseConnection();
        }

        public void ServerDisconnected()
        {
            print("Disconnected from world server");
            server = null;
            localPlayerID = 0;
            connectionStatus = ConnectionStatus.None;
            OnDisconnected?.Invoke();
        }

        private void OnOtherClientDisconnected(PackageIn data)
        {
            ulong playerID = data.ReadUnsignedLong();

            if (!connectedPlayers.Contains(playerID))
                return;

            connectedPlayers.Remove(playerID);
            OnPlayerDisconnected?.Invoke(playerID);
        }

        private void OnOtherClientInitialized(PackageIn data)
        {
            print("OnOtherCLInited " + data.ReadUnsignedLong());
        }

        private void OnOtherClientConnected(PackageIn data)
        {
            ulong playerID = data.ReadUnsignedLong();
            print("other cl connected" + playerID);

            if (connectedPlayers.Contains(playerID))
                return;

            connectedPlayers.Add(playerID);

            print("invoke onplayerconnected");

            OnPlayerConnected?.Invoke(playerID);
        }

        void OnClientInitialized(PackageIn package)
        {
            bool success = package.ReadBool();
            print("initialized success: " + success);

            if (!success)
            {
                if (connectionStatus == ConnectionStatus.Initializing)
                {
                    connectionStatus = ConnectionStatus.Connected;
                    SendInitializationRequest();
                }
                return;
            }

            localPlayerID = package.ReadUnsignedLong();
            connectionStatus = ConnectionStatus.Initialized;

            int otherPlayerCount = package.ReadInt();
            for (int i = 0; i < otherPlayerCount; i++)
                connectedPlayers.Add(package.ReadUnsignedLong());

            print("invoke onInitialized");
            print(OnInitialized);
            OnInitialized?.Invoke();
            print("done");
        }
    }
}