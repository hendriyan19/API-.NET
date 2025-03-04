﻿using System;
using System.Collections.Generic;
using System.Linq;
using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.EmployeeRepository
{
    public class GeneralRepository<Context, Entity, Key> : IRepository<Entity, Key>
        where Entity : class
        where Context : MyContext
    {

        private readonly MyContext myContext;
        private readonly DbSet<Entity> entities;


        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            entities = myContext.Set<Entity>();
        }

        public int Delete(Key key)
        {

            var entity = entities.Find(key);
            if (entity != null)
            {
                entities.Remove(entity);
            }
            var result = myContext.SaveChanges();
            return result;




        }

        public IEnumerable<Entity> Get()
        {
            //throw new NotImplementedException();
            return entities.ToList();
        }

        public Entity Get(Key key)
        {

            return entities.Find(key);
        }

        public int Insert(Entity entity)
        {

            var check = entities.Add(entity);

            if (check == null)
            {
                entities.Add(entity);
            }

            return myContext.SaveChanges();



        }



        public int Update(Entity entity, Key key)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var result = 0;
            try
            {
                myContext.SaveChanges();
                result = 1;

            }
            catch (Exception)
            {
                result = 0;
            }
            return result;

        }


    }
}
