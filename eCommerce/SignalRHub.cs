using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Linq;

namespace eCommerce.Controllers
{
    [HubName("SignalRHub")]
    public class SignalRHub : Hub
    {
        MainDbContext db = new MainDbContext();
        public void Alert(long invoiceId)
        {
            var quantity = db.Invoices.Where(x => x.Status == EntityFramework.ProductStatus.NotValidated).Count();
            var invoice = db.Invoices.FirstOrDefault(x => x.Id == invoiceId);
            var email = invoice.User.Email;
            Clients.All.addNewMessageToPage(quantity, email, invoiceId);
        }
    }
}