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
    
    public partial class forummessage
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public forummessage()
        {
            this.forumMessageBinding = new HashSet<forumMessageBinding>();
        }
    
        public int ForumMessage_ID { get; set; }
        public string ForumMessageContent { get; set; }
        public string ForumMessageTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<forumMessageBinding> forumMessageBinding { get; set; }
    }
}
