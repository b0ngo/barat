﻿using System;
using System.IO;
using System.Net.Sockets;

namespace BeARAT.Common
{
    public class Peer
    {
        private const string FORMAT = "Peer {0} ({1}) {2}";
        private const string STATUS_CONNECTED = "connected";
        private const string STATUS_DISCONNECTED = "closed";

        public string Name { get; set; } // by default the last 32 characters of the hash
        public byte[] Hash { get; } // Sha 256 hash based on the current date time
        private int Timeout { get; set; } = 200;

        TcpClient client;
        BinaryReader reader;
        BinaryWriter writer;

        public Peer(TcpClient client) {
            this.client = client;

            Hash = Common.Hash.GetHashSha256(DateTime.Now.ToBinary());
            string hashString = Common.Hash.Hash2String(Hash);
            Name = hashString.Substring(hashString.Length - 16);

            Stream stream = this.client.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
        }

        public void Send(string data)
        {
            writer.Write(data);
            writer.Flush();
        }

        public string Receive()
        {
            string data;
            data = reader.ReadString();
            return data;
        }

        public bool IsAlive()
        {
            return client.Connected;
        }

        public void Disconnect()
        {
            this.client.Close();
        }

        public override string ToString()
        {
            String status = IsAlive() ? STATUS_CONNECTED : STATUS_DISCONNECTED;
            return string.Format(FORMAT, Name, status, this.client.ToString());
        }
    }
}