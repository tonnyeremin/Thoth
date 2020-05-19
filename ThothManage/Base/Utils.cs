using System;
using ThothBase;
using ThothManage;
using ThothManage.Models;

namespace ThothBase
{
    public static class Utils
    {
        public static QuoteItemExDTO GetDTO(QuoteItem item)
       {
           QuoteItemExDTO dto = new QuoteItemExDTO();
           dto.Id = item.Id.ToString();
           dto.IsApproved = item.IsApproved.ToString();
           dto.IsVisible = item.IsVisible.ToString();
           dto.Author = item.Author;
           dto.PrimaryText = item.PrimaryText;
           dto.SecondaryText = item.SecondaryText;
           dto.PostTime = item.PostTime.ToShortDateString();

           return dto;
       }

       public static QuoteItem GetItem(QuoteItemExDTO itemExDTO)
       {
           QuoteItem item = new QuoteItem();
           item.Id = string.IsNullOrEmpty(itemExDTO.Id)? 0: long.Parse(itemExDTO.Id);
           item.Author = itemExDTO.Author;
           item.IsApproved = string.IsNullOrEmpty(itemExDTO.IsApproved)? false : bool.Parse(itemExDTO.IsApproved);
           item.IsVisible = string.IsNullOrEmpty(itemExDTO.IsVisible)? false : bool.Parse(itemExDTO.IsVisible);
           item.PrimaryText = itemExDTO.PrimaryText;
           item.SecondaryText = itemExDTO.SecondaryText;
           DateTime dateTime;
           if(DateTime.TryParse(itemExDTO.PostTime, out dateTime))
                item.PostTime = dateTime;

           return item;
       }
    }
}