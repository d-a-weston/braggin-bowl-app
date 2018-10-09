using System;
using System.Collections.Generic;
using System.Text;

namespace bragginBowl.models
{
    class Player
    {
        public int playerID { get; set; }
        public string gamertag { get; set; }
        public object photo { get; set; }
        public string name { get; set; }
        public string tagline { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
    }
}
