using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    class VoucherService
    {
        private readonly VoucherRepository _voucherRepository;
        List<Voucher> _vouchers;

        public VoucherService()
        {
            _voucherRepository = new VoucherRepository();
            _vouchers = new List<Voucher>(_voucherRepository.GetAll());
        }

        public List<Voucher> GetUpcomingVouchers(User user)
        {
            List<Voucher> Vouchers = new List<Voucher>();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (Voucher voucher in _vouchers)
            {
                if (voucher.IdUser==user.Id && voucher.ExpirationDate.CompareTo(today)>0)
                {
                    Vouchers.Add(voucher);
                }
            }
            return Vouchers;
        }
    }
}
