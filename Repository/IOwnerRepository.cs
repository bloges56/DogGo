using System;
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

    }
}
