using Mediator;

namespace Regis.Pay.Application.Handlers;

public record SettlePaymentCommand(string aggregateId) : ICommand;

