using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OOPTut.Core.Bazaar
{
    public class BazaarListItem
    {
        public int Id { get; set; }
        [Required]
        [Display(Name="Adı")]
        public string Name { get; set; }
        [Display(Name="Tamamlandı")]
        public bool IsCompleted { get; set; } = false;
        [Display(Name="İptal")]
        public bool IsCanceled { get; set; }
        [Display(Name="Liste Adı")]
        [ForeignKey("BazaarListId")]
        public virtual BazaarList BazaarList { get; set; }
        public virtual int BazaarListId { get; set; }
        [Required]
        public string CreatorUserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public static BazaarListItem Create(string name, int bazaarListId, string creatorUserId)
        {
            return new BazaarListItem
            {
                Name = name,
                BazaarListId = bazaarListId,
                CreatorUserId = creatorUserId,
                CreatedDate = DateTime.Now,
                IsCompleted = false,
                IsCanceled =false
            };
        }

    }
}
