namespace AdventOfCode.DTO
{
    public class BagContents
    {
        public BagContents(int count, string bag)
        {
            Count = count;
            Bag = bag;
        }

        public int Count { get; init; }
        public string Bag { get; init; }
    }
}
