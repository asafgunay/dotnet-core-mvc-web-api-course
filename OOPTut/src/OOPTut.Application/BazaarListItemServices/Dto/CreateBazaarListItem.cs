using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOPTut.Application.BazaarListItemServices.Dto
{
    public class CreateBazaarListItem
    {
        [Required(ErrorMessage ="Ad alani zorunlu")]
        [Display(Name = "Adı")]
        [MaxLength(70)]
        public string Name { get; set; }
        [Required]
        public int BazaarListId { get; set; }
        public string CreatorUserId { get; set; }
    }
}
