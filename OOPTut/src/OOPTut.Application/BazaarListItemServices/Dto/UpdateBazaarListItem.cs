using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOPTut.Application.BazaarListItemServices.Dto
{
    public class UpdateBazaarListItem : CreateBazaarListItem
    {
        public int Id { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
        [Required]
        public bool IsCanceled { get; set; }
    }
}
