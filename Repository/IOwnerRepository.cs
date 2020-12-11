﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogGo.Models;

namespace DogGo.Repository
{
    public interface IOwnerRepository
    {
        List<Owner> GetAllOwners();

        Owner GetOwnerById(int id);

        Owner GetOwnerByEmail(string email);

        void AddOwner(Owner owner);

        void UpdateOwner(Owner owner);

        void DeleteOwner(int ownerId);

    }
}
