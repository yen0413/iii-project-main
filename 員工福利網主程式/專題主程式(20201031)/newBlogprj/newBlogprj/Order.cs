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
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.Group_Order_Binding = new HashSet<Group_Order_Binding>();
        }
    
        public int Orderid { get; set; }
        public Nullable<int> GroupID { get; set; }
        public Nullable<int> productID { get; set; }
        public Nullable<int> quantity { get; set; }
        public string orderdate { get; set; }
        public Nullable<int> orderstate { get; set; }
        public Nullable<int> followmember_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Group_Order_Binding> Group_Order_Binding { get; set; }
    }
}
