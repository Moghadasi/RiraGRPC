using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Rira.Contracts.User.Command;
using Rira.Contracts.User.Query;
using Rira.Contracts.User.Proto;

namespace Rira.Grpc.Services;

/// <inheritdoc />
public class UserService : Rira.Contracts.User.Proto.UserService.UserServiceBase
{
    #region Private Feilds

    private readonly IMediator _mediator;

    #endregion

    #region Ctor

    /// <inheritdoc />
    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    /// <inheritdoc />
    public override async Task<GetUserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(new GetUserInput(request.UserId), context.CancellationToken);

        return new GetUserResponse()
        {
            UserId = result.UserId,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Birthday = ToProtoDate(result.Birthday),
            NationalCode = result.NationalCode
        };
    }

    /// <inheritdoc />
    public override async Task<AddUserResponse> AddUser(AddUserRequest request, ServerCallContext context)
    {
        var input = new AddUserInput(FirstName: request.FirstName,
            LastName: request.LastName,
            NationalCode: request.NationalCode,
            Birthday: DateOnly.FromDateTime(request.Birthday.ToDateTime().Date));
        var result = await _mediator.Send(input, context.CancellationToken);

        return new AddUserResponse
        {
            UserId = result.UserId
        };
    }

    /// <inheritdoc />
    public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
    {
        var input = new UpdateUserInput(UserId: request.UserId,
            FirstName: request.FirstName,
            LastName: request.LastName,
            NationalCode: request.NationalCode,
            Birthday: ToDateOnly(request.Birthday));
        var result = await _mediator.Send(input, context.CancellationToken);

        return new UpdateUserResponse
        {
            Done = result.Done
        };
    }

    /// <inheritdoc />
    public override async Task<DeleteUserResponse> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var input = new DeleteUserInput(UserId: request.UserId);
        var result = await _mediator.Send(input, context.CancellationToken);

        return new DeleteUserResponse
        {
            Done = result.Done
        };
    }

    #region Private Methods

    private static Timestamp ToProtoDate(DateOnly date)
    {
        return Timestamp.FromDateTime(date.ToDateTime(new TimeOnly(0, 0), DateTimeKind.Utc));
    }

    private static DateOnly ToDateOnly(Timestamp date)
    {
        return DateOnly.FromDateTime(date.ToDateTime().Date);
    }
    #endregion
}
