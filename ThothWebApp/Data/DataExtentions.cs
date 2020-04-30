using Thoth.Data;

namespace ThothWebApp
{
    public static class DataExtentions
    {
        public static QuoteItem ToModel(this QuoteItemDTO dto)
        {
            return new QuoteItem( ){
                PrimaryText = dto.PrimaryText,
                SecondaryText = dto.SecondaryText,
                Author = dto.Author
            };
        }

        public static QuoteItemDTO ToDTO(this QuoteItem dto)
        {
            return new QuoteItemDTO( ){
                PrimaryText = dto.PrimaryText,
                SecondaryText = dto.SecondaryText,
                Author = dto.Author
            };
        }
    }
}