using System;
using Interfaces.Entities;

namespace Interfaces.Services
{
    class ContractService
    {
        private IOnlinePaymentService _onlinePaymentService;

        public ContractService(IOnlinePaymentService onlinePaymentService)
        {
            _onlinePaymentService = onlinePaymentService;
        }

        public void ProcessContract(Contract contract, int months)
        {
            double basicQota = contract.TotalValue / months;
            for (int i = 1; i <= months; i++)
            {
                DateTime date = contract.Date.AddMonths(i);
                double updatedQota = basicQota + _onlinePaymentService.Interest(basicQota, i);
                double fullQota = updatedQota + _onlinePaymentService.PaymentFee(updatedQota);
                contract.AddInstallment(new Installment(date, fullQota));
            }
        }
    }
}
