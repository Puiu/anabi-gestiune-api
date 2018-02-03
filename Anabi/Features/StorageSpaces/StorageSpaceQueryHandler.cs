﻿using Anabi.Common.Utils;
using Anabi.DataAccess.Ef;
using Anabi.Domain;
using Anabi.Features.StorageSpaces.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Anabi.Features.StorageSpaces
{
    public class StorageSpaceQueryHandler : BaseHandler, IAsyncRequestHandler<GetStorageSpace, List<Models.StorageSpaceViewModel>>
    {
        public StorageSpaceQueryHandler(AnabiContext _ctx, IMapper _mapper) : base(_ctx, _mapper)
        {

        }

        public async Task<List<StorageSpaceViewModel>> Handle(GetStorageSpace message)
        {
            if (message.Id != null && message.Id <= 0)
            {
                throw new Exception(Constants.INVALID_ID);
            }

            var command = context.SpatiiStocare.AsQueryable();

            if (message.Id != null)
            {
                command = command.Where(m => m.Id == message.Id);
            }

            var result = await command.Select(x => Mapper.Map<Models.StorageSpaceViewModel>(x)).ToListAsync();

            if (result.Count == 0)
            {
                throw new Exception(Constants.NO_STORAGE_SPACES_FOUND);
            }

            return result;

        }
    }
}
