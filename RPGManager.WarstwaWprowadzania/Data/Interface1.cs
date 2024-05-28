﻿using Microsoft.EntityFrameworkCore;
using RPGManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGManager.WarstwaWprowadzania.Data
{
    public interface IDataContext
    {
        //3 db sery + save changes
        public DbSet<NPC> NPCs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Note> Notes { get; set; }
        public int SaveChanges();
    }
}
