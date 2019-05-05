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
        [Display(Name ="Başlık")]
        public string Title { get; set; }
        [MaxLength(160)]
        [Display(Name ="Açıklama")]
        public string Description { get; set; }
        [Display(Name ="Oluşturan Kullanıcı")]
        public string CreatorUserId { get; set; }
    }
}
