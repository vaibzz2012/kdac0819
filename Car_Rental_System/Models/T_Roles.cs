//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Car_Rental_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Roles
    {
        public T_Roles()
        {
            this.T_Users = new HashSet<T_Users>();
        }
    
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    
        public virtual ICollection<T_Users> T_Users { get; set; }
    }
}