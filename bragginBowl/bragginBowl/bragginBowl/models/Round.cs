using System;
using System.Collections.Generic;
using System.Text;

namespace bragginBowl.models
{
    class Round
    {
        public int RoundNum { get; set; }
        public string IsComplete { get; set; }
        public string GamerTagsTeam1 { get; set; }
        public string GamerTagsTeam2 { get; set; }
        public int ScoreTeam1 { get; set; }
        public int ScoreTeam2 { get; set; }
    }
}
