using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.DTO
{
    public class BagRule
    {
        public BagRule(string bag)
        {
            Bag = bag;
            IsEmpty = true;
            Contents = new List<BagContents>();
        }

        public string Bag { get; init; }
        public bool IsEmpty { get; private set; }

        public virtual ICollection<BagContents> Contents { get; private set; }

        public void AddContents (ICollection<BagContents> contents)
        {
            if (contents != null && contents.Any())
            {
                IsEmpty = false;
                Contents = contents;
            }
        }
    }
}