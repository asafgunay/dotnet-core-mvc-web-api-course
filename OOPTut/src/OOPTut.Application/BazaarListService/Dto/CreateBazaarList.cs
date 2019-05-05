using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OOPTut.Application
{
    public class CreateBazaarList
    {
        [Required]
        [MaxLength(70)]
        public string Title { get; set; }
        [MaxLength(160)]
        public string Description { get; set; }
        [Required]
        public string CreatorUserId { get; set; }
    }
}
