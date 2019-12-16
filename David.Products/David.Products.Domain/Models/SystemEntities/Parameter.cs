using David.Products.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace David.Products.Domain.Models
{
    public class Parameter : IEntity
    {
        /// <summary>
        /// Parameter Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of parameter
        /// </summary>
        [StringLength(500)]
        public string ParameterName { get; set; }

        /// <summary>
        /// Value of Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Value of LastUpdate
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// Value of CreateDate
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Value of Active
        /// </summary>
        public bool Active { get; set; }
    }
}
