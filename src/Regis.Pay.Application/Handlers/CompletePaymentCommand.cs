using Mediator;

namespace Regis.Pay.Application.Handlers;

public record CompletePaymentCommand(string aggregateId) : ICommand;

