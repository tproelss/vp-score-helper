using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPScoreHelper
{
    class ScoreEntry
    {
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                SimplifiedName = ConversionHelper.GetSimplifiedString(value);
                ReplacedAndSimplifiedName = ConversionHelper.GetReplacedAndSimplifiedString(value);
            }
        }

        public string SimplifiedName { get; set; }
        public string ReplacedAndSimplifiedName { get; set; }
        public ulong Score { get; set; }
        public string NameCell { get; set; }
        public string ScoreCell { get; set; }

        private string _name;

        public ScoreEntry()
        {
            Score = 0;
        }
    }
}
