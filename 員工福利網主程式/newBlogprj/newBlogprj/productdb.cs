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
    
    public partial class productdb
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public productdb()
        {
            this.groupProductBinding = new HashSet<groupProductBinding>();
        }
    
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public Nullable<int> Product_Price { get; set; }
        public string Product_description { get; set; }
        public Nullable<int> Product_imageID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<groupProductBinding> groupProductBinding { get; set; }
        public virtual productPictureBinding productPictureBinding { get; set; }
    }
}
