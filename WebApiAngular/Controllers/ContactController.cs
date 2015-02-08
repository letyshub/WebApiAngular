using System;
using System.Linq;
using System.Web.Http;
using WebApiAngular.Data;
using WebApiAngular.Domain;

namespace WebApiAngular.Controllers
{
    public class ContactController : ApiController
    {
        private readonly IRepository<Contact> contactRepository;

        public ContactController()
        {
            this.contactRepository = new Repository<Contact>();
        }

        [HttpGet]
        public IQueryable<Contact> List()
        {
            return contactRepository.GetAll();
        }

        [HttpGet]
        public Contact Get(Guid id)
        {
            return contactRepository.Get(x => x.Id == id).FirstOrDefault();
        }

        public void Add(Contact contact)
        {
            this.contactRepository.SaveOrUpdate(contact);
        }

        public void Update(Guid id, Contact contact)
        {
            this.contactRepository.SaveOrUpdate(contact);
        }

        public void Delete(Guid id)
        {
            this.contactRepository.Delete(this.contactRepository.Get(x => x.Id == id).FirstOrDefault());
        }
    }
}
