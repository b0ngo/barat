﻿using BeARAT.Server.Communication;
using BeARAT.Common;
using System.Collections.Generic;

namespace BeARAT.Server
{
    class PeerManager
    {
        private int SessionIndex { get; set; } = -1;
        internal List<Peer> Peers { get; set; }

        public PeerManager()
        {
            Peers = new List<Peer>();
        }

        public void AddPeer(Peer peer)
        {
            Peers.Add(peer);
            PeerListener listener = new PeerListener(peer);
            listener.Start();
        }

        public Peer GetPeer()
        {
            return GetPeer(SessionIndex);
        }

        public Peer GetPeer(int i)
        {
            return Peers[i];
        }
    }
}