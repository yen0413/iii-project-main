//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace forumAPItest
{
    using System;
    using System.Collections.Generic;
    
    public partial class pGroupdb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pGroupdb()
        {
            this.Group_Order_Binding = new HashSet<Group_Order_Binding>();
            this.Group_Product_Binding = new HashSet<Group_Product_Binding>();
        }
    
        public int Group_ID { get; set; }
        public string Group_Title { get; set; }
        public string Group_StartDate { get; set; }
        public string Group_EndDate { get; set; }
        public Nullable<int> Group_TartgetNumber1 { get; set; }
        public Nullable<int> Group_TartgetNumber2 { get; set; }
        public Nullable<int> Group_type { get; set; }
        public string Group_description { get; set; }
        public Nullable<int> OwnerMember_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group_Order_Binding> Group_Order_Binding { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group_Product_Binding> Group_Product_Binding { get; set; }
    }
}
