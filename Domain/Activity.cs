using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string lon1 { get; set; }
        public string lat1 { get; set; }
        public string lon2 { get; set; }
        public string lat2 { get; set; }

        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
