using System;
using Thoth.Data;

namespace Thoth
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

        public static QuoteItem ToModel(this NewQuotaItemDto dto)
        {
            return new QuoteItem( ){
                PrimaryText = dto.PrimaryText,
                SecondaryText = dto.SecondaryText,
                Author = dto.Author,
                IsVisible = Boolean.Parse(dto.IsVisible)
            };
        }

         public static QuoteItem ToModel(this EditQuotaItemDto dto)
        {
            return new QuoteItem( ){
                PrimaryText = dto.PrimaryText,
                SecondaryText = dto.SecondaryText,
                Author = dto.Author,
                IsVisible = Boolean.Parse(dto.IsVisible),
                Id =Int64.Parse(dto.Id)
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