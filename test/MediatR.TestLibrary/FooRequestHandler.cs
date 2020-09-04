using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.TestLibrary
{
    public class FooRequestHandler :IRequestHandler<FooRequest, FooResponse>
    {
        public Task<FooResponse> Handle(FooRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}