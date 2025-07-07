using Grpc.Core.Interceptors;
using Grpc.Core;
using Microsoft.Data.SqlClient;
using Rira.Core.Exceptions;

namespace Rira.Grpc.Interceptors
{
    /// <inheritdoc />
    public class GrpcGlobalExceptionHandlerInterceptor : Interceptor
    {
        #region Private Feilds

        private readonly ILogger<GrpcGlobalExceptionHandlerInterceptor> _logger;

        #endregion

        #region Ctor

        /// <inheritdoc />
        public GrpcGlobalExceptionHandlerInterceptor(ILogger<GrpcGlobalExceptionHandlerInterceptor> logger)
        {
            _logger = logger;
        }

        #endregion

        /// <inheritdoc />

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return await continuation(request, context);
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain exception occurred.");
                throw new RpcException(new Status(StatusCode.FailedPrecondition, dex.Message));
            }
            catch (ArgumentException aex)
            {
                _logger.LogWarning(aex, "Invalid argument.");
                throw new RpcException(new Status(StatusCode.InvalidArgument, aex.Message));
            }
            catch (SqlException sqlException)
            {
                _logger.LogError(sqlException, "Database error.");
                throw new RpcException(new Status(StatusCode.Unavailable, "Database is currently unavailable."));
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error.");
                throw new RpcException(new Status(StatusCode.Internal, "Internal server error."));
            }
        }
    }
}
