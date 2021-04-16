using System.Collections.Generic;
namespace Pro_Swapper
{
    public class Items
    {


        public class Swap
        {
            public string Search { get; set; }
            public string Replace { get; set; }
            public int Offset { get; set; }
            public string File { get; set; }
        }

        public class Item
        {
            public string Type { get; set; }
            public string SwapsFrom { get; set; }
            public string SwapsTo { get; set; }
            public string FromImage { get; set; }
            public string ToImage { get; set; }
            public List<Swap> Swaps { get; set; }
            public string Note { get; set; }
        }

        public class CPSwap
        {
            public string SwapsTo { get; set; }
            public string ToImage { get; set; }
            public string Replace { get; set; }
        }

        public class InvalidSwap
        {
            public string Search { get; set; }
            public string Replace { get; set; }
            public int Offset { get; set; }
        }

        public class Root
        {
            public List<Item> Items { get; set; }
            public string CPPak { get; set; }
            public int CPOffset { get; set; }
            public string CPSearch { get; set; }
            public List<CPSwap> CPSwaps { get; set; }
            public List<InvalidSwap> InvalidSwaps { get; set; }
        }


    }
}