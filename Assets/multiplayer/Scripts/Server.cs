using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TCPNetPackage;
using System.Net.Sockets;
using System.Net;
using TCPSocketNetwork;
using System;

public class Server : IServer
{
    public NetworkStream stream { get; set; }
    public string IP { get; set; }
    public int port { get; set; }
    public TcpClient socket { get; set; }
    public byte[] readBuffer { get; set; }
    public byte[] dataBuffer { get; set; }
    public int dataOffset { get; set; }

    public delegate void DisconnectAction();
    public event DisconnectAction OnDisconnected;
    public delegate void ConnectionAction(bool result);
    public event ConnectionAction OnConnectionResponse;

    public Server(string ip, int p)
    {
        IP = ip;
        port = p;
    }

    public void Connect(TcpClient server)
    {
        server.BeginConnect(IP, port, new AsyncCallback(ConnectionResult), server);
    }

    public void CloseConnection()
    {
        //Connection already closed
        if (socket == null)
            return;

        socket.Close();
        socket = null;
        PackageManager.UnregisterServer();
        OnDisconnected();
    }

    void ConnectionResult(IAsyncResult result)
    {
        TcpClient server = (TcpClient)result.AsyncState;
        if (server == null)
            return;

        try
        {
            server.EndConnect(result);
        }
        catch (Exception e)
        {
            Debug.Log("connection result: " + e.Message);
        }

        if (server.Connected)
        {
            server.NoDelay = true;
            socket = server;
            PackageManager.RegisterServer(this);
        }

        OnConnectionResponse(server.Connected);
    }
}
