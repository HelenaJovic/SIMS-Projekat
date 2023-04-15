using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class VoucherService
    {
        private readonly IVoucherRepository _voucherRepository;
        List<Voucher> _vouchers;

        public VoucherService()
        {
            _voucherRepository = Inject.CreateInstance<IVoucherRepository>();
            _vouchers = new List<Voucher>(_voucherRepository.GetAll());
        }

        public void Delete(Voucher voucher)
        {
            _voucherRepository.Delete(voucher);
        }

        public List<Voucher> GetUpcomingVouchers(User user)
        {
            List<Voucher> Vouchers = new List<Voucher>();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (Voucher voucher in _vouchers)
            {
                AddValidVouchers(user, Vouchers, today, voucher);
            }
            return Vouchers;
        }

        private static void AddValidVouchers(User user, List<Voucher> Vouchers, DateOnly today, Voucher voucher)
        {
            if (voucher.IdUser==user.Id && voucher.ExpirationDate.CompareTo(today)>0)
            {
                Vouchers.Add(voucher);
            }
        }
    }
}
