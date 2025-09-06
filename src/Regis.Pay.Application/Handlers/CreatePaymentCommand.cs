using Mediator;

namespace Regis.Pay.Application.Handlers;

public record CreatePaymentCommand(string aggregateId) : ICommand;
