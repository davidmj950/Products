using David.Products.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Domain.Models
{
    public class Audience : IEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// ClientId (Recourse server)
        /// </summary>
        [Index("Audience_ClientId", IsUnique = true)]
        [MaxLength(32)]
        public string ClientId { get; set; }

        /// <summary>
        /// symmetric key
        /// </summary>
        [MaxLength(80)]
        [Required]
        public string Secret { get; set; }

        /// <summary>
        /// Name Recourse server
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Last Update of record
        /// </summary>
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Create Date of record
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// is active bool
        /// </summary>
        public bool Active { get; set; }
    }
}
