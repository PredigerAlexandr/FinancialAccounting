namespace Infrastructure.Jobs.JobService;

public interface IPaymentsService
{
    public Task CheckDebtPayments();
}