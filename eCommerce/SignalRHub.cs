using eCommerce.EntityFramework;
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
            var invoice = db.Invoices.FirstOrDefault(x => x.Id == invoiceId);
            var email = invoice.User.Email;
            var listMerchant = db.InvoiceDetails.Where(x => x.Invoice.Id == invoiceId).Select(x => x.Product.Store.User.Id).Distinct().ToList();
            Clients.All.addNewMessageToMerchant(email, invoiceId, listMerchant);
        }
    }
}