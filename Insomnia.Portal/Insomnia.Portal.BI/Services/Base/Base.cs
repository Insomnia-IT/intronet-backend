using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insomnia.Portal.Data.Return;
using Insomnia.Portal.Data.Enums;
using Insomnia.Portal.EF;
using AutoMapper;

namespace Insomnia.Portal.BI.Services
{
    public class Base<TEntity, Tdto>
    {
        internal readonly ServiceDbContext _context;
        internal readonly IMapper _mapper;
        public Base (IMapper mapper, ServiceDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public virtual T Ok<T>() where T : BaseReturn, new()
        {
            return new T() { Success = true };
        }

        public virtual BaseReturn Ok(BaseReturn okModel)
        {
            okModel.Success = true;

            return okModel;
        }

        public virtual T Ok<T>(T okModel) where T : BaseReturn
        {
            okModel.Success = true;

            return okModel;
        }

        public virtual T Error<T>(string message, CodeRequest code = CodeRequest.BadRequest) where T : BaseReturn, new()
        {
            return new T() { Success = false, Message = message, Code = code };
        }

        public virtual BaseReturn Error(BaseReturn errorModel)
        {
            errorModel.Success = false;

            return errorModel;
        }

        public virtual T Error<T>(T errorModel) where T: BaseReturn
        {
            errorModel.Success = false;

            return errorModel;
        }

        public TEntity GetEntity<T>(T dto)
        {
            return _mapper.Map<TEntity>(dto);
        }
    }
}
