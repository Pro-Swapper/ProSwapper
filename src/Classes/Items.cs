using System.Collections.Generic;
namespace Pro_Swapper
{
    public class Items
    {
        public class Swap
        {
            public string Search { get; set; }
            public string Replace { get; set; }
            public long Offset { get; set; }
            public string File { get; set; }
        }

        public class Item
        {
            public string Type { get; set; }
            public string SwapsFrom { get; set; }
            public string SwapsTo { get; set; }
            public string FromImage { get; set; }
            public string ToImage { get; set; }
            public string Note { get; set; }
            public int New { get; set; }
            public List<Swap> Swaps { get; set; }
        }

        public class Root
        {
            public List<Item> Items { get; set; }
        }
    }
}
