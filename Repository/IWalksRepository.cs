﻿using DogGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Repository
{
    public interface IWalksRepository
    {
        void AddWalk(Walks walk);
    }
}
