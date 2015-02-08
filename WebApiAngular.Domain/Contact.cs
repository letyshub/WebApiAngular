using System;

namespace WebApiAngular.Domain
{
    public class Contact
    {
        public virtual Guid Id { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Secondname { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
    }
}
