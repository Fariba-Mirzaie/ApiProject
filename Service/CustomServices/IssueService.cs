using MyWebAPI.Models;
using Repository.IRepository;
using Service.ICustomServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.CustomServices
{
    public class IssueService : ICustomService<Issue>
    {
        private readonly IRepository<Issue> _issueRepository;

        public IssueService(IRepository<Issue> issueRepository)
        {
            _issueRepository = issueRepository;
        }

        public IEnumerable<Issue> GetAll()
        {
            return _issueRepository.GetAll();
        }

        public Issue GetById(int id)
        {
            var issue = _issueRepository.GetById(id);
            return issue;
        }

        public bool Insert(Issue entity)
        {
            try
            {
                _issueRepository.Insert(entity);
                _issueRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Update(Issue entity)
        {
            try
            {
                if (entity != null)
                {
                    _issueRepository.Update(entity);
                    _issueRepository.SaveChanges();

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var issue = GetById(id);
            try
            {
                if (issue != null)
                {
                    _issueRepository.Delete(id);
                    _issueRepository.SaveChanges();

                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void SaveChanges()
        {
            _issueRepository.SaveChanges();
        }


    }
}


//if و else حتما سرویس
// trycatch ریپازیتوری یا سرویس