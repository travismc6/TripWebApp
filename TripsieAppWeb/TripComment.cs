//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TripsieAppWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class TripComment
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int TripUserId { get; set; }
        public string Comment { get; set; }
        public int DetailId { get; set; }
        public int TripActivityId { get; set; }
    
        public virtual TripUser TripUser { get; set; }
    }
}
