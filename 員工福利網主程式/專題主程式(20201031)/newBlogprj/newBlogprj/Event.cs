//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace newBlogprj
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.EventBooking = new HashSet<EventBooking>();
            this.EventComment = new HashSet<EventComment>();
        }
    
        public int Event_ID { get; set; }
        public Nullable<System.DateTime> EventStartDate { get; set; }
        public Nullable<System.DateTime> EventEndDate { get; set; }
        public string EventLocation { get; set; }
        public string EventName { get; set; }
        public string EventContent { get; set; }
        public string EventRemark { get; set; }
        public Nullable<int> EventMaxPeople { get; set; }
        public Nullable<int> EventMinPeople { get; set; }
        public string EventLocationX { get; set; }
        public string EventLocationY { get; set; }
        public Nullable<int> EventNowJoin { get; set; }
        public Nullable<int> EventCreateEmployeeID { get; set; }
    
        public virtual memberdb memberdb { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventBooking> EventBooking { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventComment> EventComment { get; set; }
    }
}
