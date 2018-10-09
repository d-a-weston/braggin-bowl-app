using System;
using System.Collections.Generic;
using System.Text;

namespace bragginBowl.models
{
    class Tournament
    {
        public int tournamentID { get; set; }
        public string tournament_type { get; set; }
        public string legal_tournament { get; set; }
        public string game { get; set; }
        public string modifiers { get; set; }
        public DateTime tournament_date { get; set; }
        public int current_round { get; set; }
        public int team_size { get; set; }
        public int team_num { get; set; }
    }
}
