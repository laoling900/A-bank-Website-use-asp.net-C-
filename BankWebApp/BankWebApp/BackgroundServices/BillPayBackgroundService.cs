using BankWebApp.Data;
using Microsoft.EntityFrameworkCore;
using BankWebApp.Models;

namespace BankWebApp.BackgroudServices;

public class BillPayBackgroundService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<BillPayBackgroundService> _logger;


    public BillPayBackgroundService(IServiceProvider services, ILogger<BillPayBackgroundService> logger)
    {

        _services = services;
        _logger = logger;

    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("BillPayBackgroundService");

        while (!cancellationToken.IsCancellationRequested)
        {
            await DoWork(cancellationToken);

            _logger.LogInformation("BillPay BackgroundService is waitting a minute.");

            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
        }

       
    }

    private async Task DoWork(CancellationToken cancellationToken)
    {
        _logger.LogInformation("BillPay Background Service is working.");

        using var scope = _services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<BankAppContext>();

        var BillPays = await context.BillPays.ToListAsync(cancellationToken);

            BillPayOps billPayOps = new BillPayOps();
            AccountOps accountOps = new AccountOps();

        //compare the time , each bill pay time due should be charge and return statue
        foreach (var billPay in BillPays)
        {
            //save change in the billpay functionModel
            bool successfulPay = billPayOps.exceuteBillPay(billPay, context, accountOps);
            if(successfulPay &&billPay.BillPayPeriod == "M" )
            {
                BillPay nextMonthPay = billPayOps.nextMonthBillPay(billPay, context);
            }
        }
        await context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("BillPay Background Service work complete.");
    }
}



