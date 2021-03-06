﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Anabi.Common.ViewModels;
using Anabi.DataAccess.Ef.DbModels;
using Anabi.Domain.Person.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Anabi.Domain.Person
{
    public class PersonHandler : BaseHandler
        , IRequestHandler<AddDefendant, DefendantViewModel>
        , IRequestHandler<DeleteDefendant>
    {
        public PersonHandler(BaseHandlerNeeds needs) : base(needs)
        {
        }

        public async Task<DefendantViewModel> Handle(AddDefendant message, CancellationToken cancellationToken)
        {
            var personDb = mapper.Map<PersonDb>(message);
            personDb.UserCodeAdd = UserCode();
            personDb.AddedDate = DateTime.Now;


            var assetDefendant = new AssetDefendantDb()
            {
                AddedDate = DateTime.Now,
                UserCodeAdd = UserCode(),
                AssetId = message.AssetId,
                Defendant = personDb
            };

            context.Persons.Add(personDb);
            context.AssetDefendants.Add(assetDefendant);

            await context.SaveChangesAsync();
            var response = mapper.Map<AddDefendant, DefendantViewModel>(message);
            response.Id = personDb.Id;
            response.Journal = new JournalViewModel
            {
                AddedDate = personDb.AddedDate,
                UserCodeAdd = personDb.UserCodeAdd,
                LastChangeDate = personDb.LastChangeDate,
                UserCodeLastChange = personDb.UserCodeLastChange,
            };

            return response;

        }

        public async Task<Unit> Handle(DeleteDefendant request, CancellationToken cancellationToken)
        {
            var assetDefendantToDelete = await context.AssetDefendants
                .FirstOrDefaultAsync(x => x.AssetId == request.AssetId && x.PersonId == request.DefendantId);

            context.AssetDefendants.Remove(assetDefendantToDelete);
            context.Persons.Remove(assetDefendantToDelete.Defendant);
            await context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
