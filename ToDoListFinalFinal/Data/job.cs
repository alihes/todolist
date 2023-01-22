using Microsoft.AspNetCore.Identity;

namespace ToDoListFinalFinal.Data
{
    public class job
    {
        public int id { get; set; }
        public string? name { get; set; }
        public int priority { get; set; }
        public int state { get; set; }
        public DateTime datecreated { get; set; }
        public string? description { get; set; }
        public IdentityUser owner { get; set; }
    }
}
