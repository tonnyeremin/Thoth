using System;

namespace Thoth
{
    public class Quote
    {
        public long Id {get; set;}
        public string Text {get; set;}
        public string AdditionalText{get; set;}
        public string Author{get; set;}
        public DateTime PostDate {get; set;}
        public bool IsVisible {get; set;}
    }
}