using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOPTut.Application.BazaarListItemServices.Dto
{
    public class CreateBazaarListItem
    {
        [Required]
        public string Name { get; set; }
      
        public int BazaarListId { get; set; }
    }
}
